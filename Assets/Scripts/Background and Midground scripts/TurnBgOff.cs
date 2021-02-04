using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBgOff : MonoBehaviour
{
    [SerializeField]
    private float xTransform;
    private PoolBackground bgPool;
    // Start is called before the first frame update
    void Start()
    {
        bgPool = GetComponentInParent<PoolBackground>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= xTransform)
        {
            bgPool.SpawnBG();
            gameObject.SetActive(false);
        }
    }
}
