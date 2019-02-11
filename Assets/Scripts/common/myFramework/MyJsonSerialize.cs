using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static partial class MyJson{
    //シリアライズして保存
    static public void serializeToFile(Dictionary<string,object> data,string fileName){
        string tString = serialize(data);
    }
    //シリアライズ
    static public string serialize(Dictionary<string, object> data){
        return "";
    }
}