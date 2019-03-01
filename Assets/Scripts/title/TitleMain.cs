using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMain : MonoBehaviour {

	void Start () {
        GameObject.Find("light").GetComponent<MyBehaviour>().rotateForever(90);
        Subject.addObserver(new Observer("titleMain", (message) =>{
            if (message.name == "lightPushed")
                gameStart();
        }));
	}
	
	void Update () {
		
	}

    private void gameStart(){
        MySceneManager.changeScene("selection", new Arg());
    }
    private void OnDestroy(){
        Subject.removeObserver("titleMain");
    }
}
