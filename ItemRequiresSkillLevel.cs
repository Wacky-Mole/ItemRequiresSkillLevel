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
        public const string Version = "1.4.4";
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
        internal static ConfigEntry<string> canteatmessage;
        internal static ConfigEntry<string> cantUseAmmomessage;
        internal static ConfigEntry<bool> ShowBlockMessages;

        public static bool IsWAPInstalled() =>
            Chainloader.PluginInfos.ContainsKey("com.orianaventure.mod.WorldAdvancementProgression");
        public static bool hasWAP = false;

        Harmony _harmony = new Harmony(PluginGUID);
        
        internal static readonly string ConfigFileNameNew = PluginGUID + ".yml";
        internal static readonly string ConfigPathNew = Paths.ConfigPath + Path.DirectorySeparatorChar + ConfigFileNameNew;
        internal static readonly string ConfigFileNameOld = PluginGUIDold + ".yml";
        internal static readonly string ConfigPathOld = Paths.ConfigPath + Path.DirectorySeparatorChar + ConfigFileNameOld;

        public static string AllItemsConfigPath = Paths.ConfigPath + Path.DirectorySeparatorChar + PluginGUID + "ALLITEMS.yml";

        private FileSystemWatcher _watcher;

        private void Awake()
        {
            hasWAP = IsWAPInstalled();

            // Ensure default exists if no WackyMole files or legacy file are present
            RequirementService.Init();

            // Harmony + sync setup
            _harmony.PatchAll();
            YamlData.ValueChanged += RequirementService.Load;

            // Initial load of all matching YAML files
            AssignYamlFromActivePath();

            // Watch for any .yml file in config folder for matching prefixes
            SetupWatcher();

            // ---- Config entries ----
            serverSyncLock = config("General", "Lock Configuration", true, "Lock Configuration");
            GenerateListWithAllEquipableItems = config("General", "GenerateListWithAllEquipableItems", false, "GenerateListWithAllEquipableItems");
            canEquipColor = config("General", "canEquipColor", "green", "canEquipColor");
            cantEquipColor = config("General", "cantEquipColor", "red", "cantEquipColor");
            GenerateListWithAllEquipableItems = config("General", "GenerateListWithAllEquipableItems", false, "GenerateListWithAllEquipableItems");
            RequiresText = config("General", "RequiresText", "\nRequires <color={0}>{1} {2}</color>", "RequiresText");
            cantequipmessage = config("General", "CantEquitMessage", "You Can't Equip this!", "Message to display when a player can't equip and item.");
            canteatmessage = config("General", "CantConsumeMessage", "You Can't Consume this!", "Message to display when a player can't eat an item.");
            cantUseAmmomessage = config("General", "CantUseAmmoMessage", "You Can't Use This Ammo", "Message to display when a player can't use an ammo type.");
            ShowBlockMessages = config("General", "ShowBlockMessages", true, "Show Block Messages to Users, Not ServerSynced", false);

            configSync.AddLockingConfigEntry(serverSyncLock);
        }

        // ---------------- helpers ----------------

        private void AssignYamlFromActivePath()
        {
            var yamlFiles = Directory.GetFiles(Paths.ConfigPath, "WackyMole.ItemRequiresSkillLevel*.yml");
            var dict = new Dictionary<string, string>();

            foreach (var file in yamlFiles)
            {
                try
                {
                    dict[file] = File.ReadAllText(file);
                }
                catch (Exception ex)
                {
                    Logger.LogWarning($"Failed to read YAML file {file}: {ex.Message}");
                }
            }

            // Also check for the legacy file if not already included in the prefix search
            if (File.Exists(ConfigPathOld) && !dict.ContainsKey(ConfigPathOld))
            {
                try
                {
                    dict[ConfigPathOld] = File.ReadAllText(ConfigPathOld);
                }
                catch (Exception ex)
                {
                    Logger.LogWarning($"Failed to read legacy YAML file {ConfigPathOld}: {ex.Message}");
                }
            }

            // Push into the synced value
            YamlData.AssignLocalValue(dict);
        }

        private void SetupWatcher()
        {
            // Watch for any .yml file changes in the config directory to handle multi-file updates
            _watcher = new FileSystemWatcher(Paths.ConfigPath, "*.yml")
            {
                IncludeSubdirectories = false,
                SynchronizingObject = ThreadingHelper.SynchronizingObject,
                EnableRaisingEvents = true
            };
            _watcher.Changed += ReadFile;
            _watcher.Created += ReadFile;
            _watcher.Renamed += ReadFile;
            _watcher.Deleted += ReadFile;
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
