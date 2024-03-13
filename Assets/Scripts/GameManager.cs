using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject hpPrefab;
    public int numberOfEnemiesToSpawn = 5;
    public float spawnRadius = 100f;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public bool isGameOver = false;
    public GameObject panelGameOver;
    private void Awake()
    {
        Time.timeScale = 1;
    }
    void Start()
    {
        InvokeRepeating("SpawnEnemiesRandomly", 0f, 10f);
        InvokeRepeating("SpawnHp", 0f, 10f);
        SetScoreUI(score);
    }
    private void Update()
    {
        if (isGameOver)
        {
            //show restart
            panelGameOver.SetActive(true);
            //Debug.Log("Game Over");
            //Time.timeScale = 0;
        }
    }
    void SpawnHp()
    {
        Vector3 randomPosition = GetRandomPosition();
        Instantiate(hpPrefab, randomPosition, Quaternion.identity);
    }
    void SpawnEnemiesRandomly()
    {
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 randomPosition = new Vector3(randomCircle.x, 0f, randomCircle.y) + transform.position;
        return randomPosition;
    }
    public void IncreaseScore(int amount)
    {
        score += amount;
        //Debug.Log("Score: " + score);
        SetScoreUI(score);
        
    }
    public void SetScoreUI(int amount)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + amount;
        }
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        // Reload the current scene
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
