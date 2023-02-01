using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{

    public float maxTime, minTime;
    public GameObject blood;

    void Start()
    {
        StartCoroutine(Spawner());
    }

    void Update()
    {

    }

    IEnumerator Spawner()
    {
        while (true)
        {
            GameObject _a = Instantiate(blood, transform.position, blood.transform.rotation);
            _a.transform.SetParent(transform);
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        }
    }

}
