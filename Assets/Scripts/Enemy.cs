using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void Die()
    {
        Destroy(gameObject);
        gameManager.IncreaseScore(Random.Range(3,10));
    }
}
