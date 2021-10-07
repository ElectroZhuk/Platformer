using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Waypoint[] _patrolPoints = new Waypoint[2];
    [SerializeField] private float _speed;

    private int _pointIndex;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private Vector2[] _patrolArea;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _patrolArea = new Vector2[] { _patrolPoints[0].transform.position, _patrolPoints[1].transform.position };
        _pointIndex = 0;
    }

    private void FixedUpdate()
    {
        Vector2 target = _patrolArea[_pointIndex];

        if (Mathf.Abs(target.x - transform.position.x) < 0.5f)
        {
            _pointIndex++;

            if (_pointIndex >= _patrolArea.Length)
                _pointIndex = 0;
        }
        else
        {
            _rigidbody.velocity = new Vector2(Mathf.Sign(target.x - transform.position.x) * _speed, _rigidbody.velocity.y);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_patrolPoints[0].transform.position, _patrolPoints[1].transform.position);
    }
}
