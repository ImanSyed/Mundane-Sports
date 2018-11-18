using UnityEngine;

public class Opponent : MonoBehaviour {



	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sortingOrder = (int)(transform.position.y * 32);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
