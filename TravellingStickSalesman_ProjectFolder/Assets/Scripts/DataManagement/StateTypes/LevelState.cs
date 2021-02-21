using System;
using System.Diagnostics.CodeAnalysis;

namespace DataManagement.StateTypes
{
    [Serializable]
    public class LevelState : GameState
    {
        public string name;
        public bool visited;
        public bool current;
        public bool readyToTrade;
        public bool tradeComplete;
        public string receivedStick;
        public bool puzzleCompleted;
        public bool dialogueComplete; 

        public LevelState(string name, bool visited, bool current, bool readyToTrade, bool tradeComplete, string receivedStick, bool puzzleCompleted, bool dialogueComplete)
        {
            this.name = name;
            this.current = current;
            this.visited = visited;
            this.readyToTrade = readyToTrade;
            this.tradeComplete = tradeComplete;
            this.receivedStick = receivedStick;
            this.puzzleCompleted = puzzleCompleted;
            this.dialogueComplete = dialogueComplete;
        }
    }
}