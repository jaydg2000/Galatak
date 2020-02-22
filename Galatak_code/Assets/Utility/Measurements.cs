using UnityEngine;

public class Measurements
{
    private readonly GameObject _gameObject;
    private float _pixelWidth = 0f;
    private float _pixelHeight = 0f;
    private float _worldWidth = 0f;
    private float _worldHeight = 0f;

    public Measurements(GameObject gameObject)
    {
        _gameObject = gameObject;
        MeasureSprite();
    }

    public float WidthInPixels => _pixelWidth;
    public float HeightInPixels => _pixelHeight;
    public float WidthInWorldSize => _worldWidth;
    public float HeightInWorldSize => _worldHeight;

    private void MeasureSprite()
    {
        //get world space size (this version operates on the bounds of the object, so expands when rotating)
        //Vector3 world_size = GetComponent<SpriteRenderer>().bounds.size;

        //get world space size (this version handles rotating correctly)
        Vector2 sprite_size = _gameObject.GetComponent<SpriteRenderer>().sprite.rect.size;
        Vector2 local_sprite_size = sprite_size / _gameObject.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        Vector3 world_size = local_sprite_size;
        world_size.x *= _gameObject.transform.lossyScale.x;
        world_size.y *= _gameObject.transform.lossyScale.y;

        //convert to screen space size
        Vector3 screen_size = 0.5f * world_size / Camera.main.orthographicSize;
        screen_size.y *= Camera.main.aspect;

        //size in pixels
        Vector3 in_pixels = new Vector3(screen_size.x * Camera.main.pixelWidth, screen_size.y * Camera.main.pixelHeight, 0) * 0.5f;


        _pixelWidth = in_pixels.x;
        _pixelHeight = in_pixels.y;
        _worldWidth = world_size.x;
        _worldHeight = world_size.y;

        //Debug.Log(string.Format("World size: {0}, Screen size: {1}, Pixel size: {2}", world_size, screen_size, in_pixels));
    }
}

