using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;

namespace EquipWhileRunning.Patches
{
    [HarmonyPatch(typeof(Player), nameof(Player.CheckRun))]
    public class PlayerCheckRunPatch
    {
        private static readonly FieldInfo ActionQueueField = AccessTools.Field(typeof(Player), "m_actionQueue");
        private static readonly FieldInfo ActionTypeField = AccessTools.Field(typeof(Player.MinorActionData), "m_type");

        private static readonly HashSet<Player.MinorActionData.ActionType> _allowedActionTypes = new HashSet<Player.MinorActionData.ActionType>
        {
            Player.MinorActionData.ActionType.Equip,
            Player.MinorActionData.ActionType.Unequip
        };

        [HarmonyPrefix]
        private static void SaveAllowedActionsBeforeCheckRun(Player __instance, ref List<Player.MinorActionData> __state)
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

            if (ActionTypeField == null)
            {
                return;
            }

            __state = new List<Player.MinorActionData>();

            foreach (var action in queue)
            {
                if (action == null)
                {
                    continue;
                }

                var actionType = (Player.MinorActionData.ActionType)ActionTypeField.GetValue(action);

                if (_allowedActionTypes.Contains(actionType))
                {
                    __state.Add(action);
                }
            }
        }

        [HarmonyPostfix]
        private static void RestoreAllowedActionsAfterCheckRun(Player __instance, List<Player.MinorActionData> __state, bool __result)
        {
            if (!__result)
            {
                return;
            }

            if (EquipWhileRunningPlugin.Instance == null || !EquipWhileRunningPlugin.Instance.IsModEnabled)
            {
                return;
            }

            if (__instance != Player.m_localPlayer)
            {
                return;
            }

            if (__state == null || __state.Count == 0)
            {
                return;
            }

            if (ActionQueueField == null)
            {
                return;
            }

            var queue = ActionQueueField?.GetValue(__instance) as List<Player.MinorActionData>;

            if (queue == null)
            {
                return;
            }

            foreach (var savedAction in __state)
            {
                if (!queue.Contains(savedAction))
                {
                    queue.Add(savedAction);
                }
            }
        }
    }
}
