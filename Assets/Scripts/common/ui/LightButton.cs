using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightButton : ToggleButtonMember {
    private MyBehaviour mLight;
    public override void hold(){
        mLight = MyBehaviour.create<MyBehaviour>();
        mLight.name = "light";
        mLight.gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sprites/button/electro/light");
        mLight.transform.parent = this.gameObject.transform;
        mLight.transform.localPosition = new Vector3(0, 0, -1);
        mLight.transform.localScale = new Vector3(1, 1, 1);
        mLight.rotateZ = Random.Range(0, 359);
        mLight.rotateForever(-180f);
    }
    public override void release(){
        if (mLight != null)
            mLight.delete();
    }
}
