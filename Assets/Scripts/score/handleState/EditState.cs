using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

partial class ScoreHandler{
    public class EditState : ScoreHandleState{
        public EditState(ScoreHandler aParent) : base(aParent) { }
        private ToggleButtonGroup mPlaceTggle;
        public override void enter(){
            mPlaceTggle = GameObject.Find("placeObjectToggle").GetComponent<ToggleButtonGroup>();
        }
        public override void update(){
            //譜面スクロール
            float tScroll = Input.mouseScrollDelta.y;
            parent.mScore.positionY -= tScroll;
            //負の位置まではスクロールできないようにする
            if (parent.mScore.mCurrentQuarterBeat < 0)
                parent.mScore.mCurrentQuarterBeat = 0;
        }
        public override void getMessage(Message aMessage){
            if(aMessage.name=="editPlayButtonPushed"){//編曲再生ボタン
                parent.changeState(new EditPlayState(parent));
                return;
            }
            if(aMessage.name=="testPlayButtonPushed"){//テスト再生ボタン
                parent.changeState(new TestPlayState(parent));
                return;
            }
            if (aMessage.name == "measureBpmButtonPushed"){//bpm測定ボタン
                parent.changeState(new MeasureBpmState(parent));
                return;
            }
            if(aMessage.name=="clickNote"){
                Debug.Log("note : " + aMessage.getParameter<float>("time"));
                return;
            }
            if(aMessage.name=="clickLyrics"){
                Debug.Log("lyrics : " + aMessage.getParameter<float>("time"));
                return;
            }
            if(aMessage.name=="RightClickNote"){
                Debug.Log("note R : " + aMessage.getParameter<float>("time"));
                return;
            }
            if(aMessage.name=="RightClickLyrics"){
                Debug.Log("lyrics R : " + aMessage.getParameter<float>("time"));
                return;
            }
        }
    }
}