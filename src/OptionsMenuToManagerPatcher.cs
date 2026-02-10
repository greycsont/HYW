using HarmonyLib;

namespace HYW;


[HarmonyPatch(typeof(OptionsMenuToManager))]
public static class InstantiatePatch
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(OptionsMenuToManager.SetPauseMenu))]
    public static void Prefix(OptionsMenuToManager __instance)
        => PauseMenuChecker.CheckPauseMenu(__instance);
}