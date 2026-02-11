using System.Diagnostics;
using UnityEngine.UI;
using TMPro;
using UnityEngine;


namespace Hyw;

public class ZpPauseMenu : PauseMenuTemplate
{
    protected override void OnPreSetup()
    {
        tmpAsset = AssetLoader.mediumNoto;
    }

    protected override void OnPostSetup()
    {
        AddBackgroundImage();
        AddPausedText();
    }

    private void AddBackgroundImage()
    {
        GameObject bgObj = new GameObject("BackgroundImage");
        bgObj.transform.SetParent(this.transform, false);

        var img = bgObj.AddComponent<Image>();
        img.color = new Color(0, 0, 0, 0.7f);
        img.raycastTarget = false;

        RectTransform rt = img.rectTransform;
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        bgObj.transform.SetAsFirstSibling();
    }

    private void AddPausedText()
    {
        var txtObj = new GameObject("Title");
        txtObj.transform.SetParent(this.transform, false);

        var txt = txtObj.AddComponent<TextMeshProUGUI>();
        txt.text = "PAUSE";
        txt.fontSize = 100;
        txt.alignment = TextAlignmentOptions.Center;
        txt.raycastTarget = false;
        txt.font = AssetLoader.mediumNoto;

        RectTransform rt = txt.rectTransform;

        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);

        rt.pivot = new Vector2(0.5f, 0.5f);

        rt.anchoredPosition = Vector2.zero;

        rt.sizeDelta = new Vector2(500, 100);
    }
}