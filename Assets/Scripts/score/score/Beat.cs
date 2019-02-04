using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Beat : MyBehaviour {
    private bool mTriplet=false;
    private MyBehaviour mBeatObject;
    private Note[] mNotes;
    private Transform[] mNotePositions;
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

        if (aTriplet) mNotes = new Note[3];
        else mNotes = new Note[4];
        mNotePositions = mBeatObject.findChild("notes").GetComponent<MyBehaviour>().GetComponentsInChildrenWithoutSelf<Transform>();
    }
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
        tNote.transform.parent = mNotePositions[(mTriplet) ? tTime.mQuarterBeatNumInTriplet : (int)tTime.mQuarterBeatNumInBeat];
        tNote.transform.localPosition = new Vector3(0, 0, -1);

        mNotes[tTime.mQuarterBeatIndexInBeat] = tNote;
    }
    public bool hit(KeyCode aKey,float aSecond,Note.HitNoteType aType){
        int tLength = (mTriplet) ? 3 : 4;
        for (int i = 0; i < tLength;i++){
            if (mNotes[i] == null) continue;//音符なし
            TypeEvaluation.Evaluation tEvaluation = TypeEvaluation.evaluate(aSecond, MusicScoreData.quarterBeatToMusicTime(mNotes[i].mCorrectQuarterBeat));
            if (tEvaluation == TypeEvaluation.Evaluation.miss) continue;//タイミングがあってない

            //タイミングOK
            if (mNotes[i].hit(aKey, aType)) return true;//音符にhitしたか
        }
        return false;
    }
}
