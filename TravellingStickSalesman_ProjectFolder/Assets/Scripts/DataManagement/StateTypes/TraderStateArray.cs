using System;

namespace DataManagement.StateTypes
{
    [Serializable]
    public class TraderStateArray
    {
        public TraderState[] array;

        public TraderStateArray(TraderState[] array)
        {
            this.array = array;
        }
    }
}