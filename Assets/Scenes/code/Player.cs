using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    private Rigidbody2D _rigidbody2D;
    Vector2 moveInput;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
    }
    void onMove (InputValue value)  
    {
        moveInput  = value.Get<Vector2>();
    }
    // chuyển động của nhân vật 
    void Run()
    {
        var moveVelocity = new Vector2(moveInput.x * moveSpeed, _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = moveVelocity;
    }

    // abs: giá trị tuyệt đối 
    // sign : đấu của giá trị
    // epsilon: giá trị nhỏ nhất có thể so sánh
    // xoay hướng nhân vật theo chiều chuyển động 
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(_rigidbody2D.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(x: Mathf.Sign(_rigidbody2D.velocity.x), y: 1f);
        }
    }
    
}
