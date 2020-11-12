using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClone : MonoBehaviour
{
    //아이템 먹어서 보조비행기가 생기도록 해야 한다
    //보조비행기는 일정시간마다 자동으로 총알발사 한다

    public GameObject clone;
    public GameObject bulletFactory;    //총알공장
    public float fireTime = 1.0f;         //1초에 한번씩 총알발사
    float curTime = 0.0f;                //누적 경과시간   


    // Update is called once per frame
    void Update()
    {
        //아이템을 먹었을때 보조비행기를 보여준다
        CreateClone();
        //보조비행기가 활성화 되었을때 자동 총알발사
        AutoFire();
    }

    private void CreateClone()
    {
        //간단하게 스페이스 눌렀을때 처리하자
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //clone.SetActive(true);
            if (clone.activeSelf) clone.SetActive(false);
            else clone.SetActive(true);
        }
    }

    private void AutoFire()
    {
        //클론이 액티브상태일때 총알 자동발사 하기
        if(clone.activeSelf)
        {
            //일정시간이 흐르면 총알을 발사해야 한다
            curTime += Time.deltaTime;
            if(curTime > fireTime)
            {
                //당연히 누적시간은 0으로 초기화 해줘야 한다
                curTime = 0.0f;

                //GameObject bullet = Instantiate(bulletFactory);
                //bullet.transform.position = GameObject.Find("Sub1").transform.position;
                //bullet.transform.position = clone.transform.Find("Sub1").position;
                //bullet.transform.position = clone.transform.GetChild(0).position;

                //GameObject[] bullet = new GameObject[2];
                GameObject[] bullet = new GameObject[clone.transform.childCount];
                for(int i = 0; i < clone.transform.childCount; i++)
                {
                    bullet[i] = Instantiate(bulletFactory);
                    bullet[i].transform.position = clone.transform.GetChild(i).position;
                }
            }
        }   
    }
}
