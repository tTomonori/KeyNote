using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

static public class MusicList {
    //曲リストデータ
    static private MusicListFileData mData;
    //曲リスト
    static public List<Arg> mMusicList{
        get { return mData.list; }
    }
    //最後にプレイした曲のindex
    static public int mLastPlayIndex{
        get { return mData.lastPlayIndex; }
    }
    //リストのながさ
    static public int mLength{
        get { return mData.length; }
    }
    //最後にプレイした難易度
    static public ScoreDifficult mLastPlayDifficult{
        get { return mData.lastPlayDifficult; }
    }
    //最後に遊んだ情報を記録
    static public void updateLastPlay(int aIndex,ScoreDifficult aDifficult){
        mData.lastPlayIndex = aIndex;
        mData.lastPlayDifficult = aDifficult;
        mData.save();
    }
    static MusicList(){
        mData = DataFolder.loadListData();
    }
    //指定したindexの曲データ取得
    static public MusicListFileData.MusicListElement get(int aNum){
        return mData.getData(toCorrectIndex(aNum));
    }
    //範囲外のindexを変換
    static public int toCorrectIndex(int aIndex){
        int tNum = aIndex % mData.length;
        if (tNum < 0) tNum += mData.length;
        return tNum;
    }
    //ハイスコア更新
    static public bool updatePoint(string aFile,ScoreDifficult aDifficult,float aPoint){
        bool tUpdate = mData.updatePoint(aFile, aDifficult, aPoint);
        if(tUpdate){
            mData.save();
            return true;
        }else{
            return false;
        }
    }
    //リストに楽曲追加
    static public void addScore(string aTitle,string aFile){
        mData.addScore(aTitle, aFile);
        mData.save();
    }
    //リストから楽曲削除
    static public void remove(string aFile){
        mData.remove(aFile);
        mData.save();
    }
    //リストの楽曲データを更新
    static public void update(string aFile,string aNewTitle,string aNewFile){
        mData.update(aFile, aNewTitle, aNewFile);
        mData.save();
    }
    //リストを全更新
    static public void updateList(List<Arg> aList){
        mData.list = aList;
        mData.save();
    }


    //未完成の楽曲データファイル名一覧を取得
    static public List<string> getUnfinishedScoreFileNameList(){
        List<string> tList = new List<string>();
        foreach(string tFileName in DataFolder.getScoreFileNameList()){
            //楽曲リストに載っている(完成している)譜面は除く
            if (isContainsByList(tFileName)) continue;
            tList.Add(tFileName);
        }
        return tList;
    }
    //楽曲リストに含まれる
    static public bool isContainsByList(string aFileName){
        foreach (Arg tScoreData in mData.list){
            if (tScoreData.get<string>("file") == aFileName)
                return true;
        }
        return false;
    }
}
