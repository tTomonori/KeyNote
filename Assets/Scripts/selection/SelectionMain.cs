using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMain : MonoBehaviour {
	void Start () {
        Subject.addObserver(new Observer("selectionMain",(message) => {
            if(message.name=="play"){
                MusicDetailsDisplay tDetails = GameObject.Find("detailsDisplay").GetComponent<MusicDetailsDisplay>();
                MySceneManager.changeScene("play",new Arg(new Dictionary<string, object>() { 
                    { "file", tDetails.mSelectedMusicFileName } ,
                    { "difficult", tDetails.mDifficult}
                }));
            }
        }));
	}
	

	void Update () {
		
	}
    private void OnDestroy(){
        Subject.removeObserver("selectionMain");
    }
}
