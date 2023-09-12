using System.Collections;
using UnityEngine;

public class DragMobs : MonoBehaviour
{
   private GameObject _draggedObject;
    private Cell _objectCell;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        RaycastDrag();
    }

    private void RaycastDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryTakeObject();
        }
        else if (Input.GetMouseButtonUp(0) && _draggedObject != null)
        {
            DropObject();
        }

        if (_draggedObject != null)
        {
            DragObject();
        }
    }

    private void TryTakeObject()
    {
        var cell = GetCellAtPoint();
        if (cell != null && !cell.isFree)
        {
            _draggedObject = cell.mob;
            _objectCell = cell;
            Debug.Log("Object has been taken");
        }
    }

    private Cell GetCellAtPoint()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        int cellMask = LayerMask.GetMask("Cell");

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, cellMask))
        {
            Cell cell = hit.collider.GetComponent<Cell>();
            return cell;
        }

        return null;
    }

    private void DragObject()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        int cellMask = LayerMask.GetMask("Cell");

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, cellMask))
        {
            Vector3 objPosition = hit.point + new Vector3(0, 2, 0);
            _draggedObject.transform.position = objPosition;
            Debug.Log("Object is being dragged");
        }
    }

    private void DropObject()
    {
        var cell = GetCellAtPoint();

        if (cell != null && cell.isFree)
        {
            MoveObjectToCell(cell, true);
        }
        else
        {
            MoveObjectToCell(_objectCell, false);
        }

        _draggedObject = null;
        Debug.Log("Object has been dropped");
    }

    private void MoveObjectToCell(Cell targetCell, bool isNewCell)
    {
        if (isNewCell)
        {
            targetCell.isFree = false;
            targetCell.mob = _draggedObject;

            _objectCell.isFree = true;
            _objectCell.mob = null;
        }

        StartCoroutine(MoveObjectToPosition(0.2f, _draggedObject.transform, targetCell, () => _objectCell = null));
    }

    private IEnumerator MoveObjectToPosition(float duration, Transform mob ,Cell targetCell, System.Action onMovementComplete = null)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;
        Vector3 centerOfCell = targetCell.GetComponent<Collider>().bounds.center;
        Vector3 startPosition = mob.position;
        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / duration;

            
            mob.position = Vector3.Lerp(startPosition, centerOfCell, t);
            yield return null;
        }

        mob.position = centerOfCell;
        onMovementComplete?.Invoke();
    }
}