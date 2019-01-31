using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyNotePlayer {
    //譜面のオブジェクト
    private MusicScore mScore;
    //音声再生クラス
    private MusicPlayer mPlayer;
    //コルーチン実行用オブジェクト
    private MyBehaviour mBehaviour = MyBehaviour.create<MyBehaviour>();
    //譜面の位置調整コルーチン
    private Coroutine mAdjustScoreCoroutine;
    //音声なしで譜面を移動させるコルーチン
    private Coroutine mMoveScoreCoroutine;
    public KeyNotePlayer(MusicScore aScore,MusicPlayer aPlayer){
        mScore = aScore;
        mPlayer = aPlayer;
    }
    public void play(){
        float tCurrentMusicTime = MusicScoreData.quarterBeatToMusicTime(mScore.mCurrentQuarterBeat);
        if(tCurrentMusicTime<0){
            mPlayer.playDelayed(-tCurrentMusicTime);
            mMoveScoreCoroutine = mScore.moveBy(new Vector3(0, (mScore.mStartMusicPosition-mScore.positionY)*(1-0.5f/tCurrentMusicTime), 0), -tCurrentMusicTime+0.5f,() => {
                mMoveScoreCoroutine = null;
                mAdjustScoreCoroutine = mBehaviour.StartCoroutine(adjustScoreToPlayer());
            });
        }else{
            mPlayer.mCurrentSecond = tCurrentMusicTime;
            mPlayer.play();
            mAdjustScoreCoroutine = mBehaviour.StartCoroutine(adjustScoreToPlayer());
        }
    }
    public void pause(){
        mPlayer.pause();
        if (mAdjustScoreCoroutine != null)
            mBehaviour.StopCoroutine(mAdjustScoreCoroutine);
        mAdjustScoreCoroutine = null;
        if (mMoveScoreCoroutine != null)
            mScore.StopCoroutine(mMoveScoreCoroutine);
        mMoveScoreCoroutine = null;
    }
    private IEnumerator adjustScoreToPlayer(){
        while(true){
            mScore.adjustPozitionToMusicTime(mPlayer.mCurrentSecond);
            yield return null;
        }
    }
}
