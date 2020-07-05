using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlatformManager : Singleton<PlatformManager> {
    private int playerHealth;
    private int playerScore;
    [SerializeField] Sprite miniLabEng;
    [SerializeField] Sprite miniLabIna;
    public GameObject popUpMiniLab;
    [SerializeField] AudioSource bgmAudio;
    private void Start()
    {
        playerHealth = GameUtility.PlayerHealth;
        playerScore = GameUtility.PlayerScore;
        if(bgmAudio != null) bgmAudio.mute = GameUtility.mute;
        if(popUpMiniLab != null){
            switch (GameUtility.LangType)
            {
                case Language.Eng:
                    popUpMiniLab.GetComponentInChildren<Image>().sprite = miniLabEng;
                break;
                case Language.Indo:
                    popUpMiniLab.GetComponentInChildren<Image>().sprite = miniLabIna;
                break;
            }
            popUpMiniLab.SetActive(false);
        } 
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
        if(playerHealth == 0){
            GameManager.Instance.SceneLoad(SceneManager.GetActiveScene().buildIndex);
        }
    }
}