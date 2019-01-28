using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseUp(){
        MySceneManager.changeScene("selection", new Arg());
    }
}
