using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class TrialManager : Singleton<TrialManager>
{

    [SerializeField] List<DragHandler> tools;
    private List<DragHandler> correctTools;
    [SerializeField] GameObject checkComparison;
    private int playerHealth;
    private int playerScore;
    private void Start()
    {
        playerScore = GameUtility.PlayerScore;
        playerHealth = GameUtility.PlayerHealth;
        UIManager.Instance.UpdateUI(playerHealth, playerScore);
    }
    /*     public void Correction(bool state){
            if(state)
            {
                playerScore += 1;
            }else
            {
                playerHealth -= 2;
            }
            UIManager.Instance.UpdateUI(playerHealth,playerScore);
        } */
    public void CheckTools(List<DragHandler> otherTools)
    {
        if (tools.Count == otherTools.Count)
            checkComparison.SetActive(true);
        else
            checkComparison.SetActive(false);

        correctTools = otherTools;

    }

    public void CorrectionTools()
    {

        for (int i = 0; i < correctTools.Count; i++)
        {
            if (correctTools[i].isCorrect)
                playerScore += 1;
            else if (!correctTools[i].isCorrect)
                playerHealth -= 2;

            UIManager.Instance.UpdateUI(playerHealth, playerScore);

        }
        if (playerHealth <= 0){
            GameManager.Instance.SceneLoad(SceneManager.GetActiveScene().buildIndex);
            GameUtility.PlayerScore = 0;
        }
        GameUtility.PlayerScore = playerScore;
    }
}