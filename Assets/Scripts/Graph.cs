using UnityEngine;

public class Graph : MonoBehaviour{
    [SerializeField]
    Transform pointPrefab = default;

    [SerializeField, Range(10,100)]
    int resolution = 10;
    int lastResolution;

    [SerializeField]
    FunctionLibrary.FunctionName function = default;

    Transform[] points;

    void instantiateGraph()
    {
        lastResolution = resolution;
        float step = 2f/resolution;
        //var position = Vector3.zero;
        var scale = Vector3.one * step;
        points = new Transform[resolution * resolution];
        for (int i = 0; i < points.Length; i++)
        {
            //if (x == resolution) {
            //    x = 0;
            //    z++;
            //}
            Transform point = Instantiate(pointPrefab);
            //position.x = (x + 0.5f) * step - 1f;
            //position.z = (z + 0.5f) * step - 1f;
            //point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }

    void Awake() {
        instantiateGraph();
    }

    void Update() {
        if (lastResolution != resolution)
        {
            for (int i = 0; i < points.Length; i++)
            {
                Destroy(points[i].gameObject);
            }
            instantiateGraph();
        }
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);
        float time = Time.time;
        float step = 2f / resolution;
        float v = 0.5f * step - 1f;
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++) {
            if (x == resolution) {
                x = 0;
                z++;
                v = (z + 0.5f) * step - 1f;
            }
            float u = (x + 0.5f) * step - 1f;
            points[i].localPosition = f(u, v, time);
        }
        
    }
}