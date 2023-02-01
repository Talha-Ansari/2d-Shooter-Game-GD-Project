using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class enemyMovments : MonoBehaviour
{
    public float speedTime;
    public float distanceX, distanceY;
    public float attackRange;
    [Range(0, .5f)]
    public float attackSpeed;
    public float attackWait;
    public LayerMask attackLayerMask;
    int _a = 1;



    bool allowAttack;
    Vector2 startPosition;
    public float moveSpeed;
    bool petrolling;

    // 
    public string attackType;
    public GameObject posionBullet;

    // Start is called before the first frame update
    void Start()
    {
        MoveEnemy();
        allowAttack = true;
        petrolling = true;
        startPosition = transform.position;
    }



    RaycastHit2D ray;
    private void Update()
    {
        // Debug.Log(startPosition);

        Vector2 __direction = _a == 1 ? Vector2.left : Vector2.right;
        ray = Physics2D.Raycast(transform.position, __direction, attackRange, attackLayerMask);
        if (ray.collider != null && allowAttack)
        {
            allowAttack = false;
            petrolling = false;
            // Debug.Log(ray.collider.name);
            Debug.DrawLine(transform.position, ray.point, Color.red, 2);
            Attack(ray.collider.gameObject.transform.position, attackType);
        }
        else if (!petrolling && ray.collider == null && allowAttack)
        {
            petrolling = true;
            MoveToStartPosition();
        }
    }
    void MoveToStartPosition()
    {
        transform.DOComplete();
        transform.DOKill();
        transform.DOMove(startPosition, moveSpeed)
            .SetSpeedBased(true).SetEase(Ease.InOutSine)
            .OnComplete(MoveEnemy);

    }

    void MoveEnemy()
    {
        transform.DOMove((new Vector2(distanceX, distanceY)), speedTime)
                   .SetEase(Ease.InOutSine)
                   .SetLoops(-1, LoopType.Yoyo)
                   .OnStepComplete(RotateToOtherDirection);
    }
    void Attack(Vector2 __position, string __attackType)
    {
        transform.DOComplete();
        transform.DOKill();
        Vector2 __offset = _a == 1 ? new Vector2(1, distanceY) : new Vector2(-1, distanceY);

        if (attackType == "Range")
        {
            Vector2 __pos = transform.position;
            GameObject __bulletPoision = Instantiate(posionBullet, __pos + __offset, Quaternion.identity);
            gameObject.transform.SetParent(__bulletPoision.transform);
            __bulletPoision.GetComponent<Rigidbody2D>()
            .AddForce(Vector2.left * _a * attackRange * Time.deltaTime, ForceMode2D.Impulse);
            Invoke("WaitForNextAttack", attackSpeed);
        }
        else
        {
            transform.DOMove(__position + __offset, moveSpeed)
                       .SetEase(Ease.InOutSine).SetSpeedBased(true)
                        .OnComplete(() => allowAttack = true);
        }
    }
    void WaitForNextAttack()
    {
        allowAttack = true;
    }


    void RotateToOtherDirection()
    {
        transform.localScale = new Vector2(_a *= -1, 1);
    }


}
