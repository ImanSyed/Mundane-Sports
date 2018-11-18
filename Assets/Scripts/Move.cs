using UnityEngine;

public class Move : MonoBehaviour {

    short controlScheme;

	void Update () {
        switch (controlScheme)
        {
            case 0:
                Vector3 pos = transform.position;
                pos.x += Input.GetAxis("Horizontal") * 0.1f;
                pos.y += Input.GetAxis("Vertical") * 0.1f;
                transform.position = pos;
                break;
            case 1:
                Vector2 forcePos = transform.position;
                forcePos.x += 10f;
                if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    GetComponent<Rigidbody2D>().AddForceAtPosition((Vector2.right + Vector2.up * 2), forcePos);
                }
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    GetComponent<Rigidbody2D>().AddForceAtPosition((Vector2.right  + Vector2.down * 2), forcePos);
                }
                break;
        }
        
	}

    public void GetGame(short num)
    {
        controlScheme = num;
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
