using Backstory;
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
        const string Player = "Player";
        const string PlayerIDs = "PlayerIDs";

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

            string usedIDs = accessor.HttpContext.Session.GetString(UsedCaseIDs);
            List<Tag> tags = GetPlayerTags(playerID);
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

            return new PartyBackstoryVM
            {
                Description = validCase.Description,
                Outcomes = new string[6]
            };
        }

        public int[] GetPlayerIDs()
        {
            string res = accessor.HttpContext.Session.GetString(PlayerIDs);
            if (!string.IsNullOrEmpty(res))
            {
                int[] a = JsonConvert.DeserializeObject<int[]>(res);
                return a;
            }
            return new int[] {-1};
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

        private int GetNumberOfPlayers()
        {
            return 6;
        }

        private List<Tag> GetPlayerTags(int playerID)
        {
            //List<Tag> playerTags = JsonConvert.DeserializeObject accessor.HttpContext.Session.GetString(Player+playerID);

            return new List<Tag>();
        }
    }
}
