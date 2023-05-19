using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class EnemyFovRenderer : MonoBehaviour
{
    SpriteShapeController shapeController;
    Vector2 startPos;

    void Start()
    {
        shapeController = GetComponent<SpriteShapeController>();
        startPos = transform.parent.GetComponent<EnemyMovement>().eyesOffset;
    }

    public void UpdateFOV(List<Vector2> points)
    {
        shapeController.spline.Clear();
        shapeController.spline.InsertPointAt(0, startPos);
        for (int i = 0; i < points.Count - 1; i++)
        {
            //shapeController.spline.SetPosition(i + 1, points[i]);
            shapeController.spline.InsertPointAt(i + 1, points[i]);
        }
    }
}
