using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private string[] tags;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        for(int i = 0; i < tags.Length; i++)
        {
            if (collision.tag == tags[i])
            {
                rb2d.velocity = Vector2.zero;
                gameObject.SetActive(false);
            }
        }
        
    }
}
