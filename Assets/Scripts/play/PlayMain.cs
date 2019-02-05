using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMain : MonoBehaviour {
    private ScoreHandler tHandler;
    
	void Start () {
        Arg tArg = MySceneManager.getArg("play");
        //Arg tArg = new Arg(new Dictionary<string, object>() { { "file", "kawaikunaritai" }, { "difficult", "guru" } });
        string tFileName = tArg.get<string>("file");
        string tDifficult = tArg.get<string>("difficult");

        tHandler = MyBehaviour.create<ScoreHandler>();
        tHandler.load(tFileName, tDifficult);
        tHandler.show(new KeyTime(-3));
        tHandler.changeState(new ScoreHandler.PlayState(tHandler));
	}

	void Update () {

	}
}
