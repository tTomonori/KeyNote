using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class MusicScoreData {
    //曲のデータ
    static public Arg mMusicDate;
    //X / 4 拍子
    static public int mRhythm{
        get { return mMusicDate.get<int>("rhythm"); }
    }
    //難易度
    static public Difficult mDifficult;
    ///曲データロード
    static public void load(string aFileName){
        mMusicDate = new Arg(MyJson.deserializeFile(Application.dataPath + "/../data/score/" + aFileName + ".json"));
        //keyTimeを生成して記録
        foreach(Arg tNoteData in mMusicDate.get<List<Arg>>("note")){
            tNoteData.set("keyTime", new KeyTime(tNoteData.get<float>("time")));
        }
    }
    //指定した小節に含まれる音符のデータを返す
    static public List<Arg> getNotesInBar(KeyTime aTime){
        List<Arg> tNotes = new List<Arg>();
        int tTopTime = (int)aTime.mTopQuarterBeatInBar;
        int tTailTime = (int)aTime.mTailQuarterBeatInBar;
        foreach (Arg tNote in mMusicDate.get<List<Arg>>("note")){
            float tNoteTime = tNote.get<float>("time");
            if (tNoteTime < tTopTime) continue;
            if (tTailTime < tNoteTime) break;
            tNotes.Add(tNote);
        }
        return tNotes;
    }
    public enum Difficult{
        child,student,scholar,guru
    }
}
