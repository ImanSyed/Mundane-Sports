using UnityEngine;

public class Opponent : MonoBehaviour {

	void Start () {
        GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 32);	
	}
	
}
