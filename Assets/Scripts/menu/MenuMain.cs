using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMain : MonoBehaviour {
	void Start () {
        Subject.addObserver(new Observer("menuMain", (message) =>{
            if(message.name=="createScoreButtonPushed"){//譜面作成
                MySceneManager.changeScene("musicConfig", new Arg(new Dictionary<string, object>() { { "new", true } }), null, (aArg) =>{
                    if(aArg.get<bool>("ok")){
                        MySceneManager.changeScene("edit", aArg);
                    }else{
                        MySceneManager.changeScene("selection");
                    }
                });
                return;
            }
            if(message.name=="browseScoreButtonPushed"){//譜面一覧
                MySceneManager.changeScene("browseScoreList");
                return;
            }
            if(message.name=="backMenuButtonPushed"){//戻る
                MySceneManager.closeScene("menu");
                return;
            }
            if (message.name == "quitButtonPushed"){//ゲーム終了
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//エディタ用
                #endif
                UnityEngine.Application.Quit();//パッケージ版用
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
