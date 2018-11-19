using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameManager : MonoBehaviour {

    public short game = 0;

    public int score = 0;

    float timer = 8;

    [SerializeField] Text timeText, scoreText;

    [SerializeField] GameObject staticEffect;

    private void Start()
    {
        scoreText = FindObjectOfType<Text>();
        scoreText.text = score.ToString();
        staticEffect.SetActive(false);
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
        game = 1;
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

    private void FixedUpdate()
    {
        if (!scoreText)
        {
            scoreText = FindObjectOfType<Text>();
            scoreText.text = score.ToString();
        }
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if(timer < 0)
        {
            timer = 0;
        }
        if(timer == 0)
        {
            GameOver();
        }

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
        timer = 8;
        score++;
        short currGame = game;
        FindObjectOfType<AudioSource>().Play();
        staticEffect.SetActive(true);
        StartCoroutine(D());
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
    }

    IEnumerator D()
    {
        yield return new WaitForSeconds(0.311f);
        staticEffect.SetActive(false);
    }

    void GameOver()
    {
        score = 0;
        scoreText.text = score.ToString();
    }
}
