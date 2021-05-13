using System;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemyActions : MonoBehaviour, IMovementProvider
{
    public Vector2 MovementDirection { get; set; }
    public bool IsMovementActive { get; set; }

    public Rigidbody2D AttachedRigidbody { get; private set; }

    public event Action OnJump;

    private void Awake()
    {
        AttachedRigidbody = GetComponent<Rigidbody2D>();
    }

    protected void Jump ()
    {
        OnJump?.Invoke();
    }
}
