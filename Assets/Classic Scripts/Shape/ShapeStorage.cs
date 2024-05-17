using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeStorage : MonoBehaviour
{
    public List<ShapeData> shapeData;
    public List<Shape> shapeList;

	private void OnEnable()
	{
        GameEvent.RequestNewShapes += RequestNewShape;
	}

	private void OnDisable()
	{
		GameEvent.RequestNewShapes -= RequestNewShape;

	}


	void Start()
    {
        foreach (var shape in shapeList)
        {
            var shapeIndex = UnityEngine.Random.Range(0, shapeData.Count);
            shape.CreateShape(shapeData[shapeIndex]);
        }
    }

    public Shape GetCurrentSelectedShape()
    {
        foreach (var shape in shapeList)
        {
            if (shape.IsOnStartPos() == false && shape.isAnyOfShapeSquareActive())
                return shape;
        }
        Debug.LogError("There is no shape Selected");
        return null;
    }
    
    public void RequestNewShape()
    {
		foreach (var shape in shapeList)
		{
			var shapeIndex = UnityEngine.Random.Range(0, shapeData.Count);
			shape.RequestNewShape(shapeData[shapeIndex]);
		}
	}
}
