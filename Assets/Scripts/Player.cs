using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _invincibleTime;
    [SerializeField] private float _velocityOnHitX;
    [SerializeField] private float _velocityOnHitY;

    public delegate void OnHitted();
    public event OnHitted Hitted;

    private bool _isInvincible;
    private float _invincibleTimeCount;
    private Collider2D _collider;

    private void Start()
    {
        _isInvincible = false;
        _invincibleTimeCount = 0;
        _collider = GetComponent<Collider2D>();

        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            Physics2D.IgnoreCollision(_collider, enemy.GetComponent<Collider2D>());
        }
    }

    private void FixedUpdate()
    {
        List<Collider2D> colliders = new List<Collider2D>();

        if (_collider.OverlapCollider(new ContactFilter2D(), colliders) > 0)
        {
            var filteredForEnemy = (from Collider2D collider in colliders where collider.GetComponent<Enemy>() != null select collider.GetComponent<Enemy>()).ToArray();

            if (filteredForEnemy.Length > 0)
            {
                Hit(filteredForEnemy[0]);
            }
        }
    }

    private void Hit(Enemy enemy)
    {
        if (_isInvincible == false)
        {
            Hitted?.Invoke();
            StartCoroutine(MakeInvincible());
            PushFromHit(enemy);
            Debug.Log("Hitted!");
        }
    }

    private void PushFromHit(Enemy enemy)
    {
        float directionX = Mathf.Sign(_collider.bounds.center.x - enemy.GetComponent<Collider2D>().bounds.center.x);
        _collider.attachedRigidbody.velocity = new Vector2(directionX * _velocityOnHitX, _velocityOnHitY);
    }

    private IEnumerator MakeInvincible()
    {
        _isInvincible = true;

        while (_invincibleTimeCount < _invincibleTime)
        {
            _invincibleTimeCount += Time.deltaTime;

            yield return null;
        }

        _invincibleTimeCount = 0;
        _isInvincible = false;
    }
}
