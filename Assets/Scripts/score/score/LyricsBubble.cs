using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LyricsBubble : MonoBehaviour {
    //歌詞データ
    private Arg mData;
    //歌詞データ適用
    public void setData(Arg aData){
        mData = aData;
        GetComponentInChildren<TextMesh>().text = aData.get<string>("char");
    }
}
