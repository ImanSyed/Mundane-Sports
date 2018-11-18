using UnityEngine;

public class Follow : MonoBehaviour {

    public Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (FindObjectOfType<GameManager>().game == 0)
        {
            Vector3 pos = transform.position;
            pos.x = target.position.x;
            pos.y = target.position.y;
            transform.position = pos;
        }
	}
}
