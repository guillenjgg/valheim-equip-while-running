using HarmonyLib;
using UnityEngine;

namespace EquipWhileRunning.Patches
{
    [HarmonyPatch(typeof(Attack), "OnAttackTrigger")]
    public static class AttackOnAttackTriggerPatch
    {
        [HarmonyPrefix]
        private static void SaveGrapplingHookBeforeAttack(Attack __instance, Humanoid ___m_character, ref bool __state)
        {
            var player = ___m_character as Player;

            __state = player != null &&
                player == Player.m_localPlayer &&
                __instance.m_requiresReload &&
                __instance.GetWeapon()?.m_shared.m_name == "$item_graplinghook";
        }

        [HarmonyPostfix]
        private static void StartCooldownAfterGrapplingHookAttack(Humanoid ___m_character, bool __state)
        {
            var plugin = Plugin.Instance;

            if (!__state || plugin == null)
            {
                return;
            }

            var player = ___m_character as Player;

            // Failed attacks keep the hook loaded and must not start a cooldown.
            if (player == null || player.IsWeaponLoaded())
            {
                return;
            }

            plugin.GrapplingHookCooldownPlayer = player;
            plugin.GrapplingHookCooldownStartedAt = Time.time;
        }
    }
}
