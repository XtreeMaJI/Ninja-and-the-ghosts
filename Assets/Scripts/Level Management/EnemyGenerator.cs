using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    //Каждый раз, когда счёт увеличится на это значение будет проверка на то, нужно ли спавнить врага
    public int scoreBetweenSpawn = 10;
    public float spawnDistanceFromPlayer = 25; //Расстояние от игрока, на котором появится враг
    public float spawnShiftY = 0; //Смещение от минимального уровня для спавна врага

    public GameObject enemyInstance;

    public Transform deathZoneZTransform;

    private LevelManager lm;
    private LevelGenerator levelGenerator;

    private void Start()
    {
        lm = Object.FindObjectOfType<LevelManager>();  
        levelGenerator = Object.FindObjectOfType<LevelGenerator>();
    }

    private void Update()
    {
        if (lm.score != 0 && lm.score % scoreBetweenSpawn == 0 && isShouldSpawn())
            SpawnEnemy();
    }

    private bool isShouldSpawn()
    {
        return true;
    }

    private void SpawnEnemy()
    {
        float x = lm.transform.position.x + spawnDistanceFromPlayer;
        float y = Random.Range(deathZoneZTransform.position.y + spawnShiftY, levelGenerator.branchYPos);
        Vector3 spawnPos = new Vector3(x, y, 0f);


        Instantiate(enemyInstance, spawnPos, Quaternion.identity);
    }

}
