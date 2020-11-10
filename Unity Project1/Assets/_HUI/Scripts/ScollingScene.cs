using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScollingScene : MonoBehaviour
{
    public Vector2 offset;
    Material mt;

    void Start()
    {
        mt = GetComponent<Renderer>().material;
    }

    void Update()
    {
        mt.mainTextureOffset += offset * Time.deltaTime;
    }
}
