using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class ScoreHandler {
    public class PlayState : ScoreHandleState{
        public PlayState(ScoreHandler aParent) : base(aParent){}
        public override void enter(){
            parent.mScore.show(new KeyTime(-1));
            parent.mPlayer.play();
        }
    }
}