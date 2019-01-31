using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyNotePlayer {
    private MusicScore mScore;
    private MusicPlayer mPlayer;
    public KeyNotePlayer(MusicScore aScore,MusicPlayer aPlayer){
        mScore = aScore;
        mPlayer = aPlayer;
    }
}
