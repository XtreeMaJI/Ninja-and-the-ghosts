using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    //Каждый раз, когда счёт увеличится на это значение будет проверка на то, нужно ли спавнить врага
    public int scoreBetweenSpawn = 10;
    public float spawnDistanceFromPlayer = 50; //Расстояние от игрока, на котором появится враг

    //Смещения по высоте для спавна врага
    public float bottomSpawnShiftY = 0; 
    public float topSpawnShiftY = 1;

    public GameObject enemyInstance;

    public Transform deathZoneZTransform;

    private LevelManager lm;
    private LevelGenerator levelGenerator;

    private int lastCheckedStore = 0; //Список счетов, на которых уже проводилась проверка на необходимость спавна

    public int spawnProbThresh = 10; //Количесвтов проверок на спавн, после которого враг будет спавниться после каждой проверки

    private void Start()
    {
        lm = Object.FindObjectOfType<LevelManager>();  
        levelGenerator = Object.FindObjectOfType<LevelGenerator>();
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
        float maxProb = (float)checksCount / (float)spawnProbThresh;

        return Random.Range(0f, 1f) <= maxProb;
    }

    private void SpawnEnemy()
    {
        float x = lm.playerObj.transform.position.x + spawnDistanceFromPlayer;
        float y = Random.Range(deathZoneZTransform.position.y + bottomSpawnShiftY, levelGenerator.branchYPos - topSpawnShiftY);
        Vector3 spawnPos = new Vector3(x, y, 0f);


        Instantiate(enemyInstance, spawnPos, Quaternion.identity);
    }

}
