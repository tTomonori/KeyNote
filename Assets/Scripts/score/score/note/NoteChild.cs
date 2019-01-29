using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteChild : Note {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void setData(Arg aNoteData){
        //画像
        SpriteRenderer tSprite = GetComponentInChildren<SpriteRenderer>();
        tSprite.sprite = getNoteSprite(aNoteData.get<string>("vowel"));
    }
}
