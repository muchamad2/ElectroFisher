using UnityEngine;
using TMPro;
public class QAManager : Singleton<QAManager>
{
    private int playerHealth;
    private int playerScore;
    public GameObject firstPlay;
    public GameObject QAPanel;
    public GameObject ScorePanel;
    public QuizManager _quizManager;
    public GameEvents events;
    public TextMeshProUGUI totalScoreTxt;
    [SerializeField] AudioSource bgmAudio;
    private void Start()
    {
        playerHealth = GameUtility.PlayerHealth;
        playerScore = GameUtility.PlayerScore;
        events.scoreUpdated += updateScore;
        UIManager.Instance.UpdateUI(playerHealth, playerScore);
        if(bgmAudio != null) bgmAudio.mute = GameUtility.mute;
    }
    public void startDiscussion()
    {
        firstPlay.SetActive(false);
        QAPanel.SetActive(true);
        _quizManager.DisplayUrut();
    }
    public void updateScore(bool state)
    {
        switch (state)
        {
            case true:
                playerScore += 2;
                GameUtility.PlayerScore = playerScore;
                break;
            case false:
                playerHealth -= 1;
                GameUtility.PlayerHealth = playerHealth;
                break;
        }
        UIManager.Instance.UpdateUI(playerHealth, playerScore);
    }
    public void ShowTotalScore()
    {
        GameUtility.FinalScore += GameUtility.PlayerScore;
        ScorePanel.SetActive(true);

        totalScoreTxt.text = "Total Score : " + GameUtility.FinalScore;
        GameManager.Instance.isPaused = true;
    }
}