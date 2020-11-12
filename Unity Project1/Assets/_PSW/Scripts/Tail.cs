using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    //2D에서 지렁이 만들때
    //꼬랑지가 머리를 따라다니게 해보자
    //코랑지가 타겟(플레이어)의 위치를 알고 있어야 한다
    public GameObject target;       //플레이어 오브젝트
    public float speed = 3.0f;       //꼬랑지 속도


    // Update is called once per frame
    void Update()
    {
        //타겟 방향 구하기(벡터의 뺄셈)
        //방향 = 타겟 - 자기자신
        Vector3 dir = target.transform.position - transform.position;
        dir.Normalize(); //크기가 1인 벡터로 만들어서 방향으로만 사용한다
        transform.Translate(dir * speed * Time.deltaTime);
    }
}
