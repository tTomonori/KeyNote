using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class MusicList {
    //曲のリスト
    public List<Arg> mList;
    //曲の数
    public int mListLength;
    //最後にプレイした曲のインデックス
    public int mLastPlayIndex;
    //最後にプレイした難易度
    public string mLastPlayDifficult;
    public MusicList(){
        mList = new List<Arg>();
        //FileInfo tInfo = new FileInfo(Application.dataPath + "/../data/list.json");
        Arg tListData = new Arg(MyJson.deserializeFile(DataFolder.path + "/list.json"));
        mList = tListData.get<List<Arg>>("list");
        mListLength = mList.Count;
        mLastPlayIndex = tListData.get<int>("lastPlayIndex");
        mLastPlayDifficult = tListData.get<string>("lastPlayDifficult");

        //DirectoryInfo tDInfo = new DirectoryInfo(Application.dataPath+"/../data/score");
        //FileInfo[] tFInfo = tDInfo.GetFiles("*.json");
        //foreach (FileInfo tFile in tFInfo){
        //    MusicData tData = new MusicData();
        //    tData.mTitle = tFile.Name;
        //    mList.Add(tData);
        //}
        //mListLength = mList.Count;
        //ファイルを読み込む
        //string[] tDataStrings = File.ReadAllText("Assets/database/list/list.json").Split('\n');
    }
    public MusicData get(int aNum){
        int tNum = aNum % mListLength;
        if (tNum < 0) tNum += mListLength;
        MusicData tData = new MusicData();
        tData.mFileName = mList[tNum].get<string>("file");
        tData.mTitle = mList[tNum].get<string>("title");
        return tData;
    }
    public class MusicData{
        public string mFileName;
        public string mTitle;
    }
}
