using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataManagement.ConfigTypes;
using DataManagement.StateTypes;
using Toolbox;
using UnityEngine.SceneManagement;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DataManagement
{
    public class GameStateManager : Singleton<GameStateManager>
    {
        #region Shared Setup Stuff

        private string persistentDataPath;
        private const string GameStateFileName = "GameState.json";
        private bool gameDataInitialized; 
        private string GameStateFilePath => Path.Combine(persistentDataPath, GameStateFileName);
        public static bool StateExists
        {
            get
            {
                List<string> stateFilePaths = new List<string>
                {
                    Instance.LevelStateFilePath,
                    Instance.TraderStateFilePath,
                    Instance.InventoryItemStateFilePath,
                };
                return stateFilePaths.Select(File.Exists).All(x => x);
            }
        }
        
        #endregion
        
        #region Level State Management

        private const string LevelStateFileName = "LevelState.json";
        private string LevelStateFilePath => Path.Combine(persistentDataPath, LevelStateFileName);
        private LevelState[] levelStates;
        private bool levelStateInitialized;
        private readonly Lazy<LevelConfigManager> levelConfigManager = new Lazy<LevelConfigManager>();
        private IEnumerable<string> LevelNames => levelConfigManager.Value.LevelNames;

        #region State File Management Methods
        
        public void LoadLevelStateFromDisk()
        {
            using (StreamReader reader = new StreamReader(File.OpenRead(LevelStateFilePath)))
            {
                string jsonString = reader.ReadToEnd();
                LevelStateArray stateArray = JsonUtility.FromJson<LevelStateArray>(jsonString);
                levelStates = stateArray.array;
            }
        }

        public void UpdateLevelStateOnDisk()
        {
            LevelStateArray stateArray = new LevelStateArray(levelStates);
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(LevelStateFilePath)))
            {
                string jsonData = JsonUtility.ToJson(stateArray, true);
                char[] charArray = jsonData.ToCharArray();
                writer.Write(charArray);
                writer.Flush();
                writer.BaseStream.SetLength(writer.BaseStream.Position);
            }
        }

        private void PurgeLevelState()
        {
            if (File.Exists(LevelStateFilePath)) File.Delete(LevelStateFilePath);
            if (levelStateInitialized) levelStateInitialized = false;
        }
        
        public void InitializeLevelState()
        {
            if (File.Exists(LevelStateFilePath))
            {
                LoadLevelStateFromDisk();
            }
            else
            {
                levelStates = LevelNames.Select(levelName => new LevelState(levelName, false, false, false)).ToArray();
                UpdateLevelStateOnDisk();
            }
            levelStateInitialized = true;
        }

        #endregion

        public string CurrentLevel()
        {
            return levelConfigManager.Value.CurrentLevelName();
        }
        
        private bool ValidLevel(string levelName)
        {
            return gameDataInitialized && levelConfigManager.Value.LevelNames.Contains(levelName);
        }

        public void UpdateCurrentLevel()
        {
            string levelName = CurrentLevel();
            levelStates.ToList().ForEach(level => level.current = level.name == levelName);
            UpdateLevelStateOnDisk();
        }
        
        public void MarkLevelAsVisited(string levelName)
        {
            if (!ValidLevel(levelName)) throw new ArgumentException("Tried to mark a level as visited when it doesn't exist.");
            levelStates.Where(level => level.name == levelName).ToList().ForEach(level => level.visited = true);
            UpdateLevelStateOnDisk();
        }
        
        public void MarkLevelAsCompleted(string levelName)
        {
            if (!ValidLevel(levelName)) throw new ArgumentException("Tried to mark a level as complete when it doesn't exist.");
            if (!levelStates.Where(level => level.name == levelName).Select(level => level.completed).First()) throw new ArgumentException("Level already complete!");
            levelStates.Where(level => level.name == levelName).ToList().ForEach(level => level.completed = true);
            UpdateLevelStateOnDisk();
        }

        #endregion

        #region Trader State Management

        private const string TraderStateFileName = "TraderState.json";
        private string TraderStateFilePath => Path.Combine(persistentDataPath, TraderStateFileName);
        private TraderState[] traderStates;
        private bool traderStateInitialized;
        private readonly Lazy<TraderConfigManager> traderConfigManager = new Lazy<TraderConfigManager>();
        
        private IEnumerable<string> TraderNames => traderConfigManager.Value.TraderNames;
        
        #region State File Management Methods
        
        public void LoadTraderStateFromDisk()
        {
            using (StreamReader reader = new StreamReader(File.OpenRead(TraderStateFilePath)))
            {
                string jsonString = reader.ReadToEnd();
                TraderStateArray stateArray = JsonUtility.FromJson<TraderStateArray>(jsonString);
                traderStates = stateArray.array;
            }
        }
        
        public void UpdateTraderStateOnDisk()
        {
            TraderStateArray stateArray = new TraderStateArray(traderStates);
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(TraderStateFilePath)))
            {
                string jsonData = JsonUtility.ToJson(stateArray, true);
                char[] charArray = jsonData.ToCharArray();
                writer.Write(charArray);
                writer.Flush();
                writer.BaseStream.SetLength(writer.BaseStream.Position);
            }
        }

        public void InitializeTraderState()
        {
            if (File.Exists(TraderStateFilePath))
            {
                LoadTraderStateFromDisk();
            }
            else
            {
                traderStates = TraderNames.Select(traderName => new TraderState(traderName, false, false)).ToArray();
                UpdateTraderStateOnDisk();
            }
            traderStateInitialized = true;
        }
        private void PurgeTraderState()
        {
            if (File.Exists(TraderStateFilePath)) File.Delete(TraderStateFilePath);
            if (traderStateInitialized) traderStateInitialized = false;
        }

        #endregion

        internal string CurrentTraderName()
        {
            return levelConfigManager.Value.LevelTraderName();
        }
        
        private bool ValidTrader(string traderName)
        {
            return gameDataInitialized && traderConfigManager.Value.TraderNames.Contains(traderName);
        }
        
        public void MarkCurrentTraderAsDone()
        {
            string traderName = CurrentTraderName(); 
            traderStates.Where(trader => trader.traderName == traderName).ToList().ForEach(trader =>
            {
                trader.completed = true;
                trader.tradedWith = true;
            });
            UpdateTraderStateOnDisk();
        }

        internal bool AlreadyTradedWithThisTrader()
        {
            string traderName = levelConfigManager.Value.LevelTraderName();
            return traderStates.Where(trader => trader.traderName == traderName).Any(trader => trader.completed);
        } 

        #endregion
        
        #region Inventory State Management
        
        private const string InventoryItemStateFileName = "InventoryItemState.json";
        private string InventoryItemStateFilePath => Path.Combine(persistentDataPath, InventoryItemStateFileName);
        internal InventoryItemState[] inventoryItemStates;
        private bool inventoryItemStateInitialized;
        internal readonly Lazy<StickConfigManager> stickConfigManager = new Lazy<StickConfigManager>();
        private IEnumerable<StickConfig> StickConfigs => stickConfigManager.Value.Sticks;

        #region State File Management Methods

        public void LoadInventoryItemStateOnDisk()
        {
            using (StreamReader reader = new StreamReader(File.OpenRead(InventoryItemStateFilePath)))
            {
                string jsonString = reader.ReadToEnd();
                InventoryItemStateArray inventoryItemStateArray = JsonUtility.FromJson<InventoryItemStateArray>(jsonString);
                inventoryItemStates = inventoryItemStateArray.array;
            }
        }

        public void UpdateInventoryItemStateOnDisk()
        {
            InventoryItemStateArray stateArray = new InventoryItemStateArray(inventoryItemStates);
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(InventoryItemStateFilePath)))
            {
                string jsonData = JsonUtility.ToJson(stateArray, true);
                char[] charArray = jsonData.ToCharArray();
                writer.Write(charArray);
                writer.Flush();
                writer.BaseStream.SetLength(writer.BaseStream.Position);
            }
        }

        private void PurgeInventoryState()
        {
            if (File.Exists(InventoryItemStateFilePath)) File.Delete(InventoryItemStateFilePath);
            if (inventoryItemStateInitialized) inventoryItemStateInitialized = false;
        }
        
        public void InitializeInventoryItemState()
        {
            if (File.Exists(InventoryItemStateFilePath))
            {
                LoadInventoryItemStateOnDisk();
            }
            else
            {
                inventoryItemStates = StickConfigs.Select(stick => new InventoryItemState(stick.name, stick.startingStick, stick.startingStick, false)).ToArray();
                UpdateInventoryItemStateOnDisk();
            }
            inventoryItemStateInitialized = true;
        }
        
        #endregion
        
        public IEnumerable<InventoryItemState> CurrentInventoryStickInventoryItems()
        {
            return inventoryItemStates.Where(stick => stick.carrying).ToArray();
        }
        public IEnumerable<string> ValidStickNames() =>
            stickConfigManager.Value.Sticks.Select(stick => stick.name).ToArray();
      
        public IEnumerable<string> CurrentInventoryStickNames() => 
            CurrentInventoryStickInventoryItems().Select(stick => stick.stickName).ToArray();
        
        #endregion
        
        #region Combined State Management
        
        public void InitializeGameState()
        {
            InitializeLevelState();
            InitializeTraderState();
            InitializeInventoryItemState();
            gameDataInitialized = true;
        }
        
        public void PurgeState()
        {
            PurgeLevelState();
            PurgeTraderState();
            PurgeInventoryState();
        }

        public void ResetState()
        {
            PurgeState();
            InitializeGameState();
        }

        public void ReturnToLevel()
        {
            string levelToResume = Instance.levelStates.Where(level => level.current).Select(level => level.name).First();
            string sceneToResume = Instance.levelConfigManager.Value.SceneForLevel(levelToResume);
            SceneManager.LoadScene(sceneToResume);
        }
        
        #endregion
        
        public void Awake()
        {
            persistentDataPath = Application.persistentDataPath;
#if UNITY_EDITOR
            if (gameObject.scene.name == "MainMenu") return;
            if (!EditorApplication.isPlaying) return;
            Debug.LogWarning("Forced to Initialize game state due to editor playing scene.");
            InitializeGameState();
            Debug.Log("Game State has been initialized.");
#endif
        }

        public void TradeStickForStick(string stickWeAreGiving, string stickWeAreReceiving)
        {
            IEnumerable<string> validStickNames = (string[]) ValidStickNames();
            IEnumerable<string> currentInventoryStickNames = (string[]) CurrentInventoryStickNames();
            if (!validStickNames.Contains(stickWeAreGiving)) throw new ArgumentException("Tried to trade a stick that doesn't exist.");
            if (!validStickNames.Contains(stickWeAreReceiving)) throw new ArgumentException("Tried to trade a stick that doesn't exist.");
            if (!currentInventoryStickNames.Contains(stickWeAreGiving)) throw new ArgumentException("Tried to trade a stick you weren't carrying.");
            if (currentInventoryStickNames.Contains(stickWeAreReceiving)) throw new ArgumentException("Already carrying that stick.");
            inventoryItemStates.Where(x => x.stickName == stickWeAreGiving).ToList().ForEach(x => x.carrying = false);
            inventoryItemStates.Where(x => x.stickName == stickWeAreGiving).ToList().ForEach(x => x.traded = true);
            inventoryItemStates.Where(x => x.stickName == stickWeAreReceiving).ToList().ForEach(x => x.carrying = true);
            inventoryItemStates.Where(x => x.stickName == stickWeAreReceiving).ToList().ForEach(x => x.obtained = true);
            UpdateInventoryItemStateOnDisk();
        }

        public StickConfig[] CurrentInventoryStickConfigs(bool debug = false)
        {
            // ReSharper disable once InvertIf
            if (debug) foreach (string stickName in stickConfigManager.Value.Sticks.Select(x => x.name))
            {
                Debug.Log($"Inventory contains: {stickName}");   
            }
            return stickConfigManager.Value.Sticks.Where(stickConfig =>
                CurrentInventoryStickNames().Any(stickName => stickName == stickConfig.name)).ToArray();
        }

        public void GiveStickToTrader(string stickName)
        {
            string traderName = levelConfigManager.Value.LevelTraderName();
            string traderOneTrueStickName = traderConfigManager.Value.OneTrueStickForTrader(traderName);
            string traderOneTrueStickReward = traderConfigManager.Value.StickGivenForOneTrueStick(traderName);
            string traderOtherStickReward = traderConfigManager.Value.StickGivenForOtherStick(traderName);
            string stickTraderWillGive = traderOneTrueStickName == stickName ? traderOneTrueStickReward : traderOtherStickReward;
            TradeStickForStick(stickName, stickTraderWillGive);
        }
    }
}