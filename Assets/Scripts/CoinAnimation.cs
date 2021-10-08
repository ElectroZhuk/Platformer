using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Coin))]
public class CoinAnimation : MonoBehaviour
{
    [SerializeField] private float _maxAnimationPauseTime;
    [SerializeField] private Animator _animator;

    private Coin _coin;
    private float _time;
    private float _animationPauseTime;

    private void Start()
    {
        _coin = GetComponent<Coin>();
        _coin.Collected += PlayCollectedAnimation;
        _time = 0;
        _animationPauseTime = Random.Range(1, _maxAnimationPauseTime);
    }

    private void Update()
    {
        if (_time >= _animationPauseTime)
        {
            _animator.SetTrigger(CoinAnimator.Params.Shimmer);
            _time = 0;
            _animationPauseTime = Random.Range(1, _maxAnimationPauseTime);
        }
        else
        {
            _time += Time.deltaTime;
        }
    }

    private void OnDisable()
    {
        _coin.Collected -= PlayCollectedAnimation;
    }

    private void PlayCollectedAnimation()
    {
        _animator.SetTrigger(CoinAnimator.Params.Collected);
    }
}
