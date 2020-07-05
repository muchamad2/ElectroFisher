using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TrialManager : Singleton<TrialManager>
{


    [Header("Tools")]
    [SerializeField] List<DragHandler> tools;
    private List<DragHandler> correctTools;
    [SerializeField] GameObject checkComparison;
    [Header("Material")]
    [SerializeField] Slot areaDrop;
    [SerializeField] List<DragHandler> materials;
    [Header("References Object")]
    [SerializeField] GameObject nextLevelBtn;

    private int playerHealth;
    private int playerScore;
    private void Start()
    {
        playerScore = GameUtility.PlayerScore;
        playerHealth = GameUtility.PlayerHealth;
        UIManager.Instance.UpdateUI(playerHealth, playerScore);
        nextLevelBtn?.SetActive(false);
        foreach (var item in materials)
        {
            item.GetComponent<Image>().raycastTarget = false;
        }
    }
    public void Correction(bool state)
    {
        if (state)
        {
            playerScore += 1;
        }
        else
        {
            playerHealth -= 1;
        }
        UIManager.Instance.UpdateUI(playerHealth, playerScore);
        if(playerHealth <= 0){
            GameManager.Instance.SceneLoad(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void CheckTools(List<DragHandler> otherTools)
    {
        // if (tools.Count == otherTools.Count)
        //     checkComparison.SetActive(true);
        // else
        //     checkComparison.SetActive(false);

        correctTools = otherTools;
        if (tools.Count == otherTools.Count)
        {
            areaDrop.areaDrop = false;
            foreach (var material in materials)
            {
                material.GetComponent<Image>().raycastTarget = true;
                material.isDragable = true;
            }
            nextLevelBtn?.SetActive(true);
            GameUtility.FinalScore += playerScore;
        }

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
        if (playerHealth <= 0)
        {
            GameManager.Instance.SceneLoad(SceneManager.GetActiveScene().buildIndex);
            GameUtility.PlayerScore = 0;
        }
        GameUtility.PlayerScore = playerScore;
    }
}