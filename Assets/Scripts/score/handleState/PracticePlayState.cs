using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class ScoreHandler {
    public class PracticePlayState : ScoreHandleState{
        public PracticePlayState(ScoreHandler aParent) : base(aParent){}
        //このstateに移動した時のquarterBeat
        private float mQuarterBeat;
        public override void enter(){
            mQuarterBeat = parent.mScore.mCurrentQuarterBeat;
            parent.mPlayer.play();
        }
        public override void exit(){
            parent.mPlayer.pause();
            parent.mScore.resetBars();
        }
        public override void update(){
            if(Input.GetKeyDown(KeyCode.Return)){//enterで停止
                parent.changeState(new PracticeState(parent));
                return;
            }
            if (Input.GetKeyDown(KeyCode.Tab)){//tabでRestart
                parent.mPlayer.pause();
                parent.mScore.mCurrentQuarterBeat = mQuarterBeat;
                parent.mScore.resetBars();
                parent.mPlayer.play();
                return;
            }
            //キー入力
            foreach(KeyCode tKey in KeyMonitor.getInputKey()){
                parent.mScore.hit(tKey,parent.mPlayer.mCurrentSecond,Note.HitNoteType.decolorize);
            }
        }
        public override void getMessage(Message aMessage){
            if(aMessage.name=="hittedNote"||aMessage.name=="missedNote"){//タイピングの評価
                productHit(aMessage.getParameter<Note>("note"), aMessage.getParameter<TypeEvaluation.Evaluation>("evaluation"));
                displayTimeDifference(aMessage.getParameter<Note>("note"), aMessage.getParameter<float>("difference"));
                return;
            }
            if (aMessage.name == "practiceButtonPushed"){//練習ボタン
                parent.changeState(new PracticeState(parent));
                return;
            }
            if (aMessage.name == "difficultButtonPushed"){//難易度ボタン
                MusicScoreData.mSelectedDifficult = EnumParser.parse<ScoreDifficult>(aMessage.getParameter<string>("selected"));
                parent.mScore.resetBars();
                return;
            }
        }
    }
}