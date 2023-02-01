using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerControlar : MonoBehaviour
{
    Rigidbody2D rb;
    int lifes;

    // Bugs Solving 
    bool allowJump;

    // Inputs
    float _horizontal;
    bool _jump;
    Animator _animator;



    // Gun
    public GameObject bullet;
    public Transform attackPoint;


    bool _shoot;
    float _shootTime = 0;
    public float _direction = 1;


    public PlayerStats playerStats;
    // Start is called before the first frame update


    bool dead;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        allowJump = false;
        _animator = GetComponent<Animator>();


        lifes = playerStats.life;

    }

    // Update is called once per frame
    void Update()
    {

        if (lifes <= 0 && !dead)
        {
            dead = false;
            _animator.SetBool("Dead", true);
            GetComponent<PlayerControlar>().enabled = false;
            gameObject.layer = 0;
        }

        MyInputs();
        playerStats._direction = _direction;


    }

    private void FixedUpdate()
    {
        // Left Right Movments
        if (_horizontal != 0)
        {
            rb.AddForce(Vector2.right * playerStats.speed * _horizontal * Time.deltaTime);
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -playerStats.velocityCap, playerStats.velocityCap), rb.velocity.y);
            // Debug.Log(rb.velocity);
            if (_horizontal == 1)
            {
                _direction = 1;
                transform.localScale = new Vector2(1, 1);
            }
            else
            {
                _direction = -1;
                transform.localScale = new Vector2(-1, 1);
            }
            _animator.SetBool("Run", true);
        }
        else
            _animator.SetBool("Run", false);


        // Stop Charater when left and right is not pressed
        if (_horizontal == 0 && allowJump)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // Jump
        if (_jump && allowJump)
        {
            FindObjectOfType<AudioManager>().Play("jump");
            rb.AddForce(Vector2.up * playerStats.jumpHeight, ForceMode2D.Impulse);
            allowJump = false;
        }



        // Shooting
        if (_shoot && Time.time > playerStats.fireRate + _shootTime)
        {
            _shootTime = Time.time;
            FindObjectOfType<AudioManager>().Play("shoot");
            GameObject _bullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
            // _bullet.SetActive(true);
            // _bullet.transform.Translate(Vector2.right * _direction * bulletSpeed * Time.deltaTime);
        }

    }



    void MyInputs()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _jump = Input.GetKey(KeyCode.Space);
        _shoot = Input.GetKey(KeyCode.Mouse0);
    }




    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            allowJump = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
            lifes--;
            FindObjectOfType<AudioManager>().Play("hit");
            _animator.SetBool("hurt", true);
            Invoke("ResetHurttAnimation", 0.5f);
        }
    }

    void ResetHurttAnimation()
    {
        _animator.SetBool("hurt", false);
    }


}