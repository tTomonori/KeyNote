using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : MyScrollViewElement {
    [SerializeField] private TextMesh mTitle;
    [SerializeField] private HideButton mEditButton;
    public void set(string aTitle,Arg aParameters){
        mTitle.text = aTitle;
        mEditButton.mParameters = aParameters;
    }
    private void OnMouseOver(){
        mEditButton.positionZ = -1; 
    }
    private void OnMouseExit(){
        mEditButton.positionZ = 1;
    }
    public override void push(){
    }
    public override void pull(){
    }
    public override void grab(){
        scaleBy(new Vector3(-0.1f, -0.1f, 0), 0.1f);
    }
    public override void release(){
        scaleBy(new Vector3(0.1f, 0.1f, 0), 0.1f);
    }
}
