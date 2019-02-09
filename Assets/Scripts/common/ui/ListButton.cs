using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListButton : MyButton {
    [SerializeField] private string[] mSelections;
    public string mSelected{
        get { return mSelections[mSelectionIndex]; }
    }
    private int mSelectionIndex = 0;
    private TextMesh mText;
    private void Start(){
        mText = mContents.findChild<TextMesh>("text");
        if (mSelections.Length == 0)
            mSelections = new string[1] { "null" };
        mText.text = mSelections[mSelectionIndex];
    }
    protected override void pushed(){
        mSelectionIndex = (mSelectionIndex + 1) % mSelections.Length;
        mText.text = mSelections[mSelectionIndex];
    }
}
