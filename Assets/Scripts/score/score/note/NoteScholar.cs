using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScholar : Note {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void setData(Arg aNoteData){
        //子音
        TextMesh tText = GetComponentInChildren<TextMesh>();
        tText.text = aNoteData.get<string>("consonant");
        //画像
        SpriteRenderer tSprite = GetComponentInChildren<SpriteRenderer>();
        tSprite.sprite = getNoteSprite(aNoteData.get<string>("vowel"));
    }
}