using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseComponent : MonoBehaviour
{
    [SerializeField] private int hitPoint;
    [SerializeField] private int maxHitPoint;
    [SerializeField] private int attackPoint;

    public int HitPoint { get => hitPoint; set => hitPoint = value; }
    public int MaxHitPoint { get => maxHitPoint; set => maxHitPoint = value; }
    public int AttackPoint { get => attackPoint; set => attackPoint = value; }
}
