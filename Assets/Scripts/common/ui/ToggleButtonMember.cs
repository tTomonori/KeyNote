using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButtonMember : MyButton {
    [SerializeField] protected string mName;
    private ToggleButtonGroup mParent;
    private void Start () {
        if(mName==""){
            mName = name;
        }else{
            name = mName;
        }
        if(transform.parent!=null)
            mParent = transform.parent.GetComponent<ToggleButtonGroup>();
        if (mPushedMessage == "")
            mPushedMessage = mName + "Pushed";
	}
    protected override void pushed(){
        if (mParent == null) return;
        mParent.memberPushed(mName);
    }
    public virtual void hold(){
        
    }
    public virtual void release(){
        
    }
}
