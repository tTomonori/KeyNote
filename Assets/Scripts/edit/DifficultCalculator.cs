using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DifficultCalculator{
    //周辺の音符として難易度計算に利用する音符の時間差の最大値
    static private float kFurthestSecond = 1;
    //bpmが最大の時の難易度計算に利用する音符の数の最大値
    static private int kMaxAroundNoteNum{
        get { return Mathf.FloorToInt(KeyTime.secondsToQuarterBeat(kFurthestSecond, 250)); }
    }
    //難易度が何段階あるか
    static private int kDifficultStage = 31;
    private struct NoteKey{
        public float second;
        public string key;
        public NoteKey(float aSecond, string aKey){
            second = aSecond;
            key = aKey;
        }
    }
    static public Dictionary<ScoreDifficult, int> calculateDifficult(List<Arg> aNotes){
        float tChild = 0;
        float tStudent = 0;
        float tScholar = 0;
        float tGuru = 0;
        int tLength = aNotes.Count;
        int tGuruLength = 0;//難易度guruの時の音符の数
        for (int i = 0; i < aNotes.Count - 1;i++){
            Arg tNote1 = aNotes[i];
            float tSecond1 = MusicScoreData.quarterBeatToMusicTime(tNote1.get<float>("time"));
            string tConsonant1 = tNote1.get<string>("consonant");
            string tVowel1 = tNote1.get<string>("vowel");
            string tKey1 = (tConsonant1 == " ") ? tVowel1 : tConsonant1;

            Arg tNote2 = aNotes[i + 1];
            float tSecond2 = MusicScoreData.quarterBeatToMusicTime(tNote2.get<float>("time"));
            string tConsonant2 = tNote2.get<string>("consonant");
            string tVowel2 = tNote2.get<string>("vowel");
            string tKey2 = (tConsonant1 == " ") ? tVowel2 : tConsonant2;

            tChild += calculateNoteDifficult(new NoteKey(tSecond1, " "), new NoteKey(tSecond2, " "));
            tStudent += calculateNoteDifficult(new NoteKey(tSecond1, tVowel1), new NoteKey(tSecond2, tVowel2));
            tScholar += calculateNoteDifficult(new NoteKey(tSecond1, tKey1), new NoteKey(tSecond2, tKey2));
            if (tConsonant1 != " " && tVowel1 != " "){
                tGuru += calculateNoteDifficult(new NoteKey(tSecond1, tConsonant1), new NoteKey(tSecond1, tVowel1));
                tGuru += calculateNoteDifficult(new NoteKey(tSecond1, tVowel1), new NoteKey(tSecond2, tKey2));
                //難易度guruの時の音符の数カウント
                tGuruLength += 2;
            }else{
                tGuru += calculateNoteDifficult(new NoteKey(tSecond1, tKey1), new NoteKey(tSecond2, tKey2));
                //難易度guruの時の音符の数カウント
                tGuruLength++;
            }
        }
        //音符一つ分の難易度の平均をとる
        tChild /= tLength;
        tStudent /= tLength;
        tScholar /= tLength;
        tGuru /= tGuruLength;
        //最大難易度を超えた場合に補正
        int tRChild = (tChild > kDifficultStage) ? kDifficultStage - 1 : (int)tChild;
        int tRStudent = (tStudent > kDifficultStage) ? kDifficultStage - 1 : (int)tStudent;
        int tRScholar = (tScholar > kDifficultStage) ? kDifficultStage - 1 : (int)tScholar;
        int tRGuru = (tGuru > kDifficultStage) ? kDifficultStage - 1 : (int)tGuru;
        return new Dictionary<ScoreDifficult, int>(){
            {ScoreDifficult.child,tRChild},
            {ScoreDifficult.student,tRStudent},
            {ScoreDifficult.scholar,tRScholar},
            {ScoreDifficult.guru,tRGuru}
        };
    }
    //難易度計算
    static private float calculateNoteDifficult(NoteKey aNote1,NoteKey aNote2){
        float tDifference = aNote2.second - aNote1.second;
        if (kFurthestSecond < tDifference) return 0;
        float tSecondDifficult = calculateSecondDifficult(tDifference);
        float tKeyDifficult = calculateKeyDifficult(aNote1.key, aNote2.key);
        float tDifficult = (tSecondDifficult * tSecondDifficult * tKeyDifficult) / 500000 * kDifficultStage;
        return (tDifficult < kDifficultStage) ? tDifficult : kDifficultStage - 1;
    }
    //時間差による難易度
    static private float calculateSecondDifficult(float aSecond){
        return 100 * (1 - Mathf.Sin(aSecond / kFurthestSecond / 2 * Mathf.PI));
    }
    //キーの違いによる難易度
    static private float calculateKeyDifficult(string aKey1,string aKey2){
        return (aKey1 == aKey2) ? 20 : 100;
    }
    //周囲の音符の数による難易度
    static private float calculateNoteNumDifficult(int aNum){
        return 100 * Mathf.Sin(aNum / (1000 / 60 * kFurthestSecond) / 2 * Mathf.PI);
    }
    //static public Dictionary<ScoreDifficult, int> calculateDifficult(List<Arg> aNotes)
    //{
    //    float tChild = 0;
    //    float tStudent = 0;
    //    float tScholar = 0;
    //    float tGuru = 0;
    //    int tLength = aNotes.Count;
    //    int tGuruLength = 0;//難易度guruの時の音符の数
    //    foreach (Arg tNote1 in aNotes)
    //    {
    //        float tSecond1 = MusicScoreData.quarterBeatToMusicTime(tNote1.get<float>("time"));
    //        string tConsonant1 = tNote1.get<string>("consonant");
    //        string tVowel1 = tNote1.get<string>("vowel");
    //        string tKey1 = (tConsonant1 == "") ? tVowel1 : tConsonant1;
    //        //難易度guruの時の音符の数カウント
    //        tGuruLength++;
    //        if (tConsonant1 != "" && tVowel1 != "") tGuruLength++;

    //        //周辺の音符を探す
    //        List<NoteKey> tAroundChild = new List<NoteKey>();
    //        List<NoteKey> tAroundStudent = new List<NoteKey>();
    //        List<NoteKey> tAroundScholar = new List<NoteKey>();
    //        List<NoteKey> tAroundGuru = new List<NoteKey>();
    //        foreach (Arg tNote2 in aNotes)
    //        {
    //            float tSecond2 = MusicScoreData.quarterBeatToMusicTime(tNote2.get<float>("time"));
    //            float tDifference = Mathf.Abs(tSecond1 - tSecond2);//二つの音符の時間差
    //            if (tDifference > kFurthestSecond)
    //            {
    //                //周囲範囲外
    //                if (tSecond1 + kFurthestSecond < tSecond2) break;
    //                else continue;
    //            }
    //            string tConsonant2 = tNote2.get<string>("consonant");
    //            string tVowel2 = tNote2.get<string>("vowel");
    //            string tKey2 = (tConsonant2 == "") ? tVowel2 : tConsonant2;
    //            tAroundChild.Add(new NoteKey(tSecond2, " "));
    //            tAroundStudent.Add(new NoteKey(tSecond2, tVowel2));
    //            tAroundScholar.Add(new NoteKey(tSecond2, tConsonant2));
    //            tAroundGuru.Add(new NoteKey(tSecond2, tConsonant2));
    //            tAroundGuru.Add(new NoteKey(tSecond2, tVowel2));
    //        }

    //        tChild += calculateNoteDifficult(new NoteKey(tSecond1, " "), tAroundChild);
    //        tStudent += calculateNoteDifficult(new NoteKey(tSecond1, tVowel1), tAroundStudent);
    //        tScholar += calculateNoteDifficult(new NoteKey(tSecond1, tConsonant1), tAroundScholar);
    //        tGuru += calculateNoteDifficult(new NoteKey(tSecond1, tConsonant1), tAroundGuru);
    //        tGuru += calculateNoteDifficult(new NoteKey(tSecond1, tVowel1), tAroundGuru);
    //    }
    //    //音符一つ分の難易度の平均をとる
    //    tChild /= tLength;
    //    tStudent /= tLength;
    //    tScholar /= tLength;
    //    tGuru /= tGuruLength;
    //    //最大難易度を超えた場合に補正
    //    int tRChild = (tChild > kDifficultStage) ? kDifficultStage : (int)tChild;
    //    int tRStudent = (tStudent > kDifficultStage) ? kDifficultStage : (int)tStudent;
    //    int tRScholar = (tScholar > kDifficultStage) ? kDifficultStage : (int)tScholar;
    //    int tRGuru = (tGuru > kDifficultStage) ? kDifficultStage : (int)tGuru;
    //    return new Dictionary<ScoreDifficult, int>(){
    //        {ScoreDifficult.child,tRChild},
    //        {ScoreDifficult.student,tRStudent},
    //        {ScoreDifficult.scholar,tRScholar},
    //        {ScoreDifficult.guru,tRGuru}
    //    };
    //}
    ////難易度計算
    //static private float calculateNoteDifficult(NoteKey aNote, List<NoteKey> aNotes)
    //{
    //    float tDifficult = 0;
    //    List<NoteKey> tNotes = aNotes;
    //    tNotes.Sort((a, b) => {//昇順ソート
    //        return (int)(a.second - b.second);
    //    });
    //    int tLength = tNotes.Count;
    //    if (tLength > 0)
    //    {
    //        tDifficult += calculateSecondDifficult(Mathf.Abs(aNote.second - tNotes[0].second));
    //        tDifficult += calculateKeyDifficult(aNote.key, tNotes[0].key) * 2;
    //        //if (tLength > 1)
    //        //tDifficult += calculateKeyDifficult(aNote.key, tNotes[1].key);
    //    }
    //    tDifficult += calculateNoteNumDifficult(tLength);
    //    return tDifficult / 400 * kDifficultStage;
    //}
}
