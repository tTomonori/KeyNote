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
                parent.hit(tKey,parent.mPlayer.mCurrentSecond,Note.HitNoteType.delete,"tambourine","castanet");
            }
            //Miss判定
            parent.mScore.missHit(parent.mPlayer.mCurrentSecond - TypeEvaluation.kWorstDifference);
        }
        public override void getMessage(Message aMessage){
            if(aMessage.name=="hittedNote"||aMessage.name=="missedNote"){//タイピングの評価
                productHit(aMessage.getParameter<Note>("note"), aMessage.getParameter<TypeEvaluation.Evaluation>("evaluation"));
                return;
            }
            if(aMessage.name=="pauseButtonPushed"){//ポーズ
                parent.changeState(new PauseState(parent));
                return;
            }
            if(aMessage.name=="finishedMusic"){//曲終了
                //残った音符を全てmiss評価にする
                parent.mScore.missHit(parent.mPlayer.mMusicLength);
                //評価合計
                Arg tResult = GameObject.Find("evaluationDisplay").GetComponent<EvaluationDisplay>().getResult();
                MySceneManager.changeScene("result", new Arg(new Dictionary<string, object>(){
                    {"evaluation",tResult}
                }));
            }
        }
    }
}