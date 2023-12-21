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
    [SerializeField] private Renderer myRenderer;
    private float effectTimer;
    [SerializeField] private float effectTimeMax;
    [SerializeField] private int blinkCount;
    private bool isEffect;


    public int Damage { get => damage; set => damage = value; }
    public bool IsDamage { get => isDamage; set => isDamage = value; }
    public bool IsCoolTime { get => isCoolTime; set => isCoolTime = value; }
    public float CoolTimeMax { get => coolTimeMax; set => coolTimeMax = value; }
    public float CoolTimer { get => coolTimer; set => coolTimer = value; }
    public Renderer MyRenderer { get => myRenderer; set => myRenderer = value; }
    public float EffectTimer { get => effectTimer; set => effectTimer = value; }
    public float EffectTimeMax { get => effectTimeMax; set => effectTimeMax = value; }
    public int BlinkCount { get => blinkCount; set => blinkCount = value; }
    public bool IsEffect { get => isEffect; set => isEffect = value; }
}
