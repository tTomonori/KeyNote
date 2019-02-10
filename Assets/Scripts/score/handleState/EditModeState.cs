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
                if (parent.mScore.getLyrics(new KeyTime(tQuarterBeat)) != null) continue;//既に音符が存在する
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
                if (parent.mScore.getLyrics(new KeyTime(tQuarterBeat)) == null) continue;//音符がない
                //削除できる
                mCommandList.run(new DeleteLyricsCommand(new KeyTime(tQuarterBeat)));
                parent.mScore.resetBars();
                return true;
            }
            return false;
        }
        //bpm変更イベントを生成
        protected bool tryCreateChangeBpm(KeyTime aTime){
            return false;
        }
        //bpm変更イベントを削除
        protected bool tryDeleteChangeBpm(KeyTime aTime){
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