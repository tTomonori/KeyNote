using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

partial class ScoreHandler{
    public class EditPlayState : EditModeState{
		public EditPlayState(ScoreHandler aParent) : base(aParent) { }
        public override void enter(){
            GameObject.Find("editPlayButton").GetComponent<LightButton>().hold();
            parent.mPlayer.play();
        }
        public override void exit(){
            GameObject.Find("editPlayButton").GetComponent<LightButton>().release();
            parent.mPlayer.pause();
        }
        public override void update(){
            //キー入力
            foreach (KeyCode tKey in KeyMonitor.getInputKey()){
                tryCreateNote(new KeyTime(MusicScoreData.musicTimeToQuarterBeat(parent.getCurrentTime())));
                SoundPlayer.playSe("tanbourine");
            }
        }
        public override void getMessage(Message aMessage){
            if (aMessage.name == "editPlayButtonPushed"){//編曲編集ボタン
                parent.changeState(new EditState(parent));
                return;
            }
        }
    }
}