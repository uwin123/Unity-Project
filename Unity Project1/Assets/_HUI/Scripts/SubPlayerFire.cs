using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPlayerFire : MonoBehaviour
{
    public GameObject subBulletFactory;         //총알 공장(프리팹)
    public Transform subFirePoint;              //총알 발사위치 (2)
    public float shootCount;                    //서브 플레이어 총알 카운트
    public float shootDelay = 0.5f;
    public float shootTimer = 0;

    public Transform tfTarget;

    void Update()
    {
        subFire();
        transform.RotateAround(tfTarget.position, Vector3.forward, 300 * Time.deltaTime);
        transform.localEulerAngles = Vector3.zero;
    }

    void subFire()
    {
        if(shootTimer >shootDelay)
        {
            //총알 게임오브젝트 생성.
            GameObject bullet = Instantiate(subBulletFactory);
            bullet.transform.position = subFirePoint.transform.position;
            shootTimer = 0;
        }
        shootTimer += Time.deltaTime; 
    }
}
