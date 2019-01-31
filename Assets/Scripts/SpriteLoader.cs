using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteLoader{
    //オブジェクトに指定した画像を貼り付ける(画像を変更する)
    static public void pasteImage(GameObject aObject, string aImageName){
        //byte[] tBytes = ReadImageFile("Assets/database/background/" + aImageName);
        byte[] tBytes = ReadImageFile(aImageName);
        Vector2Int tSize = measureSize(tBytes);
        //テクスチャ生成
        Texture2D tTexture = new Texture2D(tSize.x, tSize.y);
        tTexture.LoadImage(tBytes);

        //画像貼り付け
        SpriteRenderer tRenderer = aObject.GetComponent<SpriteRenderer>();
        //サイズ決定
        float tUnit;
        float tXRate = 1600f / tSize.x;
        float tYRate = 1000f / tSize.y;
        tUnit = (tXRate > tYRate) ? tXRate : tYRate;
        tUnit = 100 / tUnit;
        Sprite tSprite = Sprite.Create(tTexture, new Rect(0, 0, tSize.x, tSize.y), new Vector2(0.5f, 0.5f), tUnit);
        tRenderer.sprite = tSprite;
        aObject.transform.localScale = new Vector3(1, 1, 1);
        //位置調整
        //        Transform tTrans = aObject.GetComponent<Transform>();
        //        Vector3 tPosition = tTrans.position;
        //        tPosition.x -= tSize.x / tUnit /2;
        //        tPosition.y -= tSize.y / tUnit /2;
        //        tTrans.position = tPosition;
    }
    static public Sprite load(string aFilePath){
        byte[] tBytes = ReadImageFile(aFilePath);
        Vector2Int tSize = measureSize(tBytes);
        //テクスチャ生成
        Texture2D tTexture = new Texture2D(tSize.x, tSize.y);
        tTexture.LoadImage(tBytes);
        //サイズ決定
        float tUnit;
        float tXRate = 1600f / tSize.x;
        float tYRate = 1000f / tSize.y;
        tUnit = (tXRate > tYRate) ? tXRate : tYRate;
        tUnit = 100 / tUnit;
        Sprite tSprite = Sprite.Create(tTexture, new Rect(0, 0, tSize.x, tSize.y), new Vector2(0.5f, 0.5f), tUnit);
        return tSprite;
    }
    static public void loadAsync(string aFilePath,Action<Sprite> aRes){
        MyBehaviour.runCoroutine(loadAsyncSprite(aFilePath, aRes));
    }
    static private IEnumerator loadAsyncSprite(string aFilePath,Action<Sprite> aRes){
        byte[] tBytes = ReadImageFile(aFilePath);
        Vector2Int tSize = measureSize(tBytes);
        //テクスチャ生成
        Texture2D tTexture = new Texture2D(tSize.x, tSize.y);
        yield return null;
        tTexture.LoadImage(tBytes);
        yield return null;
        //サイズ決定
        float tUnit;
        float tXRate = 1600f / tSize.x;
        float tYRate = 1000f / tSize.y;
        tUnit = (tXRate > tYRate) ? tXRate : tYRate;
        tUnit = 100 / tUnit;
        yield return null;
        Sprite tSprite = Sprite.Create(tTexture, new Rect(0, 0, tSize.x, tSize.y), new Vector2(0.5f, 0.5f), tUnit);
        yield return null;
        aRes(tSprite);
    }
    //画像ファイルを読み込みバイトで返す
    static public byte[] ReadImageFile(string path){
        FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        BinaryReader bin = new BinaryReader(fileStream);
        byte[] values = bin.ReadBytes((int)bin.BaseStream.Length);

        bin.Close();

        return values;
    }
    //画像のバイト列からサイズを取得する
    static public Vector2Int measureSize(byte[] aImage){
        int pos = 16; // 16バイトから開始
        int width = 0;
        for (int i = 0; i < 4; i++){
            width = width * 256 + aImage[pos++];
        }
        int height = 0;
        for (int i = 0; i < 4; i++){
            height = height * 256 + aImage[pos++];
        }
        return new Vector2Int(width, height);
    }
}
