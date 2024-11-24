## ItemRequiresSkillLevel by WackyMole

Original Mod by Detalhes https://thunderstore.io/c/valheim/p/Detalhes/ItemRequiresSkillLevel/

Keeping it warm for Detalhes or maybe until Rusty adds WackyEpicMMO capabilities to his mod. 

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

It has compatibility with WackyEpicMMO. Use these as skills: Strength, Dexterity, Intellect, Endurance, Vigour, Specializing, or EpicMMO Level. Set EpicMMO as attribute to true.

Yml will be generated in the first execution.

# Example:
```
- PrefabName: ArmorBronzeLegs
  Requirements:
  - Skill: Blocking
    Level: 10
    BlockCraft: false
    BlockEquip: true
    EpicMMO: false
    ExhibitionName: 
  - Skill: Swim
    Level: 10
    BlockCraft: true
    BlockEquip: true
    EpicMMO: false
    ExhibitionName: 
- PrefabName: ArmorBronzeChest
  Requirements:
  - Skill: Blocking
    Level: 10
    BlockCraft: false
    BlockEquip: true
    EpicMMO: false
    ExhibitionName: 
  - Skill: Swim
    Level: 10
    BlockCraft: true
    BlockEquip: true
    EpicMMO: false
    ExhibitionName: 
- PrefabName: HelmetBronze
  Requirements:
  - Skill: Blocking
    Level: 10
    BlockCraft: false
    BlockEquip: true
    EpicMMO: false
    ExhibitionName: 
  - Skill: Swim
    Level: 10
    BlockCraft: true
    BlockEquip: true
    EpicMMO: false
    ExhibitionName: 

```

# Sync
Install in the server to sync config with clients.

