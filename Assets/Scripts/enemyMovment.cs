using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovment : MonoBehaviour
{

    public Transform[] targetPoints;

    Rigidbody2D _rb;

    Animator _animator;
    bool petrolLeft, petrolRight, petrolling;

    float _offset;

    // Shooting
    public Transform attackPosition;

    bool allowShooting;
    Vector2 _attackPosition;


    public enemyStatsScriptable stats;

    int _direction;

    int bulleRecived;

    // Start is called before the first frame update
    void Start()
    {
        bulleRecived = stats.bulleRecived;
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        petrolling = true;
        petrolLeft = true;
        petrolRight = false;
        allowShooting = true;
        _offset = Vector2.Distance(targetPoints[0].position, targetPoints[1].position);
    }

    // Update is called once per frame
    RaycastHit2D ray;
    void Update()
    {

        // After Chasing Player


        if (bulleRecived <= 0) Destroy(transform.parent.gameObject);

        if (petrolling)
        {
            if (petrolLeft && Vector2.Distance(targetPoints[0].position, transform.position) > .5)
            {
                _direction = 1;
                transform.localScale = new Vector2(_direction, 1);
                Move(_direction, 1);
                if (Vector2.Distance(targetPoints[0].position, transform.position) < 1f)
                {
                    petrolLeft = false;
                    petrolRight = true;
                    _rb.velocity = new Vector2(0, _rb.velocity.y);
                }
            }
            else if (petrolRight && Vector2.Distance(targetPoints[1].position, transform.position) > .5)
            {

                _direction = -1;
                transform.localScale = new Vector2(_direction, 1);
                Move(_direction, 1);
                if (Vector2.Distance(targetPoints[1].position, transform.position) < 1f)
                {
                    petrolRight = false;
                    petrolLeft = true;
                    _rb.velocity = new Vector2(0, _rb.velocity.y);
                }
            }
        }

        Vector2 __direction = _direction == 1 ? Vector2.left : Vector2.right;
        if (stats.rangeType)
        {
            Vector3 __offset = new Vector3(0, stats.offsety, 0);
            ray = Physics2D.Raycast(transform.position - __offset, __direction, stats.dectetRange, stats.attackLayerMask);
        }
        else
            ray = Physics2D.Raycast(transform.position, __direction, stats.dectetRange, stats.attackLayerMask);
        if (ray.collider != null)
        {
            Debug.DrawLine(transform.position, ray.point, Color.red, 2);
            petrolling = false;
            if (!stats.rangeType)
                Move(_direction, 1.5f);
            if (allowShooting && stats.rangeType)
            {
                allowShooting = false;
                _attackPosition = ray.collider.transform.position;
                Invoke("ShootAnimation", stats.fireRate / 2);
            }

        }
        else if (ray.collider == null && !petrolling)
        {
            petrolling = true;
            _rb.velocity = new Vector2(_rb.velocity.x / 2, _rb.velocity.y);
            Invoke("Inverse", 1);
        }


    }

    void Inverse()
    {
        if (petrolLeft)
        {
            petrolRight = true;
            petrolLeft = false;
        }
        else
        {
            petrolRight = false;
            petrolLeft = true;
        }
    }


    void Move(float __direction, float __speedMultiplayer)
    {
        _rb.AddForce(Vector2.left * stats.speed * __speedMultiplayer * __direction * Time.deltaTime);
    }
    void ShootAnimation()
    {
        Invoke("Shoot", stats.fireRate / 2);
        _animator.SetBool("Shoot", true);

    }
    void Shoot()
    {
        allowShooting = true;
        float __currentDirection = _direction;
        float __bulletSpeed = Vector2.Distance(transform.position, _attackPosition);
        GameObject __bulletPoision = Instantiate(stats.bullet, attackPosition.position, Quaternion.identity);
        __bulletPoision.transform.SetParent(transform.parent);

        if (!stats.useGravityOnBullet) __bulletPoision.GetComponent<Rigidbody2D>().gravityScale = 0;
        __bulletPoision.GetComponent<Rigidbody2D>()
        .AddForce((Vector2.left * __currentDirection * __bulletSpeed * stats.bulletSpeed + Vector2.up * stats.upWordSpeed), ForceMode2D.Impulse);
        Invoke("ResetAnimation", 0.1f);
    }


    void ResetAnimation()
    {
        _animator.SetBool("Shoot", false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            bulleRecived--;
            Destroy(other.gameObject);
            _animator.SetBool("hurt", true);
            Invoke("ResetHurttAnimation", 0.2f);

        }
    }

    void ResetHurttAnimation()
    {
        _animator.SetBool("hurt", false);
    }




}

