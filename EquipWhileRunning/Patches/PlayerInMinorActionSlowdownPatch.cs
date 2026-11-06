using HarmonyLib;

namespace EquipWhileRunning.Patches
{
    [HarmonyPatch(typeof(Player), nameof(Player.InMinorActionSlowdown))]
    public static class PlayerInMinorActionSlowdownPatch
    {
        [HarmonyPrefix]
        private static bool PreventMinorActionSlowdownWhenRunning(Player __instance, ref bool __result)
        {
            if (Plugin.Instance == null || !Plugin.Instance.IsModEnabled.Value)
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

            __result = false;
            return false;
        }
    }
}
