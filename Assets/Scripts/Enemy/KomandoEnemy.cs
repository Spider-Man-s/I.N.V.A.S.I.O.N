using System.Collections;
using BNG;
using UnityEngine;
using UnityEngine.AI;

public class KomandoEnemy : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Awake()
    {
        SetUpEnemy();
        enemyType = EnemyType.Komando;
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

        while (animator.GetCurrentAnimatorStateInfo(0).IsName("AttackBlend"))
        {
            yield return null;
        }

        //animacija gotova
        attacking = false;
    }

    protected override void AttackPlayer()
    {
        attacking = true;
        animator.SetFloat("AttackType", Random.Range(0, 3));
        
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
