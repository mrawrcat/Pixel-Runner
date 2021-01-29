using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTilemapOffByCollider : MonoBehaviour
{
    [SerializeField]
    private ObjectPoolNS tilemap_pool;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Tilemap")
        {
            collision.gameObject.SetActive(false);
            tilemap_pool.SpawnTileMap();
            GameManager.manager.tileCount++;
        }
    }
}
