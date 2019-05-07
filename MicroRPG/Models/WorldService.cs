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

        public static Monster[] GetMonsters()
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
                new Monster {Name="Desert Cobra", Tags=new List<string>{"Desert, Forest" }, Special="Special: Whenever a player is hit by this creature, roll 1d6. On a result of 1, you become paralyzed for 1 hour by its poison.", Speed=12, HP=12, Reduction=2, Damage="2d6"},
new Monster {Name="Kingsbeetle", Tags=new List<string>{"Desert, Forest" }, Special="Special: This large insect is incredibly durable and can barely be damaged by anything other than magical energy.", Speed=4, HP=10, Reduction=9, Damage="1d6"},
new Monster {Name="Oasis Raptor", Tags=new List<string>{"Desert, Forest" }, Special="Special: Once it attacks a player, it jaws locks and will not stop biting and ripping until it, or that unlucky victim is torn to pieces.", Speed=8, HP=10, Reduction=2, Damage="2d6"},
new Monster {Name="Young Swamp Wyvern", Tags=new List<string>{"Forest" }, Special="Special: Wyverns are reptilian horrors with sharp claws, a barbed tail and a vicious bite.It can attack from unexpected angles, giving them more attacks than any other creature.", Speed=7, HP=15, Reduction=1, Damage="3d6"},
new Monster {Name="Forest Nymph", Tags=new List<string>{"Forest, Village" }, Special="Special: Nymphs are magical creatures that can be both good and evil.They are infused with magical energy, they can cast several spells at will: Control Weather, Goodberry, Cure Light Wounds, Magic Missile, Summon Animal.", Speed=8, HP=10, Reduction=1, Damage="1d6"},
new Monster {Name="Cheetah", Tags=new List<string>{"Forest, Village" }, Special="Special: Cheetahs are natural and stealthy hunters and have exceptional speed, matched by few.", Speed=18, HP=12, Reduction=0, Damage="2d6"},
new Monster {Name="Goblin", Tags=new List<string>{"Forest, Village, Ruins" }, Special="Special: They are weak and cowardly.Always attacks in groups.", Speed=5, HP=8, Reduction=1, Damage="1d6"},
new Monster {Name="Orc", Tags=new List<string>{"Ruins, Forest, Village" }, Special="Special: Orcs are brutish creatures, often naturally skilled in combat.They will critically hit any target if they roll two 6:es, for 3d6 damage.", Speed=4, HP=14, Reduction=3, Damage="2d6"},
new Monster {Name="Spectre", Tags=new List<string>{"Desert, Ruins" }, Special="Special: Special: Spectres are unnatural creatures held on the natural plane by necromantic energies.They are almost invulnerable to normal weapons. They can be damaged only by magic.", Speed=8, HP=8, Reduction=15, Damage="1d6"},
new Monster {Name="Ogre", Tags=new List<string>{"Desert, Ruins" }, Special="Special: Ogres are many times bigger than a normal human.Those unlucky enough to disturb an ogre better run or get crushed by its massive strength.Does massive damage on hit.", Speed=4, HP=30, Reduction=2, Damage="5d6"},
new Monster {Name="Town Guard", Tags=new List<string>{"Forest, Village" }, Special="Special: Humans are good planners and very organized.If you attack a townsperson, they will likely warn others and ask for help if any other people are nearby.", Speed=6, HP=12, Reduction=3, Damage="1d6"},
new Monster {Name="Werewolf", Tags=new List<string>{"Forest, Village" }, Special="Special: Whenever a player is hit by this creature, roll 1d6. On a result of 1, the player become feverish and begins the first step of transformation…", Speed=9, HP=12, Reduction=2, Damage="2d6"}
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
                new Obstacle {Name = "Trapped in a cave", Description = "Trapped in a cave, find a way out." },
                new Obstacle {Name ="Fall trap", Description="Fall trap, escape a slow death."},
                new Obstacle {Name = "Maze", Description= "You find yourself in a maze, it seems to never end."},
                new Obstacle{ Name = "Nightmare", Description= "After a rest, the party finds themselves in a dream-like state, is it reality or a nightmare?"},
                new Obstacle{ Name = "Denied Entrance", Description= "The group is denied entrance to a location of interest."},
                new Obstacle{ Name = "Imprisoned", Description="The group has been captured and imprisoned. Escape your captors."},
                new Obstacle{ Name = "Thick vegetation", Description="Thick vegetation creeps up on you and envelop you. Find your way out!"},
                new Obstacle{ Name = "Secret door", Description="The person passing through must whisper their true name or entry is barred."},
                new Obstacle{ Name = "Blood door", Description="Those seeking entry must shed their blood on the portal."},
                new Obstacle{ Name = "Door of believers", Description="Only the penitent may pass."},
                new Obstacle{ Name = "Dark door", Description="When no light touches, the way will open"},
                new Obstacle{ Name = "Invisible door", Description="The door is invisible to the naked eye"},
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
                new Puzzle {Name="Stone guards", Description="Two [medium creature] statues guard an entrance, they will come alive to defend it. Unless someone can dispel their magic. The statues will mirror the moves of the players. Hitting a group member will make the statues destroy each other."},
new Puzzle {Name="Doppelganger", Description="Give a player a note: “You have been trapped and replaced by a doppelganger. Until the others figure it out or find the real player, you will try to make them fail/ die, but not obviously."},
new Puzzle { Name ="Lonely soul", Description="The group encounters a[strong creature], she is lonely and in despair.If the players try to help her / talk to her, she will return the favour.If they attack her, she will curse them and fight them. If there is a suitable male in the party she might fall in love if they treat her with respect and join them."},
new Puzzle { Name ="Mysterious altar", Description="As they enter a room they find an altar.Suddenly noxious gas starts to fill the room.Either make a worthy sacrifice or leave the room and find another path."},
new Puzzle { Name ="Mysterious pool", Description="A room ends in a dead end with a dark pool.They must dive in to find an opening or be stuck and having to turn around."},
new Puzzle { Name ="Magical runes", Description="The walls of a room is filled with 9 magical runes on the walls.If they try to decipher the runes, they can see the following ancient text. 'Wise, Death, Nature, Will, Sleep, Agony, Enter, Guilt, Restoration'. Combining the first and every third runes will make them glow and let them progress.Any other combination will cause a keyword related reaction."},
new Puzzle { Name ="Magical corridor", Description="As the adventurers close the door behind them, they find themselves in a dark corridor with stars all over the ceiling and walls.They see a door in front of them.However, no matter how far they reach for it, the corridor keeps stretching further and further into nothing.They must touch the door they entered and hold hands so that the first person can " +
"reach the other door and open it. A powerful spellcaster can dispel the illusion. Reward creative thinking and team-work."},
new Puzzle {Name="Riddle", Description="'You eat something you neither plant nor plow.It is the son of water, but if water touches it, it dies.' Salt, Ice"},
new Puzzle {Name="Riddle", Description="Of all your possessions, I am the hardest to guard. If you have me, you will want to share me. If you share me, you no longer have me.' Secret"},
new Puzzle {Name="Riddle", Description="Alive as you but without breath, as cold in my life as in my death; never a thirst though. I always drink dressed in a mail but never a cling.' Fish"},
new Puzzle {Name="Riddle", Description="I am free for the taking through all of your life, though given but once at birth. I am less than nothing in weight, but will fell the strongest of you if held.' Breath"},
new Puzzle {Name="Riddle", Description="I have holes throughout, from back to front and top to bottom to core. More nothing than something within, and yet I still hold water.' A Sponge"},
new Puzzle {Name="Riddle", Description="They flow and leap, but only as you pass. Dress yourself in darkest black, and they are darker still. Always they flee in the light, though without the sun there would be none.' Shadows"},
new Puzzle {Name="Riddle", Description="I can have no color, though there may be darkness within. I have no weight and hold nothing, and if placed in a container it becomes lighter.' A Hole"}
            };
        }
    }
}
