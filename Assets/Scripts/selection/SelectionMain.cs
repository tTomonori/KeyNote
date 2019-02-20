using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMain : MonoBehaviour {
	void Start () {
        Subject.addObserver(new Observer("selectionMain",(message) => {
            if(message.name=="playButtonPushed"){//playボタン
                MusicDetailsDisplay tDetails = GameObject.Find("detailsDisplay").GetComponent<MusicDetailsDisplay>();
                MusicListDisplay tList = GameObject.Find("musicList").GetComponent<MusicListDisplay>();
                //最後に遊んだ曲更新
                MusicList.updateLastPlay(tList.mSelectedIndex, tDetails.mSelectedDifficult);
                //playシーンを開く
                MySceneManager.changeScene("play",new Arg(new Dictionary<string, object>() { 
                    { "file", tDetails.mSelectedMusic.file } ,
                    { "difficult", tDetails.mSelectedDifficult}
                }));
                return;
            }
            if(message.name=="practiceButtonPushed"){//練習ボタン
                MusicDetailsDisplay tDetails = GameObject.Find("detailsDisplay").GetComponent<MusicDetailsDisplay>();
                MusicListDisplay tList = GameObject.Find("musicList").GetComponent<MusicListDisplay>();
                //最後に遊んだ曲更新
                MusicList.updateLastPlay(tList.mSelectedIndex, tDetails.mSelectedDifficult);
                MySceneManager.changeScene("practice", new Arg(new Dictionary<string, object>(){
                    { "file", tDetails.mSelectedMusic.file } ,
                    { "difficult", tDetails.mSelectedDifficult}
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
