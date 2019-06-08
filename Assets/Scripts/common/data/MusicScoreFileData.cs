using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScoreFileData {
    private Arg mData;
    public MusicScoreFileData(Arg aData,string aPath){
        mData = aData;
        mData.set("filePath", new ScoreFilePath(aPath, aPath));
    }
    //空の譜面データ作成
    public MusicScoreFileData(){
        mData = new Arg();
        mData.set("filePath", new ScoreFilePath("", ""));
        title = "";
        music = "";
        thumbnail = "";
        back = "";
        movie = "";
        margin = 0;
        rhythm = 4;
        rust = 0;
        difficult = new Arg(new Dictionary<string, object>() { { "child", 0 }, { "student", 0 }, { "scholar", 0 }, { "guru", 0 } });
        bpm = new List<Arg> { new Arg(new Dictionary<string, object>() { { "bpm", 100 }, { "time", 0 } }) };
        note = new List<Arg>();
        lyrics = new List<Arg>();
        allLyrics = "";
    }
    //既にデータファイルが生成されている
    public bool isSaved{
        get { return loadPath != ""; }
    }
    //タイトル
    public string title{
        get { return mData.get<string>("title"); }
        set { mData.set("title", value); }
    }
    //保存先ファイル名(拡張子抜き)
    public string savePath{
        get { return mData.get<ScoreFilePath>("filePath").savePath; }
        set { mData.get<ScoreFilePath>("filePath").setSavePath(value); }
    }
    //読み込み元のファイル名(拡張子抜き)
    public string loadPath{
        get { return mData.get<ScoreFilePath>("filePath").loadPath; }
        set { mData.get<ScoreFilePath>("filePath").setLoadPath(value); }
    }
    //音声ファイル
    public string music{
        get { return mData.get<string>("music"); }
        set { mData.set("music", value); }
    }
    //サムネイル
    public string thumbnail{
        get { return mData.get<string>("thumbnail"); }
        set { mData.set("thumbnail", value); }
    }
    //背景
    public string back{
        get { return mData.get<string>("back"); }
        set { mData.set("back", value); }
    }
    //動画
    public string movie{
        get { return mData.get<string>("movie"); }
        set { mData.set("movie", value); }
    }
    //margin
    public float margin{
        get { return mData.get<float>("margin"); }
        set { mData.set("margin", value); }
    }
    // X / 4 拍子
    public int rhythm{
        get { return mData.get<int>("rhythm"); }
        set { mData.set("rhythm", value); }
    }
    //サビの開位置
    public float rust{
        get { return mData.get<float>("rust"); }
        set { mData.set("rust", value); }
    }
    //難易度
    public Arg difficult{
        get { return mData.get<Arg>("difficult"); }
        set { mData.set("difficult", value); }
    }
    public int getDifficult(ScoreDifficult aDifficult){
        return mData.get<Arg>("difficult").get<int>(aDifficult.ToString());
    }
    public void setDifficult(ScoreDifficult aDifficult,int value){
        mData.get<Arg>("difficult").set(aDifficult.ToString(), value);
    }
    //bpm
    public List<Arg> bpm{
        get { return mData.get<List<Arg>>("bpm"); }
        set { mData.set("bpm", value); }
    }
    //音符
    public List<Arg> note{
        get { return mData.get<List<Arg>>("note"); }
        set { mData.set("note", value); }
    }
    //歌詞
    public List<Arg> lyrics{
        get { return mData.get<List<Arg>>("lyrics"); }
        set { mData.set("lyrics", value); }
    }
    //全ての歌詞
    public string allLyrics{
        get { return mData.get<string>("allLyrics"); }
        set { mData.set("allLyrics", value); }
    }

    private class ScoreFilePath{
        public string loadPath;
        public string savePath;
        public ScoreFilePath(string aLoadPath,string aSavePath){
            loadPath = aLoadPath;
            savePath = aSavePath;
        }
        public void setSavePath(string aPath){
            savePath = aPath;
        }
        public void setLoadPath(string aPath){
            loadPath = aPath;
        }
    }
    //保存する
    public void save(){
        //元のファイル削除
        DataFolder.removeScoreData(loadPath);
        //書き込み
        DataFolder.writeScoreData(mData, savePath);
        loadPath = savePath;
    }
}