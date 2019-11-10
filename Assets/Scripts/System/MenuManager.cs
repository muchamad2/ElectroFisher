﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuManager : MonoBehaviour
{
    public GameObject InitiateContent;
    public GameObject loginContent;
    public GameObject mainContent;
    private void Awake()
    {
        if (!InitiateContent.activeInHierarchy)
            InitiateContent.SetActive(true);
        if (loginContent.activeInHierarchy)
            loginContent.SetActive(false);
        if (mainContent.activeInHierarchy)
            mainContent.SetActive(false);
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
        InitiateContent.SetActive(false);
        loginContent.SetActive(true);
    }
    public void Login()
    {
        loginContent.SetActive(false);
        mainContent.SetActive(true);
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
        GameUtility.isPlaying = true;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void OpenMateri()
    {

    }
    public void OpenRules()
    {

    }
    public void OpenTujuan()
    {

    }
}
