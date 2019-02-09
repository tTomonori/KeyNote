using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlartCreater {
    //警告文表示
    static public AlartWindow alart(string aText){
        AlartWindow tWindow = MyBehaviour.createObjectFromPrefab<AlartWindow>("ui/alartWindow");
        tWindow.set(aText);
        return tWindow;
    }
}
