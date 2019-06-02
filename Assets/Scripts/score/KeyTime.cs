using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct KeyTime {
    //先頭の８分音符を0とした時の番号(QN)
    public float mQuarterBeat;
    //正確なQN(三連符に影響)
    public float mCorrectQuarterBeat{
        get{
            float tFew = mQuarterBeat - Mathf.Floor(mQuarterBeat);
            if (tFew < 0.2) return Mathf.Floor(mQuarterBeat);
            if (tFew < 0.4) return Mathf.Floor(mQuarterBeat)+1f/3f;
            return Mathf.Floor(mQuarterBeat)+2f/3f;
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
        get { return Mathf.FloorToInt(mQuarterBeatInBar / 4); }
    }
    //小節内での音符番号
    public float mQuarterBeatInBar{
        get {
            float tQN = mQuarterBeat % (MusicScoreData.mRhythm * 4);
            if (tQN < 0) return MusicScoreData.mRhythm * 4 + tQN;
            else return tQN;
        }
    }
    //拍内での音符番号
    public float mQuarterBeatInBeat{
        get { return mQuarterBeat % 4; }
    }
    public int mQuarterBeatIndexInBeat{
        get {
            int tIndex = Mathf.FloorToInt(mQuarterBeat % 4);
            if (tIndex < 0) return tIndex + 4;
            else return tIndex;
        }
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
    //所属する拍の先頭のQN
    public float mTopQuarterBeatInBeat{
        get { return mTopQuarterBeatInBar + 4 * mBeatNumInBar; }
    }
    //接触しているQNを近い順で返す
    public float[] getNeighborQuarterBeat(bool aIsTriplet, bool aNextIsTriplet){
        //所属する拍の先頭のQN
        float tTimeOfBeat = mTopQuarterBeatInBeat;
        if (aIsTriplet){
            if (mQuarterBeat == tTimeOfBeat) return new float[1] { tTimeOfBeat + 0.1f };
            if (mQuarterBeat == tTimeOfBeat + 0.1f) return new float[1] { tTimeOfBeat + 0.1f };//←誤差のせいで無意味になってる
            if (mQuarterBeat < tTimeOfBeat + 2/3f) return new float[2] { tTimeOfBeat + 0.1f, tTimeOfBeat + 1.3f };
            if (mQuarterBeat < tTimeOfBeat + 4/3f) return new float[2] { tTimeOfBeat + 1.3f, tTimeOfBeat + 0.1f };
            if (mQuarterBeat == tTimeOfBeat + 4/3f) return new float[1] { tTimeOfBeat + 1.3f };
            if (mQuarterBeat < tTimeOfBeat + 6/3f) return new float[2] { tTimeOfBeat + 1.3f, tTimeOfBeat + 2.6f };
            if (mQuarterBeat < tTimeOfBeat + 8/3f) return new float[2] { tTimeOfBeat + 2.6f, tTimeOfBeat + 1.3f };
            if (mQuarterBeat == tTimeOfBeat + 8/3f) return new float[1] { tTimeOfBeat + 2.6f };
            if (mQuarterBeat < tTimeOfBeat + 4){
                if (aNextIsTriplet)
                    return new float[2] { tTimeOfBeat + 2.6f, tTimeOfBeat + 4.1f };
                else
                    return new float[2] { tTimeOfBeat + 2.6f, tTimeOfBeat + 4 };
            }
        }
        else{
            if (mQuarterBeat == tTimeOfBeat) return new float[1] { tTimeOfBeat };
            if (mQuarterBeat < tTimeOfBeat + 0.5f) return new float[2] { tTimeOfBeat, tTimeOfBeat + 1 };
            if (mQuarterBeat < tTimeOfBeat + 1) return new float[2] { tTimeOfBeat + 1, tTimeOfBeat };
            if (mQuarterBeat == tTimeOfBeat + 1) return new float[1] { tTimeOfBeat + 1 };
            if (mQuarterBeat < tTimeOfBeat + 1.5f) return new float[2] { tTimeOfBeat + 1, tTimeOfBeat + 2 };
            if (mQuarterBeat < tTimeOfBeat + 2) return new float[2] { tTimeOfBeat + 2, tTimeOfBeat + 1 };
            if (mQuarterBeat == tTimeOfBeat + 2) return new float[1] { tTimeOfBeat + 2 };
            if (mQuarterBeat < tTimeOfBeat + 2.5f) return new float[2] { tTimeOfBeat + 2, tTimeOfBeat + 3 };
            if (mQuarterBeat < tTimeOfBeat + 3) return new float[2] { tTimeOfBeat + 3, tTimeOfBeat + 2 };
            if (mQuarterBeat == tTimeOfBeat + 3) return new float[1] { tTimeOfBeat + 3 };
            if (mQuarterBeat < tTimeOfBeat + 4) {
                if(aNextIsTriplet)
                    return new float[2] { tTimeOfBeat + 3, tTimeOfBeat + 4.1f };
                else 
                    return new float[2] { tTimeOfBeat + 3, tTimeOfBeat + 4 };
            }
        }
        throw new Exception("KeyTime : 隣接したQN取得でエラー aIsTriplet = "+aIsTriplet+", aNextIsTriplet = "+aNextIsTriplet+
                            ", mQuarterBeat = "+mQuarterBeat+" , tTimeOfBeat = "+tTimeOfBeat);
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
