using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteEdit : Note {
    protected override void display(Arg aNoteData){
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
    //音符にhit済みかどうか
    private bool mHittedConsonant = false;
    private bool mHittedVowel = false;
    public override HitResult hit(KeyCode aKey,HitNoteType aType){
        HitResult tResult = HitResult.miss;
        //子音hit判定
        if(!mHittedConsonant){
            mHittedConsonant = true;
            hitted(findChild<MyBehaviour>("consonant"), aType);
            tResult = HitResult.consonant;
        }
        //母音hit判定
        if(!mHittedVowel){
                mHittedVowel = true;
            hitted(findChild<MyBehaviour>("vowel"),aType);
            if (tResult == HitResult.consonant)
                tResult = HitResult.consonantAndVowel;
            else
                tResult = HitResult.vowel;
        }
        return tResult;
    }
    //キー入力失敗(この音符をmiss判定にできるならtrue(既に評価がされていたらfalse))
    public override HitResult missHit(){
        if(!mHittedConsonant && !mHittedVowel){
            mHittedConsonant = true;
            mHittedVowel = true;
            return HitResult.consonantAndVowel;
        }
        if(!mHittedConsonant){
            mHittedConsonant = true;
            return HitResult.consonant;
        }
        if(!mHittedVowel){
            mHittedVowel = true;
            return HitResult.vowel;
        }
        return HitResult.miss;
    }
    protected override void hitAndDecolorize(MyBehaviour aNoteObject){
        if(aNoteObject.name=="consonant")
            aNoteObject.GetComponent<MyBehaviour>().findChild<SpriteRenderer>("note").sprite = Resources.Load<Sprite>("sprites/score/note/note0");
        else
            aNoteObject.GetComponent<MyBehaviour>().findChild<SpriteRenderer>("mininote").sprite = Resources.Load<Sprite>("sprites/score/mininote/mininote0");
    }
}
