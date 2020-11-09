using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    //플레이어 이동 
    //플레이어는 사용자가 조작한다.. 
    // 따라서 입력을 받아야 한다.  
    //키보드, 마우스 등등 입력은 Input 매니져가 담당. 

    //이동 속력
    //public ==> 인스펙트 창에 변수가 노출된다.
    //기본은 private -> 
    public float speed = 5.0f;
    
   

    Rigidbody rigid;

    //보통 접근하려고하면, 밑에 처럼 선언하고, 해야하는데 트랜스폼은 자주사용해서 필요 X
    Transform tr;


    float camHeight;
    float camWidth;
    float playerHalfWidth;
    float playerHalfHeight;
  
    public float interval;      //==> 너무 스크린에 붙지 않도록 하게 살짝 간격 

    void Start()
    {
        //플레이어 이동처리. 
        //원래 선언 하려면 일케 해야함. 
        //tr = GetComponent<Transform>();

        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Screen.width / Screen.height;

        Vector3 colHalfSize = GetComponent<Collider>().bounds.extents;  //->콜라이더의 경계 사이즈의 반 를 가져오는거 . 
        playerHalfWidth = colHalfSize.x;
        playerHalfHeight = colHalfSize.y;

      

    }

    void Update()
    {
        //GetAxis 사용하는 이유. 
        //멀티플랫폼 사용때문에... (윈도우, 안드로이드)
        //GetAxis 값은 1 ~ -1 사이의 값. 

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //이동처리 
        // 1 . transform.Translate(h * speed * Time.deltaTime, v * speed * Time.deltaTime, 0);
        // 2 . 아래 방법도 가능 ( 방향을 결정해 준 다음에 쓰는거) 
        //(벡터의 덧셈일때는 크게 상관없으나, 뺄셈을 써야 할 경우 아래 방법이 더 좋음. )
        Vector3 dir = Vector3.right * h + Vector3.up * v;  //벡터의 덧셈 이용.
        //벡터의 정규화 
        dir.Normalize();        //->원래 대각선이면 1.4속도로 갓는데 이제 1로감. 
        //rigid = GetComponent<Rigidbody>(); //(dir * speed * Time.deltaTime);
        //rigid.velocity = new Vector3(h * speed, v * speed, 0);
       
        // **3. 
        // P = P0 + vt;x    
        //위치 = 현재 위치 + (방향 * 시간);
        //만약에 드라마틱한 구현을 하고자 한다면, 아래 처럼 하는게 좋음. 

        //transform.position = transform.position + dir * speed * Time.deltaTime;
        transform.position += dir * speed * Time.deltaTime;

        //deltaTime : FixedUpadate 에서는 써줄 필요가 없음. 

        //v플레이어를 화면밖으로 나가지 못하게 막기
        //1. 화면 밖 공간에 큐브 4개를 만들어, 배치하면 충돌체 때문에 밖으로 벗어나지 못하게 한다.
        // 리지드바디가 포함되어야 충돌처리가 가능. 

        //2. 플레이어 트랜스폼포지션 x ,y값을 고정시킨다. 
        //3. 메인카메라 뷰포트를 가져와 처리. 
        //Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -camWidth, camWidth), Mathf.Clamp(transform.position.y, -camHeight, camHeight), 0);

        //2번 ====================================================================
        //if (transform.position.x < -0.45f)
        //    transform.position = new Vector3(-0.45f, transform.position.y, transform.position.z);
        //if (transform.position.x > 4.5f)
        //    transform.position = new Vector3(4.5f, transform.position.y, transform.position.z);
        //if (transform.position.y < -4.45f)
        //    transform.position = new Vector3(-4.45f, transform.position.y, transform.position.z);
        //if (transform.position.y > 4.45f)
        //    transform.position = new Vector3(4.45f, transform.position.y, transform.position.z);


        //3번 ====================================================================
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -camWidth + playerHalfWidth + interval, camWidth - playerHalfWidth - interval), 
            Mathf.Clamp(transform.position.y, -camHeight + playerHalfHeight + interval, camHeight - playerHalfHeight - interval), transform.position.z);

       

    }

   

  
}
