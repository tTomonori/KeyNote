using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

partial class ScoreHandler{
    public class EditState : EditModeState{
        public EditState(ScoreHandler aParent) : base(aParent) { }
        public CreateObjectType mCreateObjectType{
            get { return EnumParser.parse<CreateObjectType>(mPlaceTggle.pushedButtonName); }
        }
        public override void enter(){
        }
        public override void update(){
            scrollScore();
            //command操作
            if(Input.GetKeyDown(KeyCode.Z)){
                //cmd or ctr を押しているか
                if(Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.RightCommand) || 
                   Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)){
                    //shiftを押しているか
                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                        mCommandList.redo();
                    else
                        mCommandList.undo();
                    parent.mScore.resetBars();
                }
            }
        }
        public override void getMessage(Message aMessage){
            //logClickPosition(aMessage);
            if(aMessage.name=="editPlayButtonPushed"){//編曲再生ボタン
                parent.changeState(new EditPlayState(parent));
                return;
            }
            if(aMessage.name=="testPlayButtonPushed"){//テスト再生ボタン
                parent.changeState(new TestPlayState(parent));
                return;
            }
            if (aMessage.name == "measureBpmButtonPushed"){//bpm測定ボタン
                parent.changeState(new InitialState(parent));
                MySceneManager.openScene("measureBpm", new Arg(new Dictionary<string,object>(){
                    {"second",MusicScoreData.quarterBeatToMusicTime(parent.mScore.mCurrentQuarterBeat)}
                }), null, (obj) =>{
                    parent.changeState(new EditState(parent));
                });
                return;
            }
            if(aMessage.name=="editLyricsButtonPushed"){//歌詞編集ボタン
                parent.changeState(new InitialState(parent));
                MySceneManager.openScene("editLyrics",null,null,(aArg) => {
                    mCommandList.reset();
                    parent.mScore.resetBars();
                    parent.changeState(new EditState(parent));
                });
                return;
            }
            if(aMessage.name=="configButtonPushed"){//File設定ボタン
                parent.changeState(new InitialState(parent));
                MySceneManager.openScene("musicConfig", new Arg(new Dictionary<string, object>(){
                    {"initialize",true},
                    {"title",MusicScoreData.mTitle},
                    {"file",MusicScoreData.mSavePath},
                    {"music",MusicScoreData.mMusicFileName},
                    {"thumbnail",MusicScoreData.mThumbnail},
                    {"back",MusicScoreData.mBack},
                    {"movie",MusicScoreData.mMovie},
                    {"originalFile",MusicScoreData.mOriginalFileName}
                }), null, (aArg) =>{
                    if (aArg.get<bool>("ok")){
                        MusicScoreFileData tData = aArg.get<MusicScoreFileData>("scoreData");
                        MusicScoreData.mTitle = tData.title;
                        MusicScoreData.mSavePath = tData.fileName;
                        MusicScoreData.mMusicFileName = tData.music;
                        MusicScoreData.mThumbnail = tData.thumbnail;
                        MusicScoreData.mBack = tData.back;
                        MusicScoreData.mMovie = tData.movie;
                    }
                    parent.changeState(new EditState(parent));
                });
                return;
            }
            if(aMessage.name=="selectRustFromScoreButtonPushed"){//譜面からサビの開始位置を選択するボタン
                parent.changeState(new SelectRustFromScoreState(parent));
                return;
            }
            if(aMessage.name=="applySettingButtonPushed"){//設定適用ボタン
                if(mSettingForm.isChanged()){//変更がある時だけ適用
                    //marginが不正な値になっていないか
                    if(mSettingForm.mMargin<0){
                        AlartCreater.alart("Marginは0未満にできません");
                        return;
                    }
                    if(KeyTime.secondsToQuarterBeat(mSettingForm.mMargin,MusicScoreData.mInitialBpm)>=mSettingForm.mRhythm*4){
                        AlartCreater.alart("Marginが第一小節の長さを超えています");
                        return;
                    }
                    //rustが不正な値になっていないか
                    if(mSettingForm.mRust<0){
                        AlartCreater.alart("サビの位置は0以上で指定してください");
                        return;
                    }
                    if(parent.mPlayer.mMusicLength<=mSettingForm.mRust){
                        AlartCreater.alart("サビの位置が音声の長さを超えています");
                        return;
                    }
                    AlartCreater.alart("設定を適用しました");
                    mCommandList.run(new ApplySettingCommand(mSettingForm));
                    parent.mScore.resetBars();
                }else{
                    AlartCreater.alart("値が変更されていません");
                }
                return;
            }
            if(aMessage.name=="resetSettingButtonPushed"){//設定リセットボタン
                mSettingForm.reset();
                return;
            }
            if(aMessage.name=="saveButtonPushed"){//保存ボタン
                parent.changeState(new SaveState(parent));
                return;
            }
            //譜面クリック
            switch(mCreateObjectType){
                case CreateObjectType.note:
                    if (aMessage.name == "clickNote" || aMessage.name == "clickLyrics"){
                        tryCreateNote(new KeyTime(aMessage.getParameter<float>("time")));
                        return;
                    }
                    if (aMessage.name == "RightClickNote" || aMessage.name == "RightClickLyrics"){
                        tryDeleteNote(new KeyTime(aMessage.getParameter<float>("time")));
                        return;
                    }
                    break;
                case CreateObjectType.lyrics:
                    if (aMessage.name == "clickNote" || aMessage.name == "clickLyrics"){
                        tryCreateLyrics(new KeyTime(aMessage.getParameter<float>("time")));
                        return;
                    }
                    if (aMessage.name == "RightClickNote" || aMessage.name == "RightClickLyrics"){
                        tryDeleteLyrics(new KeyTime(aMessage.getParameter<float>("time")));
                        return;
                    }
                    break;
                case CreateObjectType.changeBpm:
                    if (aMessage.name == "clickNote" || aMessage.name == "clickLyrics"){
                        tryCreateChangeBpm(new KeyTime(aMessage.getParameter<float>("time")));
                        return;
                    }
                    if (aMessage.name == "RightClickNote" || aMessage.name == "RightClickLyrics"){
                        tryDeleteChangeBpm(new KeyTime(aMessage.getParameter<float>("time")));
                        return;
                    }
                    break;
                case CreateObjectType.triplet:
                    if (aMessage.name == "clickNote" || aMessage.name == "clickLyrics"){
                        tryCreateTriplet(new KeyTime(aMessage.getParameter<float>("time")));
                        return;
                    }
                    if (aMessage.name == "RightClickNote" || aMessage.name == "RightClickLyrics"){
                        tryDeleteTriplet(new KeyTime(aMessage.getParameter<float>("time")));
                        return;
                    }
                    break;
            }
        }
        public enum CreateObjectType{
            triplet,changeBpm,note,lyrics
        }
        //クリックしたQNを表示(デバッグ用)
        private void logClickPosition(Message aMessage){
            if (aMessage.name == "clickNote"){
                Debug.Log("note : " + aMessage.getParameter<float>("time"));
                return;
            }
            if (aMessage.name == "clickLyrics"){
                Debug.Log("lyrics : " + aMessage.getParameter<float>("time"));
                return;
            }
            if (aMessage.name == "RightClickNote"){
                Debug.Log("note R : " + aMessage.getParameter<float>("time"));
                return;
            }
            if (aMessage.name == "RightClickLyrics"){
                Debug.Log("lyrics R : " + aMessage.getParameter<float>("time"));
                return;
            }
        }
    }
}