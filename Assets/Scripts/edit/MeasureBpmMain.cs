using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureBpmMain : MonoBehaviour {
    private MusicPlayer mPlayer;
    private TextMesh mMarginText;
    private TextMesh mBpmText;
    private float mStartMeasureTime;
    private List<float> mTaps;
	void Start () {
        Arg tArg = MySceneManager.getArg("measureBpm");
        mStartMeasureTime = tArg.get<float>("second");
        //テキストオブジェクト取得
        mMarginText = GameObject.Find("measuredMarginLabel").GetComponent<MyBehaviour>().findChild<TextMesh>("value");
        mBpmText = GameObject.Find("measuredBpmLabel").GetComponent<MyBehaviour>().findChild<TextMesh>("value");
        //audio用意
        mPlayer = MyBehaviour.create<MusicPlayer>();
        mPlayer.setAudio(DataFolder.loadMusic(MusicScoreData.mMusicFileName));

        Subject.addObserver(new Observer("measureBpmMain", (message) =>{
            if(message.name=="endMeasureBpmButtonPushed"){
                MySceneManager.closeScene("measureBpm");
                return;
            }
            if(message.name=="restartMeasureBpmButtonPushed"){
                restartMeasure();
                return;
            }
        }));
        restartMeasure();
	}
    private void Update(){
        if(Input.GetKeyDown(KeyCode.Z)){
            restartMeasure();
            return;
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            tap();
            return;
        }
    }
    //計測やり直し
    private void restartMeasure(){
        mPlayer.pause();
        mTaps = new List<float>();
        mPlayer.mCurrentSecond = mStartMeasureTime;
        mMarginText.text = "0";
        mBpmText.text = "0";
        mPlayer.play();
    }
    //拍入力
    private void tap(){
        mTaps.Add(mPlayer.mCurrentSecond);
        if (mTaps.Count < 3) return;
        //Margin計算
        mMarginText.text = calculateMargin().ToString();
        //Bpm計算
        mBpmText.text = calculateBpm().ToString();
    }
    //margin計算
    private float calculateMargin(){
        float tBeatSecond = getBeatLength();
        float tBarSecond = tBeatSecond * 4;
        List<float> tMargins = new List<float>();
        for (int i = 0; i < mTaps.Count;i++){
            tMargins.Add((mTaps[i] - tBeatSecond * (i % 4)) % tBeatSecond);
        }
        float tAve = 0;
        foreach(float tMargin in tMargins){
            tAve += tMargin;
        }
        tAve /= tMargins.Count;
        return tBarSecond - tAve;
    }
    //bpm計算
    private float calculateBpm(){
        int tBeatNum = mTaps.Count - 1;
        float tLength = mTaps[tBeatNum - 1] - mTaps[0];
        return 60f * (float)tBeatNum / tLength;
    }
    //1拍のながさ(second)
    private float getBeatLength(){
        return 60f / calculateBpm();
    }
    private void OnDestroy(){
        Subject.removeObserver("measureBpmMain");
        mPlayer.delete();
    }
}
