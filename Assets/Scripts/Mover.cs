using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody;
    private PlayerInput _input;
    private bool _canJump;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _input = GetComponent<PlayerInput>();
        _canJump = false;
        _input.DirectionChanged += Move;
        _input.Jumping += Jump;
    }

    /*private void OnEnable()
    {
        _input.DirectionChanged += Move;
        _input.Jumping += Jump;
    }*/

    private void OnDisable()
    {
        _input.DirectionChanged -= Move;
        _input.Jumping -= Jump;
    }

    private void Move(float direction)
    {
        _rigidbody.velocity = new Vector2(_speed * direction, _rigidbody.velocity.y);
    }

    private void Jump()
    {
        if (_canJump)
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Vector2 leftPoint = new Vector2(collision.otherCollider.bounds.min.x * 0.9f, collision.otherCollider.bounds.min.y);
        Vector2 rightPoint = new Vector2(collision.otherCollider.bounds.max.x * 0.9f, collision.otherCollider.bounds.min.y);
        var bottomOverlaps = Physics2D.OverlapAreaAll(leftPoint, rightPoint);

        if (bottomOverlaps.Any(overlap => (overlap.GetComponent<Player>() == null && overlap.isTrigger == false)))
        {
            _canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _canJump = false;
    }
}
