using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler{
    private KeyNotPlayer mPlayer;
    private MusicScore mScore;
    public ScoreHandler(string aFileName){
        mScore = MyBehaviour.create<MusicScore>();
        mScore.load(aFileName);
    }
}
