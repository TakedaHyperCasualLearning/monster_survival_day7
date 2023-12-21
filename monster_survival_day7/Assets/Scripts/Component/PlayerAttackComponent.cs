using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackComponent : MonoBehaviour
{
    [SerializeField] private float attackInterval;
    private float intervalTImer;

    public float AttackInterval { get => attackInterval; set => attackInterval = value; }
    public float IntervalTImer { get => intervalTImer; set => intervalTImer = value; }
}
