using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class VideoExperimental : MonoBehaviour
{
    public VideoClip clip;
    public GameObject videoPanel;
    private void Start() {
        if(videoPanel.activeInHierarchy) videoPanel.SetActive(false);
    }
    public void invokeVideo(){
        videoPanel.SetActive(true);
        StreamVideo.Instance.SetPrepareVideo(clip,OnComplete);
        StreamVideo.Instance.PlayVideo();
    }
    void OnComplete()
    {
        Invoke("closeAction",2f);
    }
    void closeAction(){
        StreamVideo.Instance.StopVide();
        videoPanel.SetActive(false);
    }
}
