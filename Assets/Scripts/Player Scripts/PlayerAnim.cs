using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    const string PLAYER_READY = "ready to attack";
    const string PLAYER_ATTACK = "attack";

    private Animator anim;
    private string current_state;
    private bool attacking;
    private float attack_delay = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack();
            Debug.Log("e pressed");
        }
    }

    private void Attack()
    {
        if (!attacking)
        {
            attacking = true;
            Change_Anim_State(PLAYER_ATTACK);
            attack_delay = anim.GetCurrentAnimatorStateInfo(0).length;
            Invoke("Complete_Attack", attack_delay);
            
        }
    }
    private void Complete_Attack()
    {
        attacking = false;
        Change_Anim_State(PLAYER_READY);
    }
    private void Change_Anim_State(string newState)
    {
        if (current_state == newState)
        {
            return;
        }

        anim.Play(newState);
        current_state = newState;
    }
}
