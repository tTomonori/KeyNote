using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultDisplay : MonoBehaviour {
    private SpriteRenderer[] mStars;
	// Use this for initialization
	void Start () {
        mStars = gameObject.GetComponentsInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //難易度設定
    public void set(int aNum){
        for (int i = 0; i < 10;i++){
            if(i<aNum){
                mStars[i].sprite = Resources.Load<Sprite>("sprite/star/star");
            }else{
                mStars[i].sprite = Resources.Load<Sprite>("sprite/star/star_empty");
            }
        }
    }
}
