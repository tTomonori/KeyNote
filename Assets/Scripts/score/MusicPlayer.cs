using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MyBehaviour {
    private AudioMixer mMixer;
    private AudioMixerGroup mMixerGroup;
    private AudioSource mAudio;
    //音声の現在位置
    public float mCurrentSecond{
        get { return mAudio.time; }
        set {
            if (value < 0)
                mAudio.time = 0;
            else
                mAudio.time = value; 
        }
    }
    //再生中か
    public bool mIsPlaying{
        get { return mAudio.isPlaying; }
    }
    //音声のながさ
    public float mLength{
        get { return mAudio.clip.length; }
    }
    //再生速度(音の高さはそのまま)
    public float mPitch{
        get { return mAudio.pitch; }
        set {
            if (mMixer == null){
                //再生速度調整用(これを設定すると音声の先頭に雑音が入るので、必要な時のみロードする)
                mMixer = Resources.Load<AudioMixer>("myMixer");
                mMixerGroup = mMixer.FindMatchingGroups("editorMixer")[0];
                mAudio.outputAudioMixerGroup = mMixerGroup;
            }
            mAudio.pitch = value;
            mMixer.SetFloat("shift", 1 / value);
        }
    }
    void Awake () {
        name = "musicPlayer";
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
        mAudio.Stop();
    }
    public void playDelayed(float aDelay){
        mAudio.PlayDelayed(aDelay);
    }
}