using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Note : MyBehaviour {
    public enum HitNoteType{
        delete,decolorize
    }
    //音符データ
    protected Arg mData;
    //正確なQN
    public float mCorrectQuarterBeat{
        get { return mData.get<KeyTime>("keyTime").mCorrectQuarterBeat; }
    }
    static public Note create(){
        switch(MusicScoreData.mDifficult){
            case MusicScoreData.Difficult.child:
                return MyBehaviour.createObjectFromPrefab<NoteChild>("score/note/noteChild");
            case MusicScoreData.Difficult.student:
                return MyBehaviour.createObjectFromPrefab<NoteStudent>("score/note/noteStudent");
            case MusicScoreData.Difficult.scholar:
                return MyBehaviour.createObjectFromPrefab<NoteScholar>("score/note/noteScholar");
            case MusicScoreData.Difficult.guru:
                return MyBehaviour.createObjectFromPrefab<NoteGuru>("score/note/noteGuru");
            default:
                throw new Exception("Note : 未定義の難易度「" + MusicScoreData.mDifficult + "」");
        }
    }
    //音符データ適用
    public void setData(Arg aNoteData){
        mData = aNoteData;
        display(aNoteData);
    }
    protected abstract void display(Arg aNoteData);
    //音符の画像取得
    public Sprite getNoteSprite(string aVowel){
        string tNum;
        switch(aVowel){
            case "a":tNum = "4";break;
            case "i":tNum = "3"; break;
            case "u":tNum = "2"; break;
            case "e":tNum = "1"; break;
            case "o":tNum = "5"; break;
            default:tNum = "8"; break;
        }
        return Resources.Load<Sprite>("sprites/score/note/note" + tNum);
    }
    //小さい音符の画像取得
    public Sprite getMininoteSprite(string aVowel){
        string tNum;
        switch(aVowel){
            case "a":tNum = "4";break;
            case "i":tNum = "3"; break;
            case "u":tNum = "2"; break;
            case "e":tNum = "1"; break;
            case "o":tNum = "5"; break;
            default:tNum = "8"; break;
        }
        return Resources.Load<Sprite>("sprites/score/mininote/mininote" + tNum);
    }
    //音符にhitするか
    public abstract bool hit(KeyCode aKey,HitNoteType aType);
    //音符にhitした
    protected void hitted(GameObject aNoteObject,HitNoteType aType){
        switch(aType){
            case HitNoteType.delete:
                hitAndDelete(aNoteObject);
                break;
            case HitNoteType.decolorize:
                hitAndDecolorize(aNoteObject);
                break;
        }
    }
    //消滅
    protected void hitAndDelete(GameObject aNoteObject){
        aNoteObject.GetComponent<MyBehaviour>().delete();
    }
    //脱色
    protected void hitAndDecolorize(GameObject aNoteObject){
        Debug.Log("decolorize");
    }
}
