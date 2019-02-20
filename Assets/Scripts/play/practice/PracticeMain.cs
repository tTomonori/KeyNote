using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeMain : MonoBehaviour {
    private ScoreHandler mHandler;

	void Start () {
        Arg tArg = MySceneManager.getArg("practice");
        string tFileName = tArg.get<string>("file");
        ScoreDifficult tDifficult = tArg.get<ScoreDifficult>("difficult");

        //難易度選択ボタン
        string tDifficultString = StringCaseConverter.ToUpper(tDifficult.ToString())[0] + tDifficult.ToString().Substring(1);
        GameObject.Find("difficultButton").GetComponent<ListButton>().select(tDifficultString);

        mHandler = MyBehaviour.create<ScoreHandler>();
        mHandler.load(tFileName, tDifficult);
        mHandler.show(new KeyTime(0));
        mHandler.changeState(new ScoreHandler.PracticeState(mHandler));
	}
	
	void Update () {
		
	}
}
