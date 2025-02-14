using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [SerializeField] Rigidbody2D _rb;
    public Rigidbody2D Rb { get { return _rb; } }

    [SerializeField] float _jumpBufferTime;
    public float JumpBufferTime { get { return _jumpBufferTime; } }
    public float JumpBuffer { get; set; }
}
