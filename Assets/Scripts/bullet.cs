using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public float bulletSpeed;
    public float bulletRange;
    float _direction;
    public PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        _direction = playerStats._direction;
        if (_direction == -1)
            bulletRange -= transform.position.x;
        else
            bulletRange += transform.position.x;


    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * _direction * bulletSpeed * Time.deltaTime);

        if (_direction == 1 && _direction * bulletRange < transform.position.x)
            Destroy(gameObject);
        if (_direction == -1 && _direction * bulletRange > transform.position.x)
            Destroy(gameObject);

    }
}
