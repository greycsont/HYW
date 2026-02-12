using System.Diagnostics;
using UnityEngine.UI;
using TMPro;
using UnityEngine;


namespace Hyw;

public static class CNBtnState
{
    public static bool isActive = false;
    public static string currentSymbol => isActive ? "-" : "+";
}

public class ZpPauseMenu : PauseMenuTemplate
{
    private GameObject titleObject;
    private GameObject title_EN;
    private GameObject title_CN;
    private Button showTitleCNBtn;
    
    protected override void OnPreSetup()
    {
        tmpAsset = AssetLoader.mediumNoto;
        titleObject = new GameObject("TitleObject");
        titleObject.transform.SetParent(this.transform, false);
        titleObject.transform.localPosition = Vector3.zero;
        titleObject.transform.localScale = Vector3.one * 0.9f;
    }

    protected override void OnPostSetup()
    {
        AddBackgroundImage();
        AddPausedText();
        AddPausedTextCN();
        title_CN.SetActive(CNBtnState.isActive);
    }

    protected override void SetupCustomContent()
    {
        showTitleCNBtn = AddButton(CNBtnState.currentSymbol, OnShowTitileCNClicked);
        showTitleCNBtn.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
    }

    private void OnShowTitileCNClicked()
    {
        CNBtnState.isActive = !CNBtnState.isActive;
        title_CN.SetActive(CNBtnState.isActive);
        showTitleCNBtn.GetComponentInChildren<TextMeshProUGUI>().text = CNBtnState.currentSymbol;
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
        title_EN = new GameObject("Title_EN");
        title_EN.transform.SetParent(titleObject.transform, false);

        var txt = title_EN.AddComponent<TextMeshProUGUI>();
        txt.text = "PAUSE";
        txt.fontSize = 100;
        txt.alignment = TextAlignmentOptions.Center;
        txt.raycastTarget = false;
        txt.font = AssetLoader.mediumNoto;
        txt.enableKerning = false;
        txt.characterSpacing = -2f;
        RectTransform rt = txt.rectTransform;

        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);

        rt.pivot = new Vector2(0.5f, 0.5f);

        rt.anchoredPosition = Vector2.zero;

        rt.sizeDelta = new Vector2(500, 100);
    }

    private void AddPausedTextCN()
    {
        title_CN = new GameObject("Title_CN");
        title_CN.transform.SetParent(titleObject.transform, false);

        var txt = title_CN.AddComponent<TextMeshProUGUI>();
        txt.text = "----暂停中----";
        txt.fontSize = 30;
        txt.alignment = TextAlignmentOptions.Center;
        txt.raycastTarget = false;
        txt.font = AssetLoader.mediumNoto;
        txt.enableKerning = false;
        txt.characterSpacing = -1f;
        Plugin.Logger.LogError(txt.characterSpacing);
        

        RectTransform rt = txt.rectTransform;

        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);

        rt.pivot = new Vector2(0.5f, 0.5f);

        rt.anchoredPosition = new Vector2(0, -75);

        rt.sizeDelta = new Vector2(500, 100);
    }
}