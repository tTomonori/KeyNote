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
    }
}