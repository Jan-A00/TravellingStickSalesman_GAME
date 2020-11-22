using System;

namespace DataManagement.StateTypes
{
    [Serializable]
    public class TraderState : GameState
    {
    public string traderName;
    public bool tradedWith;
    public bool completed;

    public TraderState(string traderName, bool tradedWith, bool completed)
    {
        this.traderName = traderName;
        this.tradedWith = tradedWith;
        this.completed = completed;

    }
    }
}