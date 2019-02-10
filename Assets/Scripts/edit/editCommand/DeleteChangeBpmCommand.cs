using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteChangeBpmCommand : MyCommand{
    private KeyTime mTime;
    private Arg mData;
    public DeleteChangeBpmCommand(KeyTime aTime){
        mTime = aTime;
    }
    public override void run(){
        mData = MusicScoreData.deleteChangeBpm(mTime.mQuarterBeat);
    }
    public override void undo(){
        if (mData != null)
            MusicScoreData.addChangeBpm(mData);
    }
}