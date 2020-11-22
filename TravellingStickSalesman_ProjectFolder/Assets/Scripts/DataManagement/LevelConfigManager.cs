using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataManagement.ConfigTypes;
using DataManagement.StateTypes;
using Toolbox;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DataManagement
{
    public sealed class LevelConfigManager
    {
        private const string LevelConfigurationFileName = "LevelConfiguration.json";
        private readonly string levelConfigurationFilePath =
            Path.Combine(Application.streamingAssetsPath, LevelConfigurationFileName);
        
        public static LevelConfigManager Instance => Lazy.Value;
        private static readonly Lazy<LevelConfigManager> Lazy = new Lazy<LevelConfigManager>(
            () => new LevelConfigManager()
        );
        
        private readonly HashSet<LevelConfig> levelData = new HashSet<LevelConfig>();
        private LevelConfigArray rawLevelArray;
        public IEnumerable<string> LevelNames => levelData.Select(level => level.name);        

        public LevelConfigManager()
        {
            using (StreamReader reader = new StreamReader(File.OpenRead(levelConfigurationFilePath)))
            {
                string jsonString = reader.ReadToEnd();
                Debug.Log(jsonString);
                LevelConfigArray levelConfigArray = JsonUtility.FromJson<LevelConfigArray>(jsonString);
                foreach (LevelConfig levelConfig in levelConfigArray.array)
                {
                    Debug.Log($"Processing Level Config {levelConfig.name}");
                    levelData.Add(levelConfig);
                }
            }
            Debug.Log("Completed Loading Level Configurations...");
            foreach (string name in levelData.Select(x => x.name))
            {
                Debug.Log($"Level: \"{name}\", has had its configuration loaded.");   
            }
        }

        public string LevelTraderName()
        {
            Scene activeScene = SceneManager.GetActiveScene();
            LevelConfig levelConfig = levelData.First(level => level.scene == activeScene.name);
            return levelConfig.nonPlayerCharacter;
        }
        
        public string CurrentLevelName()
        {
            Scene activeScene = SceneManager.GetActiveScene();
            LevelConfig levelConfig = levelData.First(level => level.scene == activeScene.name);
            return levelConfig.name;
        }

        public string SceneForLevel(string levelName)
        {
            LevelConfig levelConfig = levelData.First(level => level.name == levelName);
            return levelConfig.scene;
        }
    }
}