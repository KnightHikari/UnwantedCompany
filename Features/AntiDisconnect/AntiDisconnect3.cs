using HarmonyLib;

namespace UnwantedCompany
{
    [HarmonyPatch(typeof(StartOfRound), "OnPlayerDC")]
    public static class OnPlayerDCPatch
    {
        public static bool Prefix(int playerObjectNumber, ulong clientId, StartOfRound __instance)
        {
            if (Settings.Instance.AntiDisconnect)
            {
                return false;
            }
            return true;
        }
    }
}