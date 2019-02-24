using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideButton : MyButton {
    private void Start(){
        opacityBy(-1, 0);
    }
    private void OnMouseOver(){
        opacityBy(1, 0.1f);
    }
    private void OnMouseExit(){
        opacityBy(-1, 0.1f);
    }
}
