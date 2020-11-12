using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCenter : MonoBehaviour
{

    public GameObject enemyCreateFactory;            //적 비행기 공장(프리팹)
    public Transform enemyCreatePointMin;               //적 비행기 발사위치 최소 
    public Transform enemyCreatePointMax;               //적 비행기 발사위치 최대
    public float createCoolTime = 0.5f;
    public float createDelay = 0.0f;

    void Update()
    {
        spawnEnemy();
    }

    void spawnEnemy()
    {
        createDelay += Time.deltaTime;
        if (createDelay > createCoolTime)
        {
            //Enemys[i].SetActive(true);
            GameObject enemys = Instantiate(enemyCreateFactory);
            enemys.transform.position = new Vector3(Random.Range(enemyCreatePointMin.position.x, enemyCreatePointMax.position.x), enemyCreatePointMin.position.y, enemyCreatePointMin.position.z);
            createDelay = 0.0f;
        }
    }
}
