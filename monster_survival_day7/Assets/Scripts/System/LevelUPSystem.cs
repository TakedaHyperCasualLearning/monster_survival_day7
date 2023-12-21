using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUPSystem
{
    private GameEvent gameEvent;
    private List<LevelUPComponent> levelUPComponentList = new List<LevelUPComponent>();
    private List<CharacterBaseComponent> characterBaseComponentList = new List<CharacterBaseComponent>();
    private List<PlayerAttackComponent> playerAttackComponentList = new List<PlayerAttackComponent>();

    public LevelUPSystem(GameEvent gameEvent)
    {
        this.gameEvent = gameEvent;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
        gameEvent.LevelUP += LevelUP;
    }

    private void Initialize(LevelUPComponent levelUPComponent, CharacterBaseComponent characterBaseComponent, PlayerAttackComponent playerAttackComponent)
    {
        levelUPComponent.AttackBase = characterBaseComponent.AttackPoint;
        levelUPComponent.HitPointBase = characterBaseComponent.HitPoint;
        levelUPComponent.AttackSpeedBase = playerAttackComponent.AttackInterval;
        levelUPComponent.SplitBase = playerAttackComponent.Split;
        levelUPComponent.IsLevelUp = false;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < levelUPComponentList.Count; i++)
        {
            LevelUPComponent levelUPComponent = levelUPComponentList[i];
            if (!levelUPComponent.gameObject.activeSelf) continue;

            if (levelUPComponent.ExperiencePoint < levelUPComponent.ExperiencePointBorder) continue;
            levelUPComponent.ExperiencePoint -= levelUPComponent.ExperiencePointBorder;
            levelUPComponent.Level++;
            levelUPComponent.IsLevelUp = true;
        }
    }

    private void LevelUP()
    {
        for (int i = 0; i < levelUPComponentList.Count; i++)
        {
            LevelUPComponent levelUPComponent = levelUPComponentList[i];
            CharacterBaseComponent characterBaseComponent = characterBaseComponentList[i];
            PlayerAttackComponent playerAttackComponent = playerAttackComponentList[i];
            if (!levelUPComponent.IsLevelUp) continue;

            if (levelUPComponent.AttackLevel != levelUPComponent.AttackLevelOld)
            {
                characterBaseComponent.AttackPoint = levelUPComponent.AttackBase + levelUPComponent.AttackRaiseValue * levelUPComponent.AttackLevel;
                levelUPComponent.AttackLevelOld = levelUPComponent.AttackLevel;
            }

            if (levelUPComponent.HitPointLevel != levelUPComponent.HitPointLevelOld)
            {
                characterBaseComponent.HitPoint += levelUPComponent.HitPointRaiseValue;
                characterBaseComponent.MaxHitPoint = levelUPComponent.HitPointBase + levelUPComponent.HitPointRaiseValue * levelUPComponent.HitPointLevel;
                levelUPComponent.HitPointLevelOld = levelUPComponent.HitPointLevel;
            }

            if (levelUPComponent.AttackSpeedLevel != levelUPComponent.AttackSpeedLevelOld)
            {
                playerAttackComponent.AttackInterval = levelUPComponent.AttackSpeedBase - levelUPComponent.AttackSpeedRaiseValue * levelUPComponent.AttackSpeedLevel;
                levelUPComponent.AttackSpeedLevelOld = levelUPComponent.AttackSpeedLevel;
            }

            if (levelUPComponent.SplitLevel != levelUPComponent.SplitLevelOld)
            {
                playerAttackComponent.Split = levelUPComponent.SplitBase + levelUPComponent.SplitRaiseValue * levelUPComponent.SplitLevel;
                levelUPComponent.SplitLevelOld = levelUPComponent.SplitLevel;
            }
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        LevelUPComponent levelUPComponent = gameObject.GetComponent<LevelUPComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();
        PlayerAttackComponent playerAttackComponent = gameObject.GetComponent<PlayerAttackComponent>();

        if (levelUPComponent == null || characterBaseComponent == null || playerAttackComponent == null) return;

        levelUPComponentList.Add(levelUPComponent);
        characterBaseComponentList.Add(characterBaseComponent);
        playerAttackComponentList.Add(playerAttackComponent);

        Initialize(levelUPComponent, characterBaseComponent, playerAttackComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        LevelUPComponent levelUPComponent = gameObject.GetComponent<LevelUPComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();
        PlayerAttackComponent playerAttackComponent = gameObject.GetComponent<PlayerAttackComponent>();

        if (levelUPComponent == null || characterBaseComponent == null || playerAttackComponent == null) return;

        levelUPComponentList.Remove(levelUPComponent);
        characterBaseComponentList.Remove(characterBaseComponent);
        playerAttackComponentList.Remove(playerAttackComponent);
    }

    public bool GetLevelUP()
    {
        for (int i = 0; i < levelUPComponentList.Count; i++)
        {
            LevelUPComponent levelUPComponent = levelUPComponentList[i];
            if (levelUPComponent.IsLevelUp) return true;
        }
        return false;
    }

}
