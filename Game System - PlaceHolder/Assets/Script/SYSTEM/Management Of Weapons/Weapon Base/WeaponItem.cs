using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    [SerializeField] private WeaponScriptableObject weaponToGive;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Destroy(gameObject); // Destroy the pickup
            if (WeaponController.Instance != null)
            {
                bool added = WeaponController.Instance.AddWeapon(weaponToGive);
                if (added)
                {
                    Debug.Log($"Player picked up: {weaponToGive.name}");
                    Destroy(gameObject); // Destroy the pickup
                }
                else
                {
                    Debug.Log("Player already has this weapon.");
                    Destroy(gameObject); // Destroy the pickup
                }
            }
        }
    }
}
