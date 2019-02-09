using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class ScoreHandler{
    public class EditState : ScoreHandleState{
        public EditState(ScoreHandler aParent) : base(aParent) { }
        public override void enter(){

        }
        public override void update(){
            
        }
        public override void getMessage(Message aMessage){
            if(aMessage.name=="clickNote"){
                Debug.Log("note : " + aMessage.getParameter<float>("time"));
                return;
            }
            if(aMessage.name=="clickLyrics"){
                Debug.Log("lyrics : " + aMessage.getParameter<float>("time"));
                return;
            }
            if(aMessage.name=="RightClickNote"){
                Debug.Log("note R : " + aMessage.getParameter<float>("time"));
                return;
            }
            if(aMessage.name=="RightClickLyrics"){
                Debug.Log("lyrics R : " + aMessage.getParameter<float>("time"));
                return;
            }
        }
    }
}