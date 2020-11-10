using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //총알 클래스 하는 일
    //플레이어가 발사 버튼을 누르면, 
    //총알이 생성된 후 발사하고 싶은 방향(윗 방향)
    public float speed = 10.0f;
    public float bulletAppearCount = 1.0f;
    public float bulletShootCoolTime = 0.0f;
   
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);       //--> 생성이 되자마자 총알이 위로 날라가는 거,
        bulletDestroy();
    }

    //나중에는 디스트로이즌을 이용해서 그곳과 충돌되면 사라지도록 맏늘예정

    //카메라 화면밖으로 나가서 보이지 않게 되면
    //호출되는 이벤트 함수 
    //유니티 내부에는 on으로 시작되는 함수는 전부 이벤트 함수들이다.
    //void OnBecameInvisible()
    //{
    //    Destroy(gameObject);    //gameObject 은 bullet 자기 자신. 
    //    //gameObject => 이 아이도 자주 사용하는 거라 소문자로 만들어져 있음. 
    //}

    void bulletDestroy()
    {
        if (bulletShootCoolTime > bulletAppearCount)
        {
            Destroy(gameObject);
            bulletShootCoolTime = 0;
        }
        bulletShootCoolTime += Time.deltaTime;
    }
}
