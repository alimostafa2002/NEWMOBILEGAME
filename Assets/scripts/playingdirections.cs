using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingDirections : MonoBehaviour
{
    public ContactFilter2D contactFilter;
    private CapsuleCollider2D touchingCol;
    private Animator animator;

    private RaycastHit2D[] groundHit = new RaycastHit2D[5];
    private RaycastHit2D[] wallHit = new RaycastHit2D[5];

    public float groundDistance = 0.1f; // slightly bigger
    public float wallDistance = 0.2f;

    [SerializeField]
    private bool _isGround = true;

    public bool IsGround
    {
        get => _isGround;
        private set
        {
            _isGround = value;
            animator.SetBool("isground", value);
        }
    }

    [SerializeField]
    private bool _isOnWall = false;

    public bool IsOnWall
    {
        get => _isOnWall;
        private set
        {
            _isOnWall = value;
            animator.SetBool("isonwall", value);
        }
    }

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // ------------------ GROUND CHECK ------------------
        int groundCount = touchingCol.Cast(Vector2.down, contactFilter, groundHit, groundDistance);
        bool foundGround = false;

        for (int i = 0; i < groundCount; i++)
        {
            if (groundHit[i].normal.y > 0.5f)
            {
                foundGround = true;
                break;
            }
        }

        IsGround = foundGround;

        // ------------------ WALL CHECK ------------------
        Vector2 wallDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        int wallCount = touchingCol.Cast(wallDirection, contactFilter, wallHit, wallDistance);
        bool foundWall = false;

        for (int i = 0; i < wallCount; i++)
        {
            if (Mathf.Abs(wallHit[i].normal.x) > 0.5f)
            {
                foundWall = true;
                break;
            }
        }

        IsOnWall = foundWall;
    }
}
