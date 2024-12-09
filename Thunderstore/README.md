## ItemRequiresSkillLevel by WackyMole

Original Mod by Detalhes https://thunderstore.io/c/valheim/p/Detalhes/ItemRequiresSkillLevel/

Keeping it warm for Detalhes.

</br>


Create skill requirement for any equipable item that you want.  

List of vanilla skills. https://valheim.fandom.com/wiki/Skills

Block craft based on skills if you want

Blocks foods and potions.

</br>
It has compatibility with Valheim Level System. Use these as skills: Intelligence, Strength, Focus, Constitution, Agility, Level 

Old Detalhes mod (still kinda maintained)

https://www.nexusmods.com/valheim/mods/2797?tab=description

</br>

It has compatibility with Smoothbrain skills. (Some of them)

</br>

It has compatibility with WackyEpicMMO. Use these as skills: Strength, Dexterity (Agility), Intellect, Endurance (Body), Vigour, Specializing (Special), or Level. Set EpicMMO as attribute to true.


A Yml will be generated in the first execution.

![https://wackymole.com/hosts/itemrequiresskillexample.png](https://wackymole.com/hosts/itemrequiresskillexample.png)

Example provided by LePunkQC


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
  - Skill: Level
    Level: 10
    BlockCraft: true
    BlockEquip: true
    EpicMMO: true
    ExhibitionName: 
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

