using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	///プレハブを取得しオブジェクトを生成
	public static Type createObjectFromPrefab<Type>(string name){
		// プレハブを取得
		GameObject prefab = (GameObject)Resources.Load ("prefab/"+name);
		// プレハブからインスタンスを生成
		return Instantiate (prefab).GetComponent<Type>();
	}
	/// 座標(X).
	public float X {
		set {
			Vector3 tPosition = transform.position;
			tPosition.x = value;
			transform.position = tPosition;
		}
		get { return transform.position.x; }
	}

	/// 座標(Y).
	public float Y {
		set {
			Vector3 tPosition = transform.position;
			tPosition.y = value;
			transform.position = tPosition;
		}
		get { return transform.position.y; }
	}
	///座標(XY)
	public Vector2 Position{
		set{
			transform.position = new Vector3 (value.x, value.y, transform.position.z);
		}
		get { return transform.position; }
	}
	///移動(引数は現在の位置からの差分)
	public void moveDelta(float x,float y){
		Vector3 tPosition=transform.position;
		transform.position=new Vector3(tPosition.x+x,tPosition.y+y,tPosition.z);
	}
	///拡大縮小倍率
	public float Scale{
		set{
			transform.localScale = new Vector3 (value, value, value);
		}
		get{ return transform.localScale.x; }
	}
	///回転Z
	public float RotationZ{
		set{
			Quaternion tRotation = transform.rotation;
			tRotation.z = value;
			transform.rotation = tRotation;
		}
		get{ return transform.rotation.z; }
	}
	///画像変更
	public void changeSprite(string aFileName){
		GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("sprites/"+aFileName);
	}
	///削除する
	public void delete(){
		Destroy(gameObject);
	}

	///移動
	public void moveTo(float x,float y,float velocity){
		if (mMoveToCoroutine != null)
			StopCoroutine (mMoveToCoroutine);
		mMoveToCoroutine = StartCoroutine (moveToCoroutine (x, y, velocity));
	}
	///移動停止
	public void stopMoveTo(){
		StopCoroutine (mMoveToCoroutine);
	}
	private Coroutine mMoveToCoroutine;
	private IEnumerator moveToCoroutine(float x,float y,float velocity){
		while (true) {
			Vector3 tPosition = transform.position;
			//移動方向ベクトル
			Vector2 tDirection = new Vector3 (x - tPosition.x, y - tPosition.y);
			//目標地点にいる
			if (tDirection.x == 0 && tDirection.y == 0)
				yield break;
			//移動方向ベクトルの大きさ
			float tLength = Mathf.Sqrt (tDirection.x * tDirection.x + tDirection.y * tDirection.y);
			//このフレームでの移動量 / 移動方向ベクトルの大きさ
			float tCurrentLength = Time.deltaTime * velocity / tLength;
			//移動量
			Vector2 tMoveVec=new Vector2(tDirection.x * tCurrentLength, tDirection.y * tCurrentLength);
			//目標地点を超えて移動しようとした
			if (Mathf.Abs (tDirection.x) < Mathf.Abs (tMoveVec.x) || Mathf.Abs (tDirection.y) < Mathf.Abs (tMoveVec.y)) {
				Position = new Vector2 (x, y);
				yield break;
			}
			//移動
			moveDelta (tDirection.x * tCurrentLength, tDirection.y * tCurrentLength);
			yield return null;
		}
	}
}
