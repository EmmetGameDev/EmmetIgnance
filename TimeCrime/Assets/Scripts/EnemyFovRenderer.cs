using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class EnemyFovRenderer : MonoBehaviour
{
    SpriteShapeController shapeController;
    SpriteShapeRenderer shapeRenderer;
    Vector2 startPos;

    void Start()
    {
        shapeController = GetComponent<SpriteShapeController>();
        shapeRenderer = GetComponent<SpriteShapeRenderer>();
        startPos = transform.parent.GetComponent<EnemyMovement>().eyesOffset;
    }

    public void UpdateFOV(List<Vector2> points)
    {
        shapeRenderer.enabled = true;
        shapeController.spline.Clear();
        shapeController.spline.InsertPointAt(0, startPos);
        for (int i = 0; i < points.Count - 1; i++)
        {
            try
            {
                //shapeController.spline.SetPosition(i + 1, points[i]);
                shapeController.spline.InsertPointAt(i + 1, points[i]);
            } catch (Exception)
            {
                shapeRenderer.enabled = false;
            }
        }
        shapeController.RefreshSpriteShape();
        Debug.Log(points.Count);
    }
}
