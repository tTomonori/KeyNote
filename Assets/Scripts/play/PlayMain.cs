using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMain : MonoBehaviour {
    private ScoreHandler mHandler;
    
	void Start () {
        Arg tArg = MySceneManager.getArg("play");
        //Arg tArg = new Arg(new Dictionary<string, object>() { { "file", "kawaikunaritai" }, { "difficult", "guru" } });
        string tFileName = tArg.get<string>("file");
        string tDifficult = tArg.get<string>("difficult");

        mHandler = MyBehaviour.create<ScoreHandler>();
        mHandler.load(tFileName, tDifficult);
        mHandler.show(new KeyTime(-3));
        mHandler.changeState(new ScoreHandler.PlayState(mHandler));
	}

	void Update () {

	}
}
