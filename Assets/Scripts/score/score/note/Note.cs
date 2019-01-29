using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Note : MonoBehaviour {
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
    public abstract void setData(Arg aNoteData);
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
}
