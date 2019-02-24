using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItemList : MyScrollViewElementDataList{
    private List<Arg> mList{
        get { return (List<Arg>)mDataList; }
        set { mDataList = value; }
    }
    public ScoreItemList(){
        List<Arg> tMusicList;
        tMusicList = MusicList.mMusicList;
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
}
//public class ScoreItemList : MyScrollView<ScoreItem> {
//    //未完成の譜面のデータ
//    private List<Arg> mUnfinishedScores;
//    //完成・未完成の譜面を合わせた合計の数
//    private int mAllScoreNum;
//	void Start () {
//        loadUnfinishedScores();
//        mAllScoreNum = MusicList.mLength + mUnfinishedScores.Count;
//        Subject.addObserver(new Observer("scoreItemList", (message) =>{
//            if(message.name=="endBrowseButtonPushed"){
//                MySceneManager.changeScene("selection");
//                return;
//            }
//            if(message.name=="editButtonPushed"){
//                MySceneManager.changeScene("edit", new Arg(new Dictionary<string, object>(){
//                    {"scoreData",DataFolder.loadScoreData(message.getParameter<string>("file"))}
//                }));
//                return;
//            }
//        }));
//        //scrollView パラメータ
//        mTop = mAllScoreNum;
//        mTail = 0;
//        mScrollDirection = MyScrollView<ScoreItem>.ScrollDirection.vertical;
//	}

//    //未完成の譜面のデータをロードして保持
//    private void loadUnfinishedScores(){
//        mUnfinishedScores = new List<Arg>();
//        foreach(string tFileName in MusicList.getUnfinishedScoreFileNameList()){
//            mUnfinishedScores.Add(new Arg(new Dictionary<string, object>(){
//                {"file",tFileName},
//                {"title",DataFolder.loadScoreData(tFileName).title}
//            }));
//        }
//    }
//    //楽曲データ取得
//    private MusicListFileData.MusicListElement getData(int aIndex){
//        if (aIndex < 0) return null;
//        if(aIndex<MusicList.mLength){
//            return MusicList.get(aIndex);
//        }else if(aIndex<mAllScoreNum){
//            return new MusicListFileData.MusicListElement(mUnfinishedScores[aIndex-MusicList.mLength]);
//        }else{
//            return null;
//        }
//    }
//    //item生成
//    protected override ScoreItem createItem(int aIndex){
//        MusicListFileData.MusicListElement tData = getData(aIndex);
//        ScoreItem tItem = (aIndex < MusicList.mLength) ? MyBehaviour.createObjectFromPrefab<ScoreItem>("ui/item/scoreItem") : MyBehaviour.createObjectFromPrefab<ScoreItem>("ui/item/scoreItemUn");
//        tItem.set(tData.title, new Arg(new Dictionary<string, object>(){
//                {"file",tData.file}
//            }));
//        return tItem;
//    }
//    //要素の座標計算
//    protected override Vector3 calculateItemPosition(int aIndex){
//        return new Vector3(0, -aIndex + 2, 0);
//    }
//    //表示する要素の範囲
//    protected override int displayMinIndex(){
//        int tIndex = Mathf.FloorToInt(positionY) - 6;
//        return (tIndex < 0) ? 0 : tIndex;
//    }
//    protected override int displayMaxIndex(){
//        int tIndex = Mathf.FloorToInt(positionY) + 6;
//        return (tIndex < mAllScoreNum) ? tIndex : mAllScoreNum - 1;
//    }
//    private void OnDestroy(){
//        Subject.removeObserver("scoreItemList");
//    }
//}
