using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playermoveming : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    private Rigidbody2D _rigidbody2d;
    private Animator _animator;
    Vector2 moveInput;
    private Animator animator;
    private bool playerHasHorizontalSpeed;
    CapsuleCollider2D _capsuleCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
    }
    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(">>>>> Move Input:" + moveInput);
        // moveInput
        //(1,0) -> Right
        //(-1,0) -> Left
        //(0,1) -> Up
        //(0,-1) -> Down
    }
    void OnJump(InputValue value)
    {
        var isTouchingGround = _capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (!isTouchingGround) return;
        if (value.isPressed)
        {
            Debug.Log(">>>>> Jump");
            _rigidbody2d.velocity += new Vector2(0, jumpSpeed);
        }
    }

    // điều khiển chuyển động của nhân vật 
    void Run()
    {
        var moveVelocity = new Vector2(moveInput.x * moveSpeed, _rigidbody2d.velocity.y);
        _rigidbody2d.velocity = moveVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(moveInput.x) > Mathf.Epsilon;
        _animator.SetBool("isRunning", playerHasHorizontalSpeed);
    }


    // Abs: giá trị tuyệt đối
    //Sign: dấu của giá trị
    //Epsilon: giá trị nhỏ nhất có thể so sánh 
    // xoay hướng nhân vật theo chiều chuyển động
    void FlipSprite()
    {
        bool plaerHasHorizontalSpeed = Mathf.Abs(_rigidbody2d.velocity.x) > Mathf.Epsilon;
        if (plaerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(_rigidbody2d.velocity.x),1f);
        }
    }
}
