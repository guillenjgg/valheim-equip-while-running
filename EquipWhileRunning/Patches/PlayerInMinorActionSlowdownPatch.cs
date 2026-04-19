using HarmonyLib;

namespace EquipWhileRunning.Patches
{
    [HarmonyPatch(typeof(Player), nameof(Player.InMinorActionSlowdown))]
    public static class PlayerInMinorActionSlowdownPatch
    {
        [HarmonyPrefix]
        private static bool PreventMinorActionSlowdownWhenRunning(Player __instance, ref bool __result)
        {
            if (EquipWhileRunningPlugin.Instance == null || !EquipWhileRunningPlugin.Instance.IsModEnabled)
            {
                return true;
            }

            if (__instance != Player.m_localPlayer)
            {
                return true;
            }

            if (!__instance.IsRunning())
            {
                return true;
            }

            __result = false; // Provide a return value without running the original method
            return false;     // Skip executing InMinorActionSlowdown
        }
    }
}