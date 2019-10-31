using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuManager : MonoBehaviour
{

    public void ChangeLanguage(string lang)
    {
        if (lang == "IND")
            GameUtility.LangType = Language.Indo;
        else if (lang == "ENG")
            GameUtility.LangType = Language.Eng;
    }
    public void Login()
    {

    }
    public void Play()
    {
        SceneManager.LoadScene(1);
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
