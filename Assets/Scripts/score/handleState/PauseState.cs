using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class ScoreHandler{
    public class PauseState : ScoreHandleState{
        public PauseState(ScoreHandler aParent) : base(aParent) { }
        public override void enter(){
            parent.mPlayer.pause();
        }
        public override void update(){
            
        }
        public override void getMessage(Message aMessage){
            if (aMessage.name == "pauseButtonPushed"){
                parent.changeState(new PlayState(parent));
            }
        }
    }
}