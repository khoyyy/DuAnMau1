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
    }
    void onMove (InputValue value)  
    {
        moveInput  = value.Get<Vector2>();
    }
    // chuyển động của nhân vật 
    void Run()
    {
        Vector2 moveVelocity = new Vector2(moveInput.x * moveSpeed, _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = new Vector2(moveInput.x, y:0f);
    }
}
