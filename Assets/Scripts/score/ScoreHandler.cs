using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ScoreHandler : MyBehaviour{
    public KeyNotePlayer mPlayer;
    private MusicScore mScore;
    private ScoreHandleState mState;
    //背景画像
    private SpriteRenderer mBackground;
    private void Awake(){
        name = "scoreHandler";
        mState = new InitialState(this);
        Subject.addObserver(new Observer("scoreHandler", (message) =>{
            mState.getMessage(message);
        }));
    }
    private void OnDestroy(){
        Subject.removeObserver("scoreHandler");
    }
    public void load(string aFileName,ScoreDifficult aDifficult){
        //譜面
        mScore = MyBehaviour.create<MusicScore>();
        //曲情報ロード
        MusicScoreData.load(aFileName);
        MusicScoreData.mSelectedDifficult = aDifficult;
        //ミュージックプレイヤー
        MusicPlayer tPlayer = MyBehaviour.create<MusicPlayer>();
        tPlayer.setAudio(DataFolder.loadMusic(MusicScoreData.mMusicFileName));
        //譜面と曲を同期させるシステム
        mPlayer = new KeyNotePlayer(mScore,tPlayer);
        //背景
        resetScoreBackground();
    }
    public void set(MusicScoreFileData aData,ScoreDifficult aDifficult){
        //譜面
        mScore = MyBehaviour.create<MusicScore>();
        //曲情報ロード
        MusicScoreData.set(aData);
        MusicScoreData.mSelectedDifficult = aDifficult;
        //ミュージックプレイヤー
        MusicPlayer tPlayer = MyBehaviour.create<MusicPlayer>();
        tPlayer.setAudio(DataFolder.loadMusic(MusicScoreData.mMusicFileName));
        //譜面と曲を同期させるシステム
        mPlayer = new KeyNotePlayer(mScore, tPlayer);
        //背景
        resetScoreBackground();
    }
    //譜面の位置
    public void show(KeyTime aTime){
        mScore.show(aTime);
    }
    //状態遷移
    public void changeState(ScoreHandleState aState){
        if (mState != null) mState.exit();
        mState = aState;
        mState.enter();
    }
    //譜面の背景画像更新
    public void resetScoreBackground(){
        Sprite tSprite;
        if (MusicScoreData.mBack != "")
            tSprite = DataFolder.loadBackImage(MusicScoreData.mBack);
        else{
            if (Random.value > 0.5)
                tSprite = Resources.Load<Sprite>("sprites/default/blueScore");
            else 
                tSprite = Resources.Load<Sprite>("sprites/default/greenScore");
        }
        setBackground(tSprite);
    }
    //背景画像設定
    public void setBackground(Sprite aSprite){
        if(mBackground==null){
            MyBehaviour tBackground = MyBehaviour.create<MyBehaviour>();
            tBackground.name = "background";
            tBackground.transform.parent = this.transform;
            tBackground.position = new Vector3(0, 0, 10);
            mBackground = tBackground.gameObject.AddComponent<SpriteRenderer>();
            mBackground.color = new Color(0.8f, 0.8f, 0.8f);
        }
        mBackground.sprite = aSprite;
        mBackground.transform.localScale = new Vector3(16 / aSprite.bounds.size.x, 16 / aSprite.bounds.size.x, 1);
    }
    //キー入力
    public bool hit(KeyCode aKey, float aSecond, Note.HitNoteType aType,string aHitSound,string aMissSound){
        if(mScore.hit(aKey, mPlayer.mCurrentSecond, Note.HitNoteType.delete)){
            //hit
            if (aHitSound != "")
                SoundPlayer.playSe(aHitSound);
            return true;
        }else{
            //miss
            if (aMissSound != "")
                SoundPlayer.playSe(aMissSound);
            return false;
        }
    }
    private void Update(){
        mState.update();
    }
}
