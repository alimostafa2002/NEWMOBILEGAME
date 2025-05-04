using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class attack : MonoBehaviour
{
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        damagable damagable = collision.GetComponent<damagable>();
        if (damagable != null && damagable.isAlive) // Only hit alive targets
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            damagable.TakeDamage(attackDamage, deliveredKnockback);

            Debug.Log($"{collision.name} hit for {attackDamage} damage");
        }
    }
}
