using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiButton : MyBehaviour {
    [SerializeField] protected string mName;
    [SerializeField] protected Dictionary<string, object> mParameters=new Dictionary<string, object>();
    [SerializeField] protected string mGroup=null;
    private bool isPushed = false;
    private void OnMouseDown(){
        isPushed = true;
        scaleBy(new Vector3(-0.1f, -0.1f, 0), 0.1f);
    }
    private void OnMouseExit(){
        if (!isPushed) return;
        isPushed = false;
        scaleBy(new Vector3(0.1f, 0.1f, 0), 0.1f);
    }
    private void OnMouseUp(){
        if (!isPushed) return;
        isPushed = false;
        Subject.sendMessage(new Message(mName, new Arg(mParameters), mGroup));
        scaleBy(new Vector3(0.1f, 0.1f, 0), 0.1f);

        //StartCoroutine(scaleBy(0.1f, 0.05f,() => {
        //    StartCoroutine(scaleBy(-0.1f, 0.1f));
        //}));
    }
}
