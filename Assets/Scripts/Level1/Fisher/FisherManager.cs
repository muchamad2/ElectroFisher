using UnityEngine;
public class FisherManager : Singleton<FisherManager> {
    private int playerHealth;
    private int playerScore;
    [SerializeField] AudioSource bgmAudio;
    [SerializeField] private GameObject _quiz;
    [SerializeField] QuizManager _quizManager;
    private GameObject fishObject;
    public bool isCorrectAnswers{get;set;}
    private void Start()
    {
        isCorrectAnswers = false;
        playerHealth = GameUtility.PlayerHealth;
        playerScore = GameUtility.PlayerScore;
        if(bgmAudio != null) bgmAudio.mute = GameUtility.mute;
    }
    public void OpenQuiz(GameObject other){
        GameManager.Instance.isPaused = true;
        _quiz.SetActive(true);
        fishObject = other;
         _quizManager.Display();
    }
    public GameObject getChildObject()
    {
        if (isCorrectAnswers)
            return fishObject;
        return null;
    }
    public void UpdateHealth(){
        playerHealth -= 1;
        UIManager.Instance.UpdateUI(playerHealth, playerScore);
        if(playerHealth == 0){
            GameManager.Instance.SceneReload();
        }
    }
    public void CloseQuiz(bool answer)
    {
        _quiz.SetActive(false);
        GameManager.Instance.isPaused = false;
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
        if(playerHealth == 0){
            GameManager.Instance.SceneReload();
        }
    }
    
}