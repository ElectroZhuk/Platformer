using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private Player _player;
    private Vector2 _lastPosition;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
        _player.Hitted += PlayHit;
        _lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Vector2 currentPosition = transform.position;
        float movingX = currentPosition.x - _lastPosition.x;
        _animator.SetFloat(PlayerAnimator.Params.VelocityX, Mathf.Abs(movingX));
        _animator.SetFloat(PlayerAnimator.Params.VelocityY, currentPosition.y - _lastPosition.y);

        if (movingX != 0)
            transform.localScale = new Vector3(Mathf.Sign(movingX) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        _lastPosition = transform.position;
    }

    private void OnDisable()
    {
        _player.Hitted -= PlayHit;
    }

    private void PlayHit()
    {
        _animator.SetTrigger(PlayerAnimator.Params.Hit);
    }
}
