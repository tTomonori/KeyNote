using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChangeBpmObject : MonoBehaviour {
    //bpmデータ
    private Arg mData;
    //bpmデータ適用
    public void setData(Arg aData){
        mData = aData;
        GetComponentInChildren<SpriteRenderer>().sprite = getObjectSprite(aData.get<BpmChange>("change"));
        GetComponentInChildren<TextMesh>().text = aData.get<float>("bpm").ToString();
    }
    private Sprite getObjectSprite(BpmChange aChange){
        switch(aChange){
            case BpmChange.up:
                return Resources.Load<Sprite>("sprites/score/parts/upBpm");
            case BpmChange.down:
                return Resources.Load<Sprite>("sprites/score/parts/downBpm");
            case BpmChange.notChange:
                return Resources.Load<Sprite>("sprites/score/parts/notChangeBpm");
        }
        throw new Exception("ChangeBpmObject : ここは実行されないはず ここが実行された原因→「"+aChange+"」");
    }
    public enum BpmChange{
        up,down,notChange
    }
    //bpmの変化方向を求める
    static public BpmChange seekChange(float aPrevious,float aNext){
        if (aPrevious < aNext) return BpmChange.up;
        if (aPrevious > aNext) return BpmChange.down;
        return BpmChange.notChange;
    }
}
