using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

static public class LyricsStringConverter {
    //変換する歌詞
    static private string mInput;
    //変換する歌詞のながさ
    static private int mLength;
    //変換中の位置(文字の読み出しを完了した位置)
    static private int mIndex;
    //変換中の音符データ
    static private List<Arg> mNote;
    //変換中の歌詞データ
    static private List<Arg> mLyrics;
    //歌詞を音符や歌詞バブルに表示する単位ごとに分割する
    static public string convert(string aLyrics, out List<Arg> oNoteKeys, out List<Arg> oLyricsChars){
        //初期化
        mInput = aLyrics;
        mLength = mInput.Length;
        mIndex = -1;
        mNote = new List<Arg>();
        mLyrics = new List<Arg>();
        //変換
        string tError = convert();
        oNoteKeys = mNote;
        oLyricsChars = mLyrics;
        //初期化
        mInput = "";
        mLength = 0;
        mIndex = -1;
        mNote = null;
        mLyrics = null;
        return tError;
    }
    static private string convert(){
        while(mIndex<mLength-1){
            string tLyrics;
            string tConsonant;
            string tVowel;
            string tError = loadNextSyllable(out tLyrics, out tConsonant, out tVowel);
            if(tError!="")return tError;
            mNote.Add(new Arg(new Dictionary<string, object>() { { "consonant", tConsonant }, { "vowel", tVowel } }));
            mLyrics.Add(new Arg(new Dictionary<string, object>() { { "char", tLyrics } }));
        }
        return "";
    }
    //次の一音を読む
    static private string loadNextSyllable(out string oLyrics,out string oConsonant,out string oVowel){
        string tKey;
        string tError = loadNextSyllableInput(out oLyrics, out tKey);
        if(tError!=""){
            oConsonant = "";
            oVowel = "";
            return tError;
        }
        if(tKey.Length>0){
            //key指定で母音子音決定
            tKey = StringCaseConverter.ToLower(tKey);//全部小文字にする
            return decomposeKey(tKey, out oConsonant, out oVowel);
        }
        //歌詞から母音子音決定
        switch(analysePhonemeLanguage(oLyrics[0])){
            case PhonemeLanguage.ja:
                decomposeSyllable(extractTopSyllable(StringCaseConverter.ToHiragana(oLyrics)), out oConsonant, out oVowel);
                return "";
            case PhonemeLanguage.en:
                decomposeAlphabet(StringCaseConverter.ToLower(oLyrics), out oConsonant, out oVowel);
                return "";
            case PhonemeLanguage.other:
                oConsonant = "";
                oVowel = "";
                return "";
        }
        throw new Exception("LyricsStringConverter : ここは実行されないはず");
    }
    //次の一音を読み込む
    static private string loadNextSyllableInput(out string oLyrics,out string oKey){
        //////////
        //歌詞を読む
        oLyrics = "";
        switch(confirmNextChar()){
            //括弧内を一音にする
            case '(':
                loadNextChar();
                while(true){
                    char tNext = loadNextChar();
                    if(tNext=='\x1a'){
                        oKey = "";
                        return "(一音)括弧が閉じられていない";
                    }
                    if (tNext == ')') break;
                    oLyrics += tNext;
                }
                break;
            case ' ':
            case '_':
                loadNextChar();
                oLyrics = " ";
                break;
            default:
                oLyrics += loadNextChar();
                //次が小文字ならそれもロードする
                if (isLowerJa(confirmNextChar()))
                    oLyrics += loadNextChar();
                break;
        }
        //////////
        //keyを読む
        oKey = "";
        if(confirmNextChar()=='<'){
            loadNextChar();
            while(true){
                char tNext = loadNextChar();
                if (tNext == '\x1a'){
                    return "<Key指定>括弧が閉じられていない";
                }
                if (tNext == '>') break;
                oKey += tNext;
            }
        }
        return "";
    }
    //key指定で母音と子音を決定
    static private string decomposeKey(string aKey, out string oConsonant, out string oVowel){
        if(aKey.Length==0){
            oConsonant = "";
            oVowel="";
            return "";
        }
        if(aKey.Length==1){
            switch(extractPhoneme(aKey[0])){
                case Phoneme.consonant:
                    oConsonant = aKey;
                    oVowel = "";
                    return "";
                case Phoneme.vowel:
                    oConsonant = "";
                    oVowel = aKey;
                    return "";
                case Phoneme.invalid:
                    oConsonant = "";
                    oVowel = "";
                    return "<Key指定>不正な文字";
            }
        }
        if(aKey.Length==2){
            switch (extractPhoneme(aKey[0])){
                case Phoneme.consonant://１文字目が子音
                    oConsonant = aKey[0].ToString();
                    switch (extractPhoneme(aKey[1])){
                        case Phoneme.consonant://2文字目が子音
                            oVowel = "";
                            return "<Key指定>子音２文字指定は不可";
                        case Phoneme.vowel://2文字目が母音
                            oVowel = aKey[1].ToString();
                            return "";
                        case Phoneme.invalid:
                            oConsonant = "";
                            oVowel = "";
                            return "<Key指定>2文字目が不正な文字";
                    }
                    oVowel = "";
                    return "LyricsStringConverter : ここは実行されないはず";
                case Phoneme.vowel://１文字目が母音
                    oVowel = aKey;
                    switch (extractPhoneme(aKey[1])){
                        case Phoneme.consonant://2文字目が子音
                            oConsonant = aKey[1].ToString();
                            return "";
                        case Phoneme.vowel://2文字目が母音
                            oConsonant = "";
                            return "<Key指定>母音２文字指定は不可";
                        case Phoneme.invalid:
                            oConsonant = "";
                            oVowel = "";
                            return "<Key指定>2文字目が不正な文字";
                    }
                    oConsonant = "";
                    return "LyricsStringConverter : ここは実行されないはず";
                case Phoneme.invalid://１文字目が不正
                    oConsonant = "";
                    oVowel = "";
                    return "<Key指定>１文字目が不正な文字";
            }
        }
        oConsonant = "";
        oVowel = "";
        return "<Key指定>３文字以上の指定は不可";
    }
    //一音を子音と母音に分解する
    static private void decomposeSyllable(string aSyllable,out string oConsonant,out string oVowel){
        switch(aSyllable){
            case "じゃ":oConsonant = "j"; oVowel = "a"; return;
            case "じゅ": oConsonant = "j"; oVowel = "u"; return;
            case "じょ": oConsonant = "j"; oVowel = "o"; return;
            case "ちゃ": oConsonant = "c"; oVowel = "a"; return;
            case "ちゅ": oConsonant = "c"; oVowel = "u"; return;
            case "ちょ": oConsonant = "c"; oVowel = "o"; return;
            case "っ": oConsonant = "x"; oVowel = ""; return;
            case "ゔ": oConsonant = "v"; oVowel = "u"; return;
            case "ゔぁ": oConsonant = "v"; oVowel = "a"; return;
            case "ゔぃ": oConsonant = "v"; oVowel = "i"; return;
            case "ゔぅ": oConsonant = "v"; oVowel = "u"; return;
            case "ゔぇ": oConsonant = "v"; oVowel = "e"; return;
            case "ゔぉ": oConsonant = "v"; oVowel = "o"; return;
            case "くぁ": oConsonant = "q"; oVowel = "a"; return;
            case "くぃ": oConsonant = "q"; oVowel = "i"; return;
            case "くぅ": oConsonant = "q"; oVowel = "u"; return;
            case "くぇ": oConsonant = "q"; oVowel = "e"; return;
            case "くぉ": oConsonant = "q"; oVowel = "o"; return;
            case "ふぁ": oConsonant = "f"; oVowel = "a"; return;
            case "ふぃ": oConsonant = "f"; oVowel = "i"; return;
            case "ふぅ": oConsonant = "f"; oVowel = "u"; return;
            case "ふぇ": oConsonant = "f"; oVowel = "e"; return;
            case "ふぉ": oConsonant = "f"; oVowel = "o"; return;
            //////////
            case "あ": oConsonant = ""; oVowel = "a"; return;
            case "い": oConsonant = ""; oVowel = "i"; return;
            case "う": oConsonant = ""; oVowel = "u"; return;
            case "え": oConsonant = ""; oVowel = "e"; return;
            case "お": oConsonant = ""; oVowel = "o"; return;
            case "ぁ": oConsonant = ""; oVowel = "a"; return;
            case "ぃ": oConsonant = ""; oVowel = "i"; return;
            case "ぅ": oConsonant = ""; oVowel = "u"; return;
            case "ぇ": oConsonant = ""; oVowel = "e"; return;
            case "ぉ": oConsonant = ""; oVowel = "o"; return;
            case "か": oConsonant = "k"; oVowel = "a"; return;
            case "き": oConsonant = "k"; oVowel = "i"; return;
            case "く": oConsonant = "k"; oVowel = "u"; return;
            case "け": oConsonant = "k"; oVowel = "e"; return;
            case "こ": oConsonant = "k"; oVowel = "o"; return;
            case "さ": oConsonant = "s"; oVowel = "a"; return;
            case "し": oConsonant = "s"; oVowel = "i"; return;
            case "す": oConsonant = "s"; oVowel = "u"; return;
            case "せ": oConsonant = "s"; oVowel = "e"; return;
            case "そ": oConsonant = "s"; oVowel = "o"; return;
            case "た": oConsonant = "t"; oVowel = "a"; return;
            case "ち": oConsonant = "t"; oVowel = "i"; return;
            case "つ": oConsonant = "t"; oVowel = "u"; return;
            case "て": oConsonant = "t"; oVowel = "e"; return;
            case "と": oConsonant = "t"; oVowel = "o"; return;
            case "な": oConsonant = "n"; oVowel = "a"; return;
            case "に": oConsonant = "n"; oVowel = "i"; return;
            case "ぬ": oConsonant = "n"; oVowel = "u"; return;
            case "ね": oConsonant = "n"; oVowel = "e"; return;
            case "の": oConsonant = "n"; oVowel = "o"; return;
            case "は": oConsonant = "h"; oVowel = "a"; return;
            case "ひ": oConsonant = "h"; oVowel = "i"; return;
            case "ふ": oConsonant = "h"; oVowel = "u"; return;
            case "へ": oConsonant = "h"; oVowel = "e"; return;
            case "ほ": oConsonant = "h"; oVowel = "o"; return;
            case "ま": oConsonant = "m"; oVowel = "a"; return;
            case "み": oConsonant = "m"; oVowel = "i"; return;
            case "む": oConsonant = "m"; oVowel = "u"; return;
            case "め": oConsonant = "m"; oVowel = "e"; return;
            case "も": oConsonant = "m"; oVowel = "o"; return;
            case "や": oConsonant = "y"; oVowel = "a"; return;
            case "ゆ": oConsonant = "y"; oVowel = "u"; return;
            case "よ": oConsonant = "y"; oVowel = "o"; return;
            case "ら": oConsonant = "r"; oVowel = "a"; return;
            case "り": oConsonant = "r"; oVowel = "i"; return;
            case "る": oConsonant = "r"; oVowel = "u"; return;
            case "れ": oConsonant = "r"; oVowel = "e"; return;
            case "ろ": oConsonant = "r"; oVowel = "o"; return;
            case "わ": oConsonant = "w"; oVowel = "a"; return;
            case "ゐ": oConsonant = "w"; oVowel = "i"; return;
            case "ゑ": oConsonant = "w"; oVowel = "e"; return;
            case "を": oConsonant = "w"; oVowel = "o"; return;
            case "ん": oConsonant = "n"; oVowel = ""; return;
            case "が": oConsonant = "g"; oVowel = "a"; return;
            case "ぎ": oConsonant = "g"; oVowel = "i"; return;
            case "ぐ": oConsonant = "g"; oVowel = "u"; return;
            case "げ": oConsonant = "g"; oVowel = "e"; return;
            case "ご": oConsonant = "g"; oVowel = "o"; return;
            case "ざ": oConsonant = "z"; oVowel = "a"; return;
            case "じ": oConsonant = "z"; oVowel = "i"; return;
            case "ず": oConsonant = "z"; oVowel = "u"; return;
            case "ぜ": oConsonant = "z"; oVowel = "e"; return;
            case "ぞ": oConsonant = "z"; oVowel = "o"; return;
            case "だ": oConsonant = "d"; oVowel = "a"; return;
            case "ぢ": oConsonant = "d"; oVowel = "i"; return;
            case "づ": oConsonant = "d"; oVowel = "u"; return;
            case "で": oConsonant = "d"; oVowel = "e"; return;
            case "ど": oConsonant = "d"; oVowel = "o"; return;
            case "ば": oConsonant = "b"; oVowel = "a"; return;
            case "び": oConsonant = "b"; oVowel = "i"; return;
            case "ぶ": oConsonant = "b"; oVowel = "u"; return;
            case "べ": oConsonant = "b"; oVowel = "e"; return;
            case "ぼ": oConsonant = "b"; oVowel = "o"; return;
            case "ぱ": oConsonant = "p"; oVowel = "a"; return;
            case "ぴ": oConsonant = "p"; oVowel = "i"; return;
            case "ぷ": oConsonant = "p"; oVowel = "u"; return;
            case "ぺ": oConsonant = "p"; oVowel = "e"; return;
            case "ぽ": oConsonant = "p"; oVowel = "o"; return;
        }
        if (aSyllable.Length == 2){
            decomposeSyllable(aSyllable[0].ToString(), out oConsonant, out oVowel);
            return;
        }
        oConsonant = "";
        oVowel = "";
        return;
    }
    //アルファベット文字列から母音子音決定
    static private void decomposeAlphabet(string aSentence,out string oConsonant,out string oVowel){
        if (aSentence.Length==0){
            oConsonant = "";
            oVowel = "";
            return;
        }
        switch(extractPhoneme(aSentence[0])){
            case Phoneme.consonant:
                oConsonant = aSentence[0].ToString();
                for (int i = 1; i < aSentence.Length;i++){
                    if(extractPhoneme(aSentence[i])==Phoneme.vowel){
                        oVowel = aSentence[i].ToString();
                        return;
                    }
                }
                oVowel = "";
                return;
            case Phoneme.vowel:
                oConsonant = "";
                oVowel = aSentence[0].ToString();
                return;
            case Phoneme.invalid:
                oConsonant = "";
                oVowel = "";
                return;
        }
        oConsonant = "";
        oVowel = "";
        throw new Exception("LyricsStringConverter : ここは実行されないはず");
    }
    //小さい文字ならtrue
    static private bool isLowerJa(char c){
        switch (c){
            case 'ゃ':case 'ャ':
            case 'ゅ':case 'ュ':
            case 'ょ':case 'ョ':
            case 'ぁ':case 'ァ':
            case 'ぃ':case 'ィ':
            case 'ぅ':case 'ゥ':
            case 'ぇ':case 'ェ':
            case 'ぉ':case 'ォ':
                return true;
            default: return false;
        }
    }
    //日本語(平仮名or片仮名)なのか英語(アルファベット)なのかそれ以外なのか
    static private PhonemeLanguage analysePhonemeLanguage(char c){
        if ((c >= 'A' && c <= 'Z')) return PhonemeLanguage.en;
        if ((c >= 'a' && c <= 'z')) return PhonemeLanguage.en;
        if ((c >= 'ぁ' && c <= 'ゖ')) return PhonemeLanguage.ja;
        if ((c >= 'ァ' && c <= 'ヶ')) return PhonemeLanguage.ja;
        return PhonemeLanguage.other;
    }
    private enum PhonemeLanguage{
        ja,en,other
    }
    //音素を識別
    static private Phoneme extractPhoneme(char c){
        switch(c){
            case 'a':case 'i':case 'u':case 'e':case 'o':
                return Phoneme.vowel;
            case 'b':case 'c':case 'd':case 'f':case 'g':case 'h':case 'j':
            case 'k':case 'l':case 'm':case 'n':case 'p':case 'q':case 'r':
            case 's':case 't':case 'v':case 'w':case 'x':case 'y':case 'z':
                return Phoneme.consonant;
        }
        return Phoneme.invalid;
    }
    private enum Phoneme{
        consonant,vowel,invalid
    }
    //先頭の音節取得
    static private string extractTopSyllable(string s){
        if (s.Length == 1) return s;
        if (!isLowerJa(s[1])) return s;
        return s.Substring(0, 2);
    }
    //次の文字を読み込む
    static private char loadNextChar(){
        if (mIndex + 1 >= mLength) return '\x1a';
        mIndex++;
        while (mInput[mIndex] == '\n'){
            if (mIndex + 1 >= mLength) return '\x1a';
            mIndex++;
        }
        return mInput[mIndex];
    }
    //次の文字を見る(indexは移動しない)
    static private char confirmNextChar(){
        if (mIndex + 1 >= mLength) return '\x1a';
        int i = 1;
        while (mInput[mIndex + i] == '\n'){
            if (mIndex + i >= mLength) return '\x1a';
            i++;
        }
        return mInput[mIndex + 1];
    }
}
