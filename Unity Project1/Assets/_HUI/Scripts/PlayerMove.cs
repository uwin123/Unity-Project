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
    public float hp;
   
    //Rigidbody rigid = null;

    //보통 접근하려고하면, 밑에 처럼 선언하고, 해야하는데 트랜스폼은 자주사용해서 필요 X
    Transform tr;

    float camHeight;
    float camWidth;
    float playerHalfWidth;
    float playerHalfHeight;

    //float curTime = 0.0f;                       //누적 경과시간 
    //public float fireTime = 1.0f;               //1초에 한 발씩 총알 발사

    GameObject goWalls;
    GameObject[] SubPlayer;


    public float interval;      //==> 너무 스크린에 붙지 않도록 하게 살짝 간격 


    //==========뷰포트용 변수================
    public Vector2 margin;      // 뷰포트 좌표는 (0,0) ~ (1,1) 사이의 값을 사용한다. 
    //======================================

    void Start()
    {
        //플레이어 이동처리. 
        //원래 선언 하려면 일케 해야함. 
        //tr = GetComponent<Transform>();

        //rigid = GetComponent<Rigidbody>();

        goWalls = GameObject.FindGameObjectWithTag("Wall");
        goWalls.SetActive(false);
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Screen.width / Screen.height;

        Vector2 colHalfSize = GetComponent<Collider2D>().bounds.extents;  //->콜라이더의 경계 사이즈의 반 를 가져오는거 . 
        playerHalfWidth = colHalfSize.x;
        playerHalfHeight = colHalfSize.y;

        //서브 플레이어 선언
        SubPlayer = GameObject.FindGameObjectsWithTag("SubPlayer");

        for (int i = 0; i < SubPlayer.Length; i++)
        {
            SubPlayer[i].SetActive(false);
        }
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
        //Vector3 dir = Vector3.right * h + Vector3.up * v;  //벡터의 덧셈 이용.
        Vector3 dir = new Vector3(h, v, 0);
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


        //Vector3.zero => new Vector3(0,0,0);
        //Vector3.one => new Vector3(1,1,1);
        //Vcetor3.right => new Vector3(1,0,0);
        //Vector3.left => new Vector3(-1,0,0);
        //Vector3.forward => new Vector3(0,0,1);
        //Vector3.up => new Vector3(0,1,0);
        //Vector3.back => new Vecotr3(0,0,-1);
        //Vector3.down => new Vector3(0,-1,0);

        //rigid.velocity += new Vector3(h * speed * Time.deltaTime, v * speed * Time.deltaTime, 0f);

        //deltaTime : FixedUpadate 에서는 써줄 필요가 없음. 

        //v플레이어를 화면밖으로 나가지 못하게 막기
        //1. 화면 밖 공간에 큐브 4개를 만들어, 배치하면 충돌체 때문에 밖으로 벗어나지 못하게 한다.
        // 리지드바디가 포함되어야 충돌처리가 가능. 

        //2. 플레이어 트랜스폼포지션 x ,y값을 고정시킨다. 
        //3. 메인카메라 뷰포트를 가져와 처리. 
        //Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -camWidth, camWidth), Mathf.Clamp(transform.position.y, -camHeight, camHeight), 0);

        //1번 ====================================================================
        //create에서 cube 4개를 만들어 벽을 맞춰 생성. 

        //if (Input.GetMouseButtonDown(0))
        //{
        //    goWalls.SetActive(!goWalls.activeSelf);
        //}
        //
        ////2번 ====================================================================
        //if (transform.position.x < -4.5f)
        //    transform.position = new Vector3(-4.5f, transform.position.y, transform.position.z);
        //if (transform.position.x > 4.5f)
        //    transform.position = new Vector3(4.5f, transform.position.y, transform.position.z);
        //if (transform.position.y < -4.5f)
        //    transform.position = new Vector3(transform.position.x, -4.5f, transform.position.z);
        //if (transform.position.y > 4.5f)
        //    transform.position = new Vector3(transform.position.x, 4.5f, transform.position.z);

        //3번 ====================================================================
        // transform.position = new Vector3(Mathf.Clamp(transform.position.x, -camWidth + playerHalfWidth + interval, camWidth - playerHalfWidth - interval), 
        //     Mathf.Clamp(transform.position.y, -camHeight + playerHalfHeight + interval, camHeight - playerHalfHeight - interval), transform.position.z);

        MoveInScreen();
        subPlayerSpawn();
    }

    void MoveInScreen()
    {
        //캐싱==//


        //if (transform.position.x > 2.5f) transform.position.x = 2.5f;
        //위에 줄은 오류가 나버림. 
        //따라서 아래처럼 해야하는데, 이 과정을 '캐싱'이라고 함. 벡터 3변수를 만들어, 트랜스폼의 포지션 벡터값을 대입후 연산해서 다시 트랜스폼에 넣어주는것.
        //Vector3 position = transform.position;
        //if (position.x > 2.5f) position.x = 2.5f; 
        //if (position.x < -2.5f) position.x = -2.5f; 
        //if (position.y > 2.5f) position.y = 2.5f; 
        //if (position.y < -2.5f) position.y = -2.5f;

        //position.x 의 값을 고정하고 싶어. 즉. clamp 사용
        //position.x = Mathf.Clamp(position.x, -2.3f, 2.3f);
        //position.y = Mathf.Clamp(position.y, -3.5f, 3.5f);
        //transform.position = position;

        //최적화 1. 정점줄이기
        //      2. 사용하지 않는 함수 줄이기. 


        //뷰퐅트========//

        //뷰포트 : 메인카메라의 뷰포트를 가져와서 처리. 
        //스크린좌표 : 모니터 해상도, 픽셀 단위, 
        //뷰포트좌표 : 카메라의 사각을 끝에 있는 사각형 왼쪽하단(0,0) ,우측상단(1,1)
        //UV좌표 : 화면 텍스트, 2D 이미지를 표시하기 위한 좌표계로 텍스쳐 좌표계라고도 함. 좌상단이 (0,0), 우측하단이 (1,1)

        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);
        position.x = Mathf.Clamp(position.x, 0.0f + margin.x , 1.0f - margin.x );
        position.y = Mathf.Clamp(position.y, 0.0f + margin.y , 1.0f - margin.y );
        transform.position = Camera.main.ViewportToWorldPoint(position);
    }

    void subPlayerSpawn()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < SubPlayer.Length; i++)
            {
                //SubPlayer[i].SetActive(true);
                //스페이스를 토글키로 만들자. 
                if (SubPlayer[i].activeSelf)
                    SubPlayer[i].SetActive(false);
                else
                    SubPlayer[i].SetActive(true);
            }
        }
    }

    //void AutoFire()
    //{
    //    for (int i = 0; i < SubPlayer.Length; i++)
    //    {
    //        if(SubPlayer[i].activeSelf)
    //        {
    //            //일정시간이 흐르면 총알을 발사해야 한다.
    //            curTime += Time.deltaTime;
    //            if(curTime > fireTime)
    //            {
    //                //당연히 누적시간은 0으로 초기화 해줘야 함.
    //                curTime = 0.0f;
    //                GameObject bullet = Instantiate(bulletFactory);
    //                bullet.transform.position = GameObject.Find("Sub1").transform.position;
    //
    //                bullet.transform.position = SubPlayer.transform.Find("Sub1").position;
    //
    //                bullet.transform.position = Sublplayer.transform.GetChild(0).position;
    //            }
    //        }
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            hp -= 3;
            if(hp < 0)
            {
                gameObject.SetActive(false);
            }
            collision.gameObject.SetActive(false);
        }
    }
}
