using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMain : MonoBehaviour {
    private ScoreHandler tHandler;
    
	void Start () {
        Arg tArg = MySceneManager.getArg("play");
        string tFileName = tArg.get<string>("file");
        string tDifficult = tArg.get<string>("difficult");

        tHandler = new ScoreHandler(tFileName);
	}

	void Update () {
		
	}
}
