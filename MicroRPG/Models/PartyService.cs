using BuildBackstoryTest;
using MicroRPG.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
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

        public PartyService(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public PartyBackstoryVM GetValidCase(string playerID)
        {
            if (allCases == null)
            {   
                allCases = Case.GenerateCases();   
            }

            List<Tag> tags = GetPlayerTags();
            Case validCase = allCases
                .Where(c => !GetUsedCaseIDs().Contains(c.ID))
                .Where(c => c.IsValid(tags, GetNumberOfPlayers()))
                .OrderBy(c => random.Next())
                .FirstOrDefault();

            if (validCase != null)
            {
                if (accessor.HttpContext.Request.Cookies.ContainsKey(UsedCaseIDs) )
                {
                    accessor.HttpContext.Response.Cookies.Append(UsedCaseIDs,
                        accessor.HttpContext.Request.Cookies[UsedCaseIDs] + "," + validCase.ID);
                } else
                {
                    accessor.HttpContext.Response.Cookies.Append(UsedCaseIDs, validCase.ID.ToString());
                }
            }

            return new PartyBackstoryVM
            {
                Description = validCase.Description,
                Outcomes = new string[6]
            };
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

        private List<Tag> GetPlayerTags()
        {
            return new List<Tag>();
        }
    }
}
