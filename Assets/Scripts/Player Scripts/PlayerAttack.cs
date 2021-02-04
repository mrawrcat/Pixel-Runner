using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Ability Stuff")]
    public float Atk_Radius;
    public Transform Atk_Pos;
    public bool holding_atk;
    public float holding_atk_timer;
    public float hit_pause_dur;

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
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardInputAttack();
    }

    public void KeyboardInputAttack()
    {
        
        if (Input.GetKey(KeyCode.E))
        {
            holding_atk_timer += Time.deltaTime;
            holding_atk = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (holding_atk_timer > 1f)
            {
                //Attack();
                Debug.Log("holding power atk");
            }
            else
            {
                //Attack();
                Shoot_Bullet();
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere((Vector2)Atk_Pos.position, Atk_Radius);
    }
}
