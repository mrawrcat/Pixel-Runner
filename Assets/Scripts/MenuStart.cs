using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStart : MonoBehaviour
{
    [SerializeField]
    private Vector2 target;

    private bool move;
    public float speed;
    [SerializeField]
    private SceneMove sceneMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (move && transform.position.x != target.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        else if(transform.position.x == target.x)
        {
            move = false;
            sceneMove.move_scene("Main");
        }
    }

    public void move_right()
    {
        move = true;
    }
}
