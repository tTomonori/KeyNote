using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigMain : MonoBehaviour {

    private void Start(){
        Arg tArg = MySceneManager.getArg("musicConfig");
        Subject.addObserver(new Observer("configMain", (message) =>{
            Arg tData = getData();
            if(message.name=="okButtonPushed"){
                tData.set("ok", true);
                MySceneManager.closeScene("musicConfig", tData);
                return;
            }
            if(message.name=="cancelButtonPushed"){
                tData.set("ok", false);
                MySceneManager.closeScene("musicConfig", tData);
                return;
            }
        }));
    }
    //入力したデータを取得
    private Arg getData(){
        return new Arg(new Dictionary<string,object>(){
            {"title",GameObject.Find("title").GetComponentInChildren<MyBehaviour>().GetComponent<Text>().text},
            {"file",GameObject.Find("file").GetComponentInChildren<MyBehaviour>().GetComponent<Text>().text},
            {"music",GameObject.Find("music").GetComponentInChildren<MyBehaviour>().GetComponent<Text>().text},
            {"thumbnail",GameObject.Find("thumbnail").GetComponentInChildren<MyBehaviour>().GetComponent<Text>().text},
            {"back",GameObject.Find("back").GetComponentInChildren<MyBehaviour>().GetComponent<Text>().text},
            {"movie",GameObject.Find("movie").GetComponentInChildren<MyBehaviour>().GetComponent<Text>().text}
        });
    }
    private void OnDestroy(){
        Subject.removeObserver("configMain");
    }
}
