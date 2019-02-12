using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

static public class MusicList {
    //曲リストデータ
    static private MusicListFileData mData;
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
}
