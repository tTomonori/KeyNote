using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ScoreHandler : MyBehaviour{
    private KeyNotePlayer mPlayer;
    private MusicScore mScore;
    private ScoreHandleState mState;
    private void Awake(){
        name = "scoreHandler";
        mState = new InitialState(this);
        Subject.addObserver(new Observer("scoreHandler", (message) =>{
            mState.getMessage(message);
        }));
    }
    private void OnDestroy(){
        Subject.removeObserver("scoreHandler");
    }
    public void load(string aFileName,ScoreDifficult aDifficult){
        //譜面
        mScore = MyBehaviour.create<MusicScore>();
        //曲情報ロード
        MusicScoreData.load(aFileName);
        MusicScoreData.mSelectedDifficult = aDifficult;
        //ミュージックプレイヤー
        MusicPlayer tPlayer = MyBehaviour.create<MusicPlayer>();
        tPlayer.setAudio(DataFolder.loadMusic(MusicScoreData.mMusicFileName));
        //譜面と曲を同期させるシステム
        mPlayer = new KeyNotePlayer(mScore,tPlayer);
    }
    public void set(MusicScoreFileData aData,ScoreDifficult aDifficult){
        //譜面
        mScore = MyBehaviour.create<MusicScore>();
        //曲情報ロード
        MusicScoreData.set(aData);
        MusicScoreData.mSelectedDifficult = aDifficult;
        //ミュージックプレイヤー
        MusicPlayer tPlayer = MyBehaviour.create<MusicPlayer>();
        tPlayer.setAudio(DataFolder.loadMusic(MusicScoreData.mMusicFileName));
        //譜面と曲を同期させるシステム
        mPlayer = new KeyNotePlayer(mScore, tPlayer);
    }
    //譜面の位置
    public void show(KeyTime aTime){
        mScore.show(aTime);
    }
    //状態遷移
    public void changeState(ScoreHandleState aState){
        if (mState != null) mState.exit();
        mState = aState;
        mState.enter();
    }
    private void Update(){
        mState.update();
    }
}
