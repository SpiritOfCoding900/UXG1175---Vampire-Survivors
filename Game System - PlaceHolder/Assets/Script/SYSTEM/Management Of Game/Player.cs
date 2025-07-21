using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SimpleSingleton<Player>
{
    [Header("Player's Current Stats: ")]
    public string className;

    public float MaxHP = 10;
    public float HP;

    public float moveSpeed = 5f;
    public string description;
    private Rigidbody2D rb;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public Vector2 lastMovedVector;

    [Header("Player screams when takes damage: ")]
    public AudioClip PlayerScreams;
    private AudioSource audioSource;

    [Header("Player's Damage Invincible Time: ")]
    private bool isInvincible = false;
    public float invincibilityDuration = 1f;

    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        lastMovedVector = new Vector2 (1, 0f);
        HP = MaxHP;
    }

    void Update()
    {
        Inputmanagement();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Inputmanagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

        if(moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f);
        }

        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector);
        }

        if (moveDir.x != 0 && moveDir.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector);
        }
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) return;

        // AudioManager.Instance.SFXSound(SoundID.PlayerScreams);
        audioSource.PlayOneShot(PlayerScreams, 1);

        HP -= amount;
        Debug.Log($"Player took {amount} damage. Remaining HP: {HP}");

        if (HP <= 0)
            Debug.Log("You're dead");

        // Start invincibility
        StartCoroutine(InvincibilityCoroutine());
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        // Optional: Add visual feedback here (e.g. blinking or flashing)
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }
}
