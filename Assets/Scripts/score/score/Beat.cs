using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Beat : MyBehaviour {
    private KeyTime time;
    public KeyTime mTime{
        get { return time; }
        set {
            time = value;
            sendTimeToColliders();
        }
    }
    private bool mTriplet=false;
    public bool isTriplet() { return mTriplet; }
    private MyBehaviour mBeatObject;
    private Note[] mNotes;
    private LyricsBubble[] mLyricses;
    private ChangeBpmObject[] mBpms;
    private Transform[] mNotePositions;
    private Transform[] mLyricsPositions;
    private Transform[] mBpmPositions;
	void Awake () {
        createBeatObject();
	}
	void Update () {
		
	}
    private void createBeatObject(bool aTriplet=false){
        if (mBeatObject != null) mBeatObject.delete();
        mBeatObject = MyBehaviour.createObjectFromPrefab<MyBehaviour>("score/" + (aTriplet ? "beatTriplet" : "beat"));
        mBeatObject.transform.parent = this.gameObject.transform;
        mBeatObject.transform.localPosition = new Vector3(0, 0, 0);

        //配列初期化
        if (aTriplet) { 
            mNotes = new Note[3];
            mLyricses = new LyricsBubble[3];
            mBpms = new ChangeBpmObject[3];
        }
        else { 
            mNotes = new Note[4];
            mLyricses = new LyricsBubble[4];
            mBpms = new ChangeBpmObject[4];
        }
        //behaviour取得
        mNotePositions = mBeatObject.findChild("notes").GetComponent<MyBehaviour>().GetComponentsInChildrenWithoutSelf<Transform>();
        mLyricsPositions = mBeatObject.findChild("lyricses").GetComponent<MyBehaviour>().GetComponentsInChildrenWithoutSelf<Transform>();
        mBpmPositions = mBeatObject.findChild("bpms").GetComponent<MyBehaviour>().GetComponentsInChildrenWithoutSelf<Transform>();
        sendTimeToColliders();
    }
    //colliderにtimeを伝達
    private void sendTimeToColliders(){
        NoteCollider[] tNCollider = mBeatObject.GetComponentsInChildren<NoteCollider>();
        LyricsCollider[] tLCollider = mBeatObject.GetComponentsInChildren<LyricsCollider>();
        if(mTriplet){
            tNCollider[0].mQuarterBeat = time.mQuarterBeat + 0.1f;
            tNCollider[1].mQuarterBeat = time.mQuarterBeat + 1.3f;
            tNCollider[2].mQuarterBeat = time.mQuarterBeat + 2.6f;
            tLCollider[0].mQuarterBeat = time.mQuarterBeat + 0.1f;
            tLCollider[1].mQuarterBeat = time.mQuarterBeat + 1.3f;
            tLCollider[2].mQuarterBeat = time.mQuarterBeat + 2.6f;
        }else{
            tNCollider[0].mQuarterBeat = time.mQuarterBeat + 0;
            tNCollider[1].mQuarterBeat = time.mQuarterBeat + 1;
            tNCollider[2].mQuarterBeat = time.mQuarterBeat + 2;
            tNCollider[3].mQuarterBeat = time.mQuarterBeat + 3;
            tLCollider[0].mQuarterBeat = time.mQuarterBeat + 0;
            tLCollider[1].mQuarterBeat = time.mQuarterBeat + 1;
            tLCollider[2].mQuarterBeat = time.mQuarterBeat + 2;
            tLCollider[3].mQuarterBeat = time.mQuarterBeat + 3;
        }
    }
    //三連符がついているか確認してbeatを生成し直す
    private void checkTriplet(bool aTriplet){
        if (mTriplet == aTriplet) return;
        mTriplet = !mTriplet;
        createBeatObject(mTriplet);
    }
    //音符追加
    public void addNote(Arg aNoteData){
        //音符生成
        Note tNote = Note.create();
        tNote.setData(aNoteData);

        KeyTime tTime = aNoteData.get<KeyTime>("keyTime");
        //三連符判定
        checkTriplet(tTime.mIsInTriplet);
        //座標
        tNote.transform.parent = mNotePositions[(mTriplet) ? tTime.mQuarterBeatNumInTriplet : (int)tTime.mQuarterBeatInBeat];
        tNote.transform.localPosition = new Vector3(0, 0, -1);

        mNotes[tTime.mQuarterBeatIndexInBeat] = tNote;
    }
    //歌詞追加
    public void addLyrics(Arg aLyricsData){
        //歌詞生成
        LyricsBubble tLyrics = MyBehaviour.createObjectFromPrefab<LyricsBubble>("score/lyricsBubble");
        tLyrics.setData(aLyricsData);

        KeyTime tTime = aLyricsData.get<KeyTime>("keyTime");
        //三連符判定
        checkTriplet(tTime.mIsInTriplet);
        //座標
        tLyrics.transform.parent = mLyricsPositions[(mTriplet) ? tTime.mQuarterBeatNumInTriplet : (int)tTime.mQuarterBeatInBeat];
        tLyrics.transform.localPosition = new Vector3(0, 0, -1);

        mLyricses[tTime.mQuarterBeatIndexInBeat] = tLyrics;
    }
    //bpm変化を示すオブジェクト追加
    public void addChangeBpm(Arg aBpmData){
        //オブジェクト生成
        ChangeBpmObject tObject = MyBehaviour.createObjectFromPrefab<ChangeBpmObject>("score/changeBpmObject");
        tObject.setData(aBpmData);

        KeyTime tTime = new KeyTime(aBpmData.get<float>("time"));
        //三連符判定
        checkTriplet(tTime.mIsInTriplet);
        //座標
        tObject.transform.parent = mBpmPositions[(mTriplet) ? tTime.mQuarterBeatNumInTriplet : (int)tTime.mQuarterBeatInBeat];
        tObject.transform.localPosition = new Vector3(0, 0, -1);

        mBpms[tTime.mQuarterBeatIndexInBeat] = tObject;
    }
    //指定したQNを含むかどうか
    public bool isContainQuarterBeat(KeyTime aTime){
        if (aTime.mQuarterBeat < mTime.mQuarterBeat) return false;
        if (aTime.mQuarterBeat < mTime.mQuarterBeat + 4) return true;
        return false;
    }
    //キー入力
    public bool hit(KeyCode aKey,float aSecond,Note.HitNoteType aType){
        int tLength = (mTriplet) ? 3 : 4;
        for (int i = 0; i < tLength;i++){
            if (mNotes[i] == null) continue;//音符なし
            TypeEvaluation.Evaluation tEvaluation = TypeEvaluation.evaluate(aSecond, MusicScoreData.quarterBeatToMusicTime(mNotes[i].mCorrectQuarterBeat));
            if (tEvaluation == TypeEvaluation.Evaluation.miss) continue;//タイミングがあってない

            //タイミングOK
            Note.HitResult tHitResult = mNotes[i].hit(aKey, aType);
            if (tHitResult == Note.HitResult.miss) continue;//hitしなかった

            //hitしたメッセージ送信
            Subject.sendMessage(new Message("hittedNote", new Arg(new Dictionary<string, object>() {
                {"note", mNotes[i] } ,
                {"evaluation", tEvaluation },
                {"hitResult", tHitResult}
            })));
            return true;
        }
        return false;
    }
    //miss判定
    public void missHit(KeyTime aTime){
        foreach(Note tNote in mNotes){
            if (tNote == null) continue;//音符なし
            if (!(tNote.mCorrectQuarterBeat < aTime.mQuarterBeat)) continue;//キー入力のタイミングが過ぎていない

            Note.HitResult tResult = tNote.missHit();
            if (tResult == Note.HitResult.miss)
                continue;//既に評価されていた
            
            //missの評価にする
            Subject.sendMessage(new Message("missedNote", new Arg(new Dictionary<string, object>(){
                {"note",tNote},
                {"hitResult",tResult}
            })));
        }
    }
    //指定したKeyTimeの音符を取得
    public Note getNote(KeyTime aTime){
        if (mTriplet != aTime.mIsInTriplet) return null;
        return mNotes[aTime.mQuarterBeatIndexInBeat];
    }
    //指定したKeyTimeの歌詞を取得
    public LyricsBubble getLyrics(KeyTime aTime){
        if (mTriplet != aTime.mIsInTriplet) return null;
        return mLyricses[aTime.mQuarterBeatIndexInBeat];
    }
}
