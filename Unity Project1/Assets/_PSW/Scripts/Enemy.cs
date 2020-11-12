using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
//자동으로 원하는 컴포넌트를 추가한다
//반드시 필요한 컴포넌트를 실수로 삭제할 수 있기때문에 강제로 붙어있게 해두는 장치

public class Enemy : MonoBehaviour
{
    //에너미의 역할?
    //똥피하기 느낌으로 위에서 아래로 떨어진다
    //에너미가 플레이어를 향해서 총알을 발사한다
    //충돌처리 - 리지드바디 사용하자

    //유니티 어트리뷰트 [] 공부하기
    //https://docs.unity3d.com/ScriptReference/AddComponentMenu.html

    public float speed = 5.0f; //에너미 이동속도


    // Update is called once per frame
    void Update()
    {
        //아래로 이동해라
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //자기가진도 없애고
        //충돌된 오브젝트도 없앤다
        Destroy(gameObject);
        //Destroy(gameObject, 2.0f);
        Destroy(collision.gameObject);
    }
}
