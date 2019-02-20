using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class ScoreHandler {
    public abstract class ScoreHandleState{
        protected ScoreHandler parent;
        public ScoreHandleState(ScoreHandler aParent){
            parent = aParent;
        }
        virtual public void enter(){
            
        }
        virtual public void exit(){
            
        }
        virtual public void update(){
            
        }
        virtual public void getMessage(Message aMessage){
            
        }
        //マウス回転量を取得し譜面をスクロール
        protected void scrollScore(){
            float tScroll = Input.mouseScrollDelta.y;
            parent.mScore.positionY -= tScroll;
            //負の位置まではスクロールできないようにする
            if (parent.mScore.mCurrentQuarterBeat < 0)
                parent.mScore.mCurrentQuarterBeat = 0;
        }
        //音符を消した時の演出
        protected void productHit(Note aNote,TypeEvaluation.Evaluation aEvaluation){
            MyBehaviour tBehaviour = MyBehaviour.create<MyBehaviour>();
            tBehaviour.name = "evaluation";
            SpriteRenderer tRenderer = tBehaviour.gameObject.AddComponent<SpriteRenderer>();
            tBehaviour.transform.parent = parent.mScore.transform;
            tBehaviour.transform.localScale = new Vector3(1.7f, 1.7f, 1);
            tBehaviour.transform.localPosition = new Vector3(0, 0, 0);
            tBehaviour.transform.Translate(aNote.transform.position - parent.mScore.transform.position);
            tBehaviour.positionZ = -2;
            switch(aEvaluation){
                case TypeEvaluation.Evaluation.perfect:
                    tRenderer.sprite = Resources.Load<Sprite>("sprites/point/perfect");
                    tBehaviour.moveBy(new Vector3(0, 1, 0), 1, () =>{
                        tBehaviour.delete();
                    });
                    break;
                case TypeEvaluation.Evaluation.great:
                    tRenderer.sprite = Resources.Load<Sprite>("sprites/point/great");
                    tBehaviour.moveBy(new Vector3(0, 0.6f, 0), 1, () => {
                        tBehaviour.delete();
                    });
                    break;
                case TypeEvaluation.Evaluation.good:
                    tRenderer.sprite = Resources.Load<Sprite>("sprites/point/good");
                    tBehaviour.moveBy(new Vector3(0, 0.4f, 0), 1, () => {
                        tBehaviour.delete();
                    });
                    break;
                case TypeEvaluation.Evaluation.bad:
                    tRenderer.sprite = Resources.Load<Sprite>("sprites/point/bad");
                    tBehaviour.moveBy(new Vector3(0, 0.1f, 0), 1, () => {
                        tBehaviour.delete();
                    });
                    break;
                case TypeEvaluation.Evaluation.miss:
                    tRenderer.sprite = Resources.Load<Sprite>("sprites/point/miss");
                    tBehaviour.moveBy(new Vector3(0, -1, 0), 1, () => {
                        tBehaviour.delete();
                    });
                    break;
            }
        }
    }
}