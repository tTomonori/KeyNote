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
        mHandler.set(tData, "guru");
        mHandler.show(new KeyTime(0));
        mHandler.changeState(new ScoreHandler.EditState(mHandler));

        //配置するオブジェクトの初期設定
        GameObject.Find("placeObjectToggle").GetComponent<ToggleButtonGroup>().memberPushed("note");
	}
	
	void Update () {
		
	}
}
