﻿using UnityEngine;

public class Goal : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            FindObjectOfType<GameManager>().NextGame();
            Debug.Log(120897);
        }
    }
}
