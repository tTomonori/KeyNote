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
    public KeyNotePlayer(MusicScore aScore,MusicPlayer aPlayer){
        mScore = aScore;
        mPlayer = aPlayer;
    }
    public void play(){
        mPlayer.play();
        mAdjustScoreCoroutine = mBehaviour.StartCoroutine(adjustScoreToPlayer());
    }
    public void pause(){
        mPlayer.pause();
        mBehaviour.StopCoroutine(mAdjustScoreCoroutine);
    }
    private IEnumerator adjustScoreToPlayer(){
        while(true){
            mScore.adjustPozitionToMusicTime(mPlayer.mCurrentSecond);
            yield return null;
        }
    }
}
