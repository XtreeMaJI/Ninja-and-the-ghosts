using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public int scoreBetweenSpawn = 10; //Каждый раз, когда счёт увеличится на это значение будет проверка на то, нужно ли спавнить врага
    public float spawnDistanceFromPlayer = 50f; //Расстояние от игрока, на котором появится враг
    public GameObject enemyInstance;

    private LevelManager lm;

    private int lastCheckedStore = 0; //Последний счёт, на котором уже проводилась проверка на необходимость спавна
    public int spawnAmountThresh = 10; //Количесвтов проверок на спавн, после которого враг будет спавниться после каждой проверки

    public float bottomSpawnBorder = 0f;
    public float topSpawnBorder = 0f;

    private EnvironmentDestroyer envDestroyer;

    private void Start()
    {
        lm = Object.FindObjectOfType<LevelManager>();
        envDestroyer = Object.FindObjectOfType<EnvironmentDestroyer>();
    }

    private void Update()
    {
        if (lm.score != 0 && lm.score % scoreBetweenSpawn == 0 && lm.score > lastCheckedStore)
        {
            lastCheckedStore = lm.score;
            if (isShouldSpawn())
                SpawnEnemy();
        }
            
    }

    private bool isShouldSpawn()
    {
        //Чем больше проверок на спавн врага прошли, тем больше вероятность спавна
        int checksCount = lastCheckedStore / scoreBetweenSpawn;
        float maxProb = (float)checksCount / (float)spawnAmountThresh;

        return Random.Range(0f, 1f) <= maxProb;
    }

    private void SpawnEnemy()
    {
        float x = lm.playerObj.transform.position.x + spawnDistanceFromPlayer;
        float y = Random.Range(bottomSpawnBorder, topSpawnBorder);
        Vector3 spawnPos = new Vector3(x, y, 0f);

        Vector3 spawnRot = new Vector3(0f, 0f, 0f);
        spawnRot.y = -90f;

        GameObject enemyObj = Instantiate(enemyInstance, spawnPos, Quaternion.identity);
        enemyObj.transform.eulerAngles = spawnRot;

        envDestroyer.AddToList(enemyObj);
    }

}