using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

partial class ScoreHandler{
    public class TestPlayState : EditModeState{
        public TestPlayState(ScoreHandler aParent) : base(aParent) { }
        public override void enter(){
            GameObject.Find("testPlayButton").GetComponent<LightButton>().hold();
            parent.mPlayer.play();
        }
        public override void exit(){
            GameObject.Find("testPlayButton").GetComponent<LightButton>().release();
            parent.mPlayer.pause();
            parent.mScore.resetBars();
        }
        public override void update(){
            //キー入力
            foreach (KeyCode tKey in KeyMonitor.getInputKey()){
                parent.hit(tKey, parent.getCurrentTime(), Note.HitNoteType.decolorize, "tambourine", "castanet");
            }
        }
        public override void getMessage(Message aMessage){
            if (aMessage.name == "hittedNote" || aMessage.name == "missedNote"){//タイピングの評価
                productHit(aMessage.getParameter<Note>("note"), aMessage.getParameter<TypeEvaluation.Evaluation>("evaluation"));
                displayTimeDifference(aMessage.getParameter<Note>("note"), aMessage.getParameter<float>("difference"));
                return;
            }
            if (aMessage.name == "testPlayButtonPushed"){//編曲編集ボタン
                parent.changeState(new EditState(parent));
                return;
            }
        }
    }
}