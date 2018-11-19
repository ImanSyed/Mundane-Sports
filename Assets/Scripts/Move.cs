using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    short controlScheme;
    bool left;
    short kayakspeed = 20;


    private void Start()
    {
        controlScheme = FindObjectOfType<GameManager>().game;

        if (FindObjectOfType<GameManager>().game == 1)
        {
            kayakspeed = (short)(20 - FindObjectOfType<GameManager>().score);
            if (kayakspeed < 1)
            {
                kayakspeed = 1;
            }
        }
        else if(FindObjectOfType<GameManager>().game == 2)
        {
            GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody2D>().AddForce(Vector2.left * 300);
        }
    }

    void Update () {
        switch (controlScheme)
        {
            case 0:
                Vector3 pos = transform.position;
                pos.x += Input.GetAxis("Horizontal") * 0.1f;
                pos.y += Input.GetAxis("Vertical") * 0.1f;
                if(pos.x != transform.position.x || pos.y != transform.position.y)
                {
                    GetComponent<Animator>().SetBool("Running", true);
                    if(pos.x < transform.position.x)
                    {
                        GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
                else
                {
                    GetComponent<Animator>().SetBool("Running", false);

                }
                transform.position = pos;
                GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 32);
                break;
            case 1:
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && left)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.right * kayakspeed);
                    GetComponent<Animator>().Play("PC_KayakL", 0);
                    left = false;
                }
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !left)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.right * kayakspeed);
                    GetComponent<Animator>().Play("PC_KayakR", 0);
                    left = true;
                }
                break;
            case 2:
                if (Input.anyKeyDown)
                {
                    StartCoroutine(Hit());
                }
                break;
        }
        
	}

    IEnumerator Hit()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Collider2D>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        GetComponent<Collider2D>().enabled = false;

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
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir.normalized * 250);
        } 
    }
}
