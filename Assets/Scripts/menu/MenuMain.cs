using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMain : MonoBehaviour {
	void Start () {
        Subject.addObserver(new Observer("menuMain", (message) =>{
            if(message.name=="createScoreButtonPushed"){//譜面作成
                MySceneManager.changeScene("musicConfig", null, null, (aArg) =>{
                    if(aArg.get<bool>("ok")){
                        MySceneManager.changeScene("edit", aArg);
                    }else{
                        MySceneManager.changeScene("selection");
                    }
                });
                return;
            }
            if(message.name=="editScoreButtonPushed"){//譜面編集
                MyBehaviour.createObjectFromPrefab<AlartWindow>("ui/alartWindow").set("未実装");
                return;
            }
            if(message.name=="sortScoreButtonPushed"){//譜面並び替え
                MyBehaviour.createObjectFromPrefab<AlartWindow>("ui/alartWindow").set("未実装");
                return;
            }
            if(message.name=="backMenuButtonPushed"){//戻る
                MySceneManager.closeScene("menu");
                return;
            }
        }));
	}
	void Update () {
		
	}
    private void OnDestroy(){
        Subject.removeObserver("menuMain");
    }
}
