using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditMain : MonoBehaviour {

	void Start () {
        Arg tArg = MySceneManager.getArg("edit");
        Debug.Log(tArg.get<string>("title"));
        Debug.Log(tArg.get<string>("file"));
        Debug.Log(tArg.get<string>("music"));
        Debug.Log(tArg.get<string>("thumbnail"));
        Debug.Log(tArg.get<string>("back"));
        Debug.Log(tArg.get<string>("movie"));
	}
	
	void Update () {
		
	}
}
