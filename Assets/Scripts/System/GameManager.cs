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
    public GameObject explanationArea = null;
    [SerializeField] AudioSource bgmAudio;

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
        if (!isQuestionOnly)
        {
            if (quiz != null)
            {
                if (GameUtility.environmentType == EnvironmentType.Forest)
                    quiz.Filename = "_Sungai_QuestionData.xml";
                else
                    quiz.Filename = "_Laut_QuestionData.xml";
            }
        }else{
            if(quiz != null){
                quiz.Filename = "_QuizOnly.xml";
            }
        }
        if(bgmAudio != null) bgmAudio.mute = GameUtility.mute;
    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown("escape"))
        {
            onOption();
        }

    }

    public void CloseExplanationArea()
    {

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
    public void SceneReload()
    {
        SceneTransasition.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void SceneLoad(int index)
    {
        SceneTransasition.Instance.LoadScene(index);
    }
    public void BackToMainMenu()
    {
        SceneTransasition.Instance.LoadScene("MainMenu");
    }
    public void SceneLoadString(string sceneName)
    {
        GameUtility.FinalScore += GameUtility.PlayerScore;
        GameUtility.PlayerHealth = 0;
        GameUtility.PlayerScore = 0;
        SceneTransasition.Instance.LoadScene(sceneName);

    }
    public void Quit()
    {
        Application.Quit();
    }
}
