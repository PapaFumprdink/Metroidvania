using UnityEngine;

[DisallowMultipleComponent]
public class WandererEnemy : EnemyBrain
{
    [SerializeField] private float m_LungeCheckDistance;
    [SerializeField] private float m_LungeCheckRadius;
    [SerializeField] private float m_LungeForce;
    [SerializeField] private float m_LungeTime;
    [SerializeField] private float m_LungeDamage;
    [SerializeField] private float m_LungeRecovery;

    private float m_LastLungeTime;

    protected override void Awake()
    {
        m_LastLungeTime = float.MinValue;
    }

    protected override void Think()
    {
        PlayerController target = null;
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, m_LungeCheckRadius, MovementDirection, m_LungeCheckDistance);
        if (hit)
        {
            hit.transform.TryGetComponent(out target);
        }

        if (Time.time > m_LastLungeTime + m_LungeTime)
        {
            Actions.IsMovementActive = true;

            if (Time.time > m_LastLungeTime + m_LungeRecovery)
            {
                if (target)
                {
                    Actions.AttachedRigidbody.velocity = MovementDirection * m_LungeForce;
                    m_LastLungeTime = Time.time;
                }
                else
                {

                }
            }
            else
            {
                MovementDirection = Vector2.zero;
            }
        }
        else
        {
            Actions.IsMovementActive = false;
        }
    }
}
