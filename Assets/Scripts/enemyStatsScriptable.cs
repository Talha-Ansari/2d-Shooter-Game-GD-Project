using UnityEngine;

[CreateAssetMenu(fileName = "enemyStatsScriptable", menuName = "Stats/Enemy Stats", order = 0)]
public class enemyStatsScriptable : ScriptableObject
{

    public float speed;
    public float dectetRange;
    public LayerMask attackLayerMask;


    public bool rangeType;

    // Shooting
    public GameObject bullet;


    public float bulletSpeed, upWordSpeed;
    public float offsety;
    public float fireRate;

    // Add Rigigbody to bullet
    public bool useGravityOnBullet;

    public int bulleRecived;



}