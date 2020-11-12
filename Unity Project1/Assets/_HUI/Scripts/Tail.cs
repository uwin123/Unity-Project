using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    //2D에서 지렁이 만들 때 처럼, 꼬랑지가 머리를 따라다니게 만들어보자.
    //꼬랑지가 타겟의 위치를 알고 있어야 한다. 
    public GameObject target;           //플레이어 오브젝트 
    public float speed = 3.0f;          //꼬랑지 속도 
    
    void Update()
    {
        //타겟 방향 구하기 (벡터의 뺄셈)
        //방향은 타겟에서 자기 자신을 빼주면 된다. 
        Vector3 dir = target.transform.position - transform.position;       //플레이어 위치에서 꼬랑지 위치 빼는거
        dir.Normalize();                                                    //크기가 1인 벡터로 만들어 방향으로만 사용한다. 
        transform.Translate(dir * speed * Time.deltaTime);
    }
}
