using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using HarmonyLib;
using ServerSync;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ItemRequiresSkillLevel
{
    [BepInPlugin(PluginGUID, PluginName, Version)]
    [BepInDependency("com.orianaventure.mod.WorldAdvancementProgression", BepInDependency.DependencyFlags.SoftDependency)]
    [HarmonyPatch]
    public class ItemRequiresSkillLevel : BaseUnityPlugin
    {
        public const string Version = "1.4.0";
        public const string PluginGUIDold = "Detalhes.ItemRequiresSkillLevel";
        public const string PluginGUID = "WackyMole.ItemRequiresSkillLevel";
        public const string PluginName = "ItemRequiresSkillLevel";

        static ConfigSync configSync = new ConfigSync(PluginGUID) { DisplayName = PluginName, CurrentVersion = Version, MinimumRequiredVersion = Version };
        public static CustomSyncedValue<Dictionary<string, string>> YamlData = new(configSync, "ItemRequiresSkillLevel yaml");

        internal static ConfigEntry<bool>? serverSyncLock;
        internal static ConfigEntry<bool> GenerateListWithAllEquipableItems;
        internal static ConfigEntry<string> RequiresText;
        internal static ConfigEntry<string> cantEquipColor;
        internal static ConfigEntry<string> canEquipColor;
        internal static ConfigEntry<string> cantequipmessage;

        public static bool IsWAPInstalled() =>
            Chainloader.PluginInfos.ContainsKey("com.orianaventure.mod.WorldAdvancementProgression");
        public static bool hasWAP = false;

        Harmony _harmony = new Harmony(PluginGUID);

        internal static string ConfigFileName;          // active file name
        internal static string ConfigPath;              // active file full path
        internal static readonly string ConfigFileNameNew = PluginGUID + ".yml";
        internal static readonly string ConfigPathNew = Paths.ConfigPath + Path.DirectorySeparatorChar + ConfigFileNameNew;
        internal static readonly string ConfigFileNameOld = PluginGUIDold + ".yml";
        internal static readonly string ConfigPathOld = Paths.ConfigPath + Path.DirectorySeparatorChar + ConfigFileNameOld;

        public static string AllItemsConfigPath = Paths.ConfigPath + Path.DirectorySeparatorChar + PluginGUID + "ALLITEMS.yml";

        private FileSystemWatcher _watcherNew;
        private FileSystemWatcher _watcherOld;

        private void Awake()
        {
            hasWAP = IsWAPInstalled();

            // Pick which file to use:
            // If legacy Detalhes file exists -> use that.
            // Else use (and if needed, create) WackyMole file.
            if (File.Exists(ConfigPathOld))
            {
                ConfigFileName = ConfigFileNameOld;
                ConfigPath = ConfigPathOld;
            }
            else
            {
                ConfigFileName = ConfigFileNameNew;
                ConfigPath = ConfigPathNew;
            }

            // Ensure defaults exist if we're on the new path and nothing is present
            RequirementService.Init();

            // Harmony + sync setup
            _harmony.PatchAll();
            YamlData.ValueChanged += RequirementService.Load;

            // Initial load from whichever path we chose
            AssignYamlFromActivePath();

            // Watch BOTH possible files so a user can swap/migrate at runtime
            SetupWatcher();

            // ---- Config entries ----
            serverSyncLock = config("General", "Lock Configuration", true, "Lock Configuration");
            GenerateListWithAllEquipableItems = config("General", "GenerateListWithAllEquipableItems", false, "GenerateListWithAllEquipableItems");
            canEquipColor = config("General", "canEquipColor", "green", "canEquipColor");
            cantEquipColor = config("General", "cantEquipColor", "red", "cantEquipColor");
            GenerateListWithAllEquipableItems = config("General", "GenerateListWithAllEquipableItems", false, "GenerateListWithAllEquipableItems");
            RequiresText = config("General", "RequiresText", "\nRequires <color={0}>{1} {2}</color>", "RequiresText");
            cantequipmessage = config("General", "CantEquitMessage", "You Can't Equip this!", "Message to display when a player can't equip and item");

            configSync.AddLockingConfigEntry(serverSyncLock);
        }

        // ---------------- helpers ----------------

        private void AssignYamlFromActivePath()
        {
            // Prefer old if present; else new.
            string path = File.Exists(ConfigPathOld) ? ConfigPathOld : ConfigPathNew;

            // Update active pointers so RequirementService.Init()/Load use the right one
            ConfigPath = path;
            ConfigFileName = Path.GetFileName(path);

            // Push into the synced value
            var dict = new[] { path }.ToDictionary(f => f, File.ReadAllText);
            YamlData.AssignLocalValue(dict);
        }

        private void SetupWatcher()
        {
            // Watch NEW file
            _watcherNew = new FileSystemWatcher(Paths.ConfigPath, ConfigFileNameNew)
            {
                IncludeSubdirectories = false,
                SynchronizingObject = ThreadingHelper.SynchronizingObject,
                EnableRaisingEvents = true
            };
            _watcherNew.Changed += ReadFile;
            _watcherNew.Created += ReadFile;
            _watcherNew.Renamed += ReadFile;

            // Watch OLD file
            _watcherOld = new FileSystemWatcher(Paths.ConfigPath, ConfigFileNameOld)
            {
                IncludeSubdirectories = false,
                SynchronizingObject = ThreadingHelper.SynchronizingObject,
                EnableRaisingEvents = true
            };
            _watcherOld.Changed += ReadFile;
            _watcherOld.Created += ReadFile;
            _watcherOld.Renamed += ReadFile;
        }

        private void ReadFile(object sender, FileSystemEventArgs e)
        {
            try
            {
                // Re-evaluate which file is authoritative (old wins if present)
                AssignYamlFromActivePath();
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"Failed to reload YAML '{e.FullPath}': {ex.Message}");
            }
        }

        // config helpers
        ConfigEntry<T> config<T>(string group, string name, T value, ConfigDescription description, bool synchronizedSetting = true)
        {
            var configEntry = Config.Bind(group, name, value, description);
            var syncedConfigEntry = configSync.AddConfigEntry(configEntry);
            syncedConfigEntry.SynchronizedConfig = synchronizedSetting;
            return configEntry;
        }

        ConfigEntry<T> config<T>(string group, string name, T value, string description, bool synchronizedSetting = true)
            => config(group, name, value, new ConfigDescription(description), synchronizedSetting);
    }
}
