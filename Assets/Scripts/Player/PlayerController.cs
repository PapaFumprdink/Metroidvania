using System;
using UnityEngine;

[SelectionBase]
[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour, IMovementProvider
{
    public event Action OnJump;

    private Controls m_Controls;

    public Vector2 MovementDirection => m_Controls.General.Movement.ReadValue<Vector2>();

    public bool IsMovementActive { get; set; }
    Vector2 IMovementProvider.MovementDirection { get; set; }

    public Rigidbody2D AttachedRigidbody { get; private set; }

    private void Awake()
    {
        AttachedRigidbody = GetComponent<Rigidbody2D>();

        m_Controls = new Controls();

        m_Controls.General.Jump.performed += (ctx) => OnJump?.Invoke();
    }

    private void OnEnable()
    {
        m_Controls.Enable();
    }

    private void OnDisable()
    {
        m_Controls.Disable();
    }
}
