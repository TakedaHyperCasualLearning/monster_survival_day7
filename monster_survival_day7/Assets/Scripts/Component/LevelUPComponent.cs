using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUPComponent : MonoBehaviour
{
    private int level;
    private int experiencePoint;
    [SerializeField] private int experiencePointBorder;
    private bool isLevelUp;

    [SerializeField] private int attackRaiseValue;
    private int attackLevel;
    private int attackLevelOld;
    private int attackBase;

    [SerializeField] private int hitPointRaiseValue;
    private int hitPointLevel;
    private int hitPointLevelOld;
    private int hitPointBase;

    [SerializeField] private float attackSpeedRaiseValue;
    private int attackSpeedLevel;
    private int attackSpeedLevelOld;
    private float attackSpeedBase;

    [SerializeField] private int splitRaiseValue;
    private int splitLevel;
    private int splitLevelOld;
    private int splitBase;


    public int Level { get => level; set => level = value; }
    public int ExperiencePoint { get => experiencePoint; set => experiencePoint = value; }
    public int ExperiencePointBorder { get => experiencePointBorder; set => experiencePointBorder = value; }
    public bool IsLevelUp { get => isLevelUp; set => isLevelUp = value; }

    public int AttackRaiseValue { get => attackRaiseValue; set => attackRaiseValue = value; }
    public int AttackLevel { get => attackLevel; set => attackLevel = value; }
    public int AttackLevelOld { get => attackLevelOld; set => attackLevelOld = value; }
    public int AttackBase { get => attackBase; set => attackBase = value; }

    public int HitPointRaiseValue { get => hitPointRaiseValue; set => hitPointRaiseValue = value; }
    public int HitPointLevel { get => hitPointLevel; set => hitPointLevel = value; }
    public int HitPointLevelOld { get => hitPointLevelOld; set => hitPointLevelOld = value; }
    public int HitPointBase { get => hitPointBase; set => hitPointBase = value; }

    public float AttackSpeedRaiseValue { get => attackSpeedRaiseValue; set => attackSpeedRaiseValue = value; }
    public int AttackSpeedLevel { get => attackSpeedLevel; set => attackSpeedLevel = value; }
    public int AttackSpeedLevelOld { get => attackSpeedLevelOld; set => attackSpeedLevelOld = value; }
    public float AttackSpeedBase { get => attackSpeedBase; set => attackSpeedBase = value; }

    public int SplitRaiseValue { get => splitRaiseValue; set => splitRaiseValue = value; }
    public int SplitLevel { get => splitLevel; set => splitLevel = value; }
    public int SplitLevelOld { get => splitLevelOld; set => splitLevelOld = value; }
    public int SplitBase { get => splitBase; set => splitBase = value; }
}
