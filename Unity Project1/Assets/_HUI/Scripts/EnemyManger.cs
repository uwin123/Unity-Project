//using System;     랜덤함수를 사용할 수가 없다. 왜 생기는지는 원인 파악 불가. 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManger : MonoBehaviour
{
    //에너미매니져 역할?
    //에너미들을 공장에서 찍어낸다.(에너미 프리팹)
    //에너미 스폰 타임 -> 랜덤처리 
    //에너미 스폰위치 -> 화면 위에 배치를 한다.

    public GameObject enemyFactory;     //에너미 공장(에너미 프리팹)
    public GameObject[] spawnPoints;    //스폰위치 여러개,
    float spawnTime = 1.0f;             //스폰시간 (몇초에 한번씩 찍어낼 것인가)
    float curTime = 0.0f;               //누적시간 

    void Update()
    {
        //에너미 생성 
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        //몇초에 한번씩 이벤트 발동
        //누적시간으로 계산을 한다. 
        //게임에서 정말 자주 사용. 

        curTime += Time.deltaTime;
        if(curTime > spawnTime)
        {
            //누적된 현재시간을 0초로 초기화(반드시 해줘야 한다)
            curTime = 0.0f;
            //스폰시간을 랜덤으로 처리하자
            spawnTime = Random.Range(0.5f, 2.0f);
            //에너미 생성 
            GameObject enemy = Instantiate(enemyFactory);   //==>여기서 찍어낼꺼임!
            int index = Random.Range(0, spawnPoints.Length);
            enemy.transform.position = spawnPoints[index].transform.position;
            //or 
            //enemy.transform.position = transform.GetChild(index).position;
        }
    }
}
