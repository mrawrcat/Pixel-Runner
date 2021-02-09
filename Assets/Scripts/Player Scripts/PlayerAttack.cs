using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    const string PLAYER_IDLE = "winged_idle";
    const string PLAYER_ATTACK = "winged_attack";
    const string PLAYER_CHARGING = "winged_charging";
    const string PLAYER_CHARGED = "winged_charged";
    const string PLAYER_GROUND_IDLE = "winged_grounded_idle";
    const string PLAYER_GROUND_ATTACK = "winged_grounded_attack";
    const string PLAYER_GROUND_CHARGING = "winged_grounded_charging";
    const string PLAYER_GROUND_CHARGED = "winged_grounded_charged";


    [Header("Ability Stuff")]
    public float Atk_Radius;
    public Transform Atk_Pos;
    public bool holding_atk;
    public float holding_atk_timer;
    public float hit_pause_dur;


    private Animator anim;
    private PlayerCollision player_collision;
    private string current_state;
    private bool attacking;
    private float attack_delay = 0.5f;


    [Header("Layers")]
    public LayerMask whatIsEnemy;

    [SerializeField]
    private BulletPool bullet_pool;

   
    //private CamShake shake;
    //private bool isFrozen = false;
    // Start is called before the first frame update
    void Start()
    {
        //anim = FindObjectOfType<PlayerUpperAnim>().gameObject.GetComponent<Animator>();
        //shake = FindObjectOfType<CamShake>();
        player_collision = GetComponent<PlayerCollision>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardInputAttack();

        if (player_collision.isGrounded && !attacking && !holding_atk)
        {
            Change_Anim_State(PLAYER_GROUND_IDLE);
        }
        else if(!player_collision.isGrounded && !attacking && !holding_atk)
        {
            Change_Anim_State(PLAYER_IDLE);
        }
    }

    public void KeyboardInputAttack()
    {
        
        if (Input.GetKey(KeyCode.E))
        {
            holding_atk_timer += Time.deltaTime;
            holding_atk = true;

            if (player_collision.isGrounded && holding_atk_timer < 1f)
            {
                Change_Anim_State(PLAYER_GROUND_CHARGING);
            }
            else if (player_collision.isGrounded && holding_atk_timer > 1f)
            {
                Change_Anim_State(PLAYER_GROUND_CHARGED);
            }
            else if(!player_collision.isGrounded && holding_atk_timer < 1f)
            {
                Change_Anim_State(PLAYER_CHARGING);
            }
            else if (!player_collision.isGrounded && holding_atk_timer > 1f)
            {
                Change_Anim_State(PLAYER_CHARGED);
            }

        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (holding_atk_timer > 1f)
            {
                //Attack();
                Shoot_Bullet();
                Debug.Log("holding power atk");
            }
            else
            {
                //Attack();
                Debug.Log("regular attack");
            }
            
            holding_atk_timer = 0;
            holding_atk = false;
        }

    }

    public void Shoot_Bullet()
    {
        bullet_pool.SpawnProjectile(Atk_Pos, Vector3.right);
    }

    public void Attack()
    {

        if (!attacking)
        {
            attacking = true;
            if (player_collision.isGrounded)
            {
                Change_Anim_State(PLAYER_GROUND_ATTACK);
                attack_delay = anim.GetCurrentAnimatorStateInfo(0).length;
            }
            else
            {
                Change_Anim_State(PLAYER_ATTACK);
                attack_delay = anim.GetCurrentAnimatorStateInfo(0).length;
            }
            Invoke("Complete_Attack", attack_delay);

        }

        Collider2D[] hit_Enemies = Physics2D.OverlapCircleAll(Atk_Pos.position, Atk_Radius, whatIsEnemy);
        foreach (Collider2D enemy in hit_Enemies)
        {
            //shake.Shake();
            /*
            if (enemy.GetComponent<Barrel>() != null)
            {
                //GameManager.manager.smash_sfx.Play();
                if (!isFrozen)
                {
                    StartCoroutine(hit_pause(hit_pause_dur));
                }
                enemy.GetComponent<Barrel>().Take_Dmg();
            }
            if (enemy.GetComponent<EnemyProjectile>() != null)
            {
                //GameManager.manager.smash_sfx.Play();
                if (!isFrozen)
                {
                    StartCoroutine(hit_pause(hit_pause_dur));
                }
                Debug.Log("attacked enemy projectile");
                enemy.GetComponent<EnemyProjectile>().Atk_Projectile();
            }
            if (enemy.GetComponent<IEnemy>() != null)
            {
                SoundManager.sound_manager.smash_sfx.Play();
                if (!isFrozen)
                {
                    StartCoroutine(hit_pause(hit_pause_dur));
                }
                enemy.GetComponent<IEnemy>().Die();
            }

            */
        }
    }

    public IEnumerator hit_pause(float dur)//for pause when melee hit maybe bullet hit too?
    {
        //isFrozen = true;
        var original_timescale = Time.timeScale;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(dur);
        Time.timeScale = original_timescale;
        //isFrozen = false;
    }

    private void Complete_Attack()
    {
        attacking = false;
        if (player_collision.isGrounded)
        {
            Change_Anim_State(PLAYER_GROUND_IDLE);
        }
        else
        {
            Change_Anim_State(PLAYER_IDLE);
        }
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere((Vector2)Atk_Pos.position, Atk_Radius);
    }
}
