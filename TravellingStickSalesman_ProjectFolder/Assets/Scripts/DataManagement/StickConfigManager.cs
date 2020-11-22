using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataManagement.ConfigTypes;
using Toolbox;
using UnityEngine;

namespace DataManagement
{
    public sealed class StickConfigManager
    {
        private const string StickConfigurationFileName = "StickConfiguration.json";

        private readonly string stickConfigurationFilePath =
            Path.Combine(Application.streamingAssetsPath, StickConfigurationFileName);

        private static readonly Lazy<StickConfigManager> Lazy = new Lazy<StickConfigManager>(
            () => new StickConfigManager()
        );
        
        private readonly HashSet<StickConfig> stickData = new HashSet<StickConfig>();

        public IEnumerable<StickConfig> Sticks => stickData.ToArray();
        public IEnumerable<string> StickNames => stickData.Select(x => x.name);

        public StickConfigManager()
        {
            using (StreamReader reader = new StreamReader(File.OpenRead(stickConfigurationFilePath)))
            {
                string jsonString = reader.ReadToEnd();
                Debug.Log(jsonString);
                StickConfigArray stickConfigArray = JsonUtility.FromJson<StickConfigArray>(jsonString);
                StickConfig[] levelConfigs = stickConfigArray.array;
                // StickConfig[] levelConfigs = JsonHelper.ReadJsonArray<StickConfig>(jsonString);
                foreach (StickConfig stickConfig in levelConfigs)
                {
                    Debug.Log($"Processing Stick Config {stickConfig.name}");
                    stickConfig.LoadSprites();
                    stickData.Add(stickConfig);
                }
            }
            Debug.Log("Completed loading Stick Configurations...");
            foreach (string name in stickData.Select(x => x.name))
            {
                Debug.Log($"Stick: \"{name}\", has had its configuration loaded.");   
            }
        }
    }
}