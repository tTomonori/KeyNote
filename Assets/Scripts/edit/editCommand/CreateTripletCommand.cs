using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTripletCommand : MyCommand{
    private KeyTime mTime;
    private Arg[] mBpmData;
    private Arg[] mLyricsData;
    private Arg[] mNoteData;
    public CreateTripletCommand(KeyTime aTime){
        mTime = new KeyTime(aTime.mTopQuarterBeatInBeat);
        mBpmData = new Arg[4];
        mLyricsData = new Arg[4];
        mNoteData = new Arg[4];
    }
    public override void run(){
        //もともとあるデータを削除して記憶
        for (int i = 0; i < 4;i++){
            mBpmData[i] = MusicScoreData.deleteChangeBpm(mTime.mQuarterBeat + i);
            mLyricsData[i] = MusicScoreData.deleteLyrics(mTime.mQuarterBeat + i);
            mNoteData[i] = MusicScoreData.deleteNote(mTime.mQuarterBeat + i);
        }
        //生成し直し
        float[] tNewTime = new float[4] { mTime.mQuarterBeat + 0.1f, mTime.mQuarterBeat + 1.3f, 0, mTime.mQuarterBeat + 2.6f };
        for (int i = 0; i < 4; i++){
            if (i == 2) continue;
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
        foreach(float tTime in new float[3]{mTime.mQuarterBeat+0.1f,mTime.mQuarterBeat+1.3f,mTime.mQuarterBeat+2.6f}){
            MusicScoreData.deleteChangeBpm(tTime);
            MusicScoreData.deleteLyrics(tTime);
            MusicScoreData.deleteNote(tTime);
        }
        //生成し直し
        for (int i = 0; i < 4; i++){
            if (mBpmData[i] != null){
                mBpmData[i].set("time", mTime.mQuarterBeat + i);
                MusicScoreData.addChangeBpm(mBpmData[i]);
            }
            if (mLyricsData[i] != null){
                mLyricsData[i].set("time", mTime.mQuarterBeat + i);
                MusicScoreData.addLyrics(mLyricsData[i]);
            }
            if (mNoteData[i] != null){
                mNoteData[i].set("time", mTime.mQuarterBeat + i);
                MusicScoreData.addNote(mNoteData[i]);
            }
        }
    }
}