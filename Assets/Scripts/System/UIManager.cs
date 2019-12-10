using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
public class UIManager : Singleton<UIManager>
{
    [SerializeField] TextMeshProUGUI scoreTxt;
    [SerializeField] TextMeshProUGUI healthTxt;
    [SerializeField] InGameUIElements uIElements;
    [Header("UI References")]
    [SerializeField] TextScript IndoScript;
    [SerializeField] TextScript EngScript;

    private void Start()
    {
        if (GameUtility.LangType == Language.Indo)
            ChangeLanguage(IndoScript);
        else
            ChangeLanguage(EngScript);
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
}
