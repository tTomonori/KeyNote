using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class ScoreHandler {
    public class PracticeState : ScoreHandleState{
        public PracticeState(ScoreHandler aParent) : base(aParent){}
        public override void enter(){
        }
        public override void update(){
            if(Input.GetKeyDown(KeyCode.Return)){//enterで再生
                parent.changeState(new PracticePlayState(parent));
                return;
            }
            scrollScore();
        }
        public override void getMessage(Message aMessage){
            if(aMessage.name=="endButtonPushed"){//終了ボタン
                MySceneManager.changeScene("selection");
                return;
            }
            if(aMessage.name=="practiceButtonPushed"){//練習ボタン
                parent.changeState(new PracticePlayState(parent));
                return;
            }
            if(aMessage.name=="difficultButtonPushed"){//難易度ボタン
                MusicScoreData.mSelectedDifficult = EnumParser.parse<ScoreDifficult>(aMessage.getParameter<string>("selected"));
                parent.mScore.resetBars();
                return;
            }
            if (aMessage.name == "clickNote" || aMessage.name == "clickLyrics"){//譜面クリック
                parent.mScore.mCurrentQuarterBeat = aMessage.getParameter<float>("time");
                return;
            }
        }
    }
}