using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSettingForm : MonoBehaviour {
    private ListButton mRhythmListButton;
    private InputField mMarginForm;
    private InputField mRustForm;
    // X / 4 拍子
    public int mRhythm{
        get { return int.Parse(mRhythmListButton.mSelected); }
    }
    //margin
    public float mMargin{
        get{
            string tText = mMarginForm.text;
            float tValue;
            if (!float.TryParse(tText, out tValue)){
                mMarginForm.text = "0";
                tValue = 0;
            }
            return tValue;
        }
    }
    //サビの位置
    public float mRust{
        get{
            string tText = mRustForm.text;
            float tValue;
            if (!float.TryParse(tText, out tValue)){
                mRustForm.text = "0";
                tValue = 0;
            }
            return tValue;  
        }
    }
    private void getComponent(){
        mRhythmListButton = GameObject.Find("rhythmLabel").GetComponentInChildren<ListButton>();
        mMarginForm = GameObject.Find("marginLabel").GetComponentInChildren<InputField>();
        mRustForm = GameObject.Find("rustLabel").GetComponentInChildren<InputField>();
    }
    //表示を現在適用されている値に戻す
    public void reset(){
        if(mRhythmListButton==null)
            getComponent();
        //拍子
        mRhythmListButton.select(MusicScoreData.mRhythm.ToString());
        //margin
        mMarginForm.text = MusicScoreData.mMargin.ToString();
        //サビ
        mRustForm.text = MusicScoreData.mRust.ToString();
    }
    //入力された値と現在適用されている値が異なる
    public bool isChanged(){
        //拍子
        if (mRhythm != MusicScoreData.mRhythm) return true;
        //margin
        if (mMargin != MusicScoreData.mMargin) return true;
        //サビ
        if (mRust != MusicScoreData.mRust) return true;
        return false;
    }
    //入力値を変更
    public void setRustForm(string aValue){
        mRustForm.text = aValue;
    }
}
