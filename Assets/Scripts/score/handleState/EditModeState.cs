using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

partial class ScoreHandler{
    public class EditModeState : ScoreHandleState{
        public EditModeState(ScoreHandler aParent) : base(aParent) { }
        static protected MyCommandList mCommandList;
        public override void enter(){
            mCommandList = new MyCommandList();
            parent.changeState(new EditState(parent));
        }
        //音符を生成する
        protected bool tryCreateNote(KeyTime aTime){
            float[] tNeighbor = parent.mScore.getNeighborTime(aTime);
            foreach(float tQuarterBeat in tNeighbor){
                if (parent.mScore.getNote(new KeyTime(tQuarterBeat)) != null) continue;//既に音符が存在する
                //追加できる
                mCommandList.run(new CreateNoteCommand(new KeyTime(tQuarterBeat)));
                parent.mScore.resetBars();
                return true;
            }
            return false;
        }
        //音符を削除する
        protected bool tryDeleteNote(KeyTime aTime){
            float[] tNeighbor = parent.mScore.getNeighborTime(aTime);
            foreach (float tQuarterBeat in tNeighbor){
                if (parent.mScore.getNote(new KeyTime(tQuarterBeat)) == null) continue;//音符がない
                //削除できる
                mCommandList.run(new DeleteNoteCommand(new KeyTime(tQuarterBeat)));
                parent.mScore.resetBars();
                return true;
            }
            return false;
        }
        //歌詞を生成する
        protected bool tryCreateLyrics(KeyTime aTime){
            float[] tNeighbor = parent.mScore.getNeighborTime(aTime);
            foreach (float tQuarterBeat in tNeighbor){
                if (parent.mScore.getLyrics(new KeyTime(tQuarterBeat)) != null) continue;//既に歌詞が存在する
                //追加できる
                mCommandList.run(new CreateLyricsCommand(new KeyTime(tQuarterBeat)));
                parent.mScore.resetBars();
                return true;
            }
            return false;
        }
        //歌詞を削除する
        protected bool tryDeleteLyrics(KeyTime aTime){
            float[] tNeighbor = parent.mScore.getNeighborTime(aTime);
            foreach (float tQuarterBeat in tNeighbor){
                if (parent.mScore.getLyrics(new KeyTime(tQuarterBeat)) == null) continue;//歌詞がない
                //削除できる
                mCommandList.run(new DeleteLyricsCommand(new KeyTime(tQuarterBeat)));
                parent.mScore.resetBars();
                return true;
            }
            return false;
        }
        //bpm変更イベントを生成
        protected bool tryCreateChangeBpm(KeyTime aTime){
            //編集画面の操作を無効にする
            parent.changeState(new InitialState(parent));
            //変更前の値があるなら取得する
            float tPreBpm = -1;
            List<Arg> tList = MusicScoreData.getChangeBpmInBar(aTime);
            foreach (Arg tData in tList){
                if (tData.get<float>("time") != aTime.mQuarterBeat) continue;
                //イベントが見つかった
                tPreBpm = tData.get<float>("bpm");
                break;
            }
            //bpm値の入力を受け付ける
            MySceneManager.openScene("inputChangeBpmValueForm", new Arg(new Dictionary<string, object>() { { "defaultValue", tPreBpm } }), null, (aArg) =>{
                if(aArg.get<bool>("change")){
                    mCommandList.run(new CreateChangeBpmCommand(aTime, aArg.get<float>("bpm")));
                    parent.mScore.resetBars();
                }
                parent.changeState(new EditState(parent));
            });
            return true;
        }
        //bpm変更イベントを削除
        protected bool tryDeleteChangeBpm(KeyTime aTime){
            List<Arg> tList = MusicScoreData.getChangeBpmInBar(aTime);
            foreach (Arg tData in tList){
                if (tData.get<float>("time") != aTime.mQuarterBeat) continue;
                //削除するイベントが見つかった
                mCommandList.run(new DeleteChangeBpmCommand(aTime));
                parent.mScore.resetBars();
                return true;
            }
            return false;
        }
        //三連符を作成する
        protected bool tryCreateTriplet(KeyTime aTime){
            return false;
        }
        //三連符を削除する
        protected bool tryDeleteTriplet(KeyTime aTime){
            return false;
        }
    }
}