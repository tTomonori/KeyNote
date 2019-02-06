using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightButton : UiButton {
    private MyBehaviour mLight;
    //
    public void lightOn(){
        mLight = MyBehaviour.create<MyBehaviour>();
        mLight.name = "light";
        mLight.gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sprites/button/electro/light");
        mLight.transform.parent = this.gameObject.transform;
        mLight.transform.localPosition = new Vector3(0, 0, -1);
        mLight.transform.localScale = new Vector3(1, 1, 1);
        mLight.rotateZ = Random.Range(0, 359);
        mLight.rotateForever(-180f);
    }
    //
    public void lightOff(){
        if (mLight != null)
            mLight.delete();
    }
}
