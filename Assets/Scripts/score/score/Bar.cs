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
}
