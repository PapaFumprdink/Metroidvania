using System;
using UnityEngine;

public interface IMovementProvider
{
    event Action OnJump;

    GameObject gameObject { get; }
    Transform transform { get; }
    Rigidbody2D AttachedRigidbody { get; }

    bool IsMovementActive { get; set; }
    Vector2 MovementDirection { get; set;  }
}
