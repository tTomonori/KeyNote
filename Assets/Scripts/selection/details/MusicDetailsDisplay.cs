using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class MusicDetailsDisplay : MyBehaviour {
    private MusicScoreFileData mMusicData;
    private ToggleButtonGroup mButtons{
        get { return findChild<ToggleButtonGroup>("difficultButtons"); }
    }
    //選択中の曲の譜面データファイル
    public string mSelectedMusicFileName;
    //選択中の難易度
    public string mDifficult{
        get { return mButtons.pushedButtonName; }
    }
	void Awake () {
        Subject.addObserver(new Observer("details",(message) => {
            if (message.isMemberOf("difficultButton")){
                if (mMusicData == null) return;
                changeDifficult(message.name);
                return;
            }
            if(message.name=="initialDifficult"){
                mButtons.memberPushed(message.getParameter<string>("difficult"));
            }
        }));
	}
    public void initDetails(){
        mMusicData = null;
        //曲名
        GameObject.Find("title").GetComponent<TextMesh>().text = "";
        //難易度
        gameObject.GetComponentInChildren<DifficultDisplay>().set(0);
        //最高得点
        gameObject.GetComponentInChildren<ScoreDisplay>().set(0);
        //音声
        AudioSource tAudio = GetComponentInChildren<AudioSource>();
        tAudio.clip = null;
        //サムネイル
        findChild("thumbnail").GetComponent<SpriteRenderer>().sprite = null;
    }
    public void showMusic(string aFileName){
        mSelectedMusicFileName = aFileName;
        mMusicData = DataFolder.loadScoreData(aFileName);
        //曲名
        GameObject.Find("title").GetComponent<TextMesh>().text = mMusicData.title;
        //選択中の難易度に合わせて表示更新
        changeDifficult(mDifficult);
        //音声
        AudioSource tAudio = GetComponentInChildren<AudioSource>();
        tAudio.clip = DataFolder.loadMusic(mMusicData.music);
        tAudio.Play();
        //サムネイル
        DataFolder.loadThumbnailAsync(mMusicData.thumbnail, (aSprite) =>{
            SpriteRenderer tRenderer = findChild("thumbnail").GetComponent<SpriteRenderer>();
            tRenderer.transform.localScale = new Vector3(7 / aSprite.bounds.size.x, 7 / aSprite.bounds.size.x, 1);
            tRenderer.sprite = aSprite;
        });
    }
    public void changeDifficult(string aDifficult){
        //難易度
        gameObject.GetComponentInChildren<DifficultDisplay>().set(mMusicData.getDifficult(aDifficult));
        //最高得点
        gameObject.GetComponentInChildren<ScoreDisplay>().set(mMusicData.getPoint(aDifficult));
    }
    private void OnDestroy(){
        Subject.removeObserver("details");
    }
}
