using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _coin;

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        Instantiate(_coin, new Vector3(Random.Range(17, -17), 1, Random.Range(-17, 17)), Quaternion.identity);
    }
    
}
