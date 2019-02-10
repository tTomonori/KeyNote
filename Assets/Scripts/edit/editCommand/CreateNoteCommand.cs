using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNoteCommand : MyCommand{
    private KeyTime mTime;
    private bool mSuccesAddLyrics;
    public CreateNoteCommand(KeyTime aTime){
        mTime = aTime;
    }
    public override void run(){
        MusicScoreData.addNote(new Arg(new Dictionary<string, object>(){
            {"type","note"},
            {"time",mTime.mQuarterBeat},
            {"consonant"," "},
            {"vowel","a"},
        }));
        mSuccesAddLyrics = MusicScoreData.addLyrics(new Arg(new Dictionary<string, object>(){
            {"char" , " "},
            {"time" , mTime.mQuarterBeat}
        }));
    }
    public override void undo(){
        MusicScoreData.deleteNote(mTime.mQuarterBeat);
        if (mSuccesAddLyrics)
            MusicScoreData.deleteLyrics(mTime.mQuarterBeat);
    }
}