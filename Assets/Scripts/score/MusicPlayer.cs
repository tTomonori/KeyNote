using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    private AudioSource mAudio;
    //音声の現在位置
    public float mCurrentSecond{
        get { return mAudio.time; }
        set { mAudio.time = value; }
    }
	void Awake () {
        mAudio = gameObject.AddComponent<AudioSource>();
	}
    //音声ファイル読み込み
    public void loadMusic(){
        
    }
    //音声を設定
    public void setAudio(AudioClip aClip){
        mAudio.clip = aClip;
    }
    public void play(){
        mAudio.Play();
    }
    public void pause(){
        mAudio.Pause();
    }
    public void playDelayed(float aDelay){
        mAudio.PlayDelayed(aDelay);
    }
}
