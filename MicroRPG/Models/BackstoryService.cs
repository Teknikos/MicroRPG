using BuildBackstoryTest;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroRPG.Models
{
    public class BackstoryService
    {
        static List<Case> allCases;

        public List<Case> GetValidCases(string playerID, IMemoryCache cache)
        {
            if (allCases == null)
            {
                
            }
            return null;
        }
    }
}
