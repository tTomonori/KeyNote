using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ScoreHandler{
    private KeyNotePlayer mPlayer;
    private MusicScore mScore;
    private ScoreHandleState mState;
    public ScoreHandler(string aFileName,string aDifficult){
        //譜面
        mScore = MyBehaviour.create<MusicScore>();
        //曲情報ロード
        MusicScoreData.load(aFileName);
        MusicScoreData.mDifficult = EnumParser.parse<MusicScoreData.Difficult>(aDifficult);
        //ミュージックプレイヤー
        MusicPlayer tPlayer = MyBehaviour.create<MusicPlayer>();
        //譜面と曲を同期させるシステム
        mPlayer = new KeyNotePlayer(mScore,tPlayer);

    }
    //状態遷移
    public void changeState(ScoreHandleState aState){
        if (mState != null) mState.exit();
        mState = aState;
        mState.enter();
    }
}
