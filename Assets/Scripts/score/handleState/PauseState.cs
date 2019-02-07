using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class ScoreHandler{
    public class PauseState : ScoreHandleState{
        public PauseState(ScoreHandler aParent) : base(aParent) { }
        public override void enter(){
            parent.mPlayer.pause();
            MySceneManager.openScene("pause");
        }
        public override void update(){
            
        }
        public override void getMessage(Message aMessage){
            if (aMessage.name == "pauseButtonPushed"){//ポーズ
                parent.changeState(new PlayState(parent));
                return;
            }
            if(aMessage.name=="endButtonPushed"){//中断
                MySceneManager.changeScene("selection");
                return;
            }
            if(aMessage.name=="continueButtonPushed"){//継続
                MySceneManager.closeScene("pause");
                parent.changeState(new PlayState(parent));
                return;
            }
        }
    }
}