using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class MyScrollView<T> : MyBehaviour where T : MyBehaviour{
    //表示中の要素
    protected List<T> mItemList = new List<T>();
    //表示中の要素の番号
    protected List<int> mItemIndexList = new List<int>();
    //position最大値
    protected float? mTop;
    //position最低値
    protected float? mTail;
    //スクロール方向
    protected ScrollDirection mScrollDirection;
    protected enum ScrollDirection{vertical,horizontal}
	void Update () {
        if (mScrollDirection == ScrollDirection.vertical){
            float tScroll = Input.mouseScrollDelta.y;
            positionY -= tScroll;
            //範囲外まではスクロールできないようにする
            if (mTail != null && positionY < mTail) positionY = (float)mTail;
            else if (mTop != null && mTop < positionY) positionY = (float)mTop;
        }else{
            float tScroll = Input.mouseScrollDelta.x;
            positionX -= tScroll;
            //範囲外まではスクロールできないようにする
            if (mTail != null && positionX < mTail) positionX = (float)mTail;
            else if (mTop != null && mTop < positionX) positionX = (float)mTop;
        }
        updataItems();
	}
    //表示更新
    public void updataItems(){
        int tMin = displayMinIndex();
        int tMax = displayMaxIndex();
        //前のはみ出た要素削除
        while(mItemIndexList.Count!=0 && mItemIndexList[0]<tMin){
            T tItem = mItemList[0];
            mItemList.RemoveAt(0);
            mItemIndexList.RemoveAt(0);
            tItem.delete();
        }
        //後ろのはみ出た要素削除
        while (mItemIndexList.Count != 0 && mItemIndexList.Last<int>() < tMin){
            T tItem = mItemList[mItemList.Count - 1];
            mItemList.RemoveAt(mItemIndexList.Count - 1);
            mItemIndexList.RemoveAt(mItemIndexList[mItemIndexList.Count - 1]);
            tItem.delete();
        }
        if(mItemIndexList.Count==0){
            T tItem = createItem(tMax);
            tItem.transform.parent = transform;
            tItem.position = calculateItemPosition(tMax);
            tItem.name = "item : " + tMax;
            mItemList.Add(tItem);
            mItemIndexList.Add(tMax);
        }
        //後ろに要素追加
        for (int i = mItemIndexList[mItemIndexList.Count - 1] +  1; i <= tMax;i++){
            T tItem = createItem(i);
            tItem.transform.parent = transform;
            tItem.position = calculateItemPosition(i);
            tItem.name = "item : " + i;
            mItemList.Add(tItem);
            mItemIndexList.Add(i);
        }
        //前に要素追加
        for (int i = mItemIndexList[0] - 1; tMin <= i; i--){
            T tItem = createItem(i);
            tItem.transform.parent = transform;
            tItem.position = calculateItemPosition(i);
            tItem.name = "item : " + i;
            mItemList.Insert(0, tItem);
            mItemIndexList.Insert(0, i);
        }
    }
    //要素生成
    protected abstract T createItem(int aIndex);
    //要素の座標計算
    protected abstract Vector3 calculateItemPosition(int aIndex);
    //表示する要素の範囲
    protected abstract int displayMinIndex();
    protected abstract int displayMaxIndex();
}
