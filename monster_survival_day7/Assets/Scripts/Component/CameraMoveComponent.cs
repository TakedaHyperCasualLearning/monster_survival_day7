using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveComponent : MonoBehaviour
{
    [SerializeField] Vector3 positionOffset;

    public Vector3 PositionOffset { get => positionOffset; set => positionOffset = value; }
}
