using HarmonyLib;
using UnityEngine;

namespace EquipWhileRunning.Patches
{
    [HarmonyPatch(typeof(GrapplingPoint), "Break")]
    public static class GrapplingPointBreakPatch
    {
        [HarmonyPrefix]
        private static void StartCooldownBeforeGrappleRelease(Character ___m_character, ZNetView ___m_nview)
        {
            var plugin = Plugin.Instance;

            if (plugin == null)
            {
                return;
            }

            if (___m_character == null || ___m_character != Player.m_localPlayer)
            {
                return;
            }

            if (___m_nview == null || !___m_nview.IsOwner())
            {
                return;
            }

            plugin.GrapplingHookCooldownPlayer = Player.m_localPlayer;
            plugin.GrapplingHookCooldownStartedAt = Time.time;
        }
    }
}
