using UnityEngine;
using UnityEngine.Events;

public class damagable : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    public UnityEvent<int, Vector2> damagehit;

    public bool isAlive => currentHealth > 0;

    private Animator animator;
    private Rigidbody2D rb;

    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage, Vector2 knockback)
    {
        if (!isAlive) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animator.SetBool("isalive", false);
            rb.velocity = Vector2.zero;
            animator.SetBool("lockvel", true); // lock movement on death
        }
        else
        {
            animator.SetTrigger("hit");
            rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);

            animator.SetBool("lockvel", true); // lock movement when hit
        }
    }

    // Call this from animation event to unlock movement after hit animation ends
    public void UnlockVelocity()
    {
        animator.SetBool("lockvel", false);
    }
}
