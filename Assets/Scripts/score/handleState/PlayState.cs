using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class ScoreHandler {
    public class PlayState : ScoreHandleState{
        public PlayState(ScoreHandler aParent) : base(aParent){}
        public override void enter(){
            parent.mPlayer.play();
        }
        public override void update(){
            //キー入力
            foreach(KeyCode tKey in KeyMonitor.getInputKey()){
                parent.mScore.hit(tKey,parent.mPlayer.mCurrentSecond,Note.HitNoteType.delete);
            }
            //Miss判定
            parent.mScore.missHit(parent.mPlayer.mCurrentSecond - TypeEvaluation.kWorstDifference);
        }
        public override void getMessage(Message aMessage){
            if(aMessage.name=="pauseButtonPushed"){
                parent.changeState(new PauseState(parent));
            }
        }
    }
}