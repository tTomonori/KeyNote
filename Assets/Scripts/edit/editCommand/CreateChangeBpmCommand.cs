using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateChangeBpmCommand : MyCommand{
    private KeyTime mTime;
    private float mBpm;
    private Arg mPreData;
    public CreateChangeBpmCommand(KeyTime aTime,float aBpm){
        mTime = aTime;
        mBpm = aBpm;
    }
    public override void run(){
        mPreData = MusicScoreData.deleteChangeBpm(mTime.mQuarterBeat);
        MusicScoreData.addChangeBpm(new Arg(new Dictionary<string, object>(){
            {"bpm",mBpm},
            {"time",mTime.mQuarterBeat}
        }));
    }
    public override void undo(){
        MusicScoreData.deleteChangeBpm(mTime.mQuarterBeat);
        if (mPreData != null)
            MusicScoreData.addChangeBpm(mPreData);
    }
}