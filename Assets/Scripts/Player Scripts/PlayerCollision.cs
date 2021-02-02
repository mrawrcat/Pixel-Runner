using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("Collision Detection")]
    public Vector2 bottomOffset;
    public Vector2 boxSize;
    //public Vector2 inWallOffset;
    //public Vector2 inWallBoxsize;

    [Header("Status")]
    public bool isGrounded;
    //public bool insideWall;

    [Header("Layers")]
    public LayerMask whatIsGround;
    //public LayerMask inWall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, boxSize, 0, whatIsGround);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + bottomOffset, boxSize);
        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireCube((Vector2)transform.position + inWallOffset, inWallBoxsize);
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere((Vector2)Atk_Pos.position, Atk_Radius);
    }
}
