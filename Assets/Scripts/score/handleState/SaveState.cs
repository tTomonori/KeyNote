using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class ScoreHandler {
    public class SaveState : ScoreHandleState{
        public SaveState(ScoreHandler aParent) : base(aParent){}
        public override void enter(){
            MySceneManager.openScene("saveConfirmForm", null, null, (aArg) =>{
                if (aArg.get<bool>("save"))
                    save();
                if(aArg.get<bool>("continue")){
                    parent.changeState(new EditState(parent));
                    return;
                }else{
                    MySceneManager.changeScene("selection");
                }
            });
        }
        public override void update(){
            
        }
        public override void getMessage(Message aMessage){
            
        }
        private void save(){
            MusicScoreData.save();
        }
    }
}