using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class playercontroller : MonoBehaviour
{
    Vector2 moveinput;
    public float walkspeed = 5f;
    public float runspeed = 5f;
    public float airwalkspeed = 3f;

    PlayingDirections playingdirection;

    damagable damagegy;

    public float currentmovespeed
    {
        get
        {
            if (ismoving)
            {
                if (isrunning)
                {
                    return runspeed;
                }
                else
                {
                    return walkspeed;
                }
            }
            else
            {
                return 0;
            }
        }
    }

    [SerializeField]
    private Boolean _ismoving = false;

    [SerializeField]
    private Boolean _isrunning = false;

    public bool ismoving
    {
        get
        {
            return _ismoving;
        }
        private set
        {
            _ismoving = value;
            animator.SetBool("ismoving", value);
        }
    }

    public bool isrunning
    {
        get
        {
            return _isrunning;
        }
        private set
        {
            _isrunning = value;
            animator.SetBool("isrunning", value);
        }
    }

    public bool isfacingright
    {
        get { return _isfacingright; }
        private set
        {
            if (_isfacingright != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isfacingright = value;
        }
    }

    Rigidbody2D rb;
    Animator animator;

    public float attackrange = 0.5f;
    public LayerMask enemyLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damagegy = GetComponent<damagable>();
        playingdirection = GetComponent<PlayingDirections>();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!lockvel)
        {
            rb.velocity = new Vector2(moveinput.x * currentmovespeed, rb.velocity.y);
        }
        animator.SetFloat("yvelocity", rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveinput = context.ReadValue<Vector2>();
        if (isalive)
        {
            ismoving = moveinput != Vector2.zero;
            setfacingdirection(moveinput);
        }
        else
        {
            ismoving = false;
        }
    }

    private void setfacingdirection(Vector2 moveinput)
    {
        if (moveinput.x > 0 && !isfacingright)
        {
            isfacingright = true;
        }
        else if (moveinput.x < 0 && isfacingright)
        {
            isfacingright = false;
        }
    }

    public bool _isfacingright = true;

    public float jumpimpulse = 10f;

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isrunning = true;
        }
        else if (context.canceled)
        {
            isrunning = false;
        }
    }

    public void onJUmp(InputAction.CallbackContext context)
    {
        if (context.started && playingdirection.IsGround)
        {
            animator.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpimpulse);
        }
    }

    public void onAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger("attack");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackrange, enemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                damagable damagable = enemy.GetComponent<damagable>();
                if (damagable != null)
                {
                    Vector2 knockback = new Vector2(isfacingright ? 5f : -5f, 2f);
                    damagable.TakeDamage(1, knockback);
                }
            }
        }
    }

    public bool canmove
    {
        get { return animator.GetBool("canmove"); }
    }

    public bool isalive
    {
        get
        {
            return animator.GetBool("isalive");
        }
    }

    public bool lockvel
    {
        get
        {
            return animator.GetBool("lockvel");
        }
        set
        {
            animator.SetBool("lockvel", value);
        }
    }

    public void onhit(int damage, Vector2 knockbsck)
    {
        rb.velocity = new Vector2(knockbsck.x, rb.velocity.y + knockbsck.y);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackrange);
    }


    // Mobile Button Support for Left and Right
    public void MoveLeft()
    {
        SetMoveX(-1f);
    }

    public void MoveRight()
    {
        SetMoveX(1f);
    }

    public void StopMoving()
    {
        SetMoveX(0f);
    }

    // Reuses existing logic
    private void SetMoveX(float direction)
    {
        moveinput = new Vector2(direction, 0f);

        if (isalive)
        {
            ismoving = direction != 0;
            setfacingdirection(moveinput);
        }
        else
        {
            ismoving = false;
        }
    }


    // Called when movement button is released
  
    public void MobileJump()
    {
        if (playingdirection.IsGround)
        {
            animator.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpimpulse);
        }
    }

    public void MobileRun(bool run)
    {
        isrunning = run;
    }

    public void MobileAttack()
    {
        animator.SetTrigger("attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackrange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            damagable damagable = enemy.GetComponent<damagable>();
            if (damagable != null)
            {
                Vector2 knockback = new Vector2(isfacingright ? 5f : -5f, 2f);
                damagable.TakeDamage(1, knockback);
            }
        }
    }
}
