using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackComponent : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float attackInterval;
    private float intervalTImer;

    public GameObject BulletPrefab { get => bulletPrefab; set => bulletPrefab = value; }
    public float AttackInterval { get => attackInterval; set => attackInterval = value; }
    public float IntervalTImer { get => intervalTImer; set => intervalTImer = value; }
}
