using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideButton : MyButton {
    private void OnMouseOver(){
        positionZ = -1; 
    }
    private void OnMouseExit(){
        positionZ = 1;
    }
}
