using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitPointUISystem : MonoBehaviour
{
    private GameEvent gameEvent;
    private GameObject uiRoot;
    private List<HitPointUIComponent> hitPointUIComponentList = new List<HitPointUIComponent>();
    private List<CharacterBaseComponent> characterBaseComponentList = new List<CharacterBaseComponent>();

    public HitPointUISystem(GameEvent gameEvent, GameObject UIRoot)
    {
        this.gameEvent = gameEvent;
        this.uiRoot = UIRoot;

        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    private void Initialize(HitPointUIComponent hitPointUIComponent)
    {
        GameObject tempUI = GameObject.Instantiate(hitPointUIComponent.HitPointPrefab, hitPointUIComponent.transform);
        tempUI.transform.localPosition = Vector3.zero;
        tempUI.transform.SetParent(uiRoot.transform);
        hitPointUIComponent.HitPointText = tempUI.GetComponent<TextMeshPro>();
    }


    public void OnUpdate()
    {
        for (int i = 0; i < hitPointUIComponentList.Count; i++)
        {
            HitPointUIComponent hitPointUIComponent = hitPointUIComponentList[i];
            CharacterBaseComponent characterBaseComponent = characterBaseComponentList[i];
            if (!hitPointUIComponent.gameObject.activeSelf)
            {
                hitPointUIComponent.HitPointText.gameObject.SetActive(false);
                continue;
            }

            hitPointUIComponent.HitPointText.gameObject.SetActive(true);
            hitPointUIComponent.HitPointText.transform.position = characterBaseComponent.transform.position + hitPointUIComponent.Offset;
            hitPointUIComponent.HitPointText.text = "HP:" + characterBaseComponent.HitPoint.ToString() + "/" + characterBaseComponent.MaxHitPoint.ToString();
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        HitPointUIComponent hitPointUIComponent = gameObject.GetComponent<HitPointUIComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();

        if (hitPointUIComponent == null || characterBaseComponent == null) return;

        hitPointUIComponentList.Add(hitPointUIComponent);
        characterBaseComponentList.Add(characterBaseComponent);

        Initialize(hitPointUIComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        HitPointUIComponent hitPointUIComponent = gameObject.GetComponent<HitPointUIComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();

        if (hitPointUIComponent == null || characterBaseComponent == null) return;

        hitPointUIComponentList.Remove(hitPointUIComponent);
        characterBaseComponentList.Remove(characterBaseComponent);
    }
}
