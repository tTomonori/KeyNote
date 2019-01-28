using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLabel : MyBehaviour {
    public int mNum;
    public void setLabel(string aLabel){
        GetComponent<TextMesh>().text = aLabel;
    }
    private void OnMouseDown(){
        Subject.sendMessage(new Message("clickMusicLabel", new Arg(new Dictionary<string, object>() { { "num", mNum } })));
    }
}
