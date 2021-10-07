using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    private Animator _animator;
    private Vector2 _lastPosition;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Vector2 currentPosition = transform.position;
        float movingX = currentPosition.x - _lastPosition.x;
        _animator.SetFloat(EnemyAnimator.Params.Speed, Mathf.Abs(movingX));

        if (movingX != 0)
            transform.localScale = new Vector3(Mathf.Sign(movingX) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        _lastPosition = transform.position;
    }
}
