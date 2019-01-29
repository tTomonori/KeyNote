using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGuru : Note {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void setData(Arg aNoteData){
        TextMesh[] tTexts = GetComponentsInChildren<TextMesh>();
        //子音
        tTexts[0].text = aNoteData.get<string>("consonant");
        //母音
        tTexts[1].text = aNoteData.get<string>("vowel");

        SpriteRenderer[] tSprites = GetComponentsInChildren<SpriteRenderer>();
        //画像
        tSprites[0].sprite = getNoteSprite(aNoteData.get<string>("vowel"));
        tSprites[1].sprite = getMininoteSprite(aNoteData.get<string>("vowel"));
    }
}
