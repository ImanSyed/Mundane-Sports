using UnityEngine;

public class Goal : MonoBehaviour {

    [SerializeField] bool antiGoal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (!antiGoal)
            {
                FindObjectOfType<GameManager>().NextGame();
            }
            else
            {
                FindObjectOfType<GameManager>().GameOver();

            }
        }
    }
}
