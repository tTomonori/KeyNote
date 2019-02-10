using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

partial class ScoreHandler{
    public class TestPlayState : ScoreHandleState{
        public TestPlayState(ScoreHandler aParent) : base(aParent) { }
        public override void enter(){
            GameObject.Find("testPlayButton").GetComponent<LightButton>().hold();
            parent.mPlayer.play();
        }
        public override void exit(){
            GameObject.Find("testPlayButton").GetComponent<LightButton>().release();
            parent.mPlayer.pause();
        }
        public override void update(){
            
        }
        public override void getMessage(Message aMessage){
            if (aMessage.name == "testPlayButtonPushed"){//編曲編集ボタン
                parent.changeState(new EditState(parent));
                return;
            }
        }
    }
}