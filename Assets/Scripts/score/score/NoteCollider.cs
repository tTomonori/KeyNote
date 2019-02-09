using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCollider : MonoBehaviour {
    public float mQuarterBeat;
    private void OnMouseDown(){
        if(!(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))){
            Subject.sendMessage(new Message("clickNote", new Arg(new Dictionary<string, object>(){
            {"time",mQuarterBeat}
        })));
        }else{
            Subject.sendMessage(new Message("RightClickNote", new Arg(new Dictionary<string, object>(){
            {"time",mQuarterBeat}
        })));
        }
    }
}
