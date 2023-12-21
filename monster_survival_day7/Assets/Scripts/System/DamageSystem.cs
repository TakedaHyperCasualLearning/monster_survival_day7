using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem
{
    private GameEvent gameEvent;
    private GameObject playerObject;

    private List<DamageComponent> damageComponentList = new List<DamageComponent>();
    private List<CharacterBaseComponent> characterBaseComponentList = new List<CharacterBaseComponent>();

    public DamageSystem(GameEvent gameEvent, GameObject playerObject)
    {
        this.gameEvent = gameEvent;
        this.playerObject = playerObject;

        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < damageComponentList.Count; i++)
        {
            DamageComponent damageComponent = damageComponentList[i];
            CharacterBaseComponent characterBaseComponent = characterBaseComponentList[i];
            if (!damageComponent.gameObject.activeSelf) continue;

            if (damageComponent.CoolTimer < damageComponent.CoolTimeMax)
            {
                damageComponent.CoolTimer += Time.deltaTime;
                continue;
            }

            if (!damageComponent.IsDamage) continue;
            characterBaseComponent.HitPoint -= damageComponent.Damage;

            damageComponent.Damage = 0;
            damageComponent.IsDamage = false;
            damageComponent.CoolTimer = 0.0f;
            if (characterBaseComponent.HitPoint <= 0)
            {
                if (damageComponent.gameObject != playerObject)
                {
                    playerObject.GetComponent<LevelUPComponent>().ExperiencePoint += 1;
                }
                else
                {
                    gameEvent.GameOver?.Invoke();
                }
                gameEvent.ReleaseObject?.Invoke(damageComponent.gameObject);
            }
        }
    }


    private void AddComponentList(GameObject gameObject)
    {
        DamageComponent damageComponent = gameObject.GetComponent<DamageComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();

        if (damageComponent == null || characterBaseComponent == null) return;

        damageComponentList.Add(damageComponent);
        characterBaseComponentList.Add(characterBaseComponent);
    }


    private void RemoveComponentList(GameObject gameObject)
    {
        DamageComponent damageComponent = gameObject.GetComponent<DamageComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();

        if (damageComponent == null || characterBaseComponent == null) return;

        damageComponentList.Remove(damageComponent);
        characterBaseComponentList.Remove(characterBaseComponent);
    }
}
