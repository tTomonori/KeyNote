using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDetailsDisplay : MonoBehaviour {
    private Arg mMusicData;
    private DifficultButtons mButtons{
        get { return GameObject.Find("difficultButtons").GetComponent<DifficultButtons>(); }
    }
    //選択中の曲の譜面データファイル
    public string mSelectedMusicFileName;
    //選択中の難易度
    public string mDifficult{
        get { return mButtons.difficult; }
    }


	void Start () {
        Subject.addObserver(new Observer("details",(message) => {
            if (!message.isMemberOf("difficultButton")) return;
            if (mMusicData == null) return;
            changeDifficult(message.name);
        }));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void initDetails(){
        mMusicData = null;
        //曲名
        GameObject.Find("title").GetComponent<TextMesh>().text = "";
        //難易度
        gameObject.GetComponentInChildren<DifficultDisplay>().set(0);
        //最高得点
        gameObject.GetComponentInChildren<ScoreDisplay>().set(0);
    }
    public void showMusic(string aFileName){
        mSelectedMusicFileName = aFileName;
        mMusicData = new Arg(MyJson.deserializeFile(Application.dataPath + "/../data/score/" + aFileName + ".json"));
        //曲名
        GameObject.Find("title").GetComponent<TextMesh>().text = mMusicData.get<string>("title");
        changeDifficult(mButtons.difficult);
    }
    public void changeDifficult(string aDifficult){
        //難易度
        gameObject.GetComponentInChildren<DifficultDisplay>().set(mMusicData.get<Arg>("difficult").get<int>(aDifficult));
        //最高得点
        gameObject.GetComponentInChildren<ScoreDisplay>().set(mMusicData.get<Arg>("point").get<float>(aDifficult));
    }
    private void OnDestroy(){
        Subject.removeObserver("details");
    }
}
