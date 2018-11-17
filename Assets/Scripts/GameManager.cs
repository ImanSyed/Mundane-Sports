using UnityEngine;

public class GameManager : MonoBehaviour {

    public int game = 0;

    void NextGame()
    {
        game = Random.Range(0, 10);
    }
}
