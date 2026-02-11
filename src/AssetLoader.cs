using UnityEngine;
using System.Reflection;
using TMPro;


namespace Hyw;

public class AssetLoader
{
    const string fontPath = "Hyw.asset.7000.bundle";

    public static AssetBundle assetBundle;
    
    public static TMP_FontAsset regularNoto;
    public static TMP_FontAsset boldNoto;
    public static TMP_FontAsset mediumNoto;
    
    public static void Initialize()
    {
        assetBundle = LoadAssetBundle(fontPath);
        regularNoto = assetBundle.LoadAsset<TMP_FontAsset>("NotoSansSC-Regular SDF");
        boldNoto = assetBundle.LoadAsset<TMP_FontAsset>("NotoSansSC-Bold SDF");
        mediumNoto = assetBundle.LoadAsset<TMP_FontAsset>("NotoSansSC-Medium SDF");
        foreach (var gameobjct in assetBundle.LoadAllAssets())
        {
            Plugin.Logger.LogInfo($"{gameobjct.name}: {gameobjct.GetType().Name}");
        }
    }

    public static AssetBundle LoadAssetBundle(string assetBundlePath)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(assetBundlePath);
        
        if (stream != null)
            Plugin.Logger.LogInfo($"Loading asset bundle {assetBundlePath}");
    
        return AssetBundle.LoadFromStream(stream);;
    }
}