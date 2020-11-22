using System;

namespace DataManagement.ConfigTypes
{
    [Serializable]
    public class LevelConfig : IEquatable<LevelConfig>
    {
        public string name;
        public string nonPlayerCharacter;
        public string scene;

        public LevelConfig(string name, string nonPlayerCharacter)
        {
            this.name = name;
            this.nonPlayerCharacter = nonPlayerCharacter;
        }

        public bool Equals(LevelConfig other)
        {
            return other != null && name == other.name;
        }
        
        public override bool Equals(object obj)
        {
            return obj is LevelConfig other && Equals(other);
        }

        public override int GetHashCode()
        {
            // ReSharper disable twice NonReadonlyMemberInGetHashCode
            return name != null ? name.GetHashCode() : 0;
        }
    }
}