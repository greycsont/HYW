using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace Hyw;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger { get; private set; } = null!;

    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        gameObject.hideFlags = HideFlags.DontSaveInEditor;
        
        AssetLoader.Initialize();
        
        var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        
        harmony.PatchAll();
    }
}