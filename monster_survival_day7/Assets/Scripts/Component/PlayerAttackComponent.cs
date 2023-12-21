using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackComponent : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float attackInterval;
    private float intervalTImer;
    [SerializeField] private int split;
    [SerializeField] private GameObject shotEffectPrefab;
    private ParticleSystem shotEffect;

    public GameObject BulletPrefab { get => bulletPrefab; set => bulletPrefab = value; }
    public float AttackInterval { get => attackInterval; set => attackInterval = value; }
    public float IntervalTImer { get => intervalTImer; set => intervalTImer = value; }
    public int Split { get => split; set => split = value; }
    public GameObject ShotEffectPrefab { get => shotEffectPrefab; set => shotEffectPrefab = value; }
    public ParticleSystem ShotEffect { get => shotEffect; set => shotEffect = value; }
}
