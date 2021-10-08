using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _motionAmplitude;

    public event UnityAction Collected;

    private List<Coroutine> _coroutines;
    private float _centerY;

    private void Start()
    {
        _centerY = transform.position.y;
        _coroutines = new List<Coroutine>();
        _coroutines.Add(StartCoroutine(CoinMover()));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player;

        if (collision.gameObject.TryGetComponent<Player>(out player))
        {
            Debug.Log("Coin collected!");
            StopCoroutine(CoinMover());
            Collected?.Invoke();
            Destroy(gameObject, 0.9f);
        }
    }

    private void OnDisable()
    {
        foreach (var coroutine in _coroutines)
            StopCoroutine(coroutine);
    }

    private IEnumerator CoinMover()
    {
        float targetY;
        float deltaRatio = 0.1f;

        while (true)
        {
            if ((_centerY + _motionAmplitude - transform.position.y) < Mathf.Abs(_centerY - _motionAmplitude - transform.position.y))
            {
                targetY = _centerY - _motionAmplitude;
            }
            else
            {
                targetY = _centerY + _motionAmplitude;
            }

            while (Mathf.Abs(transform.position.y - targetY) > 0.01)
            {
                transform.position = new Vector2(transform.position.x, Mathf.MoveTowards(transform.position.y, targetY, Mathf.Abs(transform.position.y - targetY) * deltaRatio));

                yield return new WaitForFixedUpdate();
            }
        }
    }
}
