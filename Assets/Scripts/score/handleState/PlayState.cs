using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class ScoreHandler {
    public class PlayState : ScoreHandleState{
        public PlayState(ScoreHandler aParent) : base(aParent){}
        public override void enter(){
            parent.mScore.show(new KeyTime(-3));
            parent.mPlayer.play();
        }
        public override void update(){
            foreach(KeyCode tKey in KeyMonitor.getInputKey()){
                parent.mScore.hit(tKey,parent.mPlayer.mCurrentSecond,Note.HitNoteType.delete);
            }
        }
        public override void getMessage(Message aMessage){

        }
    }
}