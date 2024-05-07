using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    [SerializeField] float speed = 2f;
    Vector2 motionVector;
    Vector2 lastmotionVector;
    Animator animator;
    public bool moving;
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        motionVector = new Vector2(horizontal, vertical);
        animator.SetFloat("horizontal", horizontal);
        animator.SetFloat("vertical", vertical);

        moving = horizontal !=0 || vertical !=0;
        animator.SetBool("moving", moving);

        if (horizontal != 0 || vertical != 0)
        {
            lastmotionVector = new Vector2(horizontal, vertical).normalized;
            animator.SetFloat("lastHorizontal", horizontal);
            animator.SetFloat("lastVertical", vertical);
        }

    }
    void FixedUpdate()
    {
        move();
    }
    private void move()
    {
        rigidbody2D.velocity = motionVector * speed;
    }
}
