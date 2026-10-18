# Equip While Running

Allows equip, unequip and reload actions while running in Valheim.

## Features

- Toggle the mod on/off in game
- Configurable hotkey

---

## Default Hotkey

```text
F7
```

The hotkey can be changed in the config file.

---

## Configuration

Config file location:

```text
BepInEx/config/hex.EquipWhileRunning.cfg
```

Example configuration:

```ini
[General]

## Enable or disable the mod
# Setting type: Boolean
# Default value: true
Enabled = true

## Toggle mod hotkey
# Setting type: KeyboardShortcut
# Default value: F7
ToggleKey = F7
```

---

## Requirements

- BepInExPack Valheim

---

## Installation

### Thunderstore / r2modman

Install using a Thunderstore-compatible mod manager such as r2modman or Gale.

### Manual Installation

Place the DLL inside:

```text
BepInEx/plugins/EquipWhileRunning/
```

Example:

```text
BepInEx/plugins/EquipWhileRunning/EquipWhileRunning.dll
```

---

## Multiplayer

This mod is designed to work as a lightweight client-side quality-of-life mod.

Server installation is typically not required.

---

## Known Issues

- Other mods that heavily modify player movement or action queue behavior may cause conflicts

---

## Support and Feedback
- https://discord.gg/wU2FXD94v4

## Source Code
- https://github.com/guillenjgg/valheim-equip-while-running