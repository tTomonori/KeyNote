using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLyricsCommand : MyCommand{
    private KeyTime mTime;
    public CreateLyricsCommand(KeyTime aTime){
        mTime = aTime;
    }
    public override void run(){
        MusicScoreData.addLyrics(new Arg(new Dictionary<string, object>(){
            {"char" , " "},
            {"time" , mTime.mQuarterBeat}
        }));
    }
    public override void undo(){
        MusicScoreData.deleteLyrics(mTime.mQuarterBeat);
    }
}