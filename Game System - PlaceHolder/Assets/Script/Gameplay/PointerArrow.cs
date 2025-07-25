using UnityEngine;
using System.Collections.Generic;

public class PointerArrow : MonoBehaviour
{
    public GameObject arrow;
    public string enemyTag = "Enemy";
    public float updateInterval = 0.2f;
    public float spinSpeed = 180f;

    private Transform nearestEnemyTransform;
    private float nextUpdateTime;
    void Start()
    {
        nextUpdateTime = Time.time + updateInterval; //assign update interval
        FindNearestEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextUpdateTime) // after the update interval has passed
        {
            FindNearestEnemy();
            nextUpdateTime = Time.time + updateInterval; //reset update interval
        }

        if (nearestEnemyTransform != null) //when enemy found 
        {
            SpinToEnemy();
        }

        /* else // hide arrow when there are no enemies. 
        {
            if (arrow.activeSelf)
            {
                arrow.SetActive(false);
            }
        } */
    }

    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); //find all enemies with the enemny tag
        if (enemies.Length == 0)
        {
            nearestEnemyTransform = null;
            return;
        }
        float shortestDistance = Mathf.Infinity;
        GameObject currentNearestEnemy = null;

        Vector2 currentPosition =  new Vector2 (transform.position.x, transform.position.y);

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null)
            {
                {
                    continue;
                }
            }

            // Get position of the enemy
            Vector2 enemyPosition = new Vector2(enemy.transform.position.x, enemy.transform.position.y);
            float distance = Vector2.Distance ( currentPosition, enemyPosition);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                currentNearestEnemy = enemy;
            }
        }

        if (currentNearestEnemy != null)
        {
            nearestEnemyTransform = currentNearestEnemy.transform;
        }

        else
        {
            nearestEnemyTransform = null;
        }
    }


    void SpinToEnemy ()
    {

        // unhide arrow when the enemy is found 
        /* if (!arrow.activeSelf)
        {
            arrow.SetActive (true);
        } */

        Vector2 arrowPosition = new Vector2 (arrow.transform.position.x, arrow.transform.position.y);
        Vector2 enemyPosition = new Vector2 (nearestEnemyTransform.position.x,  nearestEnemyTransform.position.y);

        Vector2 directionToEnemy = enemyPosition - arrowPosition;

        //Adjust angle of the arrow
        float angle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;

        // Base rotation that points to the enemy
        Quaternion targetLookRotation = Quaternion.Euler (0, 0, angle);

        //Spinning rotaton around the z axis
        Quaternion spinRotation = Quaternion.Euler(0, 0, Time.time * spinSpeed);

        //Combine the 2: First look, then spin
        arrow.transform.rotation = targetLookRotation * spinRotation;   
    }
}
