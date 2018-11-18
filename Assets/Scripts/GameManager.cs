using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public short game = 0;

    public int score = 0;

    float timer = 10;

    [SerializeField] Text time;

    private void Start()
    {
        foreach(GameManager gm in FindObjectsOfType<GameManager>())
        {
            if(gm != this)
            {
                Destroy(gm.gameObject);
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
                if (SceneManager.GetActiveScene().name != "Football")
                {
                    SceneManager.LoadScene("Football");
                }
                break;
        }
        FindObjectOfType<Move>().GetGame(game);

    }

    private void FixedUpdate()
    {
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
        score++;
        short currGame = game;
        while (currGame == game)
        {
            game = (short)Random.Range(0, 2);
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
                SceneManager.LoadScene("Football");
                FindObjectOfType<Move>().GetGame(game);
                break;
        }
    }
}
