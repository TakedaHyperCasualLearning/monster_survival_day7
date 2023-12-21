using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveComponent : MonoBehaviour
{
    [SerializeField] float speed;
    private Vector3 direction;
    [SerializeField] private float releaseRange;

    public float Speed { get => speed; set => speed = value; }
    public Vector3 Direction { get => direction; set => direction = value; }
    public float ReleaseRange { get => releaseRange; set => releaseRange = value; }
}
