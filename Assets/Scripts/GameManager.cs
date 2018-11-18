using UnityEngine;

public class GameManager : MonoBehaviour {

    public int game = 0;

    float timer = 6;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
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
        game = Random.Range(0, 3);
        if(game == 0)
        {

        }
    }
}
