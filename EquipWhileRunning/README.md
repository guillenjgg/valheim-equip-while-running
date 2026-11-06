# Equip While Running

Allows equip, unequip and reload actions while running in Valheim.

Option to instantly reload the grappling hook when equipped

## Features

- Toggle the mod on/off in game
- Configurable hotkey
- Reload grappling hook instantly on equip (has a configurable cooldown)

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

## Instantly load the grappling hook when equipped, without a reload animation
# Setting type: Boolean
# Default value: true
EnableGrapplingHookAutoReload = true

## Seconds after grapple release (or firing a missed shot) before instant loading is allowed
# Setting type: Int32
# Default value: 2
GrapplingHookAutoReloadCooldown = 2

[Key Binds]

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

## Known Issues

- Other mods that heavily modify player movement or action queue behavior may cause conflicts
- Mods that modify the grappling hook equip behavior may cause conflicts

---

## Support and Feedback
- https://discord.gg/wU2FXD94v4

## Source Code
- https://github.com/guillenjgg/valheim-equip-while-running
