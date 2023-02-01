using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class platformMovments : MonoBehaviour
{
    public float speedTime;
    public float distanceX, distanceY;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove((new Vector2(distanceX, distanceY)), speedTime)
                   .SetEase(Ease.InOutSine)
                   .SetLoops(-1, LoopType.Yoyo);
    }
}
