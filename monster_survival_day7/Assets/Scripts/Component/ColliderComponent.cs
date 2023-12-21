using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderComponent : MonoBehaviour
{
    [SerializeField] private float radius;

    public float Radius { get => radius; set => radius = value; }
}
