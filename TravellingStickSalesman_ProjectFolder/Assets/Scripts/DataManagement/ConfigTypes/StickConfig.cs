using System;
using System.IO;
using UnityEngine;
using Toolbox;

namespace DataManagement.ConfigTypes
{
    [Serializable]
    public class StickConfig : IEquatable<StickConfig>, ICloneable
    {
        public string name;
        public string description;
        public bool startingStick;
        public string spritePathForNormalMode;
        public string spritePathForHighlightedMode;
        [field:NonSerialized] public Sprite spriteForNormal;
        [field:NonSerialized] public Sprite spriteForHighlighted;
        
        public StickConfig(string name, bool startingStick)
        {
            this.name = name;
            this.startingStick = startingStick;
        }

        public void LoadSprites()
        {
            if (File.Exists(spritePathForNormalMode))
            {
                spriteForNormal = ImageToSprite.LoadNewSprite(spritePathForNormalMode);
            }

            if (File.Exists(spritePathForHighlightedMode))
            {
                spriteForHighlighted = ImageToSprite.LoadNewSprite(spritePathForHighlightedMode);
            }
        }

        public bool Equals(StickConfig other)
        {
            return other != null && name == other.name;
        }

        public override bool Equals(object obj)
        {
            return obj is StickConfig other && Equals(other);
        }

        public override int GetHashCode()
        {
            // ReSharper disable twice NonReadonlyMemberInGetHashCode
            return name != null ? name.GetHashCode() : 0;
        }
        
        #region ICloneable Members

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}