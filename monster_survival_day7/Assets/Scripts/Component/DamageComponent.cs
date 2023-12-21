using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    private int damage;
    private bool isDamage;
    [SerializeField] private bool isCoolTime;
    [SerializeField] private float coolTimeMax;
    private float coolTimer;


    public int Damage { get => damage; set => damage = value; }
    public bool IsDamage { get => isDamage; set => isDamage = value; }
    public bool IsCoolTime { get => isCoolTime; set => isCoolTime = value; }
    public float CoolTimeMax { get => coolTimeMax; set => coolTimeMax = value; }
    public float CoolTimer { get => coolTimer; set => coolTimer = value; }
}
