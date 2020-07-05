using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[System.Serializable]
public struct InGameUIElements
{
    [SerializeField] TextMeshProUGUI _homeText;
    public TextMeshProUGUI HomeText { get { return _homeText; } }

    [SerializeField] TextMeshProUGUI _quitText;
    public TextMeshProUGUI QuitText { get { return _quitText; } }
    [SerializeField] TextMeshProUGUI _labelQuit;
    public TextMeshProUGUI LabelQuitText { get { return _labelQuit; } }
    [SerializeField] TextMeshProUGUI _labelYes;
    public TextMeshProUGUI LabelYes { get { return _labelYes; } }
    [SerializeField] TextMeshProUGUI _labelNo;
    public TextMeshProUGUI LabelNo { get { return _labelNo; } }
}
[System.Serializable]
public struct ChangerText{
    public TextMeshProUGUI uiText;
    [TextArea]
    public string indoText;
    [TextArea]
    public string engText;
}

public class UIManager : Singleton<UIManager>
{
    [SerializeField] TextMeshProUGUI scoreTxt;
    [SerializeField] TextMeshProUGUI healthTxt;
    [SerializeField] InGameUIElements uIElements;
    [Header("UI References")]
    [SerializeField] TextScript IndoScript;
    [SerializeField] TextScript EngScript;
    [Header("UI Text References")]
    [SerializeField] ChangerText[] textChange;
    private List<TextChanger> textChangers;
    

    private void Start()
    {
        if (GameUtility.LangType == Language.Indo)
            ChangeLanguage(IndoScript);
        else
            ChangeLanguage(EngScript);
        
        for (int i = 0; i < textChange.Length; i++)
        {
            switch (GameUtility.LangType)
            {
                case Language.Indo: 
                textChange[i].uiText.text = textChange[i].indoText;
                break;
                case Language.Eng:
                textChange[i].uiText.text = textChange[i].engText;
                break;
            }
        }
        

    }
    void ChangeLanguage(TextScript script)
    {
        if (uIElements.HomeText != null)
            uIElements.HomeText.text = script.BtnHome.Info;
        if (uIElements.QuitText != null)
            uIElements.QuitText.text = script.BtnQuit.Info;
        if (uIElements.LabelQuitText != null)
            uIElements.LabelQuitText.text = script.LabelQuitText.Info;
        if (uIElements.LabelYes != null)
            uIElements.LabelYes.text = script.Yes.Info;
        if (uIElements.LabelNo != null)
            uIElements.LabelNo.text = script.No.Info;
    }
    public void UpdateUI(int health, int score)
    {
        scoreTxt.text = "" + score;
        healthTxt.text = "" + health;
    }

    public class TextChanger{
        public TextMeshProUGUI uiText;
        private string indoText;
        private string engText;
        public TextChanger(TextMeshProUGUI uiText,string indoTxt, string engText){
            this.uiText = uiText;
            this.indoText = indoText;
            this.engText = engText;
        }
        public void Start(){
            switch (GameUtility.LangType)
            {
                case Language.Indo: 
                uiText.text = indoText;
                break;
                case Language.Eng:
                uiText.text = engText;
                break;
            }
        }
    }
}
