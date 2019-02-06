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
    //曲開始時のbpm
    static public float mInitialBpm{
        get { return mBpm[0].get<float>("bpm"); }
    }
    //X / 4 拍子
    static public int mRhythm{
        get { return mMusicDate.get<int>("rhythm"); }
    }
    //小節の先頭から曲を再生するまでの時間(s)
    static public float mMargin{
        get { return mMusicDate.get<float>("margin"); }
    }
    //音声を再生開始するquarterBeat
    static public KeyTime mStartPlayMusicTime{
        get { return new KeyTime(KeyTime.secondsToQuarterBeat(mMargin, mInitialBpm)); }
    }
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
        foreach (Arg tLyricsData in mMusicDate.get<List<Arg>>("lyrics")){
            tLyricsData.set("keyTime", new KeyTime(tLyricsData.get<float>("time")));
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
    //指定した小節に含まれる歌詞のデータを返す
    static public List<Arg> getLyricsInBar(KeyTime aTime){
        List<Arg> tLyricsList = new List<Arg>();
        int tTopTime = (int)aTime.mTopQuarterBeatInBar;
        int tTailTime = (int)aTime.mTailQuarterBeatInBar;
        foreach (Arg tLyrics in mMusicDate.get<List<Arg>>("lyrics")){
            float tLyricsTime = tLyrics.get<float>("time");
            if (tLyricsTime < tTopTime) continue;
            if (tTailTime < tLyricsTime) break;
            tLyricsList.Add(tLyrics);
        }
        return tLyricsList;
    }
    //指定した小節に含まれるbpm変化情報を返す
    static public List<Arg> getChangeBpmInBar(KeyTime aTime){
        List<Arg> tBpms = new List<Arg>();
        int tTopTime = (int)aTime.mTopQuarterBeatInBar;
        int tTailTime = (int)aTime.mTailQuarterBeatInBar;
        List<Arg> tBpmList = mBpm;
        float tPreBpm = tBpmList[0].get<float>("bpm");
        foreach(Arg tBpm in tBpmList){
            float tBpmTime = tBpm.get<float>("time");
            tBpm.set("change", ChangeBpmObject.seekChange(tPreBpm, tBpm.get<float>("bpm")));//bpmの変化方向を記録
            tPreBpm = tBpm.get<float>("bpm");
            if (tBpmTime < tTopTime) continue;
            if (tTailTime < tBpmTime) break;
            tBpms.Add(tBpm);
        }
        return tBpms;
    }
    //曲の再生位置をQNに変換
    static public float musicTimeToQuarterBeat(float aMusicTime){
        List<Arg> tBpms = mBpm;
        int tLength = tBpms.Count;
        float tCurrentScoreSconds = mMargin + aMusicTime;
        float tTotalSeconds = 0;
        int i;
        for (i = 0; true;i++){
            if (i + 1 == tLength) break;

            float tSeconds = KeyTime.quarterBeatToSeconds(tBpms[i + 1].get<float>("time") - tBpms[i].get<float>("time"), tBpms[i].get<float>("bpm"));
            if (tCurrentScoreSconds <= tTotalSeconds + tSeconds) break;

            tTotalSeconds += tSeconds;
        }
        return tBpms[i].get<float>("time")
                       + KeyTime.secondsToQuarterBeat(tCurrentScoreSconds - tTotalSeconds, tBpms[i].get<float>("bpm"));
    }
    //QNを曲の再生位置に変換
    static public float quarterBeatToMusicTime(float aQuarterBeat){
        if (aQuarterBeat < 0)
            return KeyTime.quarterBeatToSeconds(aQuarterBeat, mBpm[0].get<float>("bpm")) - mMusicDate.get<float>("margin");
        
        List<Arg> tBpms = mBpm;
        int tLength = tBpms.Count;
        float tTotalSeconds = 0;
        int i;
        for (i = 0; true; i++){
            if (i + 1 == tLength) break;
            if (aQuarterBeat <= mBpm[i + 1].get<float>("time")) break;

            float tSeconds = KeyTime.quarterBeatToSeconds(tBpms[i + 1].get<float>("time") - tBpms[i].get<float>("time"), tBpms[i].get<float>("bpm"));
            tTotalSeconds += tSeconds;
        }
        return tTotalSeconds
                    + KeyTime.quarterBeatToSeconds(aQuarterBeat - tBpms[i].get<float>("time"), tBpms[i].get<float>("bpm"))
                    - mMusicDate.get<float>("margin");
    }
}