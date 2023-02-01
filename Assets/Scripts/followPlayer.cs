using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    void Start()
    {

    }

    void Update()
    {
        transform.position = new Vector3(player.position.x, 0, 0) + offset;


    }
}
