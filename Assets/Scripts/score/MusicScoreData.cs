using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class MusicScoreData {
    //曲のデータ
    static private Arg mMusicDate;
    //曲名
    static public string mTitle{
        get { return mMusicDate.get<string>("title"); }
    }
    //音声ファイル名
    static public string mMusicFileName{
        get { return mMusicDate.get<string>("music"); }
    }
    //bpm
    static public List<Arg> mBpm{
        get { return mMusicDate.get<List<Arg>>("bpm"); }
    }
    //X / 4 拍子
    static public int mRhythm{
        get { return mMusicDate.get<int>("rhythm"); }
    }
    //音声を再生開始するquarterBeat
    static public KeyTime mStartPlayMusicTime;
    //難易度
    static public Difficult mDifficult;
    public enum Difficult{child, student, scholar, guru}
    ///曲データロード
    static public void load(string aFileName){
        mMusicDate = new Arg(MyJson.deserializeFile(DataFolder.path + "/score/" + aFileName + ".json"));
        //keyTimeを生成して記録
        foreach(Arg tNoteData in mMusicDate.get<List<Arg>>("note")){
            tNoteData.set("keyTime", new KeyTime(tNoteData.get<float>("time")));
        }
        //音声を再生開始するquarterBeat
        mStartPlayMusicTime = new KeyTime(KeyTime.secondsToQuarterBeat(mMusicDate.get<float>("margin"), mMusicDate.get<List<Arg>>("bpm")[0].get<float>("bpm")));
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
    //曲の再生位置をQNに変換
    static public float musicTimeToQuarterBeat(float aMusicTime){
        List<Arg> tBpms = mBpm;
        int tLength = tBpms.Count;
        float tTotalSeconds = 0;
        int i;
        for (i = 0; true;i++){
            if (i + 1 == tLength) break;

            float tSeconds = KeyTime.quarterBeatToSeconds(tBpms[i + 1].get<float>("time") - tBpms[i].get<float>("time"), tBpms[i].get<float>("bpm"));
            if (aMusicTime <= tTotalSeconds + tSeconds) break;

            tTotalSeconds += tSeconds;
        }
        return tBpms[i].get<float>("time")
                       + KeyTime.secondsToQuarterBeat(aMusicTime - tTotalSeconds, tBpms[i].get<float>("bpm"))
                       + mStartPlayMusicTime.mQuarterBeat;
    }
}
