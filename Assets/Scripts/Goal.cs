using UnityEngine;

public class Goal : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            FindObjectOfType<GameManager>().NextGame();
        }
    }
}
