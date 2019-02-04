using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScholar : Note {
    protected override void display(Arg aNoteData){
        //子音
        TextMesh tText = GetComponentInChildren<TextMesh>();
        tText.text = aNoteData.get<string>("consonant");
        //画像
        SpriteRenderer tSprite = GetComponentInChildren<SpriteRenderer>();
        tSprite.sprite = getNoteSprite(aNoteData.get<string>("vowel"));
    }
    //音符にhit済みかどうか
    private bool mHitted = false;
    public override bool hit(KeyCode aKey,HitNoteType aType){
        if (mHitted) return false;//hit済み
        if (KeyMonitor.convertToCode(mData.get<string>("consonant")) != aKey)
            return false;//タイプミス
        mHitted = true;
        hitted(this.gameObject, aType);
        return true;
    }
}