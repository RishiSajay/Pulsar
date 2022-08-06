using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayBounds : MonoBehaviour
{
    private RectTransform rt;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    public Vector3 GetRandomPosInRect()
    {
        Vector3 position = new Vector3(Random.Range(rt.rect.xMin, rt.rect.xMax), Random.Range(rt.rect.yMin, rt.rect.yMax), 0) + rt.transform.position;
        return position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
