using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputChangeBpmValueFormMain : MonoBehaviour {
	void Start () {
        Subject.addObserver(new Observer("inputChangeBpmValueFormMain", (message) =>{
            if(message.name=="cancelInputChangeBpmValueButtonPushed"){
                MySceneManager.closeScene("inputChangeBpmValueForm", new Arg((new Dictionary<string, object>(){
                    {"change",false}
                })));
                return;
            }
            if(message.name=="okInputChangeBpmValueButtonPushed"){
                string tInputValue = getBpmValue();
                float tBpm=-1;
                if(!float.TryParse(tInputValue,out tBpm)){
                    AlartCreater.alart("BPMは数値で入力してください");
                    return;
                }
                if(tBpm<10 || 250<tBpm){
                    AlartCreater.alart("BPMは10から250のみ有効です");
                    return;
                }
                MySceneManager.closeScene("inputChangeBpmValueForm", new Arg((new Dictionary<string, object>(){
                    {"change",true},
                    {"bpm",tBpm}
                })));
                return;
            }
        }));
	}
    //入力したbpm値を取得
    private string getBpmValue(){
        return GameObject.Find("inputChangeBpmValueText").GetComponent<Text>().text;
    }
    private void OnDestroy(){
        Subject.removeObserver("inputChangeBpmValueFormMain");
    }
}
