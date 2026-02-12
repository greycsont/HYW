using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace Hyw;

public abstract class PauseMenuTemplate : MonoBehaviour
{
    protected Button resumeBtn;
    protected Button checkpointBtn;
    protected Button optionsBtn;
    protected Button restartBtn;
    protected Button quitBtn;
    protected List<Button> buttons = new List<Button>();
    protected PauseMenu pauseMenuScript;
    protected RectTransform buttonsContainer;
    protected GamepadObjectSelector gamepadObjectSelector;

    protected TMP_FontAsset tmpAsset;

    protected virtual void Awake()
    {
        OnPreSetup();
        EnsureFullScreen();
        SetUpLayout();
        BuildFixedContent();
        SetupCustomContent();
        SetExplicitNavigation();
        OnPostSetup();
    }

    protected virtual void OnEnable()
    {
        PauseMenuScript();
    }

    private void EnsureFullScreen()
    {
        RectTransform rt = GetComponent<RectTransform>();
        if (rt == null) rt = gameObject.AddComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
        
        rt.localScale = Vector3.one;
        rt.localPosition = Vector3.zero;
    }

    private void BuildFixedContent()
    {
        CreateButton();
    }

    private void CreateButton()
    {
        resumeBtn = AddButton("RESUME", OnResumeClicked);
        checkpointBtn = AddButton("CHECKPOINT", OnCheckpointClicked);
        restartBtn = AddButton("RESTART MISSION", OnRestartClicked);
        optionsBtn = AddButton("OPTIONS", OnOptionsClicked);
        quitBtn = AddButton("QUIT", OnQuitClicked);

        gamepadObjectSelector = gameObject.AddComponent<GamepadObjectSelector>();

        gamepadObjectSelector.mainTarget = resumeBtn.gameObject;
    }
    
    private void SetExplicitNavigation()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            Navigation nav = new Navigation();
            nav.mode = Navigation.Mode.Explicit;
            
            nav.selectOnRight = buttons[(i + 1) % buttons.Count];
            nav.selectOnLeft = buttons[(i - 1 + buttons.Count) % buttons.Count];
            
            nav.selectOnDown = nav.selectOnRight;
            nav.selectOnUp = nav.selectOnLeft;

            buttons[i].navigation = nav;
        }
    }
    protected virtual void OnResumeClicked() { OptionsMenuToManager.Instance.UnPause(); }
    protected virtual void OnCheckpointClicked() { OptionsMenuToManager.Instance.RestartCheckpoint(); }
    protected virtual void OnOptionsClicked() { OptionsMenuToManager.Instance.OpenOptions(); }
    protected virtual void OnRestartClicked() { OptionsMenuToManager.Instance.RestartMission(); }
    protected virtual void OnQuitClicked() { OptionsMenuToManager.Instance.QuitMission(); }
    
    protected virtual void OnPreSetup() { } 
    protected virtual void SetupCustomContent() { }
    protected virtual void OnPostSetup() { }
    

    public Button AddButton(string label, UnityAction onClick)
    {
        GameObject btnObj = new GameObject(label + "Button");
        btnObj.transform.SetParent(buttonsContainer, false);

        RectTransform rt = btnObj.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(150, 30);

        btnObj.AddComponent<CanvasRenderer>();
        Image img = btnObj.AddComponent<Image>(); 
        img.color = new Color(1, 1, 1, 0.95f);

        Button btn = btnObj.AddComponent<Button>();
        btn.onClick.AddListener(onClick);

        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(btnObj.transform, false);
        TextMeshProUGUI tmp = textObj.AddComponent<TextMeshProUGUI>();
        tmp.text = label;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.fontSize = 14;
        tmp.color = new Color(0, 0, 0, 1);

        RectTransform textRt = tmp.rectTransform;
        textRt.anchorMin = Vector2.zero;
        textRt.anchorMax = Vector2.one;
        textRt.offsetMin = textRt.offsetMax = Vector2.zero;

        if (tmpAsset != null)
            tmp.font = tmpAsset;
        
        buttons.Add(btn);
        
        return btn;
    }

    private void SetUpLayout()
    {
        GameObject containerObj = new GameObject("ButtonsContainer");
        containerObj.transform.SetParent(this.transform, false);
        buttonsContainer = containerObj.AddComponent<RectTransform>();
        
        buttonsContainer.anchorMin = new Vector2(0, 1);
        buttonsContainer.anchorMax = new Vector2(1, 1);
        buttonsContainer.offsetMin = new Vector2(0, 0);
        buttonsContainer.offsetMax = new Vector2(0, 0);
        
        var layout = containerObj.AddComponent<HorizontalLayoutGroup>();
        
        layout.childAlignment = TextAnchor.MiddleCenter;
        
        layout.padding = new RectOffset(25, 25, 50, 10); 

        layout.spacing = 10;
        
        layout.childControlHeight = false; 
        layout.childControlWidth = false; 
        layout.childForceExpandHeight = false;
        layout.childForceExpandWidth = false;
    }


    private void PauseMenuScript()
	{
        // Reference: PauseMenu in Assembly_CSharp
        var instance = MapInfoBase.Instance;
        if (instance == null)
        {
            checkpointBtn.interactable = false;
        }
        else if (instance.replaceCheckpointButtonWithSkip)
        {
            checkpointBtn.GetComponentInChildren<TMP_Text>().text = "SKIP";
            checkpointBtn.interactable = true;
            checkpointBtn.onClick.RemoveAllListeners();
            checkpointBtn.onClick.AddListener(OnCheckpointButton);
        }
        else
        {
            bool hasCheckpoint = MonoSingleton<StatsManager>.Instance.currentCheckPoint != null;
            checkpointBtn.interactable = hasCheckpoint;
        }
    }

    private void OnCheckpointButton()
    {
        StockMapInfo instance = StockMapInfo.Instance;
        if (!(instance == null))
        {
            string nextSceneName = instance.nextSceneName;
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                MonoSingleton<OptionsMenuToManager>.Instance.ChangeLevel(nextSceneName);
            }
        }
    }
}