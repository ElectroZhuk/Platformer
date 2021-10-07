using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _motionAmplitude;

    public delegate void OnCollected();
    public event OnCollected Collected;

    private float _centerY;
    private bool _isMoving;

    private void Start()
    {
        _centerY = transform.position.y;
        _isMoving = false;
    }

    private void FixedUpdate()
    {
        if (_isMoving == false)
            StartCoroutine(CoinMover());
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
        StopCoroutine(CoinMover());
    }

    private IEnumerator CoinMover()
    {
        _isMoving = true;
        float targetY;

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
            transform.position = new Vector2(transform.position.x, Mathf.MoveTowards(transform.position.y, targetY, Mathf.Abs(transform.position.y - targetY) / 10f));

            yield return new WaitForFixedUpdate();
        }

        _isMoving = false;
    }
}
