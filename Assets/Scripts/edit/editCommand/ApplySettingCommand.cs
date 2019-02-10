using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplySettingCommand : MyCommand {
    private MusicSettingForm mForm;
    //適用するデータ
    private int mRhythm;
    private float mMargin;
    private float mRust;
    //適用前のデータ
    private int mPreRhythm;
    private float mPreMargin;
    private float mPreRust;
    //削除したbpm
    private List<Arg> mBpms;
    public ApplySettingCommand(MusicSettingForm aFrom){
        mForm = aFrom;
        mRhythm = mForm.mRhythm;
        mMargin = mForm.mMargin;
        mRust = mForm.mRust;
    }
    public override void run(){
        mPreRhythm = MusicScoreData.mRhythm;
        mPreMargin = MusicScoreData.mMargin;
        mPreRust = MusicScoreData.mRust;
        MusicScoreData.mRhythm = mRhythm;
        MusicScoreData.mMargin = mMargin;
        MusicScoreData.mRust = mRust;
        mForm.reset();
        //音源再生位置より手前のbpm変更イベント削除
        float mPlayMusicQuarterBeat = MusicScoreData.mStartPlayMusicTime.mQuarterBeat;
        mBpms = new List<Arg>();
        //削除するbpm変更イベントを探す
        List<Arg> tBpms = MusicScoreData.getChangeBpmInBar(new KeyTime(0));
        foreach(Arg tData in tBpms){
            if (tData.get<float>("time") < 1) continue;//先頭のbpm変更イベントはそのままにする
            if (mPlayMusicQuarterBeat <= tData.get<float>("time")) continue;//音源再生位置以降
            mBpms.Add(tData);
        }
        //見つけたbpm変更イベントを削除
        foreach(Arg tData in mBpms){
            MusicScoreData.deleteChangeBpm(tData.get<float>("time"));
        }
    }
    public override void undo(){
        MusicScoreData.mRhythm = mPreRhythm;
        MusicScoreData.mMargin = mPreMargin;
        MusicScoreData.mRust = mPreRust;
        mForm.reset();
        //bpm変更イベントを元に戻す
        foreach (Arg tData in mBpms){
            MusicScoreData.addChangeBpm(tData);
        }
    }
}
