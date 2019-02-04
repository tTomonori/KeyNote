using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MyBehaviour {
    public KeyTime mTime;
    private Beat[] mBeats;
	void Awake () {
        mBeats = GetComponentsInChildren<Beat>();
	}
	void Update () {
		
	}
    public void addNote(Arg aNoteData){
        KeyTime tTime = new KeyTime(aNoteData.get<float>("time"));
        mBeats[tTime.mBeatNumInBar].addNote(aNoteData);
    }
    public bool hit(KeyCode aKey,float aSecond,Note.HitNoteType aType){
        foreach(Beat tBeat in mBeats){
            if (tBeat.hit(aKey, aSecond, aType))
                return true;
        }
        return false;
    }
}
