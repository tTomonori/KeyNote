using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGuru : Note {
    private string mConsonant;
    private string mVowel;
    //音符が存在しないかどうか
    private bool mHittedConsonant = false;
    private bool mHittedVowel = false;
    protected override void display(Arg aNoteData){
        //画像
        SpriteRenderer[] tSprites = GetComponentsInChildren<SpriteRenderer>();
        tSprites[0].sprite = getNoteSprite(aNoteData.get<string>("vowel"));
        tSprites[1].sprite = getMininoteSprite(aNoteData.get<string>("vowel"));

        //テキスト
        TextMesh[] tTexts = GetComponentsInChildren<TextMesh>();
        mConsonant = aNoteData.get<string>("consonant");
        mVowel = aNoteData.get<string>("vowel");
        if(mConsonant==""){//子音がない場合は母音を子音として扱う
            mConsonant = mVowel;
            mVowel = "";
        }

        //子音のテキスト
        tTexts[0].text = mConsonant;
        if(mVowel==""){
            findChild<MyBehaviour>("vowel").delete();//母音の音符を削除
            mHittedVowel = true;
            return;
        }else{
            //母音のテキスト
            tTexts[1].text = mVowel;
        }
    }
    public override HitResult hit(KeyCode aKey,HitNoteType aType){
        //子音hit判定
        if(!mHittedConsonant){
            if (EnumParser.parse<KeyCode>(mConsonant) == aKey){
                mHittedConsonant = true;
                hitted(findChild("consonant"),aType);
                return HitResult.consonant;
            }
        }
        //母音hit判定
        if(!mHittedVowel){
            if (EnumParser.parse<KeyCode>(mVowel) == aKey){
                mHittedVowel = true;
                hitted(findChild("vowel"),aType);
                return HitResult.vowel;
            }
        }
        return HitResult.miss;
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
}
