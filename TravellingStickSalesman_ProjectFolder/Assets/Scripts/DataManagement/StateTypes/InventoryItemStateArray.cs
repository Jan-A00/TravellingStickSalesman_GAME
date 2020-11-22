using System;

namespace DataManagement.StateTypes
{
    [Serializable]
    public class InventoryItemStateArray
    {
        public InventoryItemState[] array;

        public InventoryItemStateArray(InventoryItemState[] array)
        {
            this.array = array;
        }
    }
}