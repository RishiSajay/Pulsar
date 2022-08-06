using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ScreenBounds : MonoBehaviour
{
    /*
    public Camera mainCamera;

    //public UnityEvent<Collider2D> ExitTriggerFired;
    private RectTransform rt;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        this.mainCamera.transform.localScale = Vector3.one;
    }

    private void Start()
    {
        transform.position = Vector3.zero;
        //UpdateBoundsSize();
    }

    public Vector3 GetRandomPosInRect()
    {
        Vector3 position = new Vector3(Random.Range(rt.rect.xMin, rt.rect.xMax), Random.Range(rt.rect.yMin, rt.rect.yMax), 0) + rt.transform.position;
        return position;
    }
    
    public void UpdateBoundsSize()
    {
        //throw new NotImplementedException();
        float ySize = mainCamera.orthographicSize * 2f;

        Vector2 boxColliderSize = new Vector2(ySize * mainCamera.aspect, ySize);
        boxCollider.size = boxColliderSize;

        Vector2 rectTransformSize = new Vector2(ySize * mainCamera.aspect, ySize);
        //rt.size = rectTransformSize;
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        ExitTriggerFired?.Invoke(collision);
    }

    public bool AmIOutOfBounds(Vector3 worldPosition)
    {
        return
            Mathf.Abs(worldPosition.x) > Mathf.Abs(boxCollider.bounds.min.x) ||
            Mathf.Abs(worldPosition.x) > Mathf.Abs(boxCollider.bounds.min.y);
    }

    public Vector2 CalculateWrappedPosition(Vector2 worldPosition)
    {
        bool xBoundResult =
            Mathf.Abs(worldPosition.x) > (Mathf.Abs(boxCollider.bounds.min.x));
        bool yBoundResult =
            Mathf.Abs(worldPosition.y) > (Mathf.Abs(boxCollider.bounds.min.y));

        Vector2 signWorldPosition =
            new Vector2(Mathf.Sign(worldPosition.x), Mathf.Sign(worldPosition.y));

        if (xBoundResult && yBoundResult)
        {
            return Vector2.Scale(worldPosition, Vector2.one * -1) +
                Vector2.Scale(new Vector2(teleportOffset, teleportOffset), signWorldPosition);
        }

        else if (xBoundResult)
        {
            return new Vector2(worldPosition.x * -1, worldPosition.y) +
                new Vector2(teleportOffset * signWorldPosition.x, teleportOffset);
        }

        else if (yBoundResult)
        {
            return new Vector2(worldPosition.x, worldPosition.y * -1) +
                new Vector2(teleportOffset, teleportOffset * signWorldPosition.y);
        }

        else
        {
            return worldPosition;
        }
    }
    */
}





