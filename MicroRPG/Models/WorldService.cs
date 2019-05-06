using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroRPG.Models
{
    public class WorldService
    {
        static List<Monster> monsters;
        static List<Obstacle> obstacles;
        static List<Puzzle> puzzles;

        static Monster[] GetMonsters()
        {
            if (monsters == null)
            {
                monsters = GenerateMonsters();
            }

            return monsters.ToArray();
        }

        private static List<Monster> GenerateMonsters()
        {
            return new List<Monster>
            {
                
            };
        }

        static Obstacle[] GetObstacles()
        {
            if (obstacles == null)
            {
                obstacles = GenerateObstacles();
            }

            return obstacles.ToArray();
        }

        private static List<Obstacle> GenerateObstacles()
        {
            return new List<Obstacle>
            {

            };
        }


        static Puzzle[] GetPuzzles()
        {
            if (puzzles == null)
            {
                puzzles = GeneratePuzzles();
            }

            return puzzles.ToArray();
        }

        private static List<Puzzle> GeneratePuzzles()
        {
            return new List<Puzzle>
            {

            };
        }
    }
}
