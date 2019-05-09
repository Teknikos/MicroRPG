using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MicroRPG.Models.Backstory.BSConstants;

namespace MicroRPG.Models.Backstory
{
    public class Case
    {
        public class Outcome
        {
            public Outcome(string description, Stats statsGain, List<Tag> tags)
            {
                Description = description;
                Tags = tags;
                StatsGain = statsGain;
            }

            public string Description { get; }
            public List<Tag> Tags { get; }
            public Stats StatsGain { get; set; }
        }

        
        public string Description { get; }
        public List<Tag> RequiredTags { get; set; }
        public List<Tag> LackTags { get; set; }
        readonly private int minPartySize;
        readonly private int maxPartySize;
        readonly private List<Outcome> outcomes = new List<Outcome>();
        public int NumberOfOutcomes { get; set; }

        private static int idCount = 0;
        public int ID { get; }

        public Case(string description, List<Tag> requiredTags = null, List<Tag> lackTags = null, int minPartySize = MininumPlayers, int maxPartySize = MaximumPlayers)
        {
            Description = description;
            if (requiredTags == null)
                RequiredTags = new List<Tag>();
            else
                RequiredTags = requiredTags;
            if (lackTags == null)
                LackTags = new List<Tag>();
            else
                LackTags = lackTags;
            this.minPartySize = minPartySize;
            this.maxPartySize = maxPartySize;
            ID = idCount++;
            NumberOfOutcomes = 0;
        }

        public static List<Case> GenerateCases()
        {
            List<Case> cases = new List<Case>();
            Case c;
            //c = new Case(
            //       $"While sitting by the camp fire the party surprised by a goblin moving through the bushes." +
            //       $"\n{ThisPlayer} quickly cuts it's throat. Their reason:",
            //       lackTags: new List<Tag>
            //       {
            //           LawfulTag
            //       }
            //       );
            //c.LackTags.Add(LawfulTag);
            //c.AddOutCome($"{ThisPlayer} hates goblins having had their family slaughtered by a pack when they were young.", StatsForClass[Class.Mage]);
            //c.AddOutCome($"{ThisPlayer} has a taste for monsters and is always hungry.", StatsForClass[Class.Warrior], LawfulTag);
            //c.AddOutCome($"{ThisPlayer} had been aware of it's presence and had no interest in hearing it out.", StatsForClass[Class.Rogue]);
            //cases.Add(c);


            //c = new Case(
            //       $"An argument has broken out between {OtherPlayer}{1} and {OtherPlayer}{2}" +
            //       $"\n{ThisPlayer} decides to act.",
            //       minPartySize: 3
            //       );
            //c.AddOutCome($"I say: Screw you guys!", StatsForClass[Class.Mage]);
            //c.AddOutCome($"I cut myself.", StatsForClass[Class.Rogue], LawfulTag);
            //c.AddOutCome($"I walk away, my brotha {RelatedPlayer}", StatsForClass[Class.Cleric]);
            //cases.Add(c);


            for (int i = 0; i < 27; i++)
            {
                c = new Case("Test case " + (i + 2));
                c.AddOutCome("Warrior", StatsForClass[Class.Warrior]);
                c.AddOutCome("Mage", StatsForClass[Class.Mage]);
                c.AddOutCome("Cleric", StatsForClass[Class.Cleric]);
                c.AddOutCome("Rogue", StatsForClass[Class.Rogue]);
                cases.Add(c);
            }

            return cases;
        }

        public void AddOutCome(string description, Stats statsGain, params Tag[] tags)
        {
            outcomes.Add(new Outcome(description, statsGain, tags.ToList()));
            NumberOfOutcomes++;
        }

        public string[] GetOutcomes()
        {
            return outcomes.Select(o => o.Description).ToArray();
        }

        public string[] GetAdjustedOutcomes(Player player, List<Player> party)
        {
            return outcomes.Select(o => AdjustString(o.Description, player, party)).ToArray();
        }

        public string GetAdjustedDescription(Player player, List<Player> party)
        {
            return AdjustString(Description, player, party);
        }

        public string GetAdjustedOutcome(int outcomeIndex, Player player, List<Player> party)
        {
            return AdjustString(outcomes[outcomeIndex].Description, player, party);
        }

        private string AdjustString(string str, Player player, List<Player> party)
        {
            string res = str.Replace(ThisPlayer, player.Name);
            if (res.Contains(RelatedPlayer))
            {
                RelationTag relTag = (RelationTag)RequiredTags.FirstOrDefault(t => t is RelationTag);

                RelationTag tag = (RelationTag)player.Tags
                    .FirstOrDefault(t => t is RelationTag && string.Compare(t.Name, relTag.Name, true) == 0);
                if (tag != null)
                {
                    res = res.Replace(RelatedPlayer, party.FirstOrDefault(p => p.ID == tag.RelatesToID)?.Name);
                }
            }
            int playerRelativeIndex = 0;
            var restOfParty = party.Where(p => p.ID != player.ID);
            while (res.Contains(OtherPlayer))
            {
                if (!res.Contains(OtherPlayer + (playerRelativeIndex + 1).ToString()))
                {
                    Console.WriteLine(OtherPlayer + (playerRelativeIndex + 1).ToString());
                    throw new Exception($"{OtherPlayer}-tag used incorrectly, expected to find {OtherPlayer}{(playerRelativeIndex + 1)}");
                }
                else
                {
                    res = res.Replace(OtherPlayer + (playerRelativeIndex + 1).ToString(), restOfParty.ElementAt(playerRelativeIndex).Name);
                }
                playerRelativeIndex++;
            }
            return res;
        }

        public bool IsValid(List<Tag> playerTags, int numberOfPlayers)
        {
            if (numberOfPlayers < minPartySize || numberOfPlayers > maxPartySize)
                return false;

            if (RequiredTags != null)
            {
                foreach (Tag tag in RequiredTags)
                {
                    if (!HasTag(tag, playerTags))
                        return false;
                }
            }

            if (LackTags != null)
            {
                foreach (Tag tag in LackTags)
                {
                    if (HasTag(tag, playerTags))
                        return false;
                }
            }

            return true;
        }

        private static bool HasTag(Tag tag, List<Tag> tagList)
        {
            if (tagList == null)
                return false;

            return tagList.Contains(tag); ;
        }

        public void ApplyToPlayer(int outcomeIndex, Player player)
        {
            if (outcomes[outcomeIndex].Tags != null)
                player.Tags.AddRange(outcomes[outcomeIndex].Tags);
            if (outcomes[outcomeIndex].StatsGain != null)
            {
                player.Stats.AddStats(outcomes[outcomeIndex].StatsGain);
                player.StatGainCount++;
            }
        }

    }
}
