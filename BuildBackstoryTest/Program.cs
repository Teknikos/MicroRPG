using System;
using System.Collections.Generic;
using System.Linq;
using static BuildBackstoryTest.Constants;

namespace BuildBackstoryTest
{
    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {

            int numberOfCases = 9;

            int playerCount = GetChoice(MininumPlayers, MaximumPlayers,
                $"How many players? [{MininumPlayers}-{MaximumPlayers}] ", string.Empty);

            List<Player> party = CreatePlayers(playerCount);

            /* var relationCases = GenerateRelationCases(party); */
            CreateRelations(party);

            List<Case> cases = Case.GenerateCases();
            ShowCases(numberOfCases, playerCount, party, cases);
            foreach (Player player in party)
            {
                if (player.StatGainCount > 0)
                    player.Stats.MultiplyStats(1.0 / player.StatGainCount);
                Console.WriteLine(player.Stats);
            }

            CreateGoals();


        }

        private static void CreateGoals()
        {
            List<string> goals = new List<string>
            {
                "An important person has gone missing and your group has decided to help and be able to claim the bounty.",
                "Your party is determined to find out how to lift a curse.",
                "Your reputation has been stained and whether it is deserved or not you need to clear it.",
                "Rumours speak of a legendary artifact which you wish to find.",
                "A sudden ambush has made you all captives and now you need to escape!",
                "A shipwreck has left your group stranded in a foreign environment. You must find a way back to civilization!",
                "There has been a murder and you have all decided to solve it.",
                "Chaos ensues as magical disaster strikes the land. Find your way to safety or stop it at its source!",
                "The ruler is being overthrown in a coup and your group is forced to take action and choose sides.",
                "The city is under siege... Stop the invaders!",
            };

            Console.WriteLine("What is your groups common goal?");
            for (int i = 0; i < 3; i++)
            {
                string goal = goals[random.Next(goals.Count)];
                goals.Remove(goal);
                Console.WriteLine($"[{i+1}] {goal}");
            }
            Console.ReadKey(false);
        }

        private static void CreateRelations(List<Player> party)
        {
            const int numberOfChoices = 4;

            Player player, relatesTo;
            List<RelationTag> relationTags = null, shownTags;

            var randomOrder = party.OrderBy(p => random.Next()).ToArray();

            for (int i = 0; i < randomOrder.Length; i++)
            {
                Console.Clear();
                player = randomOrder[i];

                if (i + 1 < randomOrder.Length)
                    relatesTo = randomOrder[i + 1];
                else if (i != 0)
                    relatesTo = randomOrder[0];
                else
                    throw new Exception("At least two player must exist in the party to create relations!");

                relationTags = RelationTag.GenerateRelationTags(player, relatesTo);

                Console.WriteLine($"{player.Name} what is your relation to {relatesTo.Name}?");
                if (relationTags != null)
                {
                    shownTags = new List<RelationTag>();
                    int j;
                    for (j = 0; j < numberOfChoices && relationTags.Count > 0; j++)
                    {
                        int n = random.Next(relationTags.Count);
                        RelationTag tag = relationTags[n];
                        shownTags.Add(tag);
                        relationTags.RemoveAt(n);

                        Console.WriteLine($"[{j + 1}] {tag.Description}");
                    }
                    Console.WriteLine();
                    Console.WriteLine(player.Name);
                    int choice = GetChoice(max: j + 1) - 1;
                    //player.Tags.Add(shownTags[choice]);
                    shownTags[choice].AttachRelation(player);
                }
            }

            Console.Clear();
            foreach (Player partyMember in party)
            {
                Console.WriteLine($"{partyMember.Name}: ");
                foreach (Tag tag in partyMember.Tags)
                {
                    if (tag is RelationTag)
                    {
                        Console.WriteLine((tag as RelationTag).Description);
                    }
                }
                Console.WriteLine();
            }
            Console.ReadKey();

        }


        private static void ShowCases(int numberOfCases, int playerCount, List<Player> party, List<Case> cases)
        {

            int[] indexes = new int[playerCount];
            for (int i = 0; i < playerCount; i++)
            {
                indexes[i] = i;
            }

            for (int i = 0; i < numberOfCases; i++)
            {
                foreach (int playerIndex in indexes.OrderBy(p => random.Next()))
                {
                    var validCases = cases.Where(c => c.IsValid(party[playerIndex].Tags, party.Count));
                    if (validCases.Count() > 0)
                    {
                        Case c = validCases.ElementAt(random.Next(validCases.Count()));
                        cases.Remove(c);

                        Console.Clear();
                        Console.WriteLine(c.GetAdjustedDescription(party[playerIndex], party));
                        Console.WriteLine();

                        for (int j = 0; j < c.NumberOfOutcomes; j++)
                        {
                            Console.WriteLine(c.GetAdjustedOutcome(j, party[playerIndex], party));
                        }
                        Console.WriteLine();
                        Console.WriteLine($"{party[playerIndex].Name}");
                        c.ApplyToPlayer(GetChoice() - 1, party[playerIndex], party);
                        Console.WriteLine();
                    }
                }
            }

        }


        private static List<Player> CreatePlayers(int playerCount)
        {
            List<Player> party = new List<Player>();
            for (int n = 0; n < playerCount; n++)
            {
                Console.WriteLine($"Enter a name for player #{n + 1}");
                string name = Console.ReadLine();
                Console.WriteLine($"Enter age for {name}");
                if (!int.TryParse(Console.ReadLine(), out int age))
                {
                    age = 30;
                }
                party.Add(new Player(name, age));
            }
            return party;
        }


        private static int GetChoice(int min = 1, int max = 9, string msg = "Make your choice ", string errorMsg = "Invalid choice, try again ")
        {
            char keyChar;
            int keyNum;
            Console.Write(msg);
            while (true)
            {
                keyChar = Console.ReadKey(true).KeyChar;
                if (char.IsDigit(keyChar))
                {
                    keyNum = (int)char.GetNumericValue(keyChar);
                    if (keyNum >= min && keyNum <= max)
                    {
                        Console.WriteLine();
                        return keyNum;
                    }
                }

                if (!string.IsNullOrEmpty(errorMsg))
                {
                    Console.WriteLine();
                    Console.Write(errorMsg);
                }
            }
        }
    }
}
