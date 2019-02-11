using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditMain : MonoBehaviour {
    private ScoreHandler mHandler;
	void Start () {
        //Arg tArg = MySceneManager.getArg("edit");
        Arg tArg = new Arg(new Dictionary<string, object>(){
            {"scoreData",DataFolder.loadScoreData("kawaikunaritai")}
        });
        MusicScoreFileData tData = tArg.get<MusicScoreFileData>("scoreData");
        //譜面設定
        mHandler = MyBehaviour.create<ScoreHandler>();
        mHandler.set(tData, "edit");
        mHandler.show(new KeyTime(0));
        mHandler.changeState(new ScoreHandler.EditModeState(mHandler));
	}
	
	void Update () {
		
	}
}
