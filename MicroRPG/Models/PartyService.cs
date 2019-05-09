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
        const string RelationTagChoices = "RelationTagChoices";

        public GameMainVM GetGameMainVM()
        {
            int[] IDs = GetPartyIDs();
            string[] playerNames = new string[IDs.Length];
            for (int i = 0; i < IDs.Length; i++)
            {
                playerNames[i] = GetPlayer(IDs[i]).Name;
            }
            GameMainVM mainVM = new GameMainVM
            {
                PlayerIDs = IDs,
                PlayerNames = playerNames,
                CurrentEnvironment = accessor.HttpContext.Session.GetString(Constants.SelectedEnvironment)
            };

            return mainVM;
        }

        public PlayerDetailsVM GetPlayerDetailsVM(int id)
        {
            Player player = GetPlayer(id);
            return new PlayerDetailsVM
            {
                Name = player.Name,
                Attack = player.Stats.Attack,
                HP = player.Stats.HP,
                MP = player.Stats.MP,
                DamageReduction = player.Stats.DamageReduction,
                Speed = player.Stats.Speed,
                Wisdom = player.Stats.Wisdom
            };
        }

        public object JsonContext { get; private set; }

        public PartyService(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }


        public PartyBackstoryVM GetRandomRelations(int playerID)
        {
            int[] ids = GetPartyIDs();
            int relatedID = -1;
            const int NumberOfOptions = 4;

            for (int i = 0; i < ids.Length; i++)
            {
                if (ids[i] == playerID)
                {
                    if (i + 1 < ids.Length)
                    {
                        relatedID = ids[i + 1];
                    }
                    else if (playerID != 0)
                    {
                        relatedID = ids[0];
                    }
                }
            }
            if (relatedID == -1)
                return null;
            Player player = GetPlayer(playerID), relatesTo = GetPlayer(relatedID);
            List<RelationTag> allRelationTags = RelationTag.GenerateRelationTags(player, relatesTo);
            List<RelationTag> relationTags = allRelationTags.OrderBy(t => random.Next()).Take(NumberOfOptions).ToList();

            accessor.HttpContext.Session.SetString(RelationTagChoices+playerID,
                JsonConvert.SerializeObject(relationTags));

            return new PartyBackstoryVM
            {
                Description = $"{player.Name} what is your relation to {relatesTo.Name}?",
                Outcomes = relationTags.Select(t => t.Description).ToArray(),
                CurrentPlayerName = player.Name,
                ID = -1
            };
        }

        internal void ApplyRelationTag(int outcomeIndex, int playerID)
        {
            string json = accessor.HttpContext.Session.GetString(RelationTagChoices + playerID);
            if (string.IsNullOrEmpty(json))
                return;
            List<RelationTag> relationTags = JsonConvert.DeserializeObject<List<RelationTag>>(json);
            Player player = GetPlayer(playerID),
                relatesTo = GetPlayer(relationTags[outcomeIndex].RelatesToID);
            relationTags[outcomeIndex].AttachRelation(player, relatesTo);
            SavePlayerToSession(player);
            if (relatesTo != null)
            {
                SavePlayerToSession(relatesTo);
            }
            
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

            List<Player> party = GetParty();

            return new PartyBackstoryVM
            {
                Description = validCase.GetAdjustedDescription(player, party),
                Outcomes = validCase.GetAdjustedOutcomes(player, party),
                CurrentPlayerName = player.Name,
                ID = validCase.ID
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

        public void ApplyCase(int id, int outcomeIndex, int playerID)
        {
            Case @case = GetCaseByID(id);
            Player player = GetPlayer(playerID);
            @case.ApplyToPlayer(outcomeIndex, player);
            SavePlayerToSession(player);
        }

        private Case GetCaseByID(int id)
        {
            if (allCases == null)
            {
                allCases = Case.GenerateCases();
            }

            return allCases.FirstOrDefault(c => c.ID == id);
        }

        public void AddPlayer(PartyCreateVM playerVM)
        {
            Player player = new Player(playerVM.Name, playerVM.Age)
            {
                Gender = playerVM.Gender
            };

            int[] IDs = GetPartyIDs();
            IDs = IDs.Append(player.ID).ToArray();

            accessor.HttpContext.Session.SetString(PartyIDs,
                JsonConvert.SerializeObject(IDs));
            SavePlayerToSession(player);
        }

        public void SavePlayerToSession(Player player)
        {
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

        private List<Player> GetParty()
        {
            List<Player> party = new List<Player>();
            foreach (int index in GetPartyIDs())
            {
                party.Add(GetPlayer(index));
            }
            return party;
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
