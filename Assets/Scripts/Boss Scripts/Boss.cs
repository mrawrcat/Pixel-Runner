using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    public bool battling;
    public Vector2 trans;
    public Vector2 battleTransform;
    
    private float health;
    [SerializeField]
    private GameObject finishPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!battling)
        {
            transform.position = new Vector2(player.position.x + trans.x, transform.position.y);
            health = 5;
        }
        else if (battling)
        {
            Move_Boss_To_Start_Point();
            
        }

        if(health <= 0 && battling)
        {
            finishPanel.SetActive(true);
        }
    }

    public void Move_Boss_To_Start_Point()
    {
        if (transform.position.x != player.position.x + 10f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x + battleTransform.x, transform.position.y), 5 * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(player.position.x + battleTransform.x, battleTransform.y);
        }
    }

    public void ToggleBattlingTrue()
    {
        battling = true;
        //health = 5;
    }

    public void ToggleBattlingFalse()
    {
        battling = false;
    }

    public void Set_Tilecount_Zero()
    {
        GameManager.manager.Reset_Tile_Count();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "bullet")
        {
            Debug.Log("hit by bullet");
            health--;
        }
    }
}
