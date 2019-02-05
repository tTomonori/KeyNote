using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetroArrow : MyBehaviour {
    private MusicScore mParentScore;
    //所属する譜面を設定
    public void setParentScore(MusicScore aScore){
        mParentScore = aScore;
        this.transform.parent = mParentScore.transform;
    }
	void Start () {
        name = "metroArrow";
        gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sprites/score/doubleNote");
        positionZ = -1;
        scale = new Vector3(0.7f, 0.7f, 0.7f);
	}
	void Update () {
        KeyTime tCurrentTime = new KeyTime(mParentScore.mCurrentQuarterBeat);
        positionY = mParentScore.convertToPositionY(tCurrentTime.mBarNum) + MusicScore.kNotationSize.y / 2;
        positionX = (tCurrentTime.mQuarterBeatInBar - MusicScoreData.mRhythm * 2) * MusicScore.kNotationSize.x / 4;
        //弾む動作をつける
        positionY += Mathf.Sin(Mathf.PI * (tCurrentTime.mQuarterBeatInBar % 4) / 4);
	}
}
