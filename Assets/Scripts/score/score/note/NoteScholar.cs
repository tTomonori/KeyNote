using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScholar : Note {
    private string mConsonant;
    protected override void display(Arg aNoteData){
        mConsonant = aNoteData.get<string>("consonant");
        if (mConsonant == "") mConsonant = aNoteData.get<string>("vowel");//子音がないなら母音を使う
        if (mConsonant == "") mConsonant = " ";
        //子音
        TextMesh tText = GetComponentInChildren<TextMesh>();
        tText.text = mConsonant;
        //画像
        SpriteRenderer tSprite = GetComponentInChildren<SpriteRenderer>();
        tSprite.sprite = getNoteSprite(aNoteData.get<string>("vowel"));
    }
    //音符にhit済みかどうか
    private bool mHitted = false;
    public override HitResult hit(KeyCode aKey,HitNoteType aType){
        if (mHitted) return HitResult.miss;//hit済み
        if (KeyMonitor.convertToCode(mConsonant) != aKey)
            return HitResult.miss;//タイプミス
        mHitted = true;
        hitted(this, aType);
        return HitResult.consonant;
    }
    //キー入力失敗(この音符をmiss判定にできるならtrue(既に評価がされていたらfalse))
    public override HitResult missHit(){
        if (mHitted) return HitResult.miss;
        mHitted = true;
        return HitResult.consonant;
    }
}