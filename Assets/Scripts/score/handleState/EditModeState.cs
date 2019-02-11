using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

partial class ScoreHandler{
    public class EditModeState : ScoreHandleState{
        public EditModeState(ScoreHandler aParent) : base(aParent) { }
        static protected MyCommandList mCommandList;
        static protected ToggleButtonGroup mPlaceTggle;
        static protected MusicSettingForm mSettingForm;
        public override void enter(){
            //コマンドリスト生成
            mCommandList = new MyCommandList();
            //配置オブジェクト選択欄
            mPlaceTggle = GameObject.Find("placeObjectToggle").GetComponent<ToggleButtonGroup>();
            mPlaceTggle.memberPushed("note");//配置するオブジェクトの初期設定
            //設定欄
            mSettingForm = GameObject.Find("musicSettingForm").GetComponent<MusicSettingForm>();
            mSettingForm.reset();//設定欄表示リセット
            //編集モードに即移行
            parent.changeState(new EditState(parent));
        }
        //マウス回転量を取得し譜面をスクロール
        protected void scrollScore(){
            float tScroll = Input.mouseScrollDelta.y;
            parent.mScore.positionY -= tScroll;
            //負の位置まではスクロールできないようにする
            if (parent.mScore.mCurrentQuarterBeat < 0)
                parent.mScore.mCurrentQuarterBeat = 0;
        }
        //音符を生成する
        protected bool tryCreateNote(KeyTime aTime){
            if (aTime.mQuarterBeat < 0) return false;
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
            if (aTime.mQuarterBeat < 0) return false;
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
            if (aTime.mQuarterBeat < 0) return false;
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
            if (aTime.mQuarterBeat < 0) return false;
            float[] tNeighbor = parent.mScore.getNeighborTime(aTime);
            foreach (float tQuarterBeat in tNeighbor){
                if (parent.mScore.getLyrics(new KeyTime(tQuarterBeat)) == null) continue;//歌詞がない
                if(parent.mScore.getNote(new KeyTime(tQuarterBeat))!=null){
                    //消そうとした歌詞に音符がついてる
                    AlartCreater.alart("音符に付属した歌詞は削除できません");
                    return false;
                }
                //削除できる
                mCommandList.run(new DeleteLyricsCommand(new KeyTime(tQuarterBeat)));
                parent.mScore.resetBars();
                return true;
            }
            return false;
        }
        //bpm変更イベントを生成
        protected bool tryCreateChangeBpm(KeyTime aTime){
            if (aTime.mQuarterBeat < 0) return false;
            if(1<=aTime.mQuarterBeat && aTime.mQuarterBeat<MusicScoreData.mStartPlayMusicTime.mQuarterBeat){
                AlartCreater.alart("音声再生位置より前には配置できません");
                return false;
            }
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
            if (aTime.mQuarterBeat < 0) return false;
            if(aTime.mQuarterBeat < 1){
                AlartCreater.alart("先頭のBPMは削除できません");
                return false;
            }
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
            Beat tBeat = parent.mScore.getBar(aTime).getBeat(aTime);
            if(tBeat.isEmpty()){
                //オブジェクトが配置されていない場合はCommandListには追加しない
                tBeat.checkTriplet(true);
                return true;
            }
            if(tBeat.isTriplet()){
                //既に三連符がつけられている
                return false;
            }
            mCommandList.run(new CreateTripletCommand(aTime));
            parent.mScore.resetBars();
            return false;
        }
        //三連符を削除する
        protected bool tryDeleteTriplet(KeyTime aTime){
            Beat tBeat = parent.mScore.getBar(aTime).getBeat(aTime);
            if (tBeat.isEmpty()){
                //オブジェクトが配置されていない場合はCommandListには追加しない
                tBeat.checkTriplet(false);
                return true;
            }
            if (!tBeat.isTriplet()){
                //三連符がついていない
                return false;
            }
            mCommandList.run(new DeleteTripletCommand(aTime));
            parent.mScore.resetBars();
            return false;
        }
    }
}