using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TextWriterAssistant : MonoBehaviour
{

    
    private TextMeshProUGUI massageText;
    private AudioSource talkingAudio;
    [SerializeField]private AudioSource bgmAudio;
    [Range(0.01f,0.1f)]
    [SerializeField] float timerPerCharacter;
    [TextArea]
    [SerializeField] private string indoText;
    [SerializeField] AudioClip indoAudio;
    [TextArea]
    [SerializeField] private string engText;
    [SerializeField] AudioClip enAudio;
    private string textMassage;
    [SerializeField] GameObject btnStart;

    private void Awake()
    {
        massageText = transform.Find("massageText").GetComponent<TextMeshProUGUI>();
        talkingAudio = transform.Find("soundTalking").GetComponent<AudioSource>();
        
        //Application.targetFrameRate = 4;
    }
    private void Start()
    {
        if(btnStart != null)
            btnStart.SetActive(false);

        if (GameUtility.LangType == Language.Eng)
        {
            textMassage = engText;
            talkingAudio.clip = enAudio;
        }
        else if (GameUtility.LangType == Language.Indo)
        {
            textMassage = indoText;
            talkingAudio.clip = indoAudio;
        }
        talkingAudio.mute = GameUtility.mute;
        startTalking();
        TextWriter.AddWriter_Static(massageText, textMassage, timerPerCharacter, true,true, OnComplete);
    }
    void OnComplete()
    {
        stopTalking();
        //SceneTransasition.Instance.LoadScene(1);
        if(btnStart != null) btnStart.SetActive(true);
        else SceneTransasition.Instance.LoadScene(1);

    }
    void startTalking() { 
        talkingAudio.Play();
        bgmAudio.Pause();
    }
    void stopTalking() { 
        talkingAudio.Stop();
        bgmAudio.Play();
    }
}