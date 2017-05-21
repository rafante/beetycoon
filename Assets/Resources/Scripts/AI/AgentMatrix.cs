using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class AgentMatrix : MonoBehaviour
    {
        public float gridSize;
        public Sprite sprite;
        public List<Rect> cells;
        private float cellSizeX;
        private float cellSizeY;

        void Start()
        {
            cells = new List<Rect>();
            sprite = GetComponent<SpriteRenderer>().sprite;
            cellSizeX = sprite.rect.width;
            cellSizeY = sprite.rect.height;
        }

        public void setGrid()
        {
            cells.Clear();
            float xSize, ySize, xOffset, yOffset, xPos, yPos;
            xPos = sprite.rect.position.x;
            yPos = sprite.rect.position.y;
            xSize = sprite.rect.width;
            ySize = sprite.rect.height;
            xOffset = xSize / 2;
            yOffset = ySize / 2;

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    float x = (xPos - xOffset) * xSize;
                    float y = (yPos - yOffset) * ySize;

                    cells.Add(new Rect(new Vector2(x, y), new Vector2(cellSizeX, cellSizeY)));
                }
            }

        }

    }
}