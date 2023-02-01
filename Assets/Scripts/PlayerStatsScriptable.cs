using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsScriptable", menuName = "Stats/PlayerStats", order = 0)]
public class PlayerStats : ScriptableObject
{

    public float _direction;

    public int life;


    public float speed;
    public float velocityCap;

    public float jumpHeight;

    public float fireRate;

}