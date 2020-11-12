using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //플레이어 이동
    //플레이어는 사용자가 조작한다
    //따라서 입력을 받아야 한다
    //키보드, 마우스 등등 입력은 Input매니져가 담당한다

    //이동속력
    //public => 인스펙트 창에 변수가 노출된다
    //기본은 private => 인스펙트 창에 변수가 노출되지 않는다
    public float speed = 5.0f;

    public Vector2 margin; //뷰포트좌표는 (0,0) ~ (1,1) 사이의 값을 사용한다 


    //트렌스폼은 자주사용하기때문에 소문자로 직접 접근이 가능하다
    //Transform tr;


    // Start is called before the first frame update
    void Start()
    {
        //tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //GetAxis 사용하는 이유?
        //멀티플랫폼 사용때문에 (윈도우, 안드로이드)
        //GetAxis => 1 ~ -1 사이의 값
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //이동처리
        //transform.Translate(h * speed * Time.deltaTime, v * speed * Time.deltaTime, 0);
        //아래 방법도 가능 (덧셈일때는 크게 상관없지만 뺄셈을 써야 할 경우 아래 방법이 더 좋음)
        //Vector3 dir = Vector3.right * h + Vector3.up * v;
        Vector3 dir = new Vector3(h, v, 0);
        //벡터의 정규화
        //dir.Normalize();
        transform.Translate(dir * speed * Time.deltaTime);

        //Vector3.zero => new Vector3(0, 0, 0);
        //Vector3.one => new Vector3(1, 1, 1);
        //Vector3.right => new Vector3(1, 0, 0);
        //Vector3.left => new Vector3(-1, 0, 0);
        //Vector3.forward => new Vector3(0, 0, 1);
        //Vector3.back => new Vector3(0, 0, -1);
        //Vector3.up => new Vector3(0, 1, 0);
        //Vector3.down => new Vector3(0, -1, 0);


        //Rect rc;
        //rc.left += 5;
        //rc.left = rc.left + 5;

        //중요해
        //P = P0 + vt;
        //위치 = 현재위치 + (방향 * 시간)
        //transform.position = transform.position + dir * speed * Time.deltaTime;
        //transform.position += dir * speed * Time.deltaTime; 


        //플레이어를 화면밖으로 나가지 못하게 막기
        MoveInScreen();

    }

    void MoveInScreen()
    {
        //플레이어를 화면밖으로 나가지 못하게 막기
        //1. 화면밖 공간에 큐브 4개를 만들어서 배치하면 충돌체 때문에 밖으로 벗어나지 못한다
        //- 리지드바디가 포함되어야 충돌처리가 가능함
        //2. 플레이어 트렌스폼 포지션 x, y값을 고정시킨다
        //if (transform.position.x > 2.5) transform.position.x = 2.5f;
        //아래와 같이 벡터3 변수를 만들어서 트렌스폼의 포지션벡터값을 대입후
        //연산해서 다시 트렌스폼에 넣어주는걸 캐싱이라고 한다
        //Vector3 position = transform.position;
        //if (position.x > 2.5f) position.x = 2.5f;
        //if (position.x < -2.5f) position.x = -2.5f;
        //이놈아가 성능이 훨씬 뛰어나다
        //position.x = Mathf.Clamp(position.x, -2.3f, 2.3f);
        //position.y = Mathf.Clamp(position.y, -3.5f, 5.5f);
        //transform.position = position;

        //3. 메인카메라의 뷰포트를 가져와서 처리한다
        //스크린좌표 : 모니터해상도 픽셀단위
        //뷰포트좌표 : 카메라의 사각뿔 끝에 있는 사각형 왼쪽하단(0, 0), 우측상단(1, 1)
        //UV좌표 : 화면 텍스트, 2D 이미지를 표시하기 위한 좌표계로 텍스쳐좌표계라고도 한다
        //왼쪽상단(0, 0), 우측하단(1, 1)

        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);
        position.x = Mathf.Clamp(position.x, 0.0f + margin.x, 1.0f - margin.x);
        position.y = Mathf.Clamp(position.y, 0.0f + margin.y, 1.0f - margin.y);
        transform.position = Camera.main.ViewportToWorldPoint(position);

        /*
        메인 카메라의 중요함수
        메인 카메라또한 자주 사용하기때문에 어디에서든 접근할 수 있도록
        Camera.main 으로 사용가능하다
        1. ScreenToViewportPoint
        - Transform > position 의 화면 좌표(x, y)에서 viewport 좌표로 변환하는 역할  
        2. ScreenToWorldPoint
        - Transform > position 의 화면 좌표(x, y)에서 world 좌표로 변환하는 역할
        3. ViewportToScreenPoint
        - viewport 좌표를 Transform > position 의 화면 좌표(x, y)로 변환하는 역할
        4. ViewportToWorldPoint
        - viewport 좌표를 world 좌표로 변환하는 역할
        5. WorldToScreenPoint
        - world 좌표를 Transform > position 의 화면 좌표(x, y)로 변환하는 역할
        6. WorldToViewportPoint
        - world 좌표를 viewport 좌표로 변환하는 역할
        
        질문 - 스크린의 화면을 마우스로 클릭햇을때 3D공간의 클릭지점으로 오브젝트를 움직일때
        답 : Camera.main.ScreenToWorldPoint
        */


    }
}
