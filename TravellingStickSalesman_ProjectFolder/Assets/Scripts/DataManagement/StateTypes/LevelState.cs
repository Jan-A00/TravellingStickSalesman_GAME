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
        public bool completed;

        public LevelState(string name, bool visited, bool current, bool completed)
        {
            this.name = name;
            this.current = current;
            this.visited = visited;
            this.completed = completed;
        }
    }
}