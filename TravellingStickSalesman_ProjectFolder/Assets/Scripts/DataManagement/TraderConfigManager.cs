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

        public static IEnumerable<TraderConfig> Configurations => Instance.traderConfigurationData.ToArray();
        public static IEnumerable<string> TraderNames => Instance.traderConfigurationData.Select(x => x.name);
        public static IEnumerable<string> OneTrueStickTradeResult() =>
            Instance.traderConfigurationData.Select(trader => trader.oneTrueStickGives);
        public static string OneTrueStickForTrader(string traderName) => Instance.traderConfigurationData.Where(trader => trader.name == traderName).Select(trader => trader.oneTrueStickWants).First();
        public static string StickGivenForOneTrueStick(string traderName) => Instance.traderConfigurationData.Where(trader => trader.name == traderName).Select(trader => trader.oneTrueStickGives).First();
        public static string StickGivenForOtherStick(string traderName) => Instance.traderConfigurationData.Where(trader => trader.name == traderName).Select(trader => trader.otherStickGives).First();

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


        public string StickGivenByTrader(string traderName, string stickWeAreGiving)
        {
            string traderOneTrueStickName = OneTrueStickForTrader(traderName);
            string traderOneTrueStickReward = StickGivenForOneTrueStick(traderName);
            string traderOtherStickReward = StickGivenForOtherStick(traderName);
            string stickTraderWillGive = traderOneTrueStickName == stickWeAreGiving ? traderOneTrueStickReward : traderOtherStickReward;
            return stickTraderWillGive;
        }

    }

}