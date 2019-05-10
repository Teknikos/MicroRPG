using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MicroRPG.Models.Backstory
{
    public class RelationTag : Tag
    {
        public RelationTag(string name, string description) : base(name)
        {
            IsExclusive = false;
            Description = description;
            TagOtherBy = null;
        }

        public string Description { get; set; }
        public int RelatesToID { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public RelationTag TagOtherBy { get; set; }

        public bool IsExclusive { get; set; }

        private static RelationTag CreateRelationTag(string name, string description, Player player, Player relatesTo,
            bool isExclusive = false, string otherName = null, string otherDescription = null)
        {
            if (otherName == null)
                otherName = name;
            if (otherDescription == null)
                otherDescription = description;
            RelationTag tag = new RelationTag(name, relatesTo.Name + description)
            {
                IsExclusive = isExclusive,
                RelatesToID = relatesTo.ID
            };
            RelationTag otherTag = new RelationTag(otherName, player.Name + otherDescription)
            {
                IsExclusive = isExclusive,
                RelatesToID = player.ID
            };
            tag.TagOtherBy = otherTag;

            return tag;
        }

        public static List<RelationTag> GenerateRelationTags(Player player, Player relatesTo)
        {
            List<RelationTag> res = new List<RelationTag>();
            RelationTag tag;
            const int GrownUpAge = 15;

            if (!player.Tags.Exists(t => (t as RelationTag)?.IsExclusive == true) &&
                !relatesTo.Tags.Exists(t => (t as RelationTag)?.IsExclusive == true))
            {
                // Sibling
                if (Math.Abs(player.Age - relatesTo.Age) < GrownUpAge)
                {
                    tag = CreateRelationTag("Sibling", " is your sibling.", player, relatesTo, true);
                    res.Add(tag);
                }
                // Parent / Child
                else if (player.Age - relatesTo.Age >= GrownUpAge)
                {
                    tag = CreateRelationTag("Parent", " is your child.", player, relatesTo, true, "Child", " is your parent".);
                    res.Add(tag);
                }
                // Cousins
                tag = CreateRelationTag("Cousin", " is your distant cousin.", player, relatesTo, true);
                res.Add(tag);
            }

            // Infatuated
            if (player.Age - relatesTo.Age < GrownUpAge || relatesTo.Age > GrownUpAge)
            {
                tag = new RelationTag("Infatuated", "You are in love with " + relatesTo.Name + ".")
                {
                    RelatesToID = relatesTo.ID
                };
                res.Add(tag);
            }

            // Cursed
            tag = CreateRelationTag("Cursed", " and you have both been cursed.", player, relatesTo);
            res.Add(tag);

            // Brawn / Brains
            tag = CreateRelationTag("Brawn", " is the brains and you are the brawn.", player, relatesTo, false, "Brains", " is the brawn and you are the brains.");
            res.Add(tag);

            // Mercenary / Employer
            tag = CreateRelationTag("Mercenary", " has hired you as a mercenary.", player, relatesTo, false, "Employer", " is hired as your mercenary.");
            res.Add(tag);

            // Oathsworn
            tag = CreateRelationTag("Oathsworn", " and you are bound by a sworn oath.", player, relatesTo);
            res.Add(tag);

            // Rivals
            tag = CreateRelationTag("Rivals", " and you friendly rivals.", player, relatesTo);
            res.Add(tag);

            // Childhood Friends
            if (Math.Abs(player.Age - relatesTo.Age) < GrownUpAge)
            {
                tag = CreateRelationTag("Friends", " and you are childhood friends.", player, relatesTo);
                res.Add(tag);
            }

            // Former co-workers
            tag = CreateRelationTag("Co-workers", " and you are former co-workers.", player, relatesTo);
            res.Add(tag);

            // Adventuring Companions
            tag = CreateRelationTag("AdventuringCompanions", " and you are longtime adventuring companions.", player, relatesTo);
            res.Add(tag);

            // Only survivors
            tag = CreateRelationTag("Survivors", " and you were the only survivors of a disastrous event.", player, relatesTo);
            res.Add(tag);

            // Treasure Map carriers
            tag = CreateRelationTag("TreasureMap", " and you each carry half of a treasure map.", player, relatesTo);
            res.Add(tag);

            // Secret
            tag = new RelationTag("Secret", $"You keep {relatesTo.Name} close because they know your secret.")
            {
                RelatesToID = relatesTo.ID
            };
            res.Add(tag);

            // Brothers in arms
            tag = CreateRelationTag("BrothersInArms", " and you are brothers in arms.", player, relatesTo);
            res.Add(tag);

            // Blood Debt
            tag = CreateRelationTag("BloodDebt", " owes you a debt...  in blood.", player, relatesTo, false, "Indebted", " has you owing them a debt... in blood.");
            res.Add(tag);

            // Trained
            tag = CreateRelationTag("Trained", " and you trained together.", player, relatesTo);
            res.Add(tag);

            // Servants
            tag = CreateRelationTag("Servants", " and you both serve the same supernatural power.", player, relatesTo);
            res.Add(tag);

            // Dreamer / Savior
            tag = CreateRelationTag("Dreamer", " was the one you dreamed would save your life, long before you met.", player, relatesTo, false, "DreamSavior", " dreamed that you would save their life, long before you met.");
            res.Add(tag);

            return res;
        }

        public void AttachRelation(Player player, Player relatesTo)
        {
            player.Tags.Add(this);
            if (TagOtherBy != null && relatesTo != null)
                relatesTo.Tags.Add(TagOtherBy);
        }


    }
}
