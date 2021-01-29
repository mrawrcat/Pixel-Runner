using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveForward : MonoBehaviour
{
    public float speed;
    public float jumpforce;
    public float stopNumber;

    [SerializeField]
    private float currentspeed;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump(); //this needs to be if grounded
            Debug.Log("space pressed");
        }

        currentspeed = rb2d.velocity.x;
    }

    void FixedUpdate()
    {
        //transform.Translate(Vector2.right * speed * Time.deltaTime);
        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        if(GameManager.manager.tileCount > stopNumber)
        {
            if (speed > 0)
            {
                speed -= 1f * Time.deltaTime;
            }
            else
            {
                speed = 0;
            }
        }
        

        
    }


    private void Jump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpforce);
    }
}
