using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitPointUIComponent : MonoBehaviour
{
    [SerializeField] private GameObject hitPointPrefab;
    private TextMeshPro hitPointText;
    [SerializeField] private Vector3 offset;

    public GameObject HitPointPrefab { get => hitPointPrefab; set => hitPointPrefab = value; }
    public TextMeshPro HitPointText { get => hitPointText; set => hitPointText = value; }
    public Vector3 Offset { get => offset; set => offset = value; }
}
