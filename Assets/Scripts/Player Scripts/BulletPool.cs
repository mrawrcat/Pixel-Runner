using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }




    public Transform Grid;
    public float speed;

    [SerializeField]
    private PlayerMoveForward player_move;

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    [Header("Spawner tags")]
    public string[] tags;
    private void Start()
    {

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.SetParent(Grid.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);

            }
            poolDictionary.Add(pool.tag, objectPool);
        }

        //Debug.Log("you have " + Grid.childCount + " Tilemaps");
    }

    public GameObject SpawnProjectile(string tag, Vector2 pos, Vector3 dir)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with Tag " + tag + " doesnt exist");
            return null;
        }

        GameObject objToSpawn = poolDictionary[tag].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = pos;
        //objToSpawn.GetComponent<Player_Projectile>().dir = dir;
        //objToSpawn.GetComponent<Player_Projectile>().speed = speed;
        //objToSpawn.transform.position += dir * speed * Time.deltaTime;
        Rigidbody2D proj_rb2d = objToSpawn.GetComponent<Rigidbody2D>();
        proj_rb2d.velocity = Vector2.zero;
        proj_rb2d.AddForce(dir * (speed + player_move.speed), ForceMode2D.Impulse);
        poolDictionary[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }

    public void SpawnProjectile(Transform pos, Vector3 dir)
    {
        SpawnProjectile(tags[Random.Range(0, tags.Length)], pos.position, dir);
        ///Debug.Log("projectile spawned");
    }

}
