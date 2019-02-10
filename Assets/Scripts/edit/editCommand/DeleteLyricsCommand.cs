using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteLyricsCommand : MyCommand{
    private KeyTime mTime;
    private Arg mLyricsData;
    public DeleteLyricsCommand(KeyTime aTime){
        mTime = aTime;
    }
    public override void run(){
        mLyricsData = MusicScoreData.deleteLyrics(mTime.mQuarterBeat);
    }
    public override void undo(){
        if (mLyricsData != null)
            MusicScoreData.addLyrics(mLyricsData);
    }
}