using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    [Header("Option Elements")]
    [SerializeField] int playerHealth = 6;
    [SerializeField] int playerScore = 0;
    [SerializeField] QuizManager quiz = null;
    [Space]
    public bool isQuestionOnly = false;
    [Space]
    [Header("Reference")]
    [SerializeField] Color _riverColor = Color.blue;
    [SerializeField] Color _seaColor = Color.yellow;
    [SerializeField] SpriteRenderer spriteRenderer = null;
    public GameObject optionPanel;
    
    public bool isPaused { get; set; }
    
    private void Awake()
    {

        if (GameUtility.PlayerHealth <= 0)
        {
            GameUtility.PlayerHealth = playerHealth;
            GameUtility.PlayerScore = playerScore;
        }

        isPaused = false;

        UIManager.Instance.UpdateUI(playerHealth, playerScore);
        if (quiz != null)
        {
            if (GameUtility.environmentType == EnvironmentType.Forest)
                quiz.Filename = "_Sungai_QuestionData.xml";
            else
                quiz.Filename = "_Laut_QuestionData.xml";
        }

    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown("escape"))
        {
            onOption();
        }
    }



    public void onOption()
    {
        if (optionPanel.activeInHierarchy)
        {
            optionPanel.SetActive(false);
            isPaused = false;
        }
        else
        {
            optionPanel.SetActive(true);
            isPaused = true;
        }
    }
    public void SceneLoad(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void SceneLoadString(string sceneName)
    {
        GameUtility.FinalScore += GameUtility.PlayerScore;
        GameUtility.PlayerHealth = 0;
        GameUtility.PlayerScore = 0;
        SceneManager.LoadScene(sceneName);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
