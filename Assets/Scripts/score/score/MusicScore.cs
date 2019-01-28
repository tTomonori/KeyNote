using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScore : MonoBehaviour {
    //曲のデータ
    public Arg mMusicDate;
    //小節のオブジェクト
    private List<Bar> mSections;
    //X / 4 拍子
    public int mRhythm{
        get { return mMusicDate.get<int>("rhythm"); }
    }
	void Start () {
        mSections = new List<Bar>();
	}
	void Update () {
		
	}
    ///曲データロード
    public void load(string aFileName){
        mMusicDate = new Arg(MyJson.deserializeFile(Application.dataPath + "/../data/score/" + aFileName + ".json"));
    }
    public void show(int aTime){
        
    }
    //指定した小節に含まれる音符のデータを返す
    private List<Arg> getNotes(int aSectionNum){
        List<Arg> tNotes = new List<Arg>();
        int tTopTime = aSectionNum * mRhythm * 4;
        int tTailTime = tTopTime + mRhythm * 4 - 1;
        foreach(Arg tNote in mMusicDate.get<List<Arg>>("note")){
            float tNoteTime = tNote.get<float>("time");
            if (tNoteTime < tTopTime) continue;
            if (tTailTime < tNoteTime) break;
            tNotes.Add(tNote);
        }
        return tNotes;
    }
    //小節生成
    private void createSection(int aSectionNum){
        List<Arg> tNotes = getNotes(aSectionNum);
        Bar tSection = MyBehaviour.createObjectFromPrefab<Bar>("score/bar" + mRhythm.ToString());
    }
}
