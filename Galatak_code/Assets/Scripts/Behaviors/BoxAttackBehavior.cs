using UnityEngine;

public class BoxAttackBehavior : MonoBehaviour
{
    public bool AttackLeft = true;

    private float _moveXSpeed = 0f;
    private float _moveYSpeed = 0f;
    private float _maxXSpeed = 5.0f;
    private float _maxYSpeed = 12.0f;

    private float _initialDropDuration = .5f;
    private float _dropDuration = .25f;
    private float _attackDuration = 1.0f;
    private float _riseDuration = .25f;
    private float _repositionDuration = 1.0f;

    private Rigidbody2D _rigidbody;
    private BoxAttackState _attackState;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _attackState = BoxAttackState.Stopped;

        Invoke("RotateState", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void RotateState()
    {
        switch( _attackState)
        {
            case BoxAttackState.Stopped:
                _attackState = BoxAttackState.InitialDrop;
                _moveXSpeed = 0f;
                _moveYSpeed = -_maxYSpeed;
                Invoke("RotateState", _initialDropDuration);
                break;
            case BoxAttackState.Reposition:
                _attackState = BoxAttackState.Dropping;
                _moveXSpeed = 0f;
                _moveYSpeed = -_maxYSpeed;
                Invoke("RotateState", _dropDuration);
                break;
            case BoxAttackState.InitialDrop:
                _attackState = BoxAttackState.Attacking;
                _moveXSpeed = AttackLeft ? -_maxXSpeed : _maxXSpeed;
                _moveYSpeed = 0f;
                Invoke("RotateState", _attackDuration);
                break;
            case BoxAttackState.Dropping:
                _attackState = BoxAttackState.Attacking;
                _moveXSpeed = AttackLeft ? -_maxXSpeed : _maxXSpeed;
                _moveYSpeed = 0f;
                Invoke("RotateState", _attackDuration);
                break;
            case BoxAttackState.Attacking:
                _attackState = BoxAttackState.Rising;
                _moveXSpeed = 0f;
                _moveYSpeed = _maxYSpeed;
                Invoke("RotateState", _riseDuration);
                break;                
            case BoxAttackState.Rising:
                _attackState = BoxAttackState.Reposition;
                _moveXSpeed = AttackLeft ? _maxXSpeed : -_maxXSpeed;
                _moveYSpeed = 0f;
                Invoke("RotateState", _repositionDuration);
                break;
        }
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2(_moveXSpeed /** _maxXSpeed*/, _moveYSpeed);
    }

    //private void PerformAttackState()
    //{
    //    switch (_attackState)
    //    {
    //        case BoxAttackState.Dropping:
    //            Drop();
    //            break;
    //        case BoxAttackState.Attacking:
    //            Attack();
    //            break;
    //        case BoxAttackState.Rising:
    //            Rise();
    //            break;
    //        case BoxAttackState.Reposition:
    //            Reposition();
    //            break;
    //    }
    //}

    //private void Drop()
    //{

    //}

    //private void Attack()
    //{

    //}

    //private void Rise()
    //{

    //}

    //private void Reposition()
    //{

    //}

    public enum BoxAttackState
    {
        Stopped,
        InitialDrop,
        Dropping,
        Attacking,
        Rising,
        Reposition
    }
}
