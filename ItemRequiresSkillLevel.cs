using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using HarmonyLib;
using ServerSync;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace ItemRequiresSkillLevel
{
    [BepInPlugin(PluginGUID, PluginGUID, Version)]
    [BepInDependency("com.orianaventure.mod.WorldAdvancementProgression", BepInDependency.DependencyFlags.SoftDependency)]
    [HarmonyPatch]
    public class ItemRequiresSkillLevel : BaseUnityPlugin
    {
        public const string Version = "1.4.0";
        public const string PluginGUIDold = "Detalhes.ItemRequiresSkillLevel";
        public const string PluginGUID = "WackyMole.ItemRequiresSkillLevel";
        static ConfigSync configSync = new ConfigSync(PluginGUID) { DisplayName = PluginGUID, CurrentVersion = Version, MinimumRequiredVersion = Version };

        public static CustomSyncedValue<Dictionary<string, string>> YamlData = new CustomSyncedValue<Dictionary<string, string>>(configSync, "ItemRequiresSkillLevel yaml");
        internal static ConfigEntry<bool>? serverSyncLock;
        internal static ConfigEntry<bool> GenerateListWithAllEquipableItems;
        internal static ConfigEntry<string> RequiresText;
        internal static ConfigEntry<string> cantEquipColor;
        internal static ConfigEntry<string> canEquipColor;
        public static bool IsWAPInstalled() =>
               BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.orianaventure.mod.WorldAdvancementProgression");

        public static bool hasWAP = false;

        Harmony _harmony = new Harmony(PluginGUID);

        internal static string ConfigFileName = PluginGUID + ".yml";
        public static string ConfigPath = Paths.ConfigPath + Path.DirectorySeparatorChar + ConfigFileName;
        public static string AllItemsConfigPath = Paths.ConfigPath + Path.DirectorySeparatorChar + PluginGUID + "ALLITEMS" + ".yml";

        private void Awake()
        {
            hasWAP = IsWAPInstalled();
            RequirementService.Init();
            _harmony.PatchAll();
            YamlData.ValueChanged += RequirementService.Load;
            var val = (new string[] { ConfigPath }.ToDictionary(f => f, File.ReadAllText));
            YamlData.AssignLocalValue(val);
            SetupWatcher();

            serverSyncLock = config("General", "Lock Configuration", true, "Lock Configuration");
            GenerateListWithAllEquipableItems = config("General", "GenerateListWithAllEquipableItems", false, "GenerateListWithAllEquipableItems");
            canEquipColor = config("General", "canEquipColor", "green", "canEquipColor");
            cantEquipColor = config("General", "cantEquipColor", "red", "cantEquipColor");
            GenerateListWithAllEquipableItems = config("General", "GenerateListWithAllEquipableItems", false, "GenerateListWithAllEquipableItems");
            RequiresText = config("General", "RequiresText", "\nRequires <color={0}>{1} {2}</color>", "RequiresText");

            configSync.AddLockingConfigEntry(serverSyncLock);
        }

        private void SetupWatcher()
        {
            FileSystemWatcher watcher = new(Paths.ConfigPath, ConfigFileName);
            watcher.Changed += ReadFile;
            watcher.Created += ReadFile;
            watcher.Renamed += ReadFile;
            watcher.IncludeSubdirectories = true;
            watcher.SynchronizingObject = ThreadingHelper.SynchronizingObject;
            watcher.EnableRaisingEvents = true;
        }
        private void ReadFile(object sender, FileSystemEventArgs e)
        {
            var val = new string[] { ConfigPath }.ToDictionary(f => f, File.ReadAllText);
            YamlData.AssignLocalValue(val);
        }

        ConfigEntry<T> config<T>(string group, string name, T value, ConfigDescription description, bool synchronizedSetting = true)
        {
            ConfigEntry<T> configEntry = Config.Bind(group, name, value, description);

            SyncedConfigEntry<T> syncedConfigEntry = configSync.AddConfigEntry(configEntry);
            syncedConfigEntry.SynchronizedConfig = synchronizedSetting;

            return configEntry;
        }
      
        ConfigEntry<T> config<T>(string group, string name, T value, string description, bool synchronizedSetting = true) => config(group, name, value, new ConfigDescription(description), synchronizedSetting);
    }
}


/*
 *  ItemRequiresSkillLevel.hasWAP
if (ZoneSystem.instance.CheckKey("defeated_bonemass", GameKeyType.Player))
{

}
//  Game.instance.GetPlayerProfile().m_knownWorldKeys.IncrementOrSet(text)
// if (Game.instance.GetPlayerProfile().m_knownWorldKeys.TryGetValue("player" + __instance.m_dropPrefab.name, out var txt))
// ZoneSyste.
// 	private readonly HashSet<string> m_uniques = new HashSet<string>(); in Player


 * ZoneSystem
 * public bool CheckKey(string key, GameKeyType type = GameKeyType.Global, bool trueWhenKeySet = true)
    {
        switch (type)
        {
        case GameKeyType.Global:
            return instance.GetGlobalKey(key) == trueWhenKeySet;
        case GameKeyType.Player:
            if ((bool)Player.m_localPlayer)
            {
                return Player.m_localPlayer.HaveUniqueKey(key) == trueWhenKeySet;
            }
            return false;
        default:
            ZLog.LogError("Unknown GameKeyType type");
            return false;
        }
    }
 * 
 * 
 * 
 * 
 */