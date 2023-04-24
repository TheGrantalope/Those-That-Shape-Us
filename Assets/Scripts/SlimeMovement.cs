using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class SlimeMovement : MonoBehaviour
{
    public float speed = 3f;
    public DetectionZone attackZone;
    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;

    public enum DirectionFace { Right, Left}

    private DirectionFace _direction;
    private Vector2 directionVector = Vector2.right;
    private DirectionFace Direction
    {
        get { return _direction; }
        set
        {
            if ( _direction != value )
            {
                // flip direction
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if(value == DirectionFace.Right)
                {
                    directionVector = Vector2.right;
                }
                else if(value == DirectionFace.Left)
                {
                    directionVector = Vector2.left;
                }
            }

            _direction = value; }
    }

    public bool _hasTarget = false;

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if(touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        rb.velocity = new Vector2(speed * directionVector.x, rb.velocity.y);
    }

    private void FlipDirection()
    {
        if(Direction == DirectionFace.Right)
        {
            Direction = DirectionFace.Left;
        }
        else if(Direction == DirectionFace.Left)
        {
            Direction = DirectionFace.Right;
        }
        else
        {
            Debug.LogError("Current irection face is not set to legal values of right or left");
        }
    }

}   
