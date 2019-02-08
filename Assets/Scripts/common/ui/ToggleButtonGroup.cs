using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButtonGroup : MyBehaviour {
    private string mPushedButtonName;
    public string pushedButtonName{
        get { return mPushedButtonName; }
    }
    public void memberPushed(string aName){
        if (aName == mPushedButtonName) return;
        //選択されていたボタンをrelease
        if(mPushedButtonName!=null)
            findChild<ToggleButtonMember>(mPushedButtonName).release();
        //押されたボタンをhold
        mPushedButtonName = aName;
        findChild<ToggleButtonMember>(mPushedButtonName).hold();
    }
}
