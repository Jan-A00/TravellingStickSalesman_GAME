using System;

namespace DataManagement.StateTypes
{
    [Serializable]
    public class InventoryItemState : GameState
    {
        public string stickName;
        public bool obtained;
        public bool carrying;
        public bool traded;

        public InventoryItemState(string stickName, bool obtained, bool carrying, bool traded)
        {
            this.stickName = stickName;
            this.obtained = obtained;
            this.carrying = carrying;
            this.traded = traded;
        }
    }
}