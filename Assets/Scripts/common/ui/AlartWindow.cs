using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlartWindow : MyBehaviour {
    private bool mFade = false;
    SpriteRenderer mRenderer;
    TextMesh mText;
    //表示する文設定
    public void set(string aText){
        mText.text = aText;
    }
    private void Awake(){
        mRenderer = GetComponent<SpriteRenderer>();
        mText = GetComponentInChildren<TextMesh>();
    }
    void Start () {
        setTimeout(1.5f, () => {
            mFade = true;
        });
	}
	
	void Update () {
        if (!mFade) return;
        //ゆっくり透過
        Color tColor = mRenderer.color;
        mRenderer.color = new Color(tColor.r, tColor.g, tColor.b, tColor.a - 0.04f);
        tColor = mText.color;
        mText.color = new Color(tColor.r, tColor.g, tColor.b, tColor.a - 0.04f);

        //完全に透明になったら削除
        if (mRenderer.color.a <= 0)
            delete();
	}
}
