using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using UnityEngine;

namespace EquipWhileRunning
{
    [BepInPlugin(pluginGUID, pluginName, pluginVersion)]
    [BepInProcess("valheim.exe")]
    public class EquipWhileRunningPlugin : BaseUnityPlugin
    {
        const string pluginGUID = "hex.EquipWhileRunning";
        const string pluginName = "Equip While Running";
        const string pluginVersion = "1.0.0";
        const float messageCooldown = 0.2f;
        const KeyCode defaultKeyCode = KeyCode.F7;

        private Harmony _harmony;
        private ConfigEntry<bool> _modEnabled;
        private ConfigEntry<KeyboardShortcut> _toggleKey;
        private float _lastMessageTime;

        public static EquipWhileRunningPlugin Instance { get; private set; }
        public bool IsModEnabled => _modEnabled.Value;

        private void Awake()
        {
            Logger.LogInfo($"Loading {pluginName}");

            Instance = this;

            _modEnabled = Config.Bind("General", "Enabled", true, "Allow equip and unequip actions while running");
            _toggleKey = Config.Bind("Key Binds", "ToggleKey", new KeyboardShortcut(defaultKeyCode), "Hotkey to toggle equip while running");

            _toggleKey.SettingChanged += OnToggleKeyChanged;

            _harmony = new Harmony(pluginGUID);
            _harmony.PatchAll();

            Logger.LogInfo($"{pluginName} {pluginVersion} loaded.");
        }

        private void Update()
        {
            if(Time.timeScale == 0f || Player.m_localPlayer == null)
            {
                return;
            }

            if (_toggleKey.Value.IsDown())
            {
                _modEnabled.Value = !_modEnabled.Value;

                if (MessageHud.instance != null)
                {
                    ShowStatus(_modEnabled.Value);
                }
            }
        }

        private void OnDestroy()
        {
            if(_toggleKey != null)
            {
                _toggleKey.SettingChanged -= OnToggleKeyChanged;
            }

            Instance = null;

            _harmony?.UnpatchSelf();
        }

        private void OnToggleKeyChanged(object sender, EventArgs e)
        {
            Logger.LogInfo($"Toggle key changed to {_toggleKey.Value}");
        }

        private void ShowStatus(bool isEnabled)
        {
            if(Time.time - _lastMessageTime < messageCooldown)
            {
                return;
            }

            _lastMessageTime = Time.time;

            string message = isEnabled
                ? "Equip While Running: ENABLED"
                : "Equip While Running: DISABLED";

            Logger.LogInfo(message);

            MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, message);
        }
    }
}