using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private int enemyCount;
    public int score = 0;

    private LevelManager levelManager;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();

        CharacterControl[] enemies = FindObjectsOfType<CharacterControl>();
        enemyCount = enemies.Length - 1;
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void ResetScore()
    {
        Destroy(gameObject);
    }

    public void EnemyDead()
    {
        enemyCount--;

        if (enemyCount <= 0)
        {
            levelManager.LoadMapScene();
        }
    }
}