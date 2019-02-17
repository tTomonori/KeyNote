using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyNotePlayer {
    //譜面のオブジェクト
    private MusicScore mScore;
    //音声再生クラス
    private MusicPlayer mPlayer;
    //音声のながさ
    public float mMusicLength{
        get { return mPlayer.mLength; }
    }
    //コルーチン実行用オブジェクト
    private MyBehaviour mBehaviour;
    //譜面の位置調整コルーチン
    private Coroutine mAdjustScoreCoroutine;
    //音声なしで譜面を移動させるコルーチン
    private Coroutine mMoveScoreCoroutine;
    //現在の音声の位置
    public float mCurrentSecond{
        get { return mPlayer.mCurrentSecond; }
    }
    public KeyNotePlayer(MusicScore aScore,MusicPlayer aPlayer){
        mBehaviour = MyBehaviour.create<MyBehaviour>();
        mBehaviour.name = "KeyNotePlayer's CoroutineRunner";
        mScore = aScore;
        mPlayer = aPlayer;
    }
    //音声を変更
    public void changeMusic(MusicPlayer aPlayer){
        mPlayer = aPlayer;
    }
    public void play(){
        float tCurrentMusicTime = MusicScoreData.quarterBeatToMusicTime(mScore.mCurrentQuarterBeat);
        mPlayer.mCurrentSecond = tCurrentMusicTime;
        if(tCurrentMusicTime<0){
            mPlayer.playDelayed(-tCurrentMusicTime);
            mMoveScoreCoroutine = mScore.moveBy(new Vector3(0, (mScore.mStartMusicPosition-mScore.positionY)*(1-0.5f/tCurrentMusicTime), 0), -tCurrentMusicTime+0.5f,() => {
                mMoveScoreCoroutine = null;
                mAdjustScoreCoroutine = mBehaviour.StartCoroutine(adjustScoreToPlayer());
            });
        }else{
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
            if(!mPlayer.mIsPlaying){
                //再生終了
                yield return null;
                if(!mPlayer.mIsPlaying){
                    mAdjustScoreCoroutine = null;
                    Subject.sendMessage(new Message("finishedMusic", new Arg()));//曲終了メッセージ
                    yield break;
                }
            }
            mScore.adjustPozitionToMusicTime(mPlayer.mCurrentSecond);
            yield return null;
        }
    }
}
