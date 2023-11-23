using HarmonyLib;
using Unity.Netcode;

namespace UnwantedCompany
{
    [HarmonyPatch(typeof(NetworkConnectionManager), "OnClientDisconnectFromServer")]
    public static class OnClientDisconnectPatch
    {
        public static bool Prefix(ulong clientId)
        {
            if (Settings.Instance.AntiDisconnect)
            {
                return false;
            }
            return true;
        }
    }
}