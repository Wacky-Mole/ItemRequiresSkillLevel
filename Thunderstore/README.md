## ItemRequiresSkillLevel by WackyMole

Original mod by **Detalhes**: [https://thunderstore.io/c/valheim/p/Detalhes/ItemRequiresSkillLevel/](https://thunderstore.io/c/valheim/p/Detalhes/ItemRequiresSkillLevel/)
Maintained by **WackyMole** with permission.


### What's new in 1.4.4

* **Multiple Config Files**: The mod now reads all YAML files in the config folder that start with `WackyMole.ItemRequiresSkillLevel` This allows for better organization of requirements across different mods or item categories.

### What's new in 1.4.0

* Config renamed to `WackyMole.ItemRequiresSkillLevel.yml` (still reads legacy `Detalhes.ItemRequiresSkillLevel.yml` if present).
* Added `GlobalKeyReq`: gate crafting/equipping by world/player keys.
  If World Advancement Progression (WAP) is installed, keys are checked **globally** (WAP makes them private). Otherwise they are checked against the **player**. The **Private Player Raids** world modifier is recommended either way.
  You can use short lines like:

  ```yaml
  GlobalKeyReq: defeated_bonemass
  BlockEquip: true
  ```

  **Make sure to enable Private Player Raids if you use `GlobalKeyReq`.**

### What it does

* Create skill requirements for **any equipable item**.
* Optionally **block crafting** based on skills or keys.
* Blocks **foods and potions** when requirements are not met.
* Generates a **YAML** on first run.
* Vanilla skills reference: [https://valheim.fandom.com/wiki/Skills](https://valheim.fandom.com/wiki/Skills)

### Compatibility

* **Valheim Level System (VLS):** `Intelligence, Strength, Focus, Constitution, Agility, Level`
* **Smoothbrain Skills:** partial support (some skills)
* **WackyEpicMMO:** use `Strength, Dexterity (Agility), Intellect, Endurance (Body), Vigour, Specializing (Special), Level` and set `EpicMMO: true`

Legacy Detalhes mod (still somewhat maintained):
[https://www.nexusmods.com/valheim/mods/2797?tab=description](https://www.nexusmods.com/valheim/mods/2797?tab=description)

![Example](https://wackymole.com/hosts/itemrequiresskillexample.png)
*Example provided by LePunkQC*

### How keys are checked (scope)

* With WAP installed: checked as **Global** world keys.
* Without WAP: checked against the **Player** (private keys).
  Private Player Raids world modifier is recommended either way.



A YML will be generated on first launch.

# Configuration formats

The mod supports **two YAML formats**:

1. **Legacy top-level list**
2. **Document format** with `Requirements` and optional `RequirementGroups`

Both formats are supported at the same time for backward compatibility.

## Format 1: Legacy top-level list

Use this if you want one entry per item, like older configs.

### Example 1: Basic vanilla skill requirement

```yaml
- PrefabName: BronzeSword
  Requirements:
    - Skill: Swords
      Level: 15
      BlockCraft: true
      BlockEquip: true
```

### Example 2: EpicMMO requirement

```yaml
- PrefabName: ArmorIronChest
  Requirements:
    - Skill: Level
      Level: 20
      BlockCraft: true
      BlockEquip: true
      EpicMMO: true
      ExhibitionName: Player Level
```

### Example 3: Multiple requirements on one item

```yaml
- PrefabName: SwordIron
  Requirements:
    - Skill: Level
      Level: 30
      BlockCraft: true
      BlockEquip: true
      EpicMMO: true
      ExhibitionName: Player Level
    - Skill: Swords
      Level: 20
      BlockCraft: true
      BlockEquip: true
      ExhibitionName: Swords
```

### Example 4: Global key only

```yaml
- PrefabName: SwordBronze
  Requirements:
    - GlobalKeyReq: defeated_eikthyr
      BlockEquip: true
      ExhibitionName: Eikthyr Defeated
```

### Example 5: Mixed skill + key requirement

```yaml
- PrefabName: HelmetIron
  Requirements:
    - Skill: Level
      Level: 20
      BlockCraft: true
      BlockEquip: true
      EpicMMO: true
      ExhibitionName: Player Level
    - GlobalKeyReq: defeated_bonemass
      BlockEquip: true
      ExhibitionName: Bonemass Defeated
```

### Example 6: Consumable usage lock

```yaml
- PrefabName: SerpentStew
  Requirements:
    - GlobalKeyReq: defeated_serpent
      BlockEquip: true
      ExhibitionName: Serpent Killed
```

## Format 2: Document format

Use this if you want a cleaner file layout or want to group multiple prefabs under the same requirement block.

### Example 1: Normal per-item entries inside `Requirements`

```yaml
Requirements:
  - PrefabName: ArmorBronzeChest
    Requirements:
      - Skill: Blocking
        Level: 10
        BlockEquip: true
      - Skill: Swim
        Level: 10
        BlockCraft: true
        BlockEquip: true

  - PrefabName: SwordBronze
    Requirements:
      - GlobalKeyReq: defeated_eikthyr
        BlockEquip: true
        ExhibitionName: Eikthyr Defeated
```

### Example 2: One group applied to multiple items

```yaml
RequirementGroups:
  - Prefabs:
      - rae_OdinHorse_Helmet
      - rae_OdinHorse_Chest
      - rae_OdinHorse_Leggings
    Requirements:
      - Skill: Level
        Level: 20
        BlockCraft: true
        BlockEquip: true
        EpicMMO: true
        ExhibitionName: Player Level
```

### Example 3: Group with multiple shared requirements

```yaml
RequirementGroups:
  - Prefabs:
      - ArmorBronzeChest
      - ArmorBronzeLegs
      - HelmetBronze
    Requirements:
      - Skill: Blocking
        Level: 10
        BlockEquip: true
        ExhibitionName: Blocking
      - Skill: Swim
        Level: 10
        BlockCraft: true
        BlockEquip: true
        ExhibitionName: Swim
```

### Example 4: Mixed document with both sections

```yaml
Requirements:
  - PrefabName: SerpentStew
    Requirements:
      - GlobalKeyReq: defeated_serpent
        BlockEquip: true
        ExhibitionName: Serpent Killed

RequirementGroups:
  - Prefabs:
      - ArmorIronChest
      - ArmorIronLegs
      - HelmetIron
    Requirements:
      - Skill: Level
        Level: 20
        BlockCraft: true
        BlockEquip: true
        EpicMMO: true
        ExhibitionName: Player Level
      - GlobalKeyReq: defeated_bonemass
        BlockEquip: true
        ExhibitionName: Bonemass Defeated
```

## Notes

* `BlockCraft: true` prevents crafting.
* `BlockEquip: true` prevents equipping or using the item.
* Foods and potions use `BlockEquip: true` to block usage.
* `EpicMMO: true` switches the requirement to EpicMMO/WackyEpicMMO stats.
* `RequirementGroups` expands into normal per-item requirements internally.
* Old configs using the legacy top-level list still work.

# Sync
Install in the server to sync config with clients.

# GlobalKeys Available

    defeated_bonemass - Set when killing Bonemass
    defeated_gdking - Set when killing The Elder
    defeated_goblinking - Set when killing Yagluth
    defeated_dragon - Set when killing Moder
    defeated_eikthyr - Set when killing Eikthyr
    defeated_queen - Set when killing The Queen
    defeated_fader - Set when killing Fader
    defeated_serpent - Set when killing a Serpent
    KilledTroll - Set when killing a Troll
    killed_surtling - Set when killing a Surtling
    KilledBat - Set when killing a Bat