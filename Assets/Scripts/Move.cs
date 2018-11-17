using UnityEngine;

public class Move : MonoBehaviour {

    [SerializeField] float speed = 0.1f;

	void Update () {
        Vector3 pos = transform.position;
        pos.x += Input.GetAxis("Horizontal") * speed;
        pos.y += Input.GetAxis("Vertical") * speed;
        transform.position = pos;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>())
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Vector2 foot = transform.position;
            foot.y -= 0.75f;
            Vector2 dir = (Vector2)collision.gameObject.transform.position - foot;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir.normalized * 250);
        }
    }
}
