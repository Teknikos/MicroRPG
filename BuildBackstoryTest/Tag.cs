using System;
using System.Collections.Generic;
using System.Text;

namespace BuildBackstoryTest
{
    public class Tag
    {
        public Tag(string name)
        {
            Name = name;
        }

        public readonly string Name;

        public static bool operator == (Tag tag, Tag tag2)
        {
            return string.Compare(tag?.Name, tag2?.Name, true) == 0;
        }

        public static bool operator !=(Tag tag, Tag tag2)
        {
            return !(tag == tag2);
        }

        public override bool Equals(object obj)
        {
            return obj is Tag tag &&
                   this == tag;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }

        
    }
}
