using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItemList : MyScrollViewElementDataList{
    private List<Arg> mList{
        get { return (List<Arg>)mDataList; }
        set { mDataList = value; }
    }
    public ScoreItemList(){
        List<Arg> tMusicList=new List<Arg>();
        foreach (Arg tData in MusicList.mMusicList)
            tMusicList.Add(tData);
        tMusicList.AddRange(loadUnfinishedScores());
        mList = tMusicList;
    }
    //未完成の譜面のデータをロード
    private List<Arg> loadUnfinishedScores(){
        List<Arg> tList = new List<Arg>();
        foreach (string tFileName in MusicList.getUnfinishedScoreFileNameList()){
            tList.Add(new Arg(new Dictionary<string, object>(){
                    {"file",tFileName},
                    {"title",DataFolder.loadScoreData(tFileName).title}
                }));
        }
        return tList;
    }
    //要素生成
    public override MyScrollViewElement createElement(int aIndex){
        MusicListFileData.MusicListElement tData = new MusicListFileData.MusicListElement(mList[aIndex]);
        ScoreItem tItem = (aIndex < MusicList.mLength) ? MyBehaviour.createObjectFromPrefab<ScoreItem>("ui/item/scoreItem") : MyBehaviour.createObjectFromPrefab<ScoreItem>("ui/item/scoreItemUn");
        tItem.set(tData.title, new Arg(new Dictionary<string, object>(){
                        {"file",tData.file}
                    }));
        return tItem.GetComponent<MyScrollViewElement>();
    }
    public override bool isCanSort(int aIndex){
        return aIndex < MusicList.mLength;
    }
    //完成済みの楽曲のリスト
    public List<Arg> getCompletedMusicList(){
        List<Arg> tList = new List<Arg>();
        for (int i = 0; i < MusicList.mLength; i++)
            tList.Add(mList[i]);
        return tList;
    }
}