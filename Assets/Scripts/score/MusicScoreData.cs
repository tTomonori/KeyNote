using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public partial class MusicScoreData {
    //曲のデータ
    static private MusicScoreFileData mMusicDate;
    //曲名
    static public string mTitle{
        get { return mMusicDate.title; }
        set { mMusicDate.title = value; }
    }
    //音声ファイル名
    static public string mMusicFileName{
        get { return mMusicDate.music; }
        set { mMusicDate.music = value; }
    }
    //ロード元のファイル名
    static public string mOriginalFileName{
        get { return mMusicDate.originalFileName; }
    }
    //保存先ファイル名
    static public string mSaveFileName{
        get { return mMusicDate.fileName; }
        set { mMusicDate.fileName = value; }
    }
    //既にデータファイルが生成されている
    static public bool isSaved{
        get { return mMusicDate.isSaved; }
    }
    //サムネイル
    static public string mThumbnail{
        get { return mMusicDate.thumbnail; }
        set { mMusicDate.thumbnail = value; }
    }
    //背景
    static public string mBack{
        get { return mMusicDate.back; }
        set { mMusicDate.back = value; }
    }
    //動画
    static public string mMovie{
        get { return mMusicDate.movie; }
        set { mMusicDate.movie = value; }
    }
    //bpm
    static public List<Arg> mBpm{
        get { return mMusicDate.bpm; }
    }
    //曲開始時のbpm
    static public float mInitialBpm{
        get { return mBpm[0].get<float>("bpm"); }
    }
    //X / 4 拍子
    static public int mRhythm{
        get { return mMusicDate.rhythm; }
        set { mMusicDate.rhythm = value; }
    }
    //小節の先頭から曲を再生するまでの時間(s)
    static public float mMargin{
        get { return mMusicDate.margin; }
        set { mMusicDate.margin = value; }
    }
    //サビの開始位置(second)
    static public float mRust{
        get { return mMusicDate.rust; }
        set { mMusicDate.rust = value; }
    }
    //全ての歌詞(編集フォームに入力した文章)
    static public string mAllLyrics{
        get { return mMusicDate.allLyrics; }
        set { mMusicDate.allLyrics = value; }
    }
    //音声を再生開始するquarterBeat
    static public KeyTime mStartPlayMusicTime{
        get { return new KeyTime(KeyTime.secondsToQuarterBeat(mMargin, mInitialBpm)); }
    }
    //全ての音符
    static public List<Arg> mNotes{
        get { return mMusicDate.note; }
    }
    //難易度
    static public void setDifficult(ScoreDifficult aDifficult,int aLevel){
        mMusicDate.setDifficult(aDifficult, aLevel);
    }
    //選択した難易度
    static public ScoreDifficult mSelectedDifficult;
    //曲データロード
    static public void load(string aFileName){
        mMusicDate = DataFolder.loadScoreData(aFileName);
        setKeyTimeData();
    }
    //曲データセット
    static public void set(MusicScoreFileData aData){
        mMusicDate = aData;
        setKeyTimeData();
    }
    //keyTimeを生成して記録
    static public void setKeyTimeData(){
        foreach (Arg tNoteData in mMusicDate.note){
            tNoteData.set("keyTime", new KeyTime(tNoteData.get<float>("time")));
        }
        foreach (Arg tLyricsData in mMusicDate.lyrics){
            tLyricsData.set("keyTime", new KeyTime(tLyricsData.get<float>("time")));
        }
    }
    //指定した小節に含まれる音符のデータを返す
    static public List<Arg> getNotesInBar(KeyTime aTime){
        List<Arg> tNotes = new List<Arg>();
        int tTopTime = (int)aTime.mTopQuarterBeatInBar;
        int tTailTime = (int)aTime.mTailQuarterBeatInBar;
        foreach (Arg tNote in mMusicDate.note){
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
        foreach (Arg tLyrics in mMusicDate.lyrics){
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
            return KeyTime.quarterBeatToSeconds(aQuarterBeat, mInitialBpm) - mMusicDate.margin;
        
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
                             - mMusicDate.margin;
    }
}