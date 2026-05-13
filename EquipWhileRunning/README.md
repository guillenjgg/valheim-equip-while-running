# Equip While Running

Allows equip, unequip and reload actions while running in Valheim.

## Features

- Equip, unequip and reload actions while sprinting
- Toggle the mod on/off in game
- Configurable hotkey
- Client-side behavior for the local player
- Lightweight Harmony patching with vanilla-friendly behavior
- Vanilla-friendly gameplay integration

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

Install using a Thunderstore-compatible mod manager such as r2modman.

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

## Compatibility

- Designed to be lightweight and compatible with other mods
- Uses non-destructive Harmony patching
- Intended to work safely in multiplayer environments

---

## Known Issues

- Other mods that heavily modify player movement or action queue behavior may cause conflicts