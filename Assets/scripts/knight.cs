using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knight : MonoBehaviour
{
    public float walkspeed = 3f;
    public detectionzone attackzone;
    public detectionzone groundzone;

    public float walkStopRate = 0.6f;
    private Rigidbody2D rb;
    private PlayingDirections directionplay;
    private Animator animator;

    damagable damagegy;

    public enum WalkDirection { Right, Left }
    private WalkDirection _walkDirection = WalkDirection.Right;

    private Vector2 directionVector = Vector2.right;

    private WalkDirection walkDirection
    {
        get => _walkDirection;
        set
        {
            if (value != _walkDirection)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                directionVector = (value == WalkDirection.Right) ? Vector2.right : Vector2.left;
                _walkDirection = value;
            }
        }
    }

    private bool _hastarget = false;
    public bool hastarget
    {
        get => _hastarget;
        private set
        {
            if (_hastarget != value)
            {
                _hastarget = value;
                animator.SetBool("hastarget", value);
            }
        }
    }

    public bool canmove
    {
        get
        {
            return animator.GetBool("canmove");
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        directionplay = GetComponent<PlayingDirections>();
        animator = GetComponent<Animator>();
        damagegy = GetComponent<damagable>();
    }

    private void Update()
    {
        hastarget = attackzone.detectedcoliders.Count > 0;

        if (attackcooldn > 0)
        {

            attackcooldn -= Time.deltaTime;
        }
    }

    public bool lockvel
    {
        get
        {
            return animator.GetBool("lockvel");
        }
    }

    public float attackcooldn { get {

            return animator.GetFloat("attackco");

            
        } private set {

            animator.SetFloat("attackco", Mathf.Max(value,0));
        
        
        } }

    private void FixedUpdate()
    {
        if (hastarget)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            return;
        }
        if (!lockvel) // ✅ CORRECT: use knight's own lockvel
        {
            if (canmove)
            {
                rb.velocity = new Vector2(walkspeed * directionVector.x, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
            }
        }
        if (directionplay.IsGround && directionplay.IsOnWall)
        {
            FlipDirection();
        }
    }


    private void FlipDirection()
    {
        walkDirection = (walkDirection == WalkDirection.Right) ? WalkDirection.Left : WalkDirection.Right;
    }

    public void onhit(int damage, Vector2 knockback)
    {
        rb.velocity= new Vector2(knockback.x, rb.velocity.y*knockback.y);
    }
}
