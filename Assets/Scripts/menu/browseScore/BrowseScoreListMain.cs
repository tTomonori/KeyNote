using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowseScoreListMain : MonoBehaviour {

	void Start () {
        ScoreItemList tList = new ScoreItemList();
        MyScrollView tScrollView = GameObject.Find("scoreItemList").GetComponent<MyScrollView>();
        MyScrollView.Option tOption = new MyScrollView.Option();
        tOption.elementSize = new Vector2(12, 1);
        tOption.contentSize = new Vector2(12, 6);
        tOption.doubleTap = false;
        tOption.sortable = true;
        tScrollView.init(tList, tOption);

        Subject.addObserver(new Observer("browseScoreListMain", (message) =>{
            if(message.name == "endBrowseButtonPushed"){//譜面一覧を閉じる
                MySceneManager.changeScene("selection");
                return;
            }
            if (message.name == "editButtonPushed"){//編集へ移る
                MySceneManager.changeScene("edit", new Arg(new Dictionary<string, object>(){
                                {"scoreData",DataFolder.loadScoreData(message.getParameter<string>("file"))}
                            }));
                return;
            }
            if(message.name == "sortButtonPushed"){//譜面の並び替え適用
                List<Arg> tMusicList = tList.getCompletedMusicList();
                MusicList.updateList(tMusicList);
                AlartCreater.alart("曲の並びを保存しました");
            }
        }));
	}
	
	void Update () {
		
	}
    private void OnDestroy(){
        Subject.removeObserver("browseScoreListMain");
    }
}