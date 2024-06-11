using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playermoveming : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    private Rigidbody2D _rigidbody2d;
    private Animator _animator;
    Vector2 moveInput;
    private Animator animator;
    private bool playerHasHorizontalSpeed;
    CapsuleCollider2D _capsuleCollider2D;
    private float gravityScaleAtStart;
    private bool isAlive;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    // Start is called before the first frame update     
    void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = _rigidbody2d.gravityScale;
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        Climbladder();
        Die();
    }
    private void OnMove(InputValue value)
    {
        if(!isAlive)
        {
            return;
        }
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
        if (!isAlive)
        {
            return;
        }
        var isTouchingGround = _capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (!isTouchingGround) return;
        if (value.isPressed)
        {
            Debug.Log(">>>>> Jump");
            _rigidbody2d.velocity += new Vector2(0, jumpSpeed);
        }
    }
    void OnFire(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
            Debug.Log(">>>>> Fire");
            // tạo ra viên đạn vị trí của súng 
            var oneBullet = Instantiate(bullet, gun.position, transform.rotation);
        // cung cấp velocity cho viên đạn tùy theo hướng của nhân vật
        // nếu nhân vật đang hướng về bên trái thì viên đạn bay về bên trái 
        if (transform.localScale.x < 0)
        {
            oneBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-15,0);
            return;
        }
        else
        {
            oneBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(15, 0);
        }
        // hủy viên đạn sau 1 giây 
        Destroy(oneBullet,1);
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
            transform.localScale = new Vector2(Mathf.Sign(_rigidbody2d.velocity.x), 1f);
        }
    }
    // leo thang 
    void Climbladder()
    {
        var isTouchingLadder = _capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"));
        if (!isTouchingLadder)
        {
            _rigidbody2d.gravityScale = gravityScaleAtStart;
            _animator.SetBool ("IsClimbing", false);
            return;
        }
        var climbVelocity = new Vector2(_rigidbody2d.velocity.x,moveInput.y *climbSpeed);
        _rigidbody2d.velocity = climbVelocity;

        // điều khiển animation leo thang
        var playerHasVerticalSpeed = Mathf.Abs(moveInput.y) > Mathf.Epsilon;
        // tắt gravity khi leo thang 
        _animator.SetBool( "IsClimbing", playerHasVerticalSpeed);
    }
    void Die()
    {
        var isTouchingEnemy = _capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy","Trap"));
        if (isTouchingEnemy)
        {
            isAlive = false;
            _animator.SetTrigger("Dying");
            _rigidbody2d.velocity = new Vector2(0, 0);
            // xử lí die 
            FindAnyObjectByType<GameController>().ProcessPlayerDeath();
        }
    }
}
