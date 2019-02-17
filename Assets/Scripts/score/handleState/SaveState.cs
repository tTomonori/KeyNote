using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class ScoreHandler {
    public class SaveState : ScoreHandleState{
        public SaveState(ScoreHandler aParent) : base(aParent){}
        public override void enter(){
            MySceneManager.openScene("saveConfirmForm", null, null, (aArg) =>{
                if (aArg.get<bool>("save"))
                    save(isSelectedComplete());
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
        //完成が選択されている
        private bool isSelectedComplete(){
            return GameObject.Find("completeListButton").GetComponent<ListButton>().mSelected == "完成";
        }
        private void save(bool aCompletion){
            //楽曲リスト更新
            if(aCompletion){
                //完成
                if (MusicScoreData.isSaved){
                    MusicList.update(MusicScoreData.mLoadPath, MusicScoreData.mTitle, MusicScoreData.mSavePath);
                }
                else{
                    MusicList.addScore(MusicScoreData.mTitle, MusicScoreData.mSavePath);
                    MusicList.updateLastPlay(MusicList.mLength - 1, MusicList.mLastPlayDifficult);//追加した曲を最後に遊んだ曲とする
                }
            }else{
                //未完成
                MusicList.remove(MusicScoreData.mLoadPath);
            }
            //楽曲情報更新
            //難易度
            foreach(KeyValuePair<ScoreDifficult,int> tPare in DifficultCalculator.calculateDifficult(MusicScoreData.mNotes)){
                MusicScoreData.setDifficult(tPare.Key, tPare.Value);
            }
            MusicScoreData.save();
        }
    }
}