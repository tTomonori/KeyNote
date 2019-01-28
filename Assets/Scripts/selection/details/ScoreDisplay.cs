using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void set(float aPoint){
        foreach(TextMesh tText in gameObject.GetComponentsInChildren<TextMesh>()){
            if(tText.gameObject.name=="point"){
                tText.text = aPoint.ToString();
                if (!tText.text.Contains(".")) tText.text += ".0";
                break;
            }
        }
    }
}
