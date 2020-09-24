using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class MusicDetailsDisplay : MyBehaviour {
    //選択中の曲の譜面データ
    private MusicScoreFileData mMusicData;
    //選択中の曲のデータ
    public MusicListFileData.MusicListElement mSelectedMusic;
    //タイトルのテキストオブジェクト
    [SerializeField] private TextMesh mTitleText;
    //難易度表示領域
    [SerializeField] private DifficultDisplay mDifficultStars;
    //サムネイル表示領域
    [SerializeField] private SpriteRenderer mThumbnailRenderer;
    //HightScore表示領域
    [SerializeField] private ScoreDisplay mScoreDisplay;
    //音声再生player
    [SerializeField] private AudioSource mAudioSource;
    //難易度選択ボタンのグループ
    [SerializeField] private ToggleButtonGroup mDifficultToggle;
    //曲の再生位置選択ボタン
    [SerializeField] private MyListButton mPlayPositionListButton;
    //選択中の難易度
    public ScoreDifficult mSelectedDifficult{
        get { return EnumParser.parse<ScoreDifficult>(mDifficultToggle.pushedButtonName); }
    }
	void Awake () {
        Subject.addObserver(new Observer("details",(message) => {
            //難易度変更
            if (message.isMemberOf("difficultButton")){
                if (mMusicData == null) return;
                changeDifficult(EnumParser.parse<ScoreDifficult>(message.name));
                return;
            }
            //音声再生位置変更
            if(message.name=="playPositionListButtonPushed"){
                playMusic();
                return;
            }
            //難易度初期設定
            if(message.name=="initialDifficult"){
                mDifficultToggle.memberPushed(message.getParameter<ScoreDifficult>("difficult").ToString());
                return;
            }
        }));
	}
    //表示を初期化
    public void initDetails(){
        mMusicData = null;
        //曲名
        mTitleText.text = "";
        //難易度
        mDifficultStars.set(0);
        //最高得点
        mScoreDisplay.set(0);
        //音声
        mAudioSource.clip = null;
        //サムネイル
        mThumbnailRenderer.sprite = null;
    }
    //楽曲の情報を表示
    public void showMusic(MusicListFileData.MusicListElement aData){
        mSelectedMusic = aData;
        mMusicData = DataFolder.loadScoreData(aData.file);
        //曲名
        mTitleText.text = mMusicData.title;
        //選択中の難易度に合わせて表示更新
        changeDifficult(mSelectedDifficult);
        //音声
        playMusic();
        //サムネイル
        DataFolder.loadThumbnailAsync(mMusicData.thumbnail, (aSprite) =>{
            Sprite tSprite = SpriteCutter.setRatio(aSprite, 7, 6);
            mThumbnailRenderer.transform.localScale = new Vector3(7 / tSprite.bounds.size.x, 7 / tSprite.bounds.size.x, 1);
            mThumbnailRenderer.sprite = tSprite;
        });
    }
    //指定した難易度に合わせて表示更新
    private void changeDifficult(ScoreDifficult aDifficult){
        //難易度
        gameObject.GetComponentInChildren<DifficultDisplay>().set(mMusicData.getDifficult(aDifficult));
        //最高得点
        gameObject.GetComponentInChildren<ScoreDisplay>().set(mSelectedMusic.getPoint(aDifficult));
    }
    //曲の再生
    private void playMusic(){
        mAudioSource.Stop();
        //音声ファイルをロード
        mAudioSource.clip = DataFolder.loadMusic(mMusicData.music);
        //再生位置設定
        switch(mPlayPositionListButton.mSelected){
            case "サビ":
                mAudioSource.time = mMusicData.rust;
                break;
            case "イントロ":
                mAudioSource.time = 0;
                break;
            default:
                mAudioSource.time = 0;
                break;
        }
        //再生
        mAudioSource.Play();
    }
    private void OnDestroy(){
        Subject.removeObserver("details");
    }
}