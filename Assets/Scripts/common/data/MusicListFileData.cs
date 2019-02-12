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
    public string lastPlayDifficult{
        get { return mData.get<string>("lastPlayDifficult"); }
        set { mData.set("lastPlayDifficult", value); }
    }
    //曲リスト
    public List<Arg> list{
        get { return mData.get<List<Arg>>("list"); }
    }

}
