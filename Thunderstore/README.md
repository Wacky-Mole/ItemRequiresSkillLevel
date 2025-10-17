## ItemRequiresSkillLevel by WackyMole

Original mod by **Detalhes**: [https://thunderstore.io/c/valheim/p/Detalhes/ItemRequiresSkillLevel/](https://thunderstore.io/c/valheim/p/Detalhes/ItemRequiresSkillLevel/)
Maintained by **WackyMole** with permission.

**What’s new in 1.4.0**

* Config renamed to **`WackyMole.ItemRequiresSkillLevel.yml`** (still reads legacy `Detalhes.ItemRequiresSkillLevel.yml` if present).
* Added **`GlobalKeyReq`**: gate crafting/equipping by world/player keys.
  If World Advancement Progression (WAP) is installed, keys are checked **globally**; (because WAP makes them private)
  otherwise they’re checked against the **player**. Private player raids World Modifier is recommended regardless.
      </br> WAP mod and it's private keys are recommended for a better experience.

**What it does**

* Create skill requirements for **any equipable item**.
* Optionally **block crafting** based on skills or keys.
* Blocks **foods and potions** when requirements aren’t met.
* Generates a **YAML** on first run.
* Vanilla skills reference: [https://valheim.fandom.com/wiki/Skills](https://valheim.fandom.com/wiki/Skills)

**Compatibility**

* **Valheim Level System (VLS):** `Intelligence, Strength, Focus, Constitution, Agility, Level`
* **Smoothbrain Skills:** partial support (some skills)
* **WackyEpicMMO:** use `Strength, Dexterity (Agility), Intellect, Endurance (Body), Vigour, Specializing (Special), Level` and set `EpicMMO: true`

Legacy Detalhes mod (still somewhat maintained):
[https://www.nexusmods.com/valheim/mods/2797?tab=description](https://www.nexusmods.com/valheim/mods/2797?tab=description)

![Example](https://wackymole.com/hosts/itemrequiresskillexample.png)
*Example provided by LePunkQC*

How keys are checked (scope)

With WAP installed: checked as Global world keys.

Without WAP: checked against the Player (private keys).
Private player raids World Modifier is recommended either way.


A Yml will be generated in the first execution.



# Example:
```
- PrefabName: ArmorBronzeChest
  Requirements:
  - Skill: Level
    Level: 10
    BlockCraft: true
    BlockEquip: true
    EpicMMO: true
    ExhibitionName:
  - Skill: Strength
    Level: 20
    BlockCraft: true
    BlockEquip: true
    EpicMMO: true
    ExhibitionName:
- PrefabName: ArmorBronzeLegs
  Requirements:
  - Skill: Level
    Level: 10
    BlockCraft: true
    BlockEquip: true
    EpicMMO: true
    ExhibitionName:
  - Skill: Strength
    Level: 20
    BlockCraft: true
    BlockEquip: true
    EpicMMO: true
    ExhibitionName:
- PrefabName: HelmetBronze
  Requirements:
  - Skill: Level
    Level: 10
    BlockCraft: true
    BlockEquip: true
    EpicMMO: true
    ExhibitionName:
  - Skill: Strength
    Level: 20
    BlockCraft: true
    BlockEquip: true
    EpicMMO: true
    ExhibitionName:
- PrefabName: ArmorIronChest
  Requirements:
  - Skill: Level
    Level: 20
    BlockCraft: true
    BlockEquip: true
    EpicMMO: true
    ExhibitionName: 
  - Skill: Strength
    Level: 40
    BlockCraft: true
    BlockEquip: true
    EpicMMO: true
    ExhibitionName:
- PrefabName: ArmorIronLegs
  Requirements:
  - Skill: Level
    Level: 20
    BlockCraft: true
    BlockEquip: true
    EpicMMO: true
    ExhibitionName: 
  - Skill: Strength
    Level: 40
    BlockCraft: true
    BlockEquip: true
    EpicMMO: true
    ExhibitionName: 
- PrefabName: HelmetIron
  Requirements:
  - Skill: Level
    Level: 20
    BlockCraft: true
    BlockEquip: true
    EpicMMO: true
    ExhibitionName: 
  - Skill: Strength
    Level: 40
    BlockCraft: true
    BlockEquip: true
    EpicMMO: true
    ExhibitionName:
  - GlobalKeyReq: defeated_bonemass
    BlockEquip: true
  - GlobalKeyReq: defeated_gdking
    BlockEquip: true
- PrefabName: AxeBronze
  Requirements:
  - Skill: Level
    Level: 10
    BlockCraft: true
    BlockEquip: true
    EpicMMO: true
    ExhibitionName: 
- PrefabName: AtgeirBronze
  Requirements:
  - Skill: Level
    Level: 10
    BlockCraft: true
    BlockEquip: true
    EpicMMO: true
    ExhibitionName: 
- PrefabName: SwordBronze
  Requirements:
  - GlobalKeyReq: defeated_eikthyr
    BlockEquip: true
- PrefabName: SwordIron
  Requirements:
  - Skill: Level
    Level: 30
    BlockCraft: true
    BlockEquip: true
    EpicMMO: true
    ExhibitionName: 
  - Skill: Swords
    Level: 20
    BlockCraft: true
    BlockEquip: true
    EpicMMO: true
    ExhibitionName:
- PrefabName: AxeIron
  Requirements:
  - Skill: Level
    Level: 30
    BlockCraft: true
    BlockEquip: true
    EpicMMO: true
    ExhibitionName: 
  - Skill: Axes
    Level: 20
    BlockCraft: true
    BlockEquip: true
    EpicMMO: true
    ExhibitionName:
- PrefabName: AtgeirIron
  Requirements:
  - Skill: Level
    Level: 30
    BlockCraft: true
    BlockEquip: true
    EpicMMO: true
    ExhibitionName: 
  - Skill: Swords
    Level: 20
    BlockCraft: true
    BlockEquip: true
    EpicMMO: true
    ExhibitionName:

```

# Sync
Install in the server to sync config with clients.

# GlobalKeys Available

    defeated_bonemass — Set when killing Bonemass
    defeated_gdking — Set when killing The Elder
    defeated_goblinking — Set when killing Yagluth
    defeated_dragon — Set when killing Moder
    defeated_eikthyr — Set when killing Eikthyr
    defeated_queen — Set when killing The Queen
    defeated_fader — Set when killing Fader
    defeated_serpent — Set when killing a Serpent
    KilledTroll — Set when killing a Troll
    killed_surtling — Set when killing a Surtling
    KilledBat — Set when killing a Bat