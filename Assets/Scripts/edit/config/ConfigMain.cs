using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigMain : MonoBehaviour {

    private void Start(){
        Arg tArg = MySceneManager.getArg("musicConfig");
        //初期値設定
        if (tArg.ContainsKey("initialize") && tArg.get<bool>("initialize")){
            GameObject.Find("title").GetComponentInChildren<InputField>().text = tArg.get<string>("title");
            GameObject.Find("file").GetComponentInChildren<InputField>().text = tArg.get<string>("file");
            GameObject.Find("music").GetComponentInChildren<InputField>().text = tArg.get<string>("music").Substring(0, tArg.get<string>("music").Length - 4);
            GameObject.Find("thumbnail").GetComponentInChildren<InputField>().text = tArg.get<string>("thumbnail");
            GameObject.Find("back").GetComponentInChildren<InputField>().text = tArg.get<string>("back");
            GameObject.Find("movie").GetComponentInChildren<InputField>().text = tArg.get<string>("movie");
        }
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
            if (tArg.ContainsKey("initialize") || tData.get<string>("file") != tArg.get<string>("originalFile")){
                if (DataFolder.existScoreData(tData.get<string>("file"))){
                    AlartCreater.alart("譜面ファイル名が既に使われています");
                    return;
                }
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
            {"title",GameObject.Find("title").GetComponentInChildren<InputField>().text},
            {"file",GameObject.Find("file").GetComponentInChildren<InputField>().text},
            {"music",GameObject.Find("music").GetComponentInChildren<InputField>().text + ".wav"},
            {"thumbnail",GameObject.Find("thumbnail").GetComponentInChildren<InputField>().text},
            {"back",GameObject.Find("back").GetComponentInChildren<InputField>().text},
            {"movie",GameObject.Find("movie").GetComponentInChildren<InputField>().text}
        });
    }
    private void OnDestroy(){
        Subject.removeObserver("configMain");
    }
}