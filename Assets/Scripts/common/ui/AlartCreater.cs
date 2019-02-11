using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlartCreater {
    static private AlartWindow mLastCreated; 
    //警告文表示
    static public AlartWindow alart(string aText){
        //前回の警告文がまだ表示されている場合は消す
        if (mLastCreated != null) mLastCreated.delete();
        //ウィンドウ生成
        AlartWindow tWindow = MyBehaviour.createObjectFromPrefab<AlartWindow>("ui/alartWindow");
        mLastCreated = tWindow;
        tWindow.set(aText);
        return tWindow;
    }
}
