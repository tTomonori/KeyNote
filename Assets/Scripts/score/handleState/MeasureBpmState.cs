using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

partial class ScoreHandler{
    public class MeasureBpmState : EditModeState{
        public MeasureBpmState(ScoreHandler aParent) : base(aParent) { }
        public override void enter(){
            GameObject.Find("measureBpmButton").GetComponent<LightButton>().hold();
            parent.mPlayer.play();
        }
        public override void exit(){
            GameObject.Find("measureBpmButton").GetComponent<LightButton>().release();
            parent.mPlayer.pause();
        }
        public override void update(){
            
        }
        public override void getMessage(Message aMessage){
            if (aMessage.name == "measureBpmButtonPushed"){//編曲編集ボタン
                parent.changeState(new EditState(parent));
                return;
            }
        }
    }
}