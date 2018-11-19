using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameManager : MonoBehaviour {

    public short game = 0;

    public int score = 0;

    float timer = 0;

    [SerializeField] AudioClip click;

    [SerializeField] Sprite black;

    [SerializeField] Text scoreText;

    public Slider time;

    public bool disabled, active;


    [SerializeField] GameObject staticEffect;

    private void Start()
    {
        scoreText = FindObjectOfType<Text>();
        scoreText.text = score.ToString();
        
        time = FindObjectOfType<Slider>();
        foreach(GameManager gm in FindObjectsOfType<GameManager>())
        {
            if (gm != this)
            {
                if (gm.active)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(gm.gameObject);
                }
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {

        if(game == -1 && !disabled && Input.anyKeyDown)
        {
            active = false;
            NextGame();
        }

        if (!scoreText)
        {
            scoreText = FindObjectOfType<Text>();
            if (game != -1)
            {
                scoreText.text = score.ToString();
            }
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (timer < 0)
        {
            timer = 0;
        }
        if (timer == 0 && game != -1)
        {
            time.enabled = false;
            GameOver();
        }
        if (game != -1)
        {
            time.value = (int)timer;
        }
    }

    private void FixedUpdate()
    {
        if(game == 0)
        {
            if(GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody2D>().velocity.magnitude > 0)
            {
                if (!GameObject.FindGameObjectWithTag("Ball").GetComponent<Animator>().GetBool("Rolling"))
                {
                    GameObject.FindGameObjectWithTag("Ball").GetComponent<Animator>().SetBool("Rolling", true);
                }
            }
            else
            {
                if (GameObject.FindGameObjectWithTag("Ball").GetComponent<Animator>().GetBool("Rolling"))
                {
                    GameObject.FindGameObjectWithTag("Ball").GetComponent<Animator>().SetBool("Rolling", false);
                }
            }
        }
    }

    public void NextGame()
    {
        StartCoroutine(DisableControls(0.5f));
        timer = 10;
        score++;
        short currGame = game;

        StartCoroutine(Static());
        while (currGame == game)
        {
            game = (short)Random.Range(0, 3);
        }
        
        switch(game)
        {
            case 0:
            SceneManager.LoadScene("Football");
                FindObjectOfType<Move>().GetGame(game);
                break;
            case 1:
                SceneManager.LoadScene("Kayak");
                FindObjectOfType<Move>().GetGame(game);
                break;
            case 2:
                SceneManager.LoadScene("Cricket");
                FindObjectOfType<Move>().GetGame(game);
                break;
        }

        time = FindObjectOfType<Slider>();
        time.value = timer;
    }

    IEnumerator Static()
    {
        
        FindObjectOfType<AudioSource>().Play();
        staticEffect.SetActive(true);
        GetComponentInChildren<Animator>().Play("StaticScreen", 0);
        yield return new WaitForSeconds(0.75f);
        staticEffect.SetActive(false);
    }

    public void GameOver()
    {
        game = -1;
        StartCoroutine(DisableControls(1f));
        active = true;
        SceneManager.LoadScene("Off");
        staticEffect.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(click);
        GetComponentInChildren<Animator>().Play("SwitchOff", 0);
        scoreText.color = Color.white;
    }

    public IEnumerator DisableControls(float delay)
    {
        disabled = true;
        yield return new WaitForSeconds(delay);
        disabled = false;
    }
}
