using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectOnGrid : MonoBehaviour
{
    public Transform gridCellPrefab;
    public Transform palet;
    public Transform onMousePrefab;
    public Vector3 smoothMousePosition;
    [SerializeField] private int height;
    [SerializeField] private int width;

    private Vector3 mousePosition;
    Node[,] nodes;
    private Plane plane;


    [SerializeField] private GameObject character;
    [SerializeField] private Transform GridParent;

    void Start()
    {
        CreateGrid(); // İzgara oluşturma
        plane = new Plane(Vector3.up, transform.position);
    }

    void Update()
    {
        GetMousePositionOnGrid();
        transform.position = new Vector3(character.transform.position.x, transform.position.y, character.transform.position.z);
    }

    void GetMousePositionOnGrid()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out float enter)) ;
        {

            mousePosition = ray.GetPoint(enter);
            smoothMousePosition = mousePosition;
            mousePosition.y = 0;
            mousePosition = Vector3Int.RoundToInt(mousePosition);


            foreach (var node in nodes)
            {

                if ((new Vector3(node.obj.position.x, 0f, node.obj.position.z) - new Vector3(mousePosition.x, 0f, mousePosition.z)).magnitude < 1.5f && node.isPlacable) // Fare pozisyonu hücre pozisyonuna eşit mi ve yerleştirilebilir mi?
                {

                    if (Input.GetMouseButtonUp(0) && onMousePrefab != null) // Sol tıklama algılandı mı ve fare üzerinde nesne var mı?
                    {
                        node.isPlacable = false; // Hücre artık dolu
                        onMousePrefab.GetComponent<ObjFollowMouse>().isOnGrid = true; // Fare üzerindeki nesneyi ızgara üzerine koy

                        onMousePrefab.position = node.obj.position + new Vector3(0, 0.1f, 0); // Nesneyi hücrenin ortasına taşı
                        onMousePrefab.transform.localRotation = Quaternion.Euler(0f, 90f, 90f);

                        onMousePrefab.SetParent(node.obj);
                        onMousePrefab = null; // Fare üzerindeki nesneyi temizle
                    }
                }
            }
        }
    }

    public void OnMouseClickOnUı()
    {
        if (onMousePrefab == null) // Fare üzerinde nesne yoksa
        {
            onMousePrefab = Instantiate(palet, mousePosition, Quaternion.identity); // Fare pozisyonunda yeni nesne oluştur
        }
    }

    private void CreateGrid()
    {
        nodes = new Node[width, height]; // Hücre dizisi oluştur
        var name = 0;
        for (int i = -width / 2; i < (width / 2) + 1; i++)
        {
            for (int j = -height / 2; j < (height / 2) + 1; j++)
            {

                Vector3 worldPosition = new Vector3(i, 0, j) * 2;

                Transform obj = Instantiate(gridCellPrefab, worldPosition, Quaternion.identity, GridParent); // Hücreyi oluştur
                obj.name = "Cell" + name;
                obj.localPosition = new Vector3(worldPosition.x, worldPosition.y, (worldPosition.z - 1f));
                nodes[i + 7, j + 7] = new Node(true, obj.position, obj); // Yeni hücre oluştur
                name++;
                Debug.Log(obj.gameObject.name);
            }
        }
    }
}

public class Node
{
    public bool isPlacable;
    public Vector3 cellPosition;
    public Transform obj;

    public Node(bool isPlacable, Vector3 cellPosition, Transform obj)
    {
        this.isPlacable = isPlacable;
        this.cellPosition = cellPosition;
        this.obj = obj;
    }
}
