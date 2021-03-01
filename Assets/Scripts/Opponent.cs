using UnityEngine;

public class Opponent : MonoBehaviour {

	void Update ()
    {
        if (GetComponent<SpriteRenderer>().sortingOrder != -(int)(transform.position.y * 32))
        {
            GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 32);
        }
	}
	
}
