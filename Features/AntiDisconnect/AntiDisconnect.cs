using HarmonyLib;

namespace UnwantedCompany
{
    [HarmonyPatch(typeof(GameNetworkManager), "Disconnect")]
    public static class GameNetworkManagerDisconnectPatch
    {
        public static bool Prefix(GameNetworkManager __instance)
        {
            if (Settings.Instance.AntiDisconnect) {
                return false;
            }
            return true;
        }
    }
}