using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace EquipWhileRunning.Patches
{
    [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.EquipItem))]
    public static class HumanoidEquipItemPatch
    {
        private static readonly FieldInfo GrapplingField = AccessTools.Field(typeof(Player), "m_grappling");
        private static readonly FieldInfo BlockReloadField = AccessTools.Field(typeof(Player), "m_blockReload");

        [HarmonyPostfix]
        private static void ReloadGrapplingHookOnEquip(Humanoid __instance, ItemDrop.ItemData item, bool __result)
        {
            var plugin = Plugin.Instance;

            if (!__result || plugin == null || !plugin.IsModEnabled.Value || !plugin.EnableGrapplingHookAutoReload.Value)
            {
                return;
            }

            var player = __instance as Player;

            if (player == null ||
                player != Player.m_localPlayer ||
                item == null ||
                item.m_shared.m_name != "$item_graplinghook" ||
                player.GetCurrentWeapon() != item ||
                player.IsWeaponLoaded())
            {
                return;
            }

            float cooldown = (float)plugin.GrapplingHookAutoReloadCooldown.Value;
            float grappling = (float)GrapplingField.GetValue(player);
            float blockReload = (float)BlockReloadField.GetValue(player);

            if (grappling > 0f || blockReload > 0f)
            {
                return;
            }

            if (plugin.GrapplingHookCooldownPlayer == player &&
                Time.time - plugin.GrapplingHookCooldownStartedAt < cooldown)
            {
                return;
            }

            player.SetWeaponLoaded(item);
        }
    }
}
