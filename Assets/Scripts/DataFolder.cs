using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class DataFolder {
    static public string path{
        get{
            if (Debug.isDebugBuild)
                return Application.dataPath + "/../data";
            else
                return Application.dataPath + "/../../data";
        }
    }
}
