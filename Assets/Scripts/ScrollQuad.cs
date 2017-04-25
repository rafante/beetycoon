using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollQuad : MonoBehaviour
{
    private MeshRenderer mRenderer;
    private Material material;
    private bool touchingMap;
    public float offset;
    public float speed;
    private Collider2D collider;

    public

        // Use this for initialization
        void Start()
    {
        collider = transform.GetComponent<Collider2D>();
        mRenderer = transform.GetComponent<MeshRenderer>();
        material = mRenderer.material;
        offset = material.mainTextureOffset.x;
        touchingMap = false;
    }

    // Update is called once per frame
    void Update()
    {
        float movement;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 point = Camera.main.ScreenToWorldPoint((Input.GetTouch(0)).position);
            if (collider.OverlapPoint(point))
            {
                movement = Input.GetTouch(0).deltaPosition.x * speed * Time.deltaTime;
                offset += movement;
            }
        }

        if (Input.GetMouseButton(0))
        {
            movement = (Input.GetAxis("Mouse X")) * speed * Time.deltaTime;
            offset += movement;

            Vector2 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (collider.OverlapPoint(p))
                material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        touchingMap = true;
    }
}