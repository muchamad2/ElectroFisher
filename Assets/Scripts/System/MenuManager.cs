﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
[System.Serializable]
public struct MenuUIElement
{
    [Header("Initiate Content")]
    [SerializeField] TextMeshProUGUI _languageText;
    public TextMeshProUGUI LenguageText { get { return _languageText; } }
    [SerializeField] TextMeshProUGUI _nextText;
    public TextMeshProUGUI NextText { get { return _nextText; } }
    [Header("Main Content")]
    [SerializeField] TextMeshProUGUI _playText;
    public TextMeshProUGUI PlayText { get { return _playText; } }

    [SerializeField] TextMeshProUGUI _quitText;
    public TextMeshProUGUI QuitText { get { return _quitText; } }
    [Header("Login Content")]
    [SerializeField] TextMeshProUGUI _labelPlayerName;
    public TextMeshProUGUI PlayerName { get { return _labelPlayerName; } }
    [SerializeField] TextMeshProUGUI _labelPlayerGrade;
    public TextMeshProUGUI PlayerGrade { get { return _labelPlayerGrade; } }
    [SerializeField] TextMeshProUGUI _titleBtnLogin;
    public TextMeshProUGUI BtnLogin { get { return _titleBtnLogin; } }
}
public class MenuManager : MonoBehaviour
{

    [SerializeField] TMP_InputField inputName;
    [SerializeField] TMP_InputField inputGrade;
    [Header("GameObject References")]
    public GameObject InitiateContent;
    public GameObject loginContent;
    public GameObject mainContent;
    public GameObject InitiateContentBtnNext;
    public GameObject[] optionsContent;

    [Header("UI Text References")]
    public TextScript IndoScript;
    public TextScript EngScript;

    [Header("UI Text Elements")]
    public MenuUIElement element;
    private void Awake()
    {
        if (!InitiateContent.activeInHierarchy)
            InitiateContent.SetActive(true);
        if (loginContent.activeInHierarchy)
            loginContent.SetActive(false);
        if (mainContent.activeInHierarchy)
            mainContent.SetActive(false);
        InitiateContentBtnNext.SetActive(false);
        closeAllOption();
    }
    private void Start()
    {
        if (GameUtility.isPlaying)
        {
            InitiateContent.SetActive(false);
            loginContent.SetActive(false);
            mainContent.SetActive(true);
        }
    }
    public void ChangeLanguage(string lang)
    {
        if (lang == "IND")
            GameUtility.LangType = Language.Indo;
        else if (lang == "ENG")
            GameUtility.LangType = Language.Eng;
        InitiateContentBtnNext.SetActive(true);
        LanguageTextChange();
    }
    public void LanguageTextChange()
    {
        if (GameUtility.LangType == Language.Indo)
        {
            element.LenguageText.text = IndoScript.LanguageSelect.Info;
            element.PlayText.text = IndoScript.Play.Info;
            element.NextText.text = IndoScript.Next.Info;
            element.QuitText.text = IndoScript.BtnQuit.Info;
            element.PlayerName.text = IndoScript.LabelPlayerName.Info;
            element.PlayerGrade.text = IndoScript.LabelPlayerGrade.Info;
            element.BtnLogin.text = IndoScript.TitleBtnLogin.Info;

        }
        else
        {
            element.LenguageText.text = EngScript.LanguageSelect.Info;
            element.PlayText.text = EngScript.Play.Info;
            element.NextText.text = EngScript.Next.Info;
            element.QuitText.text = EngScript.BtnQuit.Info;
            element.PlayerName.text = EngScript.LabelPlayerName.Info;
            element.PlayerGrade.text = EngScript.LabelPlayerGrade.Info;
            element.BtnLogin.text = EngScript.TitleBtnLogin.Info;
        }
    }
    public void BtnNext()
    {
        InitiateContent.SetActive(false);
        loginContent.SetActive(true);
    }
    public void Login()
    {
        GameUtility.PlayerName = inputName.text;
        GameUtility.PlayerGrade = inputGrade.text;
        loginContent.SetActive(false);
        mainContent.SetActive(true);
    }
    public void Play()
    {
        Debug.Log("Player Name = " + GameUtility.PlayerName + " Player Grade : " + GameUtility.PlayerGrade);
        SceneManager.LoadScene(1);
        GameUtility.isPlaying = true;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void openOption(int index)
    {
        if (optionsContent.Length != 0)
        {
            closeAllOption();
            optionsContent[index].SetActive(true);
        }
    }
    public void closeAllOption()
    {
        if (optionsContent.Length != 0)
        {
            foreach (GameObject content in optionsContent)
            {
                if (content.activeInHierarchy)
                    content.SetActive(false);
            }
        }
    }
}
