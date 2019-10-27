using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    [SerializeField] int playerHealth = 6;
    [SerializeField] int playerScore = 0;
    [SerializeField] GameObject _quiz;
    [SerializeField] QuizManager _quizmanager;
    [SerializeField] Color _riverColor = Color.blue;
    [SerializeField] Color _seaColor = Color.yellow;
    [SerializeField] SpriteRenderer srenderer;
    private GameObject fishObject;
    private EnvironmentType enviType;
    public GameObject popUpMiniLab;
    public GameObject optionPanel;
    public bool isCorrectAnswers { get; set; }
    public bool isPlatformer;
    public bool isPaused { get; set; }
    private void Awake()
    {
        if (GameUtility.PlayerHealth <= 0)
        {
            GameUtility.PlayerHealth = playerHealth;
            GameUtility.PlayerScore = playerScore;
        }
        else
        {
            playerHealth = GameUtility.PlayerHealth;
            playerScore = GameUtility.PlayerScore;
        }

        isCorrectAnswers = false;
        isPaused = false;

        UIManager.Instance.UpdateUI(playerHealth, playerScore);
        if (GameUtility.environmentType == EnvironmentType.Forest)
            srenderer.color = _riverColor;
        else
            srenderer.color = _seaColor;
    }
    private void LateUpdate()
    {
        if(Input.GetKeyDown("escape")){
            onOption();
        }
    }
    public void OpenQuiz(GameObject other)
    {
        
        isPaused = true;
        _quiz.SetActive(true);
        _quizmanager.Display();
        fishObject = other;
    }
    public void CloseQuiz(bool answer)
    {
        _quiz.SetActive(false);
        isPaused = false;
        isCorrectAnswers = answer;
        if (answer == true)
        {
            playerScore += 1;
            GameUtility.PlayerScore = playerScore;
        }
        else
        {
            playerHealth -= 1;
            GameUtility.PlayerHealth = playerHealth;
        }
        UIManager.Instance.UpdateUI(playerHealth, playerScore);
    }
    public GameObject getChildObject()
    {
        if (isCorrectAnswers)
            return fishObject;
        return null;
    }

    public void TakeDamage()
    {
        playerHealth -= 1;
        UIManager.Instance.UpdateUI(playerHealth, playerScore);
        GameUtility.PlayerHealth = playerHealth;
    }

    public void OpenCrest(int index)
    {
        if (GameUtility.environmentType == EnvironmentType.Forest && isPlatformer)
        {
            GameUtility.environmentType = EnvironmentType.Sea;
            SceneLoad(index);
        }
        else
        {
            if (!popUpMiniLab.activeInHierarchy)
                popUpMiniLab.SetActive(true);
        }
    }
    public void onOption(){
        if(optionPanel.activeInHierarchy){
            optionPanel.SetActive(false);
            isPaused = false;
        }else{
            optionPanel.SetActive(true);
            isPaused = true;
        }
    }
    public void SceneLoad(int index)
    {

        SceneManager.LoadScene(index);
    }
    public void SceneLoadString(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
