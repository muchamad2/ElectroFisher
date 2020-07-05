using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
public class StreamVideo : MonoBehaviour
{

    
    private AudioSource audioSource;
    public AudioSource bgmSource;
    private VideoPlayer videoPlayer;
    private Action onComplete;
    public RawImage image;
    public RenderTexture texture;
    //public VideoClip video;

    private static StreamVideo _instance;
    public static StreamVideo Instance{
        get{
            return _instance;
        }
    }
    private void Awake() {
        if(_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
        
        
    }
    private void Start() {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        audioSource = gameObject.AddComponent<AudioSource>();

        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        audioSource.Pause();

        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        videoPlayer.EnableAudioTrack(0,true);
        videoPlayer.SetTargetAudioSource(0,audioSource);

        

        videoPlayer.loopPointReached += CompleteVideo;
    }

    public void SetPrepareVideo(VideoClip video, Action onComplete = null){
        videoPlayer.clip = video;
        //videoPlayer.texture = texture;
        videoPlayer.Prepare();
        videoPlayer.targetTexture = texture;
        audioSource.mute = GameUtility.mute;
        this.onComplete = onComplete;
    }
    void CompleteVideo(VideoPlayer videoPlayer){
        if(onComplete != null) onComplete();
    }
    public void PlayVideo(){
        //SetPrepareVideo();
        if(videoPlayer.isPlaying){
            videoPlayer.Pause();
            audioSource.Pause();
            if(bgmSource != null) bgmSource.Play();
        }else{
            videoPlayer.Play();
            audioSource.Play();
            if(bgmSource != null)
            {
                bgmSource.Stop();
            }
        }
    }
    public void StopVide(){
        videoPlayer.Stop();
        audioSource.Stop();
        if(bgmSource != null) bgmSource.Play();
    }
}
