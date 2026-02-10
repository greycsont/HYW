using System.Diagnostics;
using UnityEngine.UI;
using TMPro;
using UnityEngine;


namespace HYW;

public class ArkPauseMenu : PauseMenuTemplate
{
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
        GameObject myTextObj = new GameObject("Title");
        myTextObj.transform.SetParent(this.transform, false);

        var txt = myTextObj.AddComponent<TextMeshProUGUI>();
        txt.text = "-- PAUSED --";
        txt.fontSize = 45;
        txt.alignment = TextAlignmentOptions.Center;
        txt.raycastTarget = false;
        
        RectTransform rt = txt.rectTransform;
        
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        
        rt.pivot = new Vector2(0.5f, 0.5f);
        
        rt.anchoredPosition = Vector2.zero; 
        
        rt.sizeDelta = new Vector2(500, 100);
    }
}
