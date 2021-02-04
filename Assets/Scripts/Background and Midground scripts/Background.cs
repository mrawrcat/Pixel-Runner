using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    private PoolBackground bgPool;
    // Start is called before the first frame update
    void Start()
    {
        bgPool = GetComponentInParent<PoolBackground>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * bgPool.bgSpeed);

        if (transform.position.x <= bgPool.xTurnOff)
        {
            bgPool.SpawnBG();
            gameObject.SetActive(false);
        }
    }
}
