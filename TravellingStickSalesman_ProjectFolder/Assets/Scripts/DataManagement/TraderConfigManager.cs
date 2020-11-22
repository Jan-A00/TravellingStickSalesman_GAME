using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataManagement.ConfigTypes;
using Toolbox;
using UnityEngine;

namespace DataManagement
{
    public sealed class TraderConfigManager
    {
        private const string TraderConfigurationFileName = "TraderConfiguration.json";

        private readonly string traderConfigurationFilePath =
            Path.Combine(Application.streamingAssetsPath, TraderConfigurationFileName);

        private static readonly Lazy<TraderConfigManager> Lazy = new Lazy<TraderConfigManager>(
            () => new TraderConfigManager()
        );

        public static TraderConfigManager Instance => Lazy.Value;

        private readonly HashSet<TraderConfig> traderConfigurationData = new HashSet<TraderConfig>();

        public IEnumerable<TraderConfig> Configurations => traderConfigurationData.ToArray();
        public IEnumerable<string> TraderNames => traderConfigurationData.Select(x => x.name);

        public TraderConfigManager()
        {
            using (StreamReader reader = new StreamReader(File.OpenRead(traderConfigurationFilePath)))
            {
                string jsonString = reader.ReadToEnd();
                Debug.Log(jsonString);
                TraderConfigArray traderConfigArray = JsonUtility.FromJson<TraderConfigArray>(jsonString);
                TraderConfig[] traderConfigs = traderConfigArray.array;
                // TraderConfig[] traderConfigs = JsonHelper.ReadJsonArray<TraderConfig>(jsonString);
                foreach (TraderConfig traderConfig in traderConfigs)
                {
                    Debug.Log($"Processing Trader Config {traderConfig.name}");
                    traderConfigurationData.Add(traderConfig);
                }
            }
            Debug.Log("Completed Loading Trader Configurations...");
            foreach (string name in traderConfigurationData.Select(x => x.name))
            {
                Debug.Log($"Level: \"{name}\", has had its configuration loaded.");   
            }
        }

        public string OneTrueStickForTrader(string traderName) => traderConfigurationData.Where(trader => trader.name == traderName).Select(trader => trader.oneTrueStickWants).First();
        public string StickGivenForOneTrueStick(string traderName) => traderConfigurationData.Where(trader => trader.name == traderName).Select(trader => trader.oneTrueStickGives).First();
        public string StickGivenForOtherStick(string traderName) => traderConfigurationData.Where(trader => trader.name == traderName).Select(trader => trader.otherStickGives).First();

    }

}