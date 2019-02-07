using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluationDisplay : MyBehaviour {
    private TextMesh mPerfectPoint;
    private TextMesh mGreatPoint;
    private TextMesh mGoodPoint;
    private TextMesh mBadPoint;
    private TextMesh mMissPoint;
	void Start () {
        //ポイント表示テキストを取得
        mPerfectPoint = findChild("perfect").GetComponent<MyBehaviour>().findChild("number").GetComponent<TextMesh>();
        mGreatPoint = findChild("great").GetComponent<MyBehaviour>().findChild("number").GetComponent<TextMesh>();
        mGoodPoint = findChild("good").GetComponent<MyBehaviour>().findChild("number").GetComponent<TextMesh>();
        mBadPoint = findChild("bad").GetComponent<MyBehaviour>().findChild("number").GetComponent<TextMesh>();
        mMissPoint = findChild("miss").GetComponent<MyBehaviour>().findChild("number").GetComponent<TextMesh>();
        //表示を初期化
        mPerfectPoint.text = "0";
        mGreatPoint.text = "0";
        mGoodPoint.text = "0";
        mBadPoint.text = "0";
        mMissPoint.text = "0";

        //メッセージ監視
        Subject.addObserver(new Observer("playPoint", (message) =>{
            //hit評価
            if (message.name == "hittedNote"){
                switch (message.getParameter<TypeEvaluation.Evaluation>("evaluation")){
                    case TypeEvaluation.Evaluation.perfect:
                        mPerfectPoint.text = (int.Parse(mPerfectPoint.text) + 1).ToString();
                        break;
                    case TypeEvaluation.Evaluation.great:
                        mGreatPoint.text = (int.Parse(mGreatPoint.text) + 1).ToString();
                        break;
                    case TypeEvaluation.Evaluation.good:
                        mGoodPoint.text = (int.Parse(mGoodPoint.text) + 1).ToString();
                        break;
                    case TypeEvaluation.Evaluation.bad:
                        mBadPoint.text = (int.Parse(mBadPoint.text) + 1).ToString();
                        break;
                    case TypeEvaluation.Evaluation.miss:
                        mMissPoint.text = (int.Parse(mMissPoint.text) + 1).ToString();
                        break;
                }
                return;
            }
            //miss評価
            if (message.name == "missedNote"){
                switch(message.getParameter<Note.HitResult>("hitResult")){
                    case Note.HitResult.consonantAndVowel:
                        mMissPoint.text = (int.Parse(mMissPoint.text) + 2).ToString();
                        break;
                    case Note.HitResult.consonant:
                    case Note.HitResult.vowel:
                        mMissPoint.text = (int.Parse(mMissPoint.text) + 1).ToString();
                        break;
                }
                return;
            }
        }));
	}
    //評価結果を返す
    public Arg getResult(){
        return new Arg(new Dictionary<string, object>(){
            {"perfect",int.Parse(mPerfectPoint.text)},
            {"great",int.Parse(mGreatPoint.text)},
            {"good",int.Parse(mGoodPoint.text)},
            {"bad",int.Parse(mBadPoint.text)},
            {"miss",int.Parse(mMissPoint.text)}
        });
    }
    private void OnDestroy(){
        Subject.removeObserver("playPoint");
    }
}
