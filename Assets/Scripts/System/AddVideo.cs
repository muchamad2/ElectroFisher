using UnityEngine;
using UnityEngine.Video;
public class AddVideo : MonoBehaviour {
    public VideoClip indovideo,enVideo;
    public GameObject rawVideo;
    public void OpenVideo(){
        rawVideo.SetActive(true);
        Play();
    }
    public void Play(){
        
        switch(GameUtility.LangType){
            case Language.Indo:
            StreamVideo.Instance.SetPrepareVideo(indovideo);
            break;
            case Language.Eng:
            StreamVideo.Instance.SetPrepareVideo(enVideo);
            break;
        }
        
        StreamVideo.Instance.PlayVideo();
    }
    public void Back(){
        StreamVideo.Instance.StopVide();
        MenuManager.Instance.closeAllOption();
    }
}