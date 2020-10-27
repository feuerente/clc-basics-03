using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph2D : MonoBehaviour {

    [SerializeField]
    Transform pointPrefab = default;
    
    [SerializeField, Range(10 ,100)]
    int resolution = 50;

    [SerializeField, Range(0f, 2f)]
    float animationSpeed = 0.5f;

    [SerializeField]
    FunctionLibrary2D.FunctionName function = default;

    Transform[] points;

    void Awake() {
        points = new Transform[resolution];
        float step = 2f / resolution;
        Vector3 position = Vector3.zero;
        Vector3 scale = Vector3.one * step;
        for (int i = 0; i < points.Length; i++) {
            Transform point = Instantiate(pointPrefab);
            position.x = (i + 0.5f) * step - 1f;
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }

    void Update() {
        float time = Time.time;
        for (int i = 0; i < points.Length; i++) {
            FunctionLibrary2D.Function f = FunctionLibrary2D.GetFunction(function);
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = f(position.x, time * animationSpeed);
            point.localPosition = position;
        }
    }
}
