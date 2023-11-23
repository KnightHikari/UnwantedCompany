using HarmonyLib;
using Unity.Netcode;

namespace UnwantedCompany
{
    [HarmonyPatch(typeof(NetworkConnectionManager), "DisconnectEventHandler")]
    public static class DisconnectEventHandlerPatch
    {
        public static bool Prefix(ulong transportClientId, GameNetworkManager __instance)
        {
            if (Settings.Instance.AntiDisconnect)
            {
                return false;
            }
            return true;
        }
    }
}