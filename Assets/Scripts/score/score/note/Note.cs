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
        switch(MusicScoreData.mSelectedDifficult){
            case ScoreDifficult.child:
                return MyBehaviour.createObjectFromPrefab<NoteChild>("score/note/noteChild");
            case ScoreDifficult.student:
                return MyBehaviour.createObjectFromPrefab<NoteStudent>("score/note/noteStudent");
            case ScoreDifficult.scholar:
                return MyBehaviour.createObjectFromPrefab<NoteScholar>("score/note/noteScholar");
            case ScoreDifficult.guru:
                return MyBehaviour.createObjectFromPrefab<NoteGuru>("score/note/noteGuru");
            case ScoreDifficult.edit:
                return MyBehaviour.createObjectFromPrefab<NoteEdit>("score/note/noteEdit");
            default:
                throw new Exception("Note : 未定義の難易度「" + MusicScoreData.mSelectedDifficult + "」");
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
    public abstract HitResult hit(KeyCode aKey,HitNoteType aType);
    //音符にhitした
    protected void hitted(MyBehaviour aNoteObject,HitNoteType aType){
        switch(aType){
            case HitNoteType.delete:
                hitAndDelete(aNoteObject);
                break;
            case HitNoteType.decolorize:
                hitAndDecolorize(aNoteObject);
                break;
        }
    }
    //キー入力失敗(この音符をmiss判定にできるならtrue(既に評価がされていたらfalse))
    public abstract HitResult missHit();
    //消滅
    protected void hitAndDelete(MyBehaviour aNoteObject){
        //透明度
        aNoteObject.opacityBy(-1, 0.4f, () => {
            aNoteObject.delete();
        });
        //大きさ
        aNoteObject.scaleBy(new Vector3(0.3f, 0.5f, 0), 0.2f, null);
    }
    //脱色
    protected virtual void hitAndDecolorize(MyBehaviour aNoteObject){
        Debug.Log("decolorize");
    }
    public enum HitResult{
        consonantAndVowel,consonant,vowel,miss
    }
}