using UnityEngine;

public class GameManager : SimpleSingleton<GameManager>
{
    private PlayerStats _playerStats;
    private EnemyStats _enemyStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.Instance.Open(GameUIID.Logo);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
