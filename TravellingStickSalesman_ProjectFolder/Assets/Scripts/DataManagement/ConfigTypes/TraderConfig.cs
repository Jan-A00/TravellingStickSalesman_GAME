using System;

namespace DataManagement.ConfigTypes
{
    [Serializable]
    public class TraderConfig : IEquatable<TraderConfig>
    {
        public string name;
        public string oneTrueStickWants;
        public string oneTrueStickGives;
        public string otherStickGives;

        public TraderConfig(
            string name, string oneTrueStickWants, 
            string oneTrueStickGives, string otherStickGives)
        {
            this.name = name;
            this.oneTrueStickGives = oneTrueStickGives;
            this.oneTrueStickWants = oneTrueStickWants;
            this.otherStickGives = otherStickGives;
        }

        public bool Equals(TraderConfig other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return name == other.name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((TraderConfig) obj);
        }

        public override int GetHashCode()
        {
            // ReSharper disable twice NonReadonlyMemberInGetHashCode
            return name != null ? name.GetHashCode() : 0;
        }
    }
}