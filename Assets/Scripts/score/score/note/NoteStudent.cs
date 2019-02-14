using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteStudent : Note {
    private string mVowel;
    protected override void display(Arg aNoteData){
        //画像
        SpriteRenderer tSprite = GetComponentInChildren<SpriteRenderer>();
        tSprite.sprite = getNoteSprite(aNoteData.get<string>("vowel"));
        //母音
        mVowel = aNoteData.get<string>("vowel");
        if (mVowel == "") mVowel = " ";
        GetComponentInChildren<TextMesh>().text = mVowel;
    }
    //音符にhit済みかどうか
    private bool mHitted = false;
    public override HitResult hit(KeyCode aKey,HitNoteType aType){
        if (mHitted) return HitResult.miss;//hit済み
        if (KeyMonitor.convertToCode(mVowel) != aKey)
            return HitResult.miss;//タイプミス
        mHitted = true;
        hitted(this.gameObject, aType);
        return HitResult.consonant;
    }
    //キー入力失敗(この音符をmiss判定にできるならtrue(既に評価がされていたらfalse))
    public override HitResult missHit(){
        if (mHitted) return HitResult.miss;
        mHitted = true;
        return HitResult.consonant;
    }
}
