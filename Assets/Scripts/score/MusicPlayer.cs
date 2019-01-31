using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
	void Awake () {
        gameObject.AddComponent<AudioSource>();
	}
    //音声ファイル読み込み
}
