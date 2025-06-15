using System.Collections;
using BNG;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //base enemy skripta za nasljedivanje
    public enum EnemyType
    {
        Radnik,
        AranianMac,
        AranianPistolj,
        Kukac,
        Komando,
        Sviker
    }

    public EnemyType enemyType;

    public float hp;

    //score when defeated
    public int score;

    public int damageDealt;

    public float attackRange;

    protected float playerDistance;

    protected bool attacking;

    public GameObject enemyPrefab;

    protected Transform player;

    protected NavMeshAgent agent;

    protected Animator animator;

    public bool canMove;

    public bool dead;

    public float despawnTime = 10f;

    protected Damageable damageable;

    protected Collider enemyCollider;



    protected void SetUpEnemy()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        damageable = this.GetComponent<Damageable>();
        enemyCollider = GetComponent<Collider>();
        enemyType = EnemyType.Radnik;
        canMove = true;
        dead = false;
        damageable.Health = hp;

    }

    public void ResetEnemy()
    {
        //kad se respawna iz poola
        canMove = true;
        dead = false;
        damageable.Health = hp;
        damageable.destroyed = false;
        enemyCollider.enabled = true;
    }

    public void DamagePlayer(int hp)
    {
        //pricaj sa gamestats skriptom za dmg
        if (GameStats.PlayerShields >= damageDealt)
        {
            //samo stit prima dmg
            GameStats.PlayerShields -= damageDealt;
        }
        else if (GameStats.PlayerShields < damageDealt && GameStats.PlayerShields != 0)
        {
            //stit prima dmg i ide na 0 i ostatak na hp
            damageDealt -= GameStats.PlayerShields;
            GameStats.PlayerShields = 0;
            GameStats.PlayerHealth -= damageDealt;
        }
        else
        {
            //sav dmg na hp
            GameStats.PlayerHealth -= damageDealt;
        }

    }

    public virtual void Die()
    {
        

    }

    protected IEnumerator DespawnAfterDeath()
    {
        yield return new WaitForSeconds(despawnTime);
        this.gameObject.SetActive(false);
    }

    protected void TrackPlayer()
    {
        //pronadi igraca
        agent.SetDestination(player.position);

    }

    protected void StopAgent()
    {
        //pronadi igraca
        agent.SetDestination(transform.position);

    }

    protected virtual void AttackPlayer()
    {
        //ako je u odredenom dometu, napadni igraca
    }

    public void ResetAttackFlag()
    {
        attacking = false;
    }
}
