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

            //Cases start -----------------------------------------------------------------------------------------------------------------------------------

            //Case 1

            c = new Case(
                $"{ThisPlayer} is dreaming that you, {OtherPlayer}{1} and several strangers are running past a bridge as it starts collapsing. " +
                $"\nYou get to the end first, while the others stumble and cry for help."
                );
            c.AddOutCome($"I go back and try to help all of them.", StatsForClass[Class.Cleric]);
            c.AddOutCome($"I turn back to help {OtherPlayer}{1}, only.", StatsForClass[Class.Rogue]);
            c.AddOutCome($"I stay at a safe distance and try pull them to the ledge.", StatsForClass[Class.Mage]);
            c.AddOutCome($"I throw down a thick rope down the pit and try to pull them all up.", StatsForClass[Class.Warrior]);
            cases.Add(c);

            //Case 2

            c = new Case(
           $"A crippled old soldier sits in a drunken stupor in a shady bar of the city slums, he becomes more and more violent as the night goes on." +
           $" \nEventually, he spits on { ThisPlayer } in contempt.",
           lackTags: new List<Tag>
                   {
                       LawfulTag, ReligiousTag
                   }
           );
            c.AddOutCome($"I wipe off the spit and leave the bar.", StatsForClass[Class.Cleric]);
            c.AddOutCome($"I decide to win him over with kindness and give him enough coins to stay the night.", StatsForClass[Class.Cleric]);
            c.AddOutCome($"I have had enough of this low-life and smash him off his chair.", StatsForClass[Class.Warrior]);
            c.AddOutCome($"I go past the man as an show of submission, while grabbing his purse of coins before I exit.", StatsForClass[Class.Rogue]);
            cases.Add(c);

            //Case 3

            c = new Case(
            $"{ThisPlayer} and the rest of the group is heading to their accommodation as they see a ”lady of the night” who is crying and arguing with man. " +
            $"\nThe man and his friends, laugh and make fun of the woman.",
            requiredTags: new List<Tag>
                   {
                       HonorableTag
                   }
            );
            c.AddOutCome($"I stop the abuse and intimidate the men.", StatsForClass[Class.Warrior]);
            c.AddOutCome($"I avoid the men entirely and try to reform the woman in the name of my deity.", StatsForClass[Class.Cleric]);
            c.AddOutCome($"I walk away and ask the group to do the same, she is not worth bloodshed.", StatsForClass[Class.Rogue]);
            c.AddOutCome($"I cast “Hold person” on the group of men and search them for magical possessions.", StatsForClass[Class.Mage], UnlawfulTag);
            cases.Add(c);

            //Case 4

            c = new Case(
            $"{ThisPlayer} sets up camp near the city. In the pitch dark, {ThisPlayer} stumbles into an orc. {ThisPlayer} had no choice but to engage the orc in combat."
            );
            c.AddOutCome($"I try my best to reason with the creature in its own language.", StatsForClass[Class.Warrior], OutcastTag);
            c.AddOutCome($"After the orc has been slain, I give it its last rites. Every creature has an eternal soul.", StatsForClass[Class.Cleric], HonorableTag, ReligiousTag);
            c.AddOutCome($"I stab it in the gut multiple times with my dagger and run off.", StatsForClass[Class.Rogue]);
            c.AddOutCome($"I cast a shielding spell and back off while the orc savagely swings at me.", StatsForClass[Class.Mage]);
            cases.Add(c);

            //Case 5

            c = new Case(
            $"{ThisPlayer} is visiting a temple of Talos and stumbles upon a sacrificial ritual."
            );
            c.AddOutCome($"I shout at the people in the ritual to stop.", StatsForClass[Class.Warrior]);
            c.AddOutCome($"I sneak into the shadows to see more clearly what is happening.", StatsForClass[Class.Cleric]);
            c.AddOutCome($"I immediately leave and let the leader of my temple deal with them.", StatsForClass[Class.Rogue]);
            c.AddOutCome($"I walk forward and try to blend in.", StatsForClass[Class.Mage], OutcastTag);
            cases.Add(c);

            //Case 6

            c = new Case(
            $"{ThisPlayer}, {OtherPlayer}{1} and {OtherPlayer}{2} are together and planning how to approach their mission. You are offered some wine.",
            minPartySize: 3
            );
            c.AddOutCome($"I rather keep my wits sharp.", StatsForClass[Class.Rogue]);
            c.AddOutCome($"I gladly accept.", StatsForClass[Class.Warrior]);
            c.AddOutCome($"I do not drink spirits.", StatsForClass[Class.Cleric], ReligiousTag);
            cases.Add(c);

            //Case 7

            c = new Case(
            $"{ThisPlayer} witnesses a merchant being robbed by several thieves."
            );
            c.AddOutCome($"I help the victim.", StatsForClass[Class.Cleric]);
            c.AddOutCome($"I thwart the thieves.", StatsForClass[Class.Warrior]);
            c.AddOutCome($"I watch the scene unfold to learn how to better protect myself.", StatsForClass[Class.Warrior]);
            c.AddOutCome($"I make notes on the members of the gang to increase my knowledge of the criminal underworld.", StatsForClass[Class.Rogue]);
            c.AddOutCome($"I ignore the entire incident.", StatsForClass[Class.Mage]);
            cases.Add(c);

            //Case 8

            c = new Case(
            $"{ThisPlayer} meets an old childhood friend, and finds out that the supplies {ThisPlayer} has been buying, are black market goods."
            );
            c.AddOutCome($"I urge my friend to stop selling stolen goods, and return them to the owner.", StatsForClass[Class.Cleric]);
            c.AddOutCome($"I do not bother, it is a win-win situation. Gold for both of us.", StatsForClass[Class.Rogue], UnlawfulTag);
            c.AddOutCome($"Threaten your friend to stop, otherwise you will inform the garrison of his crimes.", StatsForClass[Class.Warrior]);
            c.AddOutCome($"This does not concern me, I do not have use for trinkets and common supplies anyway.", StatsForClass[Class.Mage]);
            cases.Add(c);

            //Case 9

            c = new Case(
            $"A woman is fleeing from an angry mob of torch - wielding villagers at night. { ThisPlayer } could try to help her, but risk getting attacked by the villagers."
            );
            c.AddOutCome($"I try to shield the woman, then try to reason for her release.", StatsForClass[Class.Cleric]);
            c.AddOutCome($"I ask what she has done, perhaps she is guilty of a crime.", StatsForClass[Class.Warrior]);
            c.AddOutCome($"If I could get a hold of the woman, her secrets might lead to personal power or knowledge.", StatsForClass[Class.Mage]);
            c.AddOutCome($"I stand ready to fend off the pursuers, no matter the consequences.", StatsForClass[Class.Cleric], HonorableTag);
            c.AddOutCome($"I try to divert the crowd's attention to let the woman escape.", StatsForClass[Class.Rogue]);
            cases.Add(c);

            //Case 10

            c = new Case(
            $"{ ThisPlayer} spots a strange ring on the ground. As { ThisPlayer} grabs it, a strong presence of magic emanates from it…."
            );
            c.AddOutCome($"I immediately put it in my pocket, it could be very valuable.", StatsForClass[Class.Rogue]);
            c.AddOutCome($"I show it to my group and try to find its rightful owner.", StatsForClass[Class.Warrior]);
            c.AddOutCome($"I study its magical essence as best as I can.", StatsForClass[Class.Mage]);
            c.AddOutCome($"I decide to bring it to a high priest or arcane practitioner to have it examined, it could be of use for greater good .", StatsForClass[Class.Cleric]);
            cases.Add(c);

            //Case 11

            c = new Case(
            $"{ ThisPlayer} and the rest of the group enters a temple garden in the kingdom capitol. Several men in plate armor with matching sigils, are restraining a group of noblemen. " +
            $"\nThe armored men accuse the noblemen of being vampires, and declare that they must be destroyed."
            );
            c.AddOutCome($"I side with the paladin order.", StatsForClass[Class.Cleric], LawfulTag);
            c.AddOutCome($"I side with the noblemen.", StatsForClass[Class.Cleric], UnlawfulTag);
            c.AddOutCome($"I turn around and gesture to back off.", StatsForClass[Class.Rogue]);
            c.AddOutCome($"I do not side with any side but wait to see what will happen.", StatsForClass[Class.Mage]);
            cases.Add(c);

            //Case 12

            c = new Case(
            $"{ ThisPlayer} watches as {OtherPlayer}{1} and {OtherPlayer}{2} get into a heated debate, and a confrontation seems inevitable."
            );
            c.AddOutCome($"I side with {OtherPlayer}{1} because {OtherPlayer}{1} seems like a kind, wise and honourable person.", StatsForClass[Class.Cleric]);
            c.AddOutCome($"I side with {OtherPlayer}{2} because I believe {OtherPlayer}{2} is stronger and more assertive.", StatsForClass[Class.Warrior]);
            c.AddOutCome($"I let them deal with it by themselves.", StatsForClass[Class.Rogue]);
            c.AddOutCome($"I do not side with anyone, but wait to see what will happen.", StatsForClass[Class.Warrior]);
            cases.Add(c);

            //Case 13

            c = new Case(
            $"{ ThisPlayer} feels a sharp pain in the back and hears a whisper: “Hand over your coins or you are dead...”. As {ThisPlayer} turns around you see two youths with sinister expressions."
            );
            c.AddOutCome($"I try to reason with my assailants.", StatsForClass[Class.Cleric]);
            c.AddOutCome($"I shrug of the pain and shove my attacker away, as I try to escape", StatsForClass[Class.Warrior]);
            c.AddOutCome($"After murdering the young boys, I make sure there are no witnesses left", StatsForClass[Class.Mage], OutcastTag);
            c.AddOutCome($"I yield, and hand over my bag of decoy currency and polished glass.", StatsForClass[Class.Rogue]);
            cases.Add(c);

            //Case 14

            c = new Case(
            $"{ ThisPlayer} ponders on the past and the recurring dreams, that seems more vivid for each day that passes."
            );
            c.AddOutCome($"I hope to live a life of humility and search for a way to help others...", StatsForClass[Class.Cleric], HonorableTag);
            c.AddOutCome($"I am and have always been searching for the true source of wisdom and power…", StatsForClass[Class.Mage]);
            c.AddOutCome($"When my life is over, I will always be remembered for my heroism and personal beliefs…", StatsForClass[Class.Warrior]);
            c.AddOutCome($"Influence, riches and personal power is what i desire most...", StatsForClass[Class.Rogue]);
            cases.Add(c);

            //Case 15

            c = new Case(
            $"{ThisPlayer} were hired as help to an adventure expedition. While returning from a hunt {ThisPlayer} spot raiders wreaking havoc in your camp. " +
            $"{ThisPlayer} saw friends slaughtered and maimed around the campfire. {ThisPlayer} managed to save his colleague {OtherPlayer}{1} by executing vengeance on the raiders."
            );
            c.AddOutCome($"Through a quick prayer to your deity, I blade burst into flames and the blade finds it’s targets with ease. After the battle {ThisPlayer} soothes the wounds of {OtherPlayer}{1}.", StatsForClass[Class.Cleric]);
            c.AddOutCome($"Arcane sparks shoot out from my fingertips which strikes my enemies.", StatsForClass[Class.Mage]);
            c.AddOutCome($"I ready my blade and sneak up behind my foes to deliver the killing blow.", StatsForClass[Class.Rogue]);
            c.AddOutCome($"The brutal sight of my slain companions invokes an uncontrollable rage. I charge forward!", StatsForClass[Class.Warrior]);
            cases.Add(c);


            //Cases end -----------------------------------------------------------------------------------------------------------------------------------


            for (int i = 0; i < 27; i++)
            {
                c = new Case("Test case " + (i + 1));
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