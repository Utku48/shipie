using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject prefabToPlace;

    private void Start()
    {
        int gridSize = 18;
        Vector3 middleCell = new Vector3(gridSize / 2, 0, gridSize / 2);

        Debug.Log(middleCell);
        if (!IsCellOccupied(middleCell))
        {
            Instantiate(prefabToPlace, middleCell, Quaternion.identity);
        }
    }

    bool IsCellOccupied(Vector3 position)
    {

        return false;
    }
}
