using System.Collections.Generic;
using UnityEngine;

public class World2D : MonoBehaviour {

    private int[,] matrix;
    public int width, height;
    private float cellWidth, cellHeight;
    public float zDistance;
    public Transform origin;
    public string textSeed;
    private int seed;
    public SpriteRenderer refSpriteRenderer;
    private Sprite refSprite;
    public const int AIR = 0;
    public const int GROUND = 1;
    private Dictionary<string, SpriteRenderer> tilesSprites;
    public Dictionary<string, SpriteRenderer> envirSprites;

    void Start()
    {
        matrix = new int[width, height];
        tilesSprites = new Dictionary<string, SpriteRenderer>();
        envirSprites = new Dictionary<string, SpriteRenderer>();
        refSprite = refSpriteRenderer.sprite;
        seed = textSeed.GetHashCode();
        cellWidth = refSprite.bounds.size.x;
        cellHeight = refSprite.bounds.size.y;
        set();
    }
    
    public int getTile(int posX, int posY)
    {
        if (posY == 0)
            return GROUND;
        return AIR;
        //float fixedSeed = seed / 1000.0f;
        //float perlinX = fixedSeed + posX;
        //float perlinY = fixedSeed + posY;
        //float perlin = Mathf.PerlinNoise(perlinX, perlinY);
        //return Mathf.RoundToInt(perlin);
    }

    public void set()
    {
        for(int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                float x, y, z;
                x = i * cellWidth;
                y = j * cellHeight;
                x += origin.position.x;
                y += origin.position.y;
                z = 0 + zDistance;
                Vector3 pos = new Vector3(x, y, z);
                SpriteRenderer sprite = GameObject.Instantiate(refSpriteRenderer, pos, Quaternion.identity);
                if (getTile(i, j) == AIR)
                    sprite.gameObject.SetActive(false);
                tilesSprites.Add(getKey(i, j), sprite);
            }
        }

        
    }

    public string getKey(int x, int y)
    {
        return x + ";" + y;
    }

    public int getKeyX(string key)
    {
        string[] values = key.Split(';');
        return int.Parse(values[0]);
    }

    public int getKeyY(string key)
    {
        string[] values = key.Split(';');
        return int.Parse(values[1]);
    }
}