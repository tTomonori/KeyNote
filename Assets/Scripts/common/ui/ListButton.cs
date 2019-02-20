using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListButton : MyButton {
    [SerializeField] private string[] mSelections;
    public string mSelected{
        get { return mSelections[mSelectionIndex]; }
    }
    private int mSelectionIndex = 0;
    [SerializeField] private TextMesh mText;
    private void Awake(){
        if (mSelections.Length == 0)
            mSelections = new string[1] { "null" }; 
    }
    private void Start(){
        mText.text = mSelections[mSelectionIndex];
    }
    protected override void pushed(){
        mSelectionIndex = (mSelectionIndex + 1) % mSelections.Length;
        mText.text = mSelections[mSelectionIndex];
        mParameters.set("selected", mSelections[mSelectionIndex]);
    }
    //指定した選択肢を選択している状態にする
    public void select(string aChoice){
        for (int i = 0; i < mSelections.Length;i++){
            if (mSelections[i] != aChoice) continue;
            mSelectionIndex = i;
            mText.text = mSelections[mSelectionIndex];
        }
    }
}
