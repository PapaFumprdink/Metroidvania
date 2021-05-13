using UnityEngine;

[DisallowMultipleComponent]
public abstract class EnemyBrain : MonoBehaviour
{
    protected Vector2 MovementDirection { get => Actions.MovementDirection; set => Actions.MovementDirection = value; }
    protected EnemyActions Actions { get; private set; }

    protected virtual void Awake ()
    {
        Actions = GetComponent<EnemyActions>();
    }

    private void Update()
    {
        Think();
    }

    protected abstract void Think();
}
