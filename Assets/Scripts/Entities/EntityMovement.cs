using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovement : MonoBehaviour
{
    [SerializeField] private float m_MovementSpeed;
    [SerializeField] private float m_GroundAccelerationTime;
    [SerializeField] private float m_AirAccelerationTime;

    [Space]
    [SerializeField] private bool m_CanFly;

    [Space]
    [SerializeField] private float m_JumpPower;
    [SerializeField] private float m_DownGravity;
    [SerializeField] private float m_UpGravity;

    [Space]
    [SerializeField] private Transform m_GroundPoint;
    [SerializeField] private float m_GroundCheckRadius;
    [SerializeField] private LayerMask m_GroundCheckMask;


    private IMovementProvider m_MovementProvider;
    private Rigidbody2D m_Rigidbody;

    private void Awake()
    {
        m_MovementProvider = GetComponent<IMovementProvider>();
        m_MovementProvider.OnJump += Jump;

        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ApplyMovement();
        SetGravity();
    }

    private void ApplyMovement()
    {
        if (m_MovementProvider.IsMovementActive)
        {
            Vector2 currentVelocity = m_Rigidbody.velocity;
            Vector2 targetVelocity = m_MovementProvider.MovementDirection * m_MovementSpeed;
            Vector2 velocityDifference = targetVelocity - currentVelocity;
            Vector2 acceleration = velocityDifference / (IsGrounded() ? m_GroundAccelerationTime : m_AirAccelerationTime);

            acceleration = Vector2.Scale(acceleration, new Vector2(1f, m_CanFly ? 1f : 0f));
            m_Rigidbody.velocity += acceleration * Time.deltaTime;
        }
    }

    private void SetGravity()
    {
        if (m_CanFly)
        {
            m_Rigidbody.gravityScale = 0f;
        }
        else if (m_MovementProvider.MovementDirection.y < 0.1f)
        {
            m_Rigidbody.gravityScale = m_DownGravity;
        }
        else
        {
            m_Rigidbody.gravityScale = m_Rigidbody.velocity.y < 0 ? m_DownGravity : m_UpGravity;
        }
    }

    public void Jump ()
    {
        if (IsGrounded() && !m_CanFly)
        {
            m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, m_JumpPower);
        }
    }

    public bool IsGrounded ()
    {
        Collider2D[] queryList = new Collider2D[2];
        Physics2D.OverlapCircleNonAlloc(m_GroundPoint.position, m_GroundCheckRadius, queryList, m_GroundCheckMask);

        foreach (Collider2D query in queryList)
        {
            if (query)
            {
                if (query.gameObject != gameObject)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        if (m_GroundPoint)
        {
            Gizmos.color = IsGrounded() ? Color.green : Color.red;
            Gizmos.DrawSphere(m_GroundPoint.position, m_GroundCheckRadius);
            Gizmos.color = Color.white;
        }
    }
}
