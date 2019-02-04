using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct KeyTime {
    //先頭の８分音符を0とした時の番号(QN)
    public float mQuarterBeat;
    //正確なQN(三連符に影響)
    public float mCorrectQuarterBeat{
        get{
            float tFew = mQuarterBeat - Mathf.Floor(mQuarterBeat);
            if (tFew < 0.2) return Mathf.Floor(mQuarterBeat);
            if (tFew < 0.4) return Mathf.Floor(mQuarterBeat)+1/3;
            return Mathf.Floor(mQuarterBeat)+2/3;
        }
    }
    //三連符に属する
    public bool mIsInTriplet{
        get { return !(mQuarterBeat == Mathf.Floor(mQuarterBeat)); }
    }
    //所属する小節の番号
    public int mBarNum{
        get { return Mathf.FloorToInt(mQuarterBeat / (MusicScoreData.mRhythm * 4)); }
    }
    //所属する拍の小節内での番号
    public int mBeatNumInBar{
        get { return Mathf.FloorToInt(mQuarterBeatNumInBar / 4); }
    }
    //小節内での音符番号
    public float mQuarterBeatNumInBar{
        get { return mQuarterBeat % (MusicScoreData.mRhythm * 4); }
    }
    //拍内での音符番号
    public float mQuarterBeatNumInBeat{
        get { return mQuarterBeat % 4; }
    }
    public int mQuarterBeatIndexInBeat{
        get { return Mathf.FloorToInt(mQuarterBeat % 4); }
    }
    //拍内での音符番号(三連符に含まれる時)
    public int mQuarterBeatNumInTriplet{
        get {
            float tFew = mQuarterBeat - Mathf.Floor(mQuarterBeat);
            if (tFew < 0.2) return 0;
            if (tFew < 0.4) return 1;
            return 2;
        }
    }
    //所属する小節の先頭のQN
    public float mTopQuarterBeatInBar{
        get { return mBarNum * MusicScoreData.mRhythm * 4; }
    }
    //所属する小節の末尾のQN
    public float mTailQuarterBeatInBar{
        get { return (mBarNum * MusicScoreData.mRhythm * 4) + (MusicScoreData.mRhythm * 4 - 1); }
    }

    public KeyTime(float aQuarterBeatTime){
        mQuarterBeat = aQuarterBeatTime;
    }
    public KeyTime(int aBarNum){
        mQuarterBeat = aBarNum * MusicScoreData.mRhythm * 4;
    }
    //秒をQNに変換
    static public float secondsToQuarterBeat(float aSecond,float aBpm){
        return aSecond / (15 / aBpm);
    }
    //QNを秒に変換
    static public float quarterBeatToSeconds(float aQuarterBeat,float aBpm){
        return aQuarterBeat * (15 / aBpm);
    }
}
