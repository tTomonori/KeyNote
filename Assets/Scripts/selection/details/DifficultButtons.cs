using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultButtons : MonoBehaviour {
    ///選択されている難易度
    private string mPushedButtonName=null;
    public string difficult{
        get { return mPushedButtonName; }
    }
    ///選択されている難易度のボタンのサイズ比
    private Vector3 mPushedRatio = new Vector3(0.15f, 0.15f, 0);

	void Start () {
        Subject.addObserver(new Observer("difficultButtons",(message) => {
            if(message.isMemberOf("difficultButton")){
                push(message.name);
            }
        }));
	}

    void push(string aButtonName){
        UiButton[] tButtons = GetComponentsInChildren<UiButton>();
        //選択中の難易度を選択解除
        if(mPushedButtonName!=null){
            foreach(UiButton tButton in tButtons){
                if(tButton.name == mPushedButtonName + "Button"){
                    tButton.scaleBy(-mPushedRatio, 0.1f);
                    break;
                }
            }
        }
        //選択された難易度を設定
        foreach (UiButton tButton in tButtons){
            if (tButton.name == aButtonName + "Button"){
                tButton.scaleBy(mPushedRatio, 0.1f);
                break;
            }
        }
        mPushedButtonName = aButtonName;
    }

    private void OnDestroy(){
        Subject.removeObserver("difficultButtons");
    }
}
