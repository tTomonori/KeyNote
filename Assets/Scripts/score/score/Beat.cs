using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Beat : MyBehaviour {
    private bool mTriplet=false;
    private MyBehaviour mBeatObject;
    private Transform[] mNotes;
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

        mNotes = mBeatObject.findChild("notes").GetComponent<MyBehaviour>().GetComponentsInChildrenWithoutSelf<Transform>();
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
        tNote.transform.parent = mNotes[(mTriplet) ? tTime.mQuarterBeatNumInTriplet : (int)tTime.mQuarterBeatNumInBeat];
        tNote.transform.localPosition = new Vector3(0, 0, -1);
    }
}
