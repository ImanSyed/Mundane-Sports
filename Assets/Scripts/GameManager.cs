using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameManager : MonoBehaviour {

    public short game = 0;

    public int score = 0;

    float timer = 9;

    [SerializeField] Text scoreText;

    public Slider time;

    [SerializeField] GameObject staticEffect;

    private void Start()
    {
        scoreText = FindObjectOfType<Text>();
        scoreText.text = score.ToString();
        staticEffect.SetActive(false);
        time = FindObjectOfType<Slider>();
        foreach(GameManager gm in FindObjectsOfType<GameManager>())
        {
            if (gm != this)
            {
                if (gm.score > score)
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
        game = (short)Random.Range(0, 3);
        switch (game)
        {
            case 0:
                if (SceneManager.GetActiveScene().name != "Football")
                {
                    SceneManager.LoadScene("Football");
                }
                break;
            case 1:
                if (SceneManager.GetActiveScene().name != "Kayak")
                {
                    SceneManager.LoadScene("Kayak");
                }
                break;
            case 2:
                if (SceneManager.GetActiveScene().name != "Cricket")
                {
                    SceneManager.LoadScene("Cricket");
                }
                break;
        }
        FindObjectOfType<Move>().GetGame(game);

    }

    private void Update()
    {

        if (!scoreText)
        {
            scoreText = FindObjectOfType<Text>();
            scoreText.text = score.ToString();
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            
        }
        else if (timer < 0)
        {
            timer = 0;
        }
        if (timer == 0)
        {
            time.enabled = false;
            GameOver();
        }
        time.value = (int)timer;
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
        timer = 9;
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
        yield return new WaitForSeconds(0.311f);
        staticEffect.SetActive(false);
    }

    void GameOver()
    {
        score = 0;
        scoreText.text = score.ToString();
    }
}
