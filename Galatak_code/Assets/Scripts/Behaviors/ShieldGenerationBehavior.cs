using UnityEngine;

public class ShieldGenerationBehavior : MonoBehaviour
{
    public GameObject ShieldPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GenerateShieldSegments();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateShieldSegments()
    {
        var measurements = new Measurements(ShieldPrefab);
        float shieldSegmentWidth = measurements.WidthInWorldSize;
        float currentX = this.transform.position.x + shieldSegmentWidth;
        float currentY = this.transform.position.y;

        for (int i = 0; i < 125; i++)
        {
            var newSegment = Instantiate(this.ShieldPrefab, transform);
            newSegment.transform.position = new Vector2(currentX, currentY);
            currentX += shieldSegmentWidth;
        }
    }
}
