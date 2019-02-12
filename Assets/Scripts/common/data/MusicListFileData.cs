using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicListFileData {
    private Arg mData;
    public MusicListFileData(Arg aArg){
        mData = aArg;
    }
    //最後にプレイした曲のリスト番号
    public int lastPlayIndex{
        get { return mData.get<int>("lastPlayIndex"); }
        set { mData.set("lastPlayIndex", value); }
    }
    //最後にプレイした難易度
    public ScoreDifficult lastPlayDifficult{
        get { return EnumParser.parse<ScoreDifficult>(mData.get<string>("lastPlayDifficult")); }
        set { mData.set("lastPlayDifficult", value.ToString()); }
    }
    //曲リスト
    public List<Arg> list{
        get { return mData.get<List<Arg>>("list"); }
    }
    //曲の数
    public int length{
        get { return list.Count; }
    }
    //指定したindexの曲データ取得
    public MusicListElement getData(int aIndex){
        return new MusicListElement(mData.get<List<Arg>>("list")[aIndex]);
    }
    //指定したファイルを参照する曲のデータ取得
    private Arg getData(string aFile){
        List<Arg> tList = list;
        for (int i = 0; i < tList.Count; i++){
            Arg tData = tList[i];
            if (tData.get<string>("file") == aFile)
                return tData;
        }
        return null;
    }
    //リストに楽曲追加
    public void addScore(string aTitle,string aFile){
        list.Add(new Arg(new Dictionary<string, object>(){
            {"title",aTitle},
            {"file",aFile},
            {"point",new Arg(new Dictionary<string,object>(){{"child", 0},{ "student", 0 },{ "scholar", 0 },{ "guru", 0 }})}
        }));
    }
    //リストから楽曲削除
    public void remove(string aFile){
        List<Arg> tList = list;
        for (int i = 0; i < tList.Count;i++){
            Arg tData = tList[i];
            if (tData.get<string>("file") != aFile) continue;
            tList.RemoveAt(i);
            return;
        }
    }
    //リストの楽曲データを更新
    public void update(string aFile,string aNewTitle,string aNewFile){
        List<Arg> tList = list;
        for (int i = 0; i < tList.Count; i++){
            Arg tData = tList[i];
            if (tData.get<string>("file") != aFile) continue;
            tList[i] = new Arg(new Dictionary<string, object>(){
                {"title",aNewTitle},
                {"file",aNewFile},
                {"point",new Arg(new Dictionary<string,object>(){{"child", 0},{ "student", 0 },{ "scholar", 0 },{ "guru", 0 }})}
            });
        }
    }
    //ハイスコア更新
    public void updatePoint(string aFile,ScoreDifficult aDifficult,float aPoint){
        Arg tPoint = getData(aFile).get<Arg>("point");
        if(aPoint>tPoint.get<float>(aDifficult.ToString())){
            tPoint.set(aDifficult.ToString(), aPoint);
        }
    }
    //jsonファイルに保存する
    public void save(){
        DataFolder.writeListData(mData);
    }

    public class MusicListElement{
        private Arg mData;
        public string title{
            get { return mData.get<string>("title"); }
        }
        public string file{
            get { return mData.get<string>("file"); }
        }
        public float getPoint(ScoreDifficult aDifficult){
            return mData.get<Arg>("point").get<float>(aDifficult.ToString());
        }
        public MusicListElement(Arg aArg){
            mData = aArg;
        }
    }
}
