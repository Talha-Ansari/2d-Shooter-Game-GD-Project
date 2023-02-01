using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{
    public bool speed, jump, fireRate;
    public PlayerStats playerStats;
    public PowerUpStats powerUpStats;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (speed) StartCoroutine(Speed());
            else if (jump) StartCoroutine(Jump());
            else if (fireRate) StartCoroutine(FireRate());
        }
    }



    IEnumerator Speed()
    {
        speed = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        playerStats.speed *= powerUpStats.speedMultiplayer;
        yield return new WaitForSeconds(powerUpStats.boostUpSpeed);
        playerStats.speed /= powerUpStats.speedMultiplayer;
        Destroy(gameObject);

    }


    IEnumerator Jump()
    {
        jump = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        playerStats.jumpHeight *= powerUpStats.jumpHeightMultiplayer;
        yield return new WaitForSeconds(powerUpStats.boostUpSpeed);
        playerStats.jumpHeight /= powerUpStats.jumpHeightMultiplayer;
        Destroy(gameObject);

    }
    IEnumerator FireRate()
    {
        fireRate = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        playerStats.fireRate *= powerUpStats.fireRate;
        yield return new WaitForSeconds(powerUpStats.boostUpSpeed);
        playerStats.fireRate /= powerUpStats.fireRate;
        Destroy(gameObject);

    }
}
