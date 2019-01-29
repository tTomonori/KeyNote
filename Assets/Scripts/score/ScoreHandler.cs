using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ScoreHandler{
    private KeyNotPlayer mPlayer;
    private MusicScore mScore;
    private ScoreHandleState mState;
    public ScoreHandler(string aFileName,string aDifficult){
        mScore = MyBehaviour.create<MusicScore>();
        MusicScoreData.load(aFileName);
        MusicScoreData.mDifficult = EnumParser.parse<MusicScoreData.Difficult>(aDifficult);
    }
    //状態遷移
    public void changeState(ScoreHandleState aState){
        if (mState != null) mState.exit();
        mState = aState;
        mState.enter();
    }
}
