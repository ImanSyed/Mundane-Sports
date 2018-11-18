using UnityEngine;

public class Follow : MonoBehaviour {

    Transform target;

	// Use this for initialization
	void Start () {
        if (FindObjectOfType<GameManager>().game == 0)
        {
            target = GameObject.FindGameObjectWithTag("Ball").transform;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (FindObjectOfType<GameManager>().game == 0 && target)
        {
            Vector3 pos = transform.position;
            pos.x = target.position.x;
            pos.y = target.position.y;
            transform.position = pos;
        }
	}
}
