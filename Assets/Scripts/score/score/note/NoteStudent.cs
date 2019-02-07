using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteStudent : Note {
    protected override void display(Arg aNoteData){
        //画像
        SpriteRenderer tSprite = GetComponentInChildren<SpriteRenderer>();
        tSprite.sprite = getNoteSprite(aNoteData.get<string>("vowel"));
    }
    //音符にhit済みかどうか
    private bool mHitted = false;
    public override HitResult hit(KeyCode aKey,HitNoteType aType){
        if (mHitted) return HitResult.miss;//hit済み
        if (EnumParser.parse<KeyCode>(mData.get<string>("vowel")) != aKey)
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
