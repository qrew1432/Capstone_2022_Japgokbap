using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Monster : MonoBehaviour
{
    protected NavMeshAgent MyNavMesh { get; private set; }
    public float enemyHp = 1.0f;

    void Start()
    {
        MyNavMesh = GetComponent<NavMeshAgent>();
    }

    protected abstract void SpawnExpObjet();

    protected abstract void GetDamaged();
}
