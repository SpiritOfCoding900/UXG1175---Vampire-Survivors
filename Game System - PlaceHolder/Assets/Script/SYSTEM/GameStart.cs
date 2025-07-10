using UnityEngine;

public class GameStart : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.spawnPlayerOnce(new Vector3(0, 0, 0));
        UIManager.Instance.Open(GameUIID.PlayerSelection);
    }
}
