using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public partial class MyBehaviour : MonoBehaviour{
    /// <summary>
    /// scaleを変化させる
    /// </summary>
    /// <param name="delta">変化量</param>
    /// <param name="duration">変化時間</param>
    /// <param name="callback">変化終了時関数</param>
    public void scaleBy(Vector3 delta,float duration,Action callback=null){
        StartCoroutine(scaleDelta(delta,duration,callback));
    }
    private IEnumerator scaleDelta(Vector3 delta,float duration,Action callback){
        float tLeftTime = duration;
        Vector3 tLeftDistance = delta;
        while(true){
            tLeftTime -= Time.deltaTime;
            if(tLeftTime<=0){//拡大縮小完了
                gameObject.transform.localScale += tLeftDistance;
                if (callback != null) callback();
                yield break;
            }
            Vector3 tDelta = delta * (Time.deltaTime / duration);
            gameObject.transform.localScale += tDelta;
            tLeftDistance -= tDelta;
            yield return null;
        }
    }
    /// <summary>
    /// 移動させる
    /// </summary>
    /// <param name="delta">移動量</param>
    /// <param name="duration">移動時間</param>
    /// <param name="callback">移動終了時関数</param>
    public void moveBy(Vector3 delta, float duration, Action callback = null){
        StartCoroutine(moveDelta(delta, duration, callback));
    }
    private IEnumerator moveDelta(Vector3 delta, float duration, Action callback){
        float tLeftTime = duration;
        Vector3 tLeftDistance = delta;
        while (true){
            tLeftTime -= Time.deltaTime;
            if (tLeftTime <= 0){//移動完了
                gameObject.transform.localPosition += tLeftDistance;
                if (callback != null) callback();
                yield break;
            }
            Vector3 tDelta = delta * (Time.deltaTime / duration);
            gameObject.transform.localPosition += tDelta;
            tLeftDistance -= tDelta;
            yield return null;
        }
    }
    /// <summary>
    /// 回転させる
    /// </summary>
    /// <param name="delta">回転量</param>
    /// <param name="duration">回転時間</param>
    /// <param name="callback">回転終了時関数</param>
    public void rotateBy(float delta, float duration, Action callback = null){
        StartCoroutine(rotateDelta(delta, duration, callback));
    }
    private IEnumerator rotateDelta(float delta, float duration, Action callback){
        float tLeftTime = duration;
        float tLeftDistance = delta;
        while (true){
            tLeftTime -= Time.deltaTime;
            if (tLeftTime <= 0){//回転完了
                this.rotateZ += tLeftDistance;
                if (callback != null) callback();
                yield break;
            }
            float tDelta = delta * (Time.deltaTime / duration);
            this.rotateZ += tDelta;
            tLeftDistance -= tDelta;
            yield return null;
        }
    }
    /// <summary>
    /// 回転させ続ける
    /// </summary>
    /// <param name="speed">回転速度(度/s)</param>
    public void rotateForever(float speed){
        StartCoroutine(rotateForeverDelta(speed));
    }
    private IEnumerator rotateForeverDelta(float speed){
        while(true){
            rotateZ += speed * Time.deltaTime;
            yield return null;
        }
    }
}
