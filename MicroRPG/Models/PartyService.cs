using MicroRPG.Models.Backstory;
using MicroRPG.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MicroRPG.Models
{
    public class PartyService
    {
        static List<Case> allCases;
        static Random random = new Random();
        IHttpContextAccessor accessor;

        const string UsedCaseIDs = "UsedCaseIDs";
        const string PlayerObj = "Player";
        const string PartyIDs = "PartyIDs";

        public object JsonContext { get; private set; }

        public PartyService(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public PartyBackstoryVM GetValidCase(int playerID)
        {
            if (allCases == null)
            {   
                allCases = Case.GenerateCases();   
            }

            Player player = GetPlayer(playerID);

            string usedIDs = accessor.HttpContext.Session.GetString(UsedCaseIDs);
            List<Tag> tags = player.Tags;
            Case validCase = allCases
                .Where(c => string.IsNullOrEmpty(usedIDs) || !usedIDs.Split(',').Contains(c.ID.ToString()))
                .Where(c => c.IsValid(tags, GetNumberOfPlayers()))
                .OrderBy(c => random.Next())
                .FirstOrDefault();

            if (validCase == null)
            {
                return new PartyBackstoryVM();
            }
            
            if (!string.IsNullOrEmpty(usedIDs))
            {
                accessor.HttpContext.Session.SetString(UsedCaseIDs, $"{usedIDs},{validCase.ID}");
            }
            else
            {
                accessor.HttpContext.Session.SetString(UsedCaseIDs, validCase.ID.ToString());
            }

            List<Player> party = new List<Player>();
            foreach (int id in GetPartyIDs())
            {
                party.Add(GetPlayer(id));
            }

            return new PartyBackstoryVM
            {
                Description = validCase.GetAdjustedDescription(player, party),
                Outcomes = validCase.GetAdjustedOutcomes(player, party),
                CurrentPlayerName = player.Name
            };
        }

        public void GeneratePlayers()
        {
            AddPlayer(new PartyCreateVM
            {
                Name = "Hokanius Orkanius",
                Age = 36,
                Gender = "Male"
            });

            AddPlayer(new PartyCreateVM
            {
                Name = "Pontonius Maximus",
                Age = 48,
                Gender = "Male"
            });
        }

        internal void AddPlayer(PartyCreateVM playerVM)
        {
            Player player = new Player(playerVM.Name, playerVM.Age)
            {
                Gender = playerVM.Gender
            };

            int[] IDs = GetPartyIDs();
            IDs = IDs.Append(player.ID).ToArray();

            accessor.HttpContext.Session.SetString(PartyIDs,
                JsonConvert.SerializeObject(IDs));
            accessor.HttpContext.Session.SetString(PlayerObj + player.ID,
                JsonConvert.SerializeObject(player));
        }

        private int[] GetUsedCaseIDs()
        {
            string casesStr = accessor.HttpContext.Request.Cookies[UsedCaseIDs];
            if (casesStr == null)
                return new int[0];

            string[] strings = casesStr.Split(',');
            int[] res = new int[strings.Length];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = int.Parse(strings[i]);
            }
            return res;
        }

        public PartySummaryVM GetPartySummary()
        {
            int[] IDs = GetPartyIDs();
            string[] playerNames = new string[IDs.Length];
            for (int i = 0; i < IDs.Length; i++)
            {
                playerNames[i] = GetPlayer(IDs[i]).Name;
            }
            PartySummaryVM partySummary = new PartySummaryVM
            {
                PlayerIDs = IDs,
                PlayerNames = playerNames
            };

            return partySummary;
        }

        public int[] GetPartyIDs()
        {
            int[] res;
            string json = accessor.HttpContext.Session.GetString(PartyIDs);
            if (!string.IsNullOrEmpty(json))
            {
                res = JsonConvert.DeserializeObject<int[]>(json);
            }
            else
            {
                res = new int[0];
            }
            return res;
        }

        private int GetNumberOfPlayers()
        {
            return GetPartyIDs().Length;
        }

        private List<Tag> GetPlayerTags(int playerID)
        {
            return GetPlayer(playerID).Tags;
        }

        private Player GetPlayer(int playerID)
        {
            string json = accessor.HttpContext.Session.GetString(PlayerObj + playerID);
            if (json != null)
            {
                return JsonConvert.DeserializeObject<Player>(json);
            }
            return null;
        }
    }
}
