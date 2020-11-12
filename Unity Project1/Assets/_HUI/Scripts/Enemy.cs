using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[RequireComponent(typeof(Rigidbody2D))]
//자동으로 원하는 컴포넌트를 추가한다.
//반드시 필요한 컴포넌트를 실수로 삭제할 수 있기 때문에 강제로 붙어있게 해준다. 
public class Enemy : MonoBehaviour
{
    //에너미의 역할 
    //똥피하기 느낌으로 위에서 아래로 떨어진다
    //에너미가 플레이어를 향해서 총알을 발사한다.
    // **충돌처리**  -- 리지드 바디를 사용하자. 
    //유니티 어트리뷰트 [] 공부하기 
    public GameObject bulletFactory;    //에너미 총알 공장(프리팹)
    public Transform firePoint;         //에너미 총알 발사 위치 
    public float speed;                 //에너미 이동속도 
    public float hp;                    //에너미 체력

    public float curTime = 0.0f;                       //누적 경과시간 
    public float fireTime = 1.0f;               //1초에 한 발씩 총알 발사

    void Update()
    {
        //아래로 이동해라.
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        enemyFire();
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    //자기 자신도 없애고, 충돌된 오브젝트도 없앤다.
    //    Destroy(gameObject);                    //자기자신
    //    //Destroy(gameObject, 2.0f);            //자기자신 2초뒤에 삭제
    //    Debug.Log("충돌");
    //    Destroy(collision.gameObject);          //충돌된 오브젝트 
    //
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "SubPlayer")
        {
            hp -= 5;
            if(hp < 0)
            {
                gameObject.SetActive(false);
            }
            collision.gameObject.SetActive(false);
        }
    }
 
    void enemyFire()
    {
        curTime += Time.deltaTime;
        if(curTime > fireTime)
        {
            GameObject bullet = Instantiate(bulletFactory);
            bullet.transform.position = firePoint.transform.position;
            curTime = 0.0f;
            bullet.tag = "Enemy";
        }
    }
}
