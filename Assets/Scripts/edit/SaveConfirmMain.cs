using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveConfirmMain : MonoBehaviour {

	void Start () {
        Subject.addObserver(new Observer("saveConfirmMain", (message) =>{
            if(message.name=="saveEndButtonPushed"){
                MySceneManager.closeScene("saveConfirmForm", new Arg(new Dictionary<string, object>(){
                    {"save",true},
                    {"continue",false}
                }));
                return;
            }
            if (message.name == "nonsaveEndButtonPushed"){
                MySceneManager.closeScene("saveConfirmForm", new Arg(new Dictionary<string, object>(){
                    {"save",false},
                    {"continue",false}
                }));
                return;
            }
            if (message.name == "saveContinueButtonPushed"){
                MySceneManager.closeScene("saveConfirmForm", new Arg(new Dictionary<string, object>(){
                    {"save",true},
                    {"continue",true}
                }));
                return;
            }
            if (message.name == "nonsaveContinueButtonPushed"){
                MySceneManager.closeScene("saveConfirmForm", new Arg(new Dictionary<string, object>(){
                    {"save",false},
                    {"continue",true}
                }));
                return;
            }
        }));
	}
    private void OnDestroy(){
        Subject.removeObserver("saveConfirmMain");
    }
}
