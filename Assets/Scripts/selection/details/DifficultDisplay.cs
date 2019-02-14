using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultDisplay : MonoBehaviour {
    private SpriteRenderer[] mStars;
    [SerializeField] private Sprite[] mStarImage;
	// Use this for initialization
	void Start () {
        mStars = gameObject.GetComponentsInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //難易度設定
    public void set(int aNum){
        int tTen = aNum / 10;
        for (int i = 0; i < 10;i++){
            if(i<aNum - tTen*10){
                mStars[i].sprite = mStarImage[tTen + 1];
            }else{
                mStars[i].sprite = mStarImage[tTen];
            }
        }
    }
}
