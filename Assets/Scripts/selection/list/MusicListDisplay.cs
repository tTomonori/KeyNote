using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MusicListDisplay : MyBehaviour {
    private MusicList mList;
    private List<MusicLabel> mLabels;
    private MusicDetailsDisplay mDetails{
        get { return GameObject.Find("detailsDisplay").GetComponent<MusicDetailsDisplay>(); }
    }
	void Start () {
        mList = new MusicList();
        mLabels = new List<MusicLabel>();
        display(mList.mLastPlayIndex);

        //曲名のラベルクリック時の動作
        Subject.addObserver(new Observer("listDisplay",(message) => {
            if(message.name=="clickMusicLabel"){
                if (message.getParameter<float>("num") == positionY) return;
                mDetails.initDetails();
                this.moveBy(new Vector3(0,message.getParameter<float>("num")-positionY,0),0.5f,() => {
                    mDetails.showMusic(mList.get(message.getParameter<int>("num")).mFileName);
                });
            }
        }));

        //初期の設定の難易度を選択
        Subject.sendMessage(new Message(mList.mLastPlayDifficult, new Arg(),"difficultButton"));
        mDetails.showMusic(mList.get(mList.mLastPlayIndex).mFileName);
	}
	
	void Update () {
        float tIndex = positionY;
        //はみ出たラベル削除
        List<MusicLabel> tDeletedLabels = new List<MusicLabel>();
        foreach(MusicLabel tLabel in mLabels){
            if (tIndex - 10 <= tLabel.mNum && tLabel.mNum <= tIndex + 10) continue;
            tDeletedLabels.Add(tLabel);
        }
        foreach(MusicLabel tLabel in tDeletedLabels){
            mLabels.Remove(tLabel);
            tLabel.delete();
        }
        //新しくラベル生成
        int tCreatedTop = mLabels.First<MusicLabel>().mNum;
        int tCreatedTail = mLabels.Last<MusicLabel>().mNum;
        for (int i = tCreatedTop-1; i > tIndex-10;i--){
            mLabels.Insert(0, createLabel(i));
        }
        for (int i = tCreatedTail+1; i < (int)tIndex + 10; i++){
            mLabels.Add(createLabel(i));
        }
	}

    //リストを生成して表示
    void display(int aNum){
        for (int i = -10; i < 11;i++){
            MusicLabel tLabel = createLabel(aNum+i);
            mLabels.Add(tLabel);
        }
    }
    //曲名のラベル生成
    MusicLabel createLabel(int aNum){
        MusicLabel tLabel = MyBehaviour.createObjectFromPrefab<MusicLabel>("selection/musicLabel");
        tLabel.name = "musicLabel:" + aNum;
        tLabel.mNum = aNum;
        tLabel.setLabel(mList.get(aNum).mTitle);
        tLabel.transform.parent = gameObject.transform;
        tLabel.positionY = -aNum;
        return tLabel;
    }
    private void OnDestroy(){
        Subject.removeObserver("listDisplay");
    }
}
