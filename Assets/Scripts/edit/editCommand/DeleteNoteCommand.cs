using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteNoteCommand : MyCommand{
    private KeyTime mTime;
    private Arg mNoteData;
    private Arg mLyricsData;
    public DeleteNoteCommand(KeyTime aTime){
        mTime = aTime;
    }
    public override void run(){
        mNoteData = MusicScoreData.deleteNote(mTime.mQuarterBeat);
        mLyricsData = MusicScoreData.deleteLyrics(mTime.mQuarterBeat);
    }
    public override void undo(){
        if (mNoteData != null)
            MusicScoreData.addNote(mNoteData);
        if (mLyricsData != null)
            MusicScoreData.addLyrics(mLyricsData);
    }
}