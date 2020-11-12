using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletFactory;            //총알 공장(프리팹)
    public Transform firePoint;     //총알 발사위치 (2)

    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        //마우스 왼쪽 버튼 or 왼쪽 컨트롤 키 
        if(Input.GetButtonDown("Fire1"))
        {
            //총알공장(총알프리팹)에서 총알을 무한대로 찍어낼 수 있다.
            //Instantiate() 함수로, 프리팹 파일을 게임오브젝트로 만든다. 

            //총알 게임오브젝트 생성.
            GameObject bullet = Instantiate(bulletFactory);     //프리팹을 넣어서, 프리팹을 게임오브젝트로 만들어줌. 
            //총알 오브젝트의 위치 지정. 
            //bullet.transform.position = transform.position;     //->플레이어 위치에 넣어줘야함. 
            bullet.transform.position = firePoint.transform.position;
            bullet.tag = "Player";
        }
        //GetMouseButton(0) =>마우스 왼쪽버튼
        //GetMouseButton(1) =>마우스 오른쪽버튼
        //GetMouseButton(2) =>마우스 미들버튼(휠)
    }
}
