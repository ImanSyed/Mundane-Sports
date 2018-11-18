using UnityEngine;

public class Move : MonoBehaviour {

    short controlScheme;
    bool left;
    short kayakspeed = 20;


    private void Start()
    {
        if (FindObjectOfType<GameManager>().game == 1)
        {
            kayakspeed = (short)(20 - FindObjectOfType<GameManager>().score);
            if (kayakspeed < 1)
            {
                kayakspeed = 1;
            }
        }
    }

    void Update () {
        switch (controlScheme)
        {
            case 0:
                Vector3 pos = transform.position;
                pos.x += Input.GetAxis("Horizontal") * 0.1f;
                pos.y += Input.GetAxis("Vertical") * 0.1f;
                transform.position = pos;
                GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 32);
                break;
            case 1:
                Vector2 forcePos = transform.position;
                forcePos.x += 1f;
                if((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && left)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.right * kayakspeed);
                    left = false;
                }
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !left)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.right * kayakspeed);
                    left = true;
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
        if (collision.gameObject.tag == "Ball")
        {
            Vector2 foot = transform.position;
            foot.y -= 1;
            Vector2 dir = (Vector2)collision.gameObject.transform.position - foot;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir.normalized * 200);
        }
        
    }
}
