using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditMain : MonoBehaviour {
    private ScoreHandler mHandler;
	void Start () {
        Arg tArg = MySceneManager.getArg("edit");
        //デバッグ用
        //Arg tArg = new Arg(new Dictionary<string, object>(){
        //    {"scoreData",DataFolder.loadScoreData("kawaikunaritai")}
        //});

        MusicScoreFileData tData = tArg.get<MusicScoreFileData>("scoreData");
        //譜面設定
        mHandler = MyBehaviour.create<ScoreHandler>();
        mHandler.set(tData, ScoreDifficult.edit);
        mHandler.show(new KeyTime(0));
        mHandler.changeState(new ScoreHandler.EditModeState(mHandler));

        //再生速度変更イベント
        Subject.addObserver(new Observer("changeAudioEventMonitor", (message) =>{
            if (message.name != "audioSpeedListPushed") return;
            switch (message.getParameter<string>("selected")){
                case "1倍速":
                    mHandler.mPlayer.mPitch = 1;
                    break;
                case "3/4倍速":
                    mHandler.mPlayer.mPitch = 3f / 4f;
                    break;
                case "1/2倍速":
                    mHandler.mPlayer.mPitch = 1f / 2f;
                    break;
            }
        }));
	}
	
	void Update () {
		
	}
    private void OnDestroy(){
        Subject.removeObserver("changeAudioEventMonitor");
    }
}
