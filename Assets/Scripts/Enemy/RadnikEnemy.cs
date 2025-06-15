using System.Collections;
using BNG;
using UnityEngine;
using UnityEngine.AI;

public class RadnikEnemy : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Awake()
    {
        SetUpEnemy();
        enemyType = EnemyType.Radnik;
        
    }



    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(transform.position, player.position);
        if (playerDistance <= attackRange && !attacking)
        {
            canMove = false;
            AttackPlayer();
            StartCoroutine(CheckIfAttackFinished());
        }
        else if (!attacking)
        {
            canMove = true;
        }


        if (canMove && !dead)
        {
            TrackPlayer();
        }
        else if (!canMove && !dead)
        {
            StopAgent();
        }

        
        
        
    }

    private IEnumerator CheckIfAttackFinished()
    {
        
        //provjeri je li animacija napada gotova
        yield return null;

        bool dealtDamage = false;

        while (animator.GetCurrentAnimatorStateInfo(0).IsName("AttackBlend"))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && !dealtDamage)
            {
                dealtDamage = true;
                //ako je proslo pola animacije, dealaj dmg
                Debug.Log("Dmgam");
                DamagePlayer(damageDealt);
            }
            yield return null;
        }

        //animacija gotova
        attacking = false;
    }

    protected override void AttackPlayer()
    {
        attacking = true;
        
        animator.SetFloat("AttackType", Random.Range(0, 2));
        animator.SetTrigger("Attack");


    }

    public override void Die()
    {
        StopAgent();
        animator.SetFloat("DeathType", Random.Range(0, 2));
        animator.SetTrigger("Die");
        canMove = false;
        dead = true;
        StartCoroutine(DespawnAfterDeath());

    }

}
