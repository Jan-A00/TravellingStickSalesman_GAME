using System;

namespace DataManagement.StateTypes
{
    [Serializable]
    public class LevelStateArray
    {
        public LevelState[] array;

        public LevelStateArray(LevelState[] array)
        {
            this.array = array;
        }
    }
}