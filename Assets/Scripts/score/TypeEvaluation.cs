using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class TypeEvaluation {
    static public float kWorstDifference= 0.2f;
    public enum Evaluation{
        perfect,great,good,bad,miss
    }
    static public Evaluation evaluate(float aQarterBeat1,float aQuarterBeat2,float aBpm){
        return evaluate(KeyTime.quarterBeatToSeconds(Mathf.Abs(aQarterBeat1 - aQuarterBeat2), aBpm));
    }
    static public Evaluation evaluate(float aSeconds1,float aSeconds2){
        return evaluate(Mathf.Abs(aSeconds1 - aSeconds2));
    }
    static public Evaluation evaluate(float aDifference){
        if (aDifference < 0.04) return Evaluation.perfect;
        if (aDifference < 0.072) return Evaluation.great;
        if (aDifference < 0.112) return Evaluation.good;
        if (aDifference < kWorstDifference) return Evaluation.bad;
        return Evaluation.miss;
    }
}