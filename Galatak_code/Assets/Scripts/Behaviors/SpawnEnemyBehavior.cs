using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyBehavior : MonoBehaviour
{
    public GameObject EnemyPrefab;        
    public int RowCount = 4;
    public int ColumnCount = 8;

    private Transform _centerPointTransform;
    private int _count;
    private float _spawnRate = .25f;
    private bool _canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        _centerPointTransform = transform.GetChild(0);
        _count = RowCount * ColumnCount;
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Spawn()
    {
        var newEnemy = Instantiate(EnemyPrefab);
        var behavior = newEnemy.GetComponent<PackMemberBehavior>();

        newEnemy.transform.position = new Vector2(
            transform.position.x,
            transform.position.y
            );

        int row = _count / ColumnCount;
        int col = _count % ColumnCount;

        behavior.ColumnPosition = col;
        behavior.RowPosition = row;
        behavior.CenterPoint = _centerPointTransform.position;

        if (--_count == 0)
        {
            _canSpawn = false;
        }
        else
        {
            Invoke("Spawn", _spawnRate);
        }
    }
}
