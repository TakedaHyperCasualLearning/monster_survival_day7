using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveComponent : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector3 direction;
    [SerializeField] private bool isChase;
    [SerializeField] private bool isLokAt;
    private Vector3 targetPosition;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public Vector3 Direction { get => direction; set => direction = value; }
    public bool IsChase { get => isChase; set => isChase = value; }
    public bool IsLokAt { get => isLokAt; set => isLokAt = value; }
    public Vector3 TargetPosition { get => targetPosition; set => targetPosition = value; }
}
