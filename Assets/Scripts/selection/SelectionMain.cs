using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMain : MonoBehaviour {
	void Start () {
        Subject.addObserver(new Observer("selectionMain",(message) => {
            if(message.name=="play"){//playボタン
                MusicDetailsDisplay tDetails = GameObject.Find("detailsDisplay").GetComponent<MusicDetailsDisplay>();
                MySceneManager.changeScene("play",new Arg(new Dictionary<string, object>() { 
                    { "file", tDetails.mSelectedMusicFileName } ,
                    { "difficult", tDetails.mDifficult}
                }));
                return;
            }
            if(message.name=="menuButtonPushed"){//メニューボタン
                MySceneManager.openScene("menu");
                return;
            }
        }));
	}
	

	void Update () {
		
	}
    private void OnDestroy(){
        Subject.removeObserver("selectionMain");
    }
}
