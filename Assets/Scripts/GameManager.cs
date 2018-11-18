using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public short game = 0;

    int score = 0;

    float timer = 6;

    GameManager gm;

    private void Start()
    {
        if (gm == null)
        {
            gm = this;
        }
        else if(gm != this)
        {
            Destroy(gameObject);
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
    }

    public void NextGame()
    {
        score++;
        game = (short)Random.Range(0, 2);
        switch(game)
        {
            case 0:
            SceneManager.LoadScene("Football");
                FindObjectOfType<Move>().GetGame(game);
                break;
            case 1:
                SceneManager.LoadScene("Kayak");
                break;
            case 2:
                SceneManager.LoadScene("Football");
                break;
        }
    }
}
