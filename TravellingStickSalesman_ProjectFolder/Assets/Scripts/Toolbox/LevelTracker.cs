using System;
using DataManagement;
using UnityEngine;

namespace Toolbox
{
    public class LevelTracker : MonoBehaviour
    {
        public void Start()
        {
            GameStateManager.Instance.UpdateCurrentLevel();
        }
    }
}