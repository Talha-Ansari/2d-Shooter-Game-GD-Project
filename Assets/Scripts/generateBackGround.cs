using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateBackGround : MonoBehaviour
{
    public float offsetx, offsetY;
    public GameObject background;
    public int noOfImages;
    void Start()
    {
        CreateBackGround();
    }

    void CreateBackGround()
    {
        float __x = 0;
        for (int i = 0; i < noOfImages; i++)
        {

            GameObject __a = Instantiate(background, new Vector2(__x, 5 + offsetY), Quaternion.identity);
            __a.transform.SetParent(transform);
            __x += offsetx;
        }
    }

    void Update()
    {

    }
}
