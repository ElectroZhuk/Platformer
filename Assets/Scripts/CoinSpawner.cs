using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Map _map;
    [SerializeField] private Coin _coin;
    [SerializeField] private int _spawnCount;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _distanceFromGround;

    private List<Coroutine> _coroutines;

    private void Start()
    {
        _coroutines = new List<Coroutine>();
        _coroutines.Add(StartCoroutine(SpawnCoins()));
    }

    private void OnDisable()
    {
        foreach (var coroutine in _coroutines)
            StopCoroutine(coroutine);
    }

    private IEnumerator SpawnCoins()
    {
        int spawnedCoins = 0;
        CompositeCollider2D mapCollider = _map.GetComponent<CompositeCollider2D>();
        Collider2D coinCollider = _coin.GetComponent<Collider2D>();

        while (spawnedCoins < _spawnCount)
        {
            float pointX = Random.Range(mapCollider.bounds.min.x + coinCollider.bounds.extents.x, mapCollider.bounds.max.x - coinCollider.bounds.extents.x);
            List<float> pointsY = new List<float>();
            float step = 0.01f;

            for (float checkingPoint = mapCollider.bounds.min.y; checkingPoint <= mapCollider.bounds.max.y; checkingPoint += step)
            {
                if (mapCollider.attachedRigidbody.OverlapPoint(new Vector2(pointX, checkingPoint)))
                {
                    if (mapCollider.attachedRigidbody.OverlapPoint(new Vector2(pointX, checkingPoint + step)) == false)
                    {
                        pointsY.Add(checkingPoint);
                    }
                }
            }

            float pointY = pointsY[Random.Range(0, pointsY.Count)];
            Instantiate(_coin, new Vector3(pointX, pointY + _distanceFromGround, 0), Quaternion.identity);
            spawnedCoins++;

            yield return new WaitForSeconds(_spawnDelay);
        }
    }
}
