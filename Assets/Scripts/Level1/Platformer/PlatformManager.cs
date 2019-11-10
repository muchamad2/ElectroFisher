using UnityEngine;

public class PlatformManager : Singleton<PlatformManager> {
    private int playerHealth;
    private int playerScore;
    public GameObject popUpMiniLab;
    private void Start()
    {
        playerHealth = GameUtility.PlayerHealth;
        playerScore = GameUtility.PlayerScore;
    }
    public void OpenCrest(int index)
    {
        
        if (GameUtility.environmentType == EnvironmentType.Forest)
        {
            GameUtility.environmentType = EnvironmentType.Sea;
            GameManager.Instance.SceneLoad(index);
        }
        else
        {
            if (!popUpMiniLab.activeInHierarchy)
                popUpMiniLab.SetActive(true);
        }
        playerScore -= 3;
        GameUtility.PlayerScore = playerScore;
        GameUtility.FinalScore = playerScore;
        UIManager.Instance.UpdateUI(playerHealth,playerScore);
    }
    public void TakeDamage()
    {
        playerHealth -= 1;
        UIManager.Instance.UpdateUI(playerHealth, playerScore);
        GameUtility.PlayerHealth = playerHealth;
    }
}