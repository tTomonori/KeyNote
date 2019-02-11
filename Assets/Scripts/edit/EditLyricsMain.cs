using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditLyricsMain : MonoBehaviour {
    private InputField tLyricsText;
	void Start () {
        tLyricsText = GameObject.Find("lyricsForm").GetComponent<InputField>();
        Arg tArg = MySceneManager.getArg("editLyrics");
        tLyricsText.text = MusicScoreData.mAllLyrics;
        Subject.addObserver(new Observer("editLyricsMain", (message) =>{
            if(message.name=="closeLyricsEditorButtonPushed"){
                MySceneManager.closeScene("editLyrics", null);
                return;
            }
            if(message.name=="applyLyricsButtonPushed"){
                MusicScoreData.mAllLyrics = tLyricsText.text;
                List<Arg> tNotes;
                List<Arg> tLyrics;
                string tError = LyricsStringConverter.convert(tLyricsText.text, out tNotes, out tLyrics);
                if(tError!=""){
                    AlartCreater.alart(tError);
                    return;
                }
                tError = MusicScoreData.applyLyrics(tNotes, tLyrics);
                if (tError != ""){
                    AlartCreater.alart(tError);
                    return;
                }
                return;
            }
        }));
	}
    private void OnDestroy(){
        Subject.removeObserver("editLyricsMain");
    }
}