using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackControls : MonoBehaviour
{
    [SerializeField]
    private float stopNumber;
    [SerializeField]
    private float setXSpeed;
    private float xSpeed;
    [SerializeField]
    private float setYSpeed;
    private float ySpeed;
    private bool holdkey;
    private Rigidbody2D rb2d;
    [SerializeField]
    private Boss bossScripts;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        xSpeed = setXSpeed;
        ySpeed = setYSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            holdkey = true;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            holdkey = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            holdkey = false;
        }

        if (GameManager.manager.tileCount > stopNumber)
        {
            if (xSpeed > 0)
            {
                xSpeed -= ((setXSpeed/10f) * 2) * Time.deltaTime;
                //xSpeed = Mathf.MoveTowards(xSpeed, 0, 1);

            }
            else
            {
                xSpeed = 0f;
                bossScripts.ToggleBattlingTrue();
            }
        }
    }

    private void FixedUpdate()
    {
        //rb2d.velocity = new Vector2(xSpeed, ySpeed);
        if (holdkey)
        {
            rb2d.velocity = new Vector2(xSpeed, ySpeed);
        }
        else if(!holdkey)
        {
            rb2d.velocity = new Vector2(xSpeed, rb2d.velocity.y);
        }
    }
}
