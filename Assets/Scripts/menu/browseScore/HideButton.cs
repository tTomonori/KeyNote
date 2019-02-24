using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideButton : MyButton {
    private void Start(){
        opacityBy(-1, 0);
    }
    private void OnMouseEnter(){
        opacityBy(1, 0);
    }
    private void OnMouseExit(){
        opacityBy(-1, 0);
        base.OnMouseExit();
    }
}