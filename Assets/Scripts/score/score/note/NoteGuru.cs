using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGuru : Note {
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
        //子音hit判定
        if(!mHittedConsonant){
            if (EnumParser.parse<KeyCode>(mData.get<string>("consonant")) == aKey){
                mHittedConsonant = true;
                hitted(findChild("consonant"),aType);
                return HitResult.consonant;
            }
        }
        //母音hit判定
        if(!mHittedVowel){
            if (EnumParser.parse<KeyCode>(mData.get<string>("vowel")) == aKey){
                mHittedVowel = true;
                hitted(findChild("vowel"),aType);
                return HitResult.vowel;
            }
        }
        return HitResult.miss;
    }
}
