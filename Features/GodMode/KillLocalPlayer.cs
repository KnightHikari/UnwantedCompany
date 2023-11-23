using HarmonyLib;
using GameNetcodeStuff;

namespace UnwantedCompany
{
    [HarmonyPatch(typeof(KillLocalPlayer), "KillPlayer")]
    public static class KillPlayerPatch
    {
        public static bool Prefix(PlayerControllerB playerWhoTriggered)
        {
            if (playerWhoTriggered.isPlayerControlled && Settings.Instance.GodMode)
            {
                return false;
            }
            return true;
        }
    }
}