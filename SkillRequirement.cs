using System.Collections.Generic;
using YamlDotNet.Serialization;
using System.Linq;
using System.IO;
using System.Text;
using UnityEngine;
using YamlDotNet.Serialization.NamingConventions;

namespace ItemRequiresSkillLevel
{
    public class SkillRequirement
    {
        [YamlMember]
        public string PrefabName { get; set; }

        [YamlIgnore]
        public int StableHashCode { get; set; }

        [YamlMember]
        public List<Requirement> Requirements { get; set; }

        public static List<SkillRequirement> Parse(string yaml)
        {
            List<SkillRequirement> list = ParseString(yaml);
            foreach (SkillRequirement skillRequirement in list)
            {
                skillRequirement.StableHashCode = skillRequirement.PrefabName.GetStableHashCode();
                foreach (var x in skillRequirement.Requirements)
                {
                    if (string.IsNullOrEmpty(x.ExhibitionName)) x.ExhibitionName = x.Skill;
                }
            }

            return list;
        }

        private static List<SkillRequirement> ParseString(string yaml) => new DeserializerBuilder().IgnoreFields().Build().Deserialize<List<SkillRequirement>>(yaml);
    }

    public class Requirement
    {
        public string Skill { get; set; }
        public int Level { get; set; }
        public bool BlockCraft { get; set; }
        public bool BlockEquip { get; set; }
        public bool EpicMMO { get; set; }
        public string GlobalKeyReq { get; set; }
        public string ExhibitionName { get; set; }
    }

    public class RequirementService
    {
        public static List<SkillRequirement> list = new();

        public static void Init()
        {
            if (!File.Exists(ItemRequiresSkillLevel.ConfigPath))
            {
                List<SkillRequirement> initials = new();
                initials.Add(new SkillRequirement
                {
                    PrefabName = "ArmorBronzeLegs",
                    Requirements = new List<Requirement>() {
                        new Requirement()
                        {
                            Skill = "Blocking",
                            Level = 10,
                            BlockCraft = false,
                            BlockEquip = true,
                            EpicMMO = false,
                        },
                        new Requirement()
                        {
                            Skill = "Swim",
                            Level = 10,
                            BlockCraft = true,
                            BlockEquip = true,
                        }
                    }

                });
                initials.Add(new SkillRequirement
                {
                    PrefabName = "ArmorBronzeChest",
                    Requirements = new List<Requirement>() {
                        new Requirement()
                        {
                            Skill = "Blocking",
                            Level = 10,
                            BlockCraft = false,
                            BlockEquip = true,
                        },
                        new Requirement()
                        {
                            Skill = "Swim",
                            Level = 10,
                            BlockCraft = true,
                            BlockEquip = true,
                        }
                    }

                });
                initials.Add(new SkillRequirement
                {
                    PrefabName = "HelmetBronze",
                    Requirements = new List<Requirement>() {
                        new Requirement()
                        {
                            Skill = "Blocking",
                            Level = 10,
                            BlockCraft = false,
                            BlockEquip = true,
                        },
                        new Requirement()
                        {
                            Skill = "Swim",
                            Level = 10,
                            BlockCraft = true,
                            BlockEquip = true,
                        }
                    }

                });

                // --- NEW: GlobalKeyReq examples --------------------------------------


                // 2) Gate EQUIP only (can craft early), after Eikthyr
                initials.Add(new SkillRequirement
                {
                    PrefabName = "SwordBronze",
                    Requirements = new List<Requirement>() {
                new Requirement {
                    GlobalKeyReq = "defeated_eikthyr",
                    BlockEquip   = true,
                    ExhibitionName = "Eikthyr Defeated"
                }
                 }
                });

                // 3) Gate CRAFT only (can equip if looted), after The Elder
                initials.Add(new SkillRequirement
                {
                    PrefabName = "AtgeirIron",
                    Requirements = new List<Requirement>() {
                new Requirement {
                    GlobalKeyReq = "defeated_gdking",
                    BlockCraft   = true,
                    ExhibitionName = "The Elder Defeated"
                }
                 }
                });

                // 4) Combine skill + key (both must pass) for Iron Chest
                initials.Add(new SkillRequirement
                {
                    PrefabName = "ArmorIronChest",
                    Requirements = new List<Requirement>() {
                new Requirement {
                    Skill       = "Level",   // EpicMMO example
                    Level       = 20,
                    EpicMMO     = true,
                    BlockCraft  = true,
                    BlockEquip  = true,
                    ExhibitionName = "Player Level"
                },
                new Requirement {
                    GlobalKeyReq = "defeated_dragon", // Moder
                    BlockCraft   = true,
                    BlockEquip   = true,
                    ExhibitionName = "Moder Defeated"
                }
                    }
                });

                // 5) Consumable: block using Serpent Stew until a Serpent has been killed
                initials.Add(new SkillRequirement
                {
                    PrefabName = "SerpentStew",
                    Requirements = new List<Requirement>() {
                new Requirement {
                    GlobalKeyReq = "defeated_serpent",
                    BlockEquip   = true, // for consumables, use BlockEquip to gate use
                    ExhibitionName = "Serpent Killed"
                }
                    }
                });

                var serializer = new SerializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .Build();

                var yaml = serializer.Serialize(initials);

                using StreamWriter streamWriter = File.CreateText(ItemRequiresSkillLevel.ConfigPath);
                streamWriter.Write(new StringBuilder()
                        .AppendLine(yaml));
                streamWriter.Close();
            }
        }

        public static void GenerateListWithAllEquipments()
        {
            if (!File.Exists(ItemRequiresSkillLevel.AllItemsConfigPath))
            {
                List<SkillRequirement> initials = new();

                foreach (var item in ObjectDB.instance.m_items)
                {
                    ItemDrop itemDrop = item.GetComponent<ItemDrop>();
                    if (!itemDrop) continue;

                    if (itemDrop.m_itemData is null) continue;

                    if (itemDrop.m_itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Tool || itemDrop.m_itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.OneHandedWeapon || (itemDrop.m_itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon || itemDrop.m_itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow) || (itemDrop.m_itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shield || itemDrop.m_itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Helmet || (itemDrop.m_itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Chest || itemDrop.m_itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Legs)) || (itemDrop.m_itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shoulder || itemDrop.m_itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Ammo || itemDrop.m_itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Torch) || itemDrop.m_itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Utility || itemDrop.m_itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Consumable)
                    {
                        initials.Add(new SkillRequirement
                        {
                            PrefabName = item.name,
                            Requirements = new List<Requirement>() {
                            new Requirement()
                            {
                                Skill = "Blocking",
                                Level = 10,
                                BlockCraft = false,
                                BlockEquip = true                                
                            }
                            }
                        });
                    }
                }

                var serializer = new SerializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .Build();

                var yaml = serializer.Serialize(initials);

                using StreamWriter streamWriter = File.CreateText(ItemRequiresSkillLevel.AllItemsConfigPath);
                streamWriter.Write(new StringBuilder()
                        .AppendLine(yaml));
                streamWriter.Close();
            }
        }

        public static void Load()
        {
            list.Clear();

            foreach (KeyValuePair<string, string> yamlFile in ItemRequiresSkillLevel.YamlData.Value)
            {
                list.AddRange(SkillRequirement.Parse(yamlFile.Value));
            }
            Debug.Log("ItemRequiresSkillLevel Loaded: " + list.Count());
        }
    }
}
