using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MyBehaviour {
    private KeyTime time;
    public KeyTime mTime{
        get { return time; }
        set {
            time = value;
            sendTimeToBeats();
        }
    }
    private Beat[] mBeats;
	void Awake () {
        mBeats = GetComponentsInChildren<Beat>();
	}
    //timeをbeatに伝達
    private void sendTimeToBeats(){
        for (int i = 0; i < mBeats.Length;i++){
            mBeats[i].mTime = new KeyTime(time.mQuarterBeat + i * 4);
        }
    }
    //音符追加
    public void addNote(Arg aNoteData){
        KeyTime tTime = new KeyTime(aNoteData.get<float>("time"));
        mBeats[tTime.mBeatNumInBar].addNote(aNoteData);
    }
    //歌詞追加
    public void addLyrics(Arg aLyricsData){
        KeyTime tTime = new KeyTime(aLyricsData.get<float>("time"));
        mBeats[tTime.mBeatNumInBar].addLyrics(aLyricsData);
    }
    //bpm変化を示すオブジェクト追加
    public void addChangeBpm(Arg aBpmData){
        KeyTime tTime = new KeyTime(aBpmData.get<float>("time"));
        mBeats[tTime.mBeatNumInBar].addChangeBpm(aBpmData);
    }
    //キー入力
    public bool hit(KeyCode aKey,float aSecond,Note.HitNoteType aType){
        foreach(Beat tBeat in mBeats){
            if (tBeat.hit(aKey, aSecond, aType))
                return true;
        }
        return false;
    }
    //miss判定
    public void missHit(KeyTime aTime){
        foreach(Beat tBeat in mBeats){
            tBeat.missHit(aTime);
        }
    }
}
