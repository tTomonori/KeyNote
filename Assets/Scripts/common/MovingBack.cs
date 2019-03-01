using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBack : MyBehaviour {
    [SerializeField] private SpriteRenderer mSprite;
    private Vector2 mMaxPozition;
    private Vector2 mMinPozition;
	void Start () {
        Vector3 tSize = mSprite.sprite.bounds.size;
        mMaxPozition = new Vector2(tSize.x * transform.lossyScale.x / 2 - 8, tSize.y * transform.lossyScale.y / 2 - 5);
        mMinPozition = new Vector2(-tSize.x * transform.lossyScale.x / 2 + 8, -tSize.y * transform.lossyScale.y / 2 + 5);
        move();
	}
    private void move(){
        Vector3 tGoal = new Vector3(Random.Range(mMinPozition.x, mMaxPozition.x), Random.Range(mMinPozition.y, mMaxPozition.y),positionZ);
        moveToWithSpeed(tGoal, 1, () =>{
            move();
        });
    }
}