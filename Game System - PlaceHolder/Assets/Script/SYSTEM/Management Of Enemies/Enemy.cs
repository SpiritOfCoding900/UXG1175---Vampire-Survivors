using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    Transform player;
    public float HP;
    public float moveSpeed;
    private bool isMoving = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null || UIManager.Instance.OpenReplace(GameUIID.YouWin) != null)
            isMoving = true;
        else
            isMoving = false;

        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }

        EnemyDead();
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
    }

    public void EnemyDead()
    {
        if (HP <= 0)
        {
            // Give Exp
            float expValueWhenDead = 2f;
            PlayerLevelUpStats.Instance.SetExperience(expValueWhenDead);

            // Death
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
            Destroy(gameObject, 1.5f);
        }
    }
}
