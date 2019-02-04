using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KeyMonitor {
    /// <summary>
    /// 現在のフレームで押されたkeyを返す
    /// </summary>
    /// <returns>押されたkeyのリスト</returns>
    static public List<KeyCode> getInputKey(){
        List<KeyCode> tKeys = new List<KeyCode>();
        foreach (char s in Input.inputString){
            KeyCode tCode = convertToCode(s);
            if (!Input.GetKeyDown(tCode)) continue;
            tKeys.Add(tCode);
        }
        return tKeys;
    }
    static public KeyCode convertToCode(char s){
        if (s == ' ') return KeyCode.Space;
        else return EnumParser.parse<KeyCode>(s.ToString());
    }
    static public KeyCode convertToCode(string s){
        if (s == " ") return KeyCode.Space;
        else return EnumParser.parse<KeyCode>(s.ToString());
    }
}
