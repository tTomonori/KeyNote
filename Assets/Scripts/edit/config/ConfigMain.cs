using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigMain : MonoBehaviour {

    private void Start(){
        Arg tArg = MySceneManager.getArg("musicConfig");
        Subject.addObserver(new Observer("configMain", (message) =>{
            if (message.name == "cancelButtonPushed"){
                MySceneManager.closeScene("musicConfig", new Arg(new Dictionary<string, object>() { { "ok", false } }));
                return;
            }
            if (message.name != "okButtonPushed") return;
            Arg tData = getData();
            //必須のデータが入力されているか確認
            //title
            if(tData.get<string>("title")==""){
                AlartCreater.alart("タイトルが入力されていません");
                return;
            }
            //file
            if(tData.get<string>("file")==""){
                AlartCreater.alart("譜面ファイル名が入力されていません");
                return;
            }
            if(DataFolder.existScoreData(tData.get<string>("file"))){
                AlartCreater.alart("譜面ファイル名が既に使われています");
                return;
            }
            //music
            if(tData.get<string>("music")==".wav"){
                AlartCreater.alart("音声ファイル名が入力されていません");
                return;
            }
            if(!DataFolder.existMusic(tData.get<string>("music"))){
                AlartCreater.alart("音声ファイルが見つかりません");
                return;
            }
            //必須条件クリア
            //譜面データ作成
            MusicScoreFileData tNewScore = new MusicScoreFileData();
            tNewScore.title = tData.get<string>("title");
            tNewScore.fileName = tData.get<string>("file");
            tNewScore.music = tData.get<string>("music");
            tNewScore.thumbnail = tData.get<string>("thumbnail");
            tNewScore.back = tData.get<string>("back");
            tNewScore.movie = tData.get<string>("movie");
            Arg tArgument = new Arg(new Dictionary<string, object>() { { "ok", true }, { "scoreData", tNewScore } });
            MySceneManager.closeScene("musicConfig", tArgument);
        }));
    }
    //入力したデータを取得
    private Arg getData(){
        return new Arg(new Dictionary<string,object>(){
            {"title",GameObject.Find("title").GetComponentInChildren<MyBehaviour>().GetComponent<Text>().text},
            {"file",GameObject.Find("file").GetComponentInChildren<MyBehaviour>().GetComponent<Text>().text},
            {"music",GameObject.Find("music").GetComponentInChildren<MyBehaviour>().GetComponent<Text>().text + ".wav"},
            {"thumbnail",GameObject.Find("thumbnail").GetComponentInChildren<MyBehaviour>().GetComponent<Text>().text},
            {"back",GameObject.Find("back").GetComponentInChildren<MyBehaviour>().GetComponent<Text>().text},
            {"movie",GameObject.Find("movie").GetComponentInChildren<MyBehaviour>().GetComponent<Text>().text}
        });
    }
    private void OnDestroy(){
        Subject.removeObserver("configMain");
    }
}
