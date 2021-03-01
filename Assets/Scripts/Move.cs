using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Move : MonoBehaviour {


    [SerializeField] AudioClip hit;
    short controlScheme;
    bool left = true;
    int kayakspeed = 40, runSpeed = 10;


    private void Start()
    {
        controlScheme = FindObjectOfType<GameManager>().game;

        FindObjectOfType<GameManager>().time = FindObjectOfType<Slider>();

        if (FindObjectOfType<GameManager>().game == 1)
        {
            kayakspeed = 40 - FindObjectOfType<GameManager>().score;
            if (kayakspeed < 15)
            {
                kayakspeed = 15;
            }
        }
        else if(FindObjectOfType<GameManager>().game == 2)
        {
            Invoke("Bowl", Random.Range(0.5f, 2.5f));
        }
        else
        {
            foreach(Opponent op in FindObjectsOfType<Opponent>())
            {
                Vector3 pos = op.transform.position;
                pos.y = Random.Range(-2.5f, 8.5f);
                op.transform.position = pos;
            }
        }
    }

    void Bowl()
    {

        GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody2D>().AddForce(Vector2.left * ((FindObjectOfType<GameManager>().score * 2) + Random.Range(275, 375)));
    }

    void Update () {
        switch (controlScheme)
        {
            case 0:
                Vector3 pos = transform.position;
                pos.x += Input.GetAxis("Horizontal") * Time.deltaTime * runSpeed;
                pos.y += Input.GetAxis("Vertical") * Time.deltaTime * runSpeed;
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
                if (left)
                {
                    if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)))
                    {
                        GetComponent<Rigidbody2D>().AddForce(Vector2.right * kayakspeed);
                        GetComponent<Animator>().Play("PC_KayakL", 0);
                        FindObjectOfType<GameManager>().GetComponent<AudioSource>().PlayOneShot(hit);
                        left = false;
                    }
                    
                }
                else
                {
                    if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)))
                    {
                        GetComponent<Rigidbody2D>().AddForce(Vector2.right * kayakspeed);
                        GetComponent<Animator>().Play("PC_KayakR", 0);
                        FindObjectOfType<GameManager>().GetComponent<AudioSource>().PlayOneShot(hit);
                        left = true;
                    }
                }
                break;
            case 2:
                if (Input.anyKeyDown && !FindObjectOfType<GameManager>().disabled)
                {
                    if (!GetComponent<Collider2D>().enabled)
                    {
                        GetComponent<Animator>().Play("PC_Cricket", 0);
                        GetComponent<Collider2D>().enabled = true;
                        StartCoroutine(Hit());
                    }
                }
                break;
        }
	}

    IEnumerator Hit()
    {
        
        yield return new WaitForSeconds(0.25f);
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
            FindObjectOfType<AudioSource>().PlayOneShot(hit);
            Vector2 foot = transform.position;
            foot.y -= 1;
            Vector2 dir = (Vector2)collision.gameObject.transform.position - foot;
            if(controlScheme == 2)
            {
                dir.y = Random.Range(-1f, 1f);
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir.normalized * Random.Range(200, 350));
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir.normalized * Random.Range(150, 250));
            }
        } 
    }
}
