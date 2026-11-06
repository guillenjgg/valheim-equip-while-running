using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace EquipWhileRunning
{
    [BepInPlugin(pluginGUID, pluginName, pluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        const string pluginGUID = "hex.EquipWhileRunning";
        const string pluginName = "Equip While Running";
        const string pluginVersion = "1.1.3";
        const float messageCooldown = 0.2f;
        const KeyCode defaultKeyCode = KeyCode.F7;

        private Harmony _harmony;
        private float _lastMessageTime;

        internal ConfigEntry<bool> IsModEnabled;
        internal ConfigEntry<KeyboardShortcut> ToggleKey;
        internal ConfigEntry<bool> EnableGrapplingHookAutoReload;
        internal ConfigEntry<int> GrapplingHookAutoReloadCooldown;
        internal Player GrapplingHookCooldownPlayer;
        internal float GrapplingHookCooldownStartedAt = float.NegativeInfinity;

        public static Plugin Instance { get; private set; }

        private void Awake()
        {
            Logger.LogInfo($"Loading {pluginName}");

            Instance = this;

            IsModEnabled = Config.Bind("General", "Enabled", true, "Allow equip and unequip actions while running");
            ToggleKey = Config.Bind("Key Binds", "ToggleKey", new KeyboardShortcut(defaultKeyCode), "Hotkey to toggle equip while running");
            EnableGrapplingHookAutoReload = Config.Bind("General", "EnableGrapplingHookAutoReload", true, "Instantly load the grappling hook when equipped, without a reload animation. Reloading after use is unchanged.");
            GrapplingHookAutoReloadCooldown = Config.Bind("General", "GrapplingHookAutoReloadCooldown", 2,
                new ConfigDescription("Seconds after releasing the grapple before equipping can instantly load the hook. Also applies after firing if no grapple attaches. Auto-loading is blocked while grappling or while the game's reload block is active. Normal reloading is unaffected.", new AcceptableValueRange<int>(0, 60)));
            
            _harmony = new Harmony(pluginGUID);
            _harmony.PatchAll(Assembly.GetExecutingAssembly());

            Logger.LogInfo($"{pluginName} {pluginVersion} loaded.");
        }

        private void Update()
        {
            if(Time.timeScale == 0f || Player.m_localPlayer == null)
            {
                return;
            }

            if (ToggleKey.Value.IsDown())
            {
                IsModEnabled.Value = !IsModEnabled.Value;

                if (MessageHud.instance != null)
                {
                    ShowStatus(IsModEnabled.Value);
                }
            }
        }

        private void OnDestroy()
        {
            Logger.LogInfo($"{pluginName} {pluginVersion} unloaded.");

            _harmony?.UnpatchSelf();
            _harmony = null;
            Instance = null;
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

            MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, message);
        }
    }
}
