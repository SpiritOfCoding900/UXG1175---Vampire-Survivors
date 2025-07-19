using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    [SerializeField] private WeaponController weaponController;   // Assign this in Inspector
    [SerializeField] private WeaponScriptableObject weaponToGive;

    private void Update()
    {
        if (weaponController == null)
            weaponController = FindObjectOfType<Player>().GetComponentInChildren<WeaponController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            weaponController.weaponDataList.Add(weaponToGive); // Add to player's inventory
            Destroy(gameObject); // Destroy the pickup

            if (WeaponController.Instance != null)
            {
                WeaponController.Instance.AddWeapon(weaponToGive);
                Destroy(gameObject); // Destroy the pickup

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
