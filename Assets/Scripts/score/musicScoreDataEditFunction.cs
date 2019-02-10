using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public partial class MusicScoreData{
    //音符追加
    static public bool addNote(Arg aData){
        List<Arg> tList = mMusicDate.note;
        int tLength = tList.Count;
        float tTargetTime = aData.get<float>("time");
        int i;
        for (i = 0; i < tLength;i++){
            float tTime = tList[i].get<float>("time");
            if (tTargetTime < tList[i].get<float>("time")) break;
            if (tTargetTime == tTime) return false;//既に同じ位置に存在する
        }
        aData.set("keyTime", new KeyTime(aData.get<float>("time")));
        tList.Insert(i, aData);
        mMusicDate.note = tList;
        return true;
    }
    //音符削除
    static public Arg deleteNote(float aTime){
        List<Arg> tList = mMusicDate.note;
        int tLength = tList.Count;
        int i;
        for (i = 0; i < tLength; i++){
            if (aTime == tList[i].get<float>("time")){
                Arg tData = tList[i];
                tList.RemoveAt(i);
                mMusicDate.note = tList;
                return tData;
            }
        }
        return null;
    }
    //歌詞追加
    static public bool addLyrics(Arg aData){
        List<Arg> tList = mMusicDate.lyrics;
        int tLength = tList.Count;
        float tTargetTime = aData.get<float>("time");
        int i;
        for (i = 0; i < tLength; i++){
            float tTime = tList[i].get<float>("time");
            if (tTargetTime < tList[i].get<float>("time")) break;
            if (tTargetTime == tTime) return false;//既に同じ位置に存在する
        }
        aData.set("keyTime", new KeyTime(aData.get<float>("time")));
        tList.Insert(i, aData);
        mMusicDate.lyrics = tList;
        return true;
    }
    //音符削除
    static public Arg deleteLyrics(float aTime){
        List<Arg> tList = mMusicDate.lyrics;
        int tLength = tList.Count;
        int i;
        for (i = 0; i < tLength; i++){
            if (aTime == tList[i].get<float>("time")){
                Arg tData = tList[i];
                tList.RemoveAt(i);
                mMusicDate.lyrics = tList;
                return tData;
            }
        }
        return null;
    }
    //bpm変更イベント追加
    static public bool addChangeBpm(Arg aData){
        List<Arg> tList = mMusicDate.bpm;
        int tLength = tList.Count;
        float tTargetTime = aData.get<float>("time");
        int i;
        for (i = 0; i < tLength; i++){
            float tTime = tList[i].get<float>("time");
            if (tTargetTime < tList[i].get<float>("time")) break;
            if (tTargetTime == tTime) return false;//既に同じ位置に存在する
        }
        tList.Insert(i, aData);
        mMusicDate.bpm = tList;
        return true;
    }
    //bpm変更イベント削除
    static public Arg deleteChangeBpm(float aTime){
        List<Arg> tList = mMusicDate.bpm;
        int tLength = tList.Count;
        int i;
        for (i = 0; i < tLength; i++){
            if (aTime == tList[i].get<float>("time")){
                Arg tData = tList[i];
                tList.RemoveAt(i);
                mMusicDate.bpm = tList;
                return tData;
            }
        }
        return null;
    }
}
