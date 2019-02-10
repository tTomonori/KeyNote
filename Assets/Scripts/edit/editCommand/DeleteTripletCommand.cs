using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTripletCommand : MyCommand{
    private KeyTime mTime;
    private Arg[] mBpmData;
    private Arg[] mLyricsData;
    private Arg[] mNoteData;
    public DeleteTripletCommand(KeyTime aTime){
        mTime = new KeyTime(aTime.mTopQuarterBeatInBeat);
        mBpmData = new Arg[3];
        mLyricsData = new Arg[3];
        mNoteData = new Arg[3];
    }
    public override void run(){
        //もともとあるデータを削除して記憶
        float[] tTripletTimes = new float[3] { mTime.mQuarterBeat + 0.1f, mTime.mQuarterBeat + 1.3f, mTime.mQuarterBeat + 2.6f };
        for (int i = 0; i < 3;i++){
            mBpmData[i] = MusicScoreData.deleteChangeBpm(tTripletTimes[i]);
            mLyricsData[i] = MusicScoreData.deleteLyrics(tTripletTimes[i]);
            mNoteData[i]= MusicScoreData.deleteNote(tTripletTimes[i]);
        }
        //生成し直し
        float[] tNewTime = new float[3] { mTime.mQuarterBeat + 0, mTime.mQuarterBeat + 1, mTime.mCorrectQuarterBeat + 3 };
        for (int i = 0; i < 3; i++){
            if(mBpmData[i]!=null){
                mBpmData[i].set("time", tNewTime[i]);
                MusicScoreData.addChangeBpm(mBpmData[i]);
            }
            if (mLyricsData[i] != null){
                mLyricsData[i].set("time", tNewTime[i]);
                MusicScoreData.addLyrics(mLyricsData[i]);
            }
            if (mNoteData[i] != null){
                mNoteData[i].set("time", tNewTime[i]);
                MusicScoreData.addNote(mNoteData[i]);
            }
        }
    }
    public override void undo(){
        //もともとあるデータを削除
        for (int i = 0; i < 4; i++){
             MusicScoreData.deleteChangeBpm(mTime.mQuarterBeat + i);
            MusicScoreData.deleteLyrics(mTime.mQuarterBeat + i);
            MusicScoreData.deleteNote(mTime.mQuarterBeat + i);
        }
        //生成し直し
        float[] tNewTime = new float[3] { mTime.mQuarterBeat + 0.1f, mTime.mQuarterBeat + 1.3f, mTime.mQuarterBeat + 2.6f };
        for (int i = 0; i < 3; i++){
            if (mBpmData[i] != null){
                mBpmData[i].set("time", tNewTime[i]);
                MusicScoreData.addChangeBpm(mBpmData[i]);
            }
            if (mLyricsData[i] != null){
                mLyricsData[i].set("time", tNewTime[i]);
                MusicScoreData.addLyrics(mLyricsData[i]);
            }
            if (mNoteData[i] != null){
                mNoteData[i].set("time", tNewTime[i]);
                MusicScoreData.addNote(mNoteData[i]);
            }
        }
    }
}