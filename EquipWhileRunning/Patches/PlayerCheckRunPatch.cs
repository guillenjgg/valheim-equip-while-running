using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;

namespace EquipWhileRunning.Patches
{
    [HarmonyPatch(typeof(Player), nameof(Player.CheckRun))]
    public static class PlayerCheckRunPatch
    {
        private static readonly FieldInfo ActionQueueField = AccessTools.Field(typeof(Player), "m_actionQueue");

        [HarmonyPrefix]
        private static void SaveActionsBeforeCheckRun(Player __instance, ref List<Player.MinorActionData> __state)
        {
            __state = null;

            if (EquipWhileRunningPlugin.Instance == null || !EquipWhileRunningPlugin.Instance.IsModEnabled)
            {
                return;
            }

            if (__instance != Player.m_localPlayer)
            {
                return;
            }

            var queue = ActionQueueField?.GetValue(__instance) as List<Player.MinorActionData>;

            if (queue == null || queue.Count == 0)
            {
                return;
            }

            __state = new List<Player.MinorActionData>(queue);
        }

        [HarmonyPostfix]
        private static void RestoreActionsAfterCheckRun(Player __instance, List<Player.MinorActionData> __state)
        {
            if (EquipWhileRunningPlugin.Instance == null || !EquipWhileRunningPlugin.Instance.IsModEnabled)
            {
                return;
            }

            if (__instance != Player.m_localPlayer || __state == null)
            {
                return;
            }

            var queue = ActionQueueField?.GetValue(__instance) as List<Player.MinorActionData>;

            if (queue == null)
            {
                return;
            }

            foreach (var action in __state)
            {
                if (action != null && !queue.Contains(action))
                {
                    queue.Add(action);
                }
            }
        }
    }
}