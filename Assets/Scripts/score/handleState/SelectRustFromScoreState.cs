using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class ScoreHandler {
    public class SelectRustFromScoreState : EditModeState{
        public SelectRustFromScoreState(ScoreHandler aParent) : base(aParent){}
        public override void enter(){
            AlartCreater.alart("サビの開始位置をクリックしてください");
            GameObject.Find("selectRustFromScoreButton").GetComponent<LightButton>().hold();
        }
        public override void exit(){
            GameObject.Find("selectRustFromScoreButton").GetComponent<LightButton>().release();
        }
        public override void update(){
            scrollScore();
        }
        public override void getMessage(Message aMessage){
            if(aMessage.name=="clickNote" || aMessage.name=="clickLyrics"){
                float tRust = MusicScoreData.quarterBeatToMusicTime(aMessage.getParameter<float>("time"));
                if(tRust<0){
                    AlartCreater.alart("音声再生開始前の位置です");
                    return;
                }
                if(parent.mPlayer.mMusicLength<=tRust){
                    AlartCreater.alart("音声の再生が終了している位置です");
                    return;
                }
                AlartCreater.alart("フォームの値を更新しました");
                mSettingForm.setRustForm(tRust.ToString());
                parent.changeState(new EditState(parent));
                return;
            }
            if (aMessage.name == "selectRustFromScoreButtonPushed"){//譜面からサビの開始位置を選択するボタン
                parent.changeState(new EditState(parent));
                return;
            }
        }
    }
}