using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class MyBehaviour : MonoBehaviour {
    static private MyBehaviour instance;
    static private MyBehaviour ins{
        get{
            if (instance == null){
                instance = MyBehaviour.create<MyBehaviour>();
                instance.name = "MyBehaviourInstance";
            }
            return instance;
        }
    }
    /// <summary>
    /// 指定したComponentもつGameObjectを生成
    /// </summary>
    /// <returns>生成されたGameObjectがもつComponent</returns>
    /// <typeparam name="T">取り付けるComponent</typeparam>
    static public T create<T>() where T : MonoBehaviour{
        return new GameObject().AddComponent<T>();
    }
    /// <summary>
    /// 指定したパスのプレハブを生成
    /// </summary>
    /// <returns>生成したプレハブがもつComponent</returns>
    /// <param name="aFilePath">プレハブへのパス("prefab/" + X)</param>
    /// <typeparam name="Type">取得するComponent</typeparam>
    public static Type createObjectFromPrefab<Type>(string aFilePath){
        // プレハブを取得
        GameObject prefab = (GameObject)Resources.Load("prefab/" + aFilePath);
        // プレハブからインスタンスを生成
        return Instantiate(prefab).GetComponent<Type>();
    }
    /// <summary>
    /// aSecond秒後にaFunctionを実行
    /// </summary>
    /// <param name="aSeconds">aFunctionを実行するまでの時間(秒)</param>
    /// <param name="aFunction">実行する関数</param>
    public void setTimeout(float aSeconds,Action aFunction){
        StartCoroutine(coroutine(aSeconds, aFunction));
    }
    private IEnumerator coroutine(float aSeconds, Action aFunction){
        yield return new WaitForSeconds(aSeconds);
        aFunction();
    }
    ///子ルーチン実行
    static public void runCoroutine(IEnumerator aCoroutine){
        ins.StartCoroutine(aCoroutine);
    }
    ///削除する
    public void delete(){
        Destroy(gameObject);
    }
    ///座標
    public Vector3 position{
        get { return gameObject.transform.localPosition; }
        set { gameObject.transform.localPosition = value; }
    }
    public float positionX{
        get { return gameObject.transform.localPosition.x; }
        set {
            Vector3 tPosition = gameObject.transform.localPosition;
            gameObject.transform.localPosition = new Vector3(value, tPosition.y, tPosition.z);
        }
    }
    public float positionY{
        get { return gameObject.transform.localPosition.y; }
        set{
            Vector3 tPosition = gameObject.transform.localPosition;
            gameObject.transform.localPosition = new Vector3(tPosition.x, value, tPosition.z);
        }
    }
    public float positionZ{
        get { return gameObject.transform.localPosition.z; }
        set{
            Vector3 tPosition = gameObject.transform.localPosition;
            gameObject.transform.localPosition = new Vector3(tPosition.x, tPosition.y, value);
        }
    }
    //スケール
    public Vector3 scale{
        get { return gameObject.transform.localScale; }
        set { gameObject.transform.localScale = value; }
    }
    public float scaleX{
        get { return gameObject.transform.localScale.x; }
        set {
            Vector3 tScale = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector3(value, tScale.y, tScale.z);
        }
    }
    public float scaleY{
        get { return gameObject.transform.localScale.x; }
        set {
            Vector3 tScale = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector3(tScale.x, value, tScale.z);
        }
    }
    public float scaleZ{
        get { return gameObject.transform.localScale.x; }
        set {
            Vector3 tScale = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector3(tScale.x, tScale.y, value);
        }
    }
}
