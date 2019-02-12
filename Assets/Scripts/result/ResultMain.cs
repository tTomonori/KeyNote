using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResultMain : MonoBehaviour {
	void Start () {
        //得点表示
        Arg tArg = MySceneManager.getArg("result");
        float tTotalPoint = calculatePoint(tArg.get<Arg>("evaluation"));
        GameObject.Find("totalPoint").GetComponent<MyBehaviour>().findChild<TextMesh>("score").text = tTotalPoint.ToString();
        //オブザーバ追加
        Subject.addObserver(new Observer("resultMain", (message) =>{
            if (message.name == "endButtonPushed"){
                MySceneManager.changeScene("selection");
            }
        }));
        GameObject.Find("endButton").GetComponent<LightButton>().hold();

        //ハイスコア更新
        MusicList.updatePoint(MusicScoreData.mSaveFileName, MusicScoreData.mSelectedDifficult, tTotalPoint);
	}
	void Update () {
		
	}
    private void OnDestroy(){
        Subject.removeObserver("resultMain");
    }
    //得点計算
    private float calculatePoint(Arg aResult){
        float tTotal = 0;
        tTotal += aResult.get<int>("perfect")*100;
        tTotal += aResult.get<int>("great")*90;
        tTotal += aResult.get<int>("good")*70;
        tTotal += aResult.get<int>("bad")*30;
        tTotal = tTotal / (aResult.get<int>("perfect") + aResult.get<int>("great") + aResult.get<int>("good") + aResult.get<int>("bad") + aResult.get<int>("miss"));
        return (float)Math.Round(tTotal, 1);
    }
}
