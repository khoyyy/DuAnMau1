using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private float movespeed;
    [SerializeField] private float jumpspeed;
    [SerializeField] private float climSpeed = 5f;
    private bool isclimp;
    Rigidbody2D rb;
    Animator animator;
    public Transform _canjump;
    public LayerMask nen;
    private bool canjump;
    //private bool doublejump;
    private bool _flip;
    [SerializeField] private float down;
    Vector2 Vector;
    Vector2 up;
    public GameObject bulletPrefab;
    public Transform guntransform;
    CapsuleCollider2D capsuleCollider;
    private float gravti;

    


    void Start()
    {
        Vector = new Vector2(0, -Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        gravti = rb.gravityScale;
        health = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
        //ClimpLadder();
    }
    private void Move()
    {
        canjump = Physics2D.OverlapCircle(_canjump.position, 0.2f, nen);
        var Move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(Move * movespeed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && canjump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpspeed);
            animator.SetBool("2Jump", true);
            animator.SetBool("ak", false);
        }
        if (Move > 0)
        {
            _flip = true;
        }
        else if (Move < 0)
        {
            _flip = false;
        }
        if (rb.velocity.y == 0 && Move > 0 || rb.velocity.y == 0 && Move < 0)
        {
            animator.SetBool("Chay", true); animator.SetBool("ak", false);
        }
        if (Move == 0 && rb.velocity.y == 0)
        {
            animator.SetBool("Chay", false);

            animator.SetBool("Chay", false);
        }
        if (rb.velocity.y == 0)
        {
            animator.SetBool("Chay", false);
            animator.SetBool("falling", false);

        }
        if (rb.velocity.y < 0)
        {
            animator.SetBool("falling", true);

            rb.velocity -= Vector * down * Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            animator.SetBool("ak", true);
        }
        else animator.SetBool("ak", false);
        transform.localScale = _flip ? new Vector2(1.510177f, 1.169045f) : new Vector2(-1.510177f, 1.169045f);
    }

}
