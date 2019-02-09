using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditMain : MonoBehaviour {

	void Start () {
        Arg tArg = MySceneManager.getArg("edit");
        MusicScoreFileData tData = tArg.get<MusicScoreFileData>("scoreData");
        Debug.Log(tData.title);
        Debug.Log(tData.fileName);
        Debug.Log(tData.music);
        Debug.Log(tData.thumbnail);
        Debug.Log(tData.back);
        Debug.Log(tData.movie);
	}
	
	void Update () {
		
	}
}
