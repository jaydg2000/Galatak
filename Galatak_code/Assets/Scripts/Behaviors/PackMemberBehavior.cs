using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackMemberBehavior : MonoBehaviour
{
    public int ColumnPosition;
    public int RowPosition;
    public Vector2 CenterPoint;

    private Rigidbody2D _rigidbody;

    private float _assemblySpeed = 10.0f;
    private float _floatSpeed = 2.0f;
    private float _diveSpeed = 12.0f;
    private Vector2 _positionInPack;

    private float _circleAngle = 0f;
    private float _circleSpeed = (2.0f * Mathf.PI) / 2.0f; //2*PI in degress is 360, so you get 5 seconds to complete a circle
    private float _circleRadius = 10.0f;

    private PackMemberState _state;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _state = PackMemberState.Diving;
        // Invoke("SetStateToAssembly", 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case PackMemberState.Diving:
                Dive();
                break;
            case PackMemberState.Assembly:
                Assemble();
                break;
        }
    }

    private void Dive()
    {
        var dir = (ColumnPosition % 2 == 0) ? -1f : 1f;
        _circleAngle -= _circleSpeed * Time.deltaTime; //if you want to switch direction, use -= instead of +=
        var x = dir * (Mathf.Cos(_circleAngle) * _circleRadius);
        var y = Mathf.Sin(_circleAngle) * _circleRadius;

        _rigidbody.velocity = new Vector2(x, y);
    }

    //private float _xStep;
    //private float _yStep;
    private void Assemble()
    {
        float step = _assemblySpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, CenterPoint, step);
    }

    private void FindPositionInPack()
    {

    }

    //private void InitAssemblyMode()
    //{
    //    float xDistance = transform.position.x - CenterPoint.x;
    //    float yDistance = transform.position.y - CenterPoint.y;
    //    float step;
    //    if (Mathf.Abs(xDistance) > Mathf.Abs(yDistance))
    //    {
    //        step = xDistance / yDistance;
    //    }
    //    else
    //    {
    //        step = yDistance / xDistance;
    //    }
    //}

    private void SetStateToAssembly()
    {
        _state = PackMemberState.Assembly;
    }

    public enum PackMemberState
    {
        Diving,
        Scatter,
        Assembly,
        Float
    }
}
