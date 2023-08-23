﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlaceObjectOnGrid : MonoBehaviour
{
    public Transform gridCellPrefab;
    public Transform[] Palets;
    public Transform onMousePrefab;
    public Vector3 smoothMousePosition;
    [SerializeField] private int height;
    [SerializeField] private int width;

    private Vector3 mousePosition;
    Node[,] nodes;
    private Plane plane;
    public int id;


    [SerializeField] private GameObject character;
    [SerializeField] private Transform GridParent;
    [SerializeField] private Transform Plane;

    private void Awake()
    {
        CreateGrid(); // İzgara oluşturma
    }
    void Start()
    {

        plane = new Plane(Vector3.up, transform.position);

    }

    void Update()
    {
        GetMousePositionOnGrid();
        transform.position = new Vector3(Plane.transform.position.x, transform.position.y, Plane.transform.position.z);
    }

    void GetMousePositionOnGrid()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out float enter))
        {

            mousePosition = ray.GetPoint(enter);
            smoothMousePosition = mousePosition;
            mousePosition.y = 0;
            mousePosition = Vector3Int.RoundToInt(mousePosition);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Node node = nodes[i, j];



                    if ((new Vector3(node.obj.position.x, 0f, node.obj.position.z) - new Vector3(mousePosition.x, 0f, mousePosition.z)).magnitude < 1.5f && node.isEmpty) // Fare pozisyonu hücre pozisyonuna eşit mi ve yerleştirilebilir mi?
                    {
                        if (IsGridPlacable(i, j))
                        {

                            if (Input.GetMouseButtonUp(0) && onMousePrefab != null) // Sol tıklama algılandı mı ve fare üzerinde nesne var mı?
                            {
                                node.isEmpty = false; // Hücre artık dolu

                                onMousePrefab.GetComponent<ObjFollowMouse>().isOnGrid = true; // Fare üzerindeki nesneyi ızgara üzerine koy
                                onMousePrefab.position = node.obj.position + new Vector3(0, 0.1f, 0); // Nesneyi hücrenin ortasına taşı


                                if (id == 1 || id == 2 || id == 3)
                                {
                                    int[] RotY = { 0, 90, 180, 270 };
                                    var choose = Random.Range(0, RotY.Length);

                                    onMousePrefab.transform.localRotation = Quaternion.Euler(0f, RotY[choose], 90f);

                                }
                                else
                                {
                                    onMousePrefab.transform.localRotation = Quaternion.Euler(0f, 90f, 90f);
                                }
                                onMousePrefab.GetComponent<BoxCollider>().enabled = true;


                                ClickToMove.Instance.enabled = true;
                                ClickToMove.Instance.agent.enabled = true;

                                nodes[i, j].obj.transform.GetChild(0).gameObject.SetActive(false);

                                onMousePrefab.SetParent(node.obj);
                                onMousePrefab = null; // Fare üzerindeki nesneyi temizle
                            }

                        }

                    }
                }
            }

        }
    }

    public void OnMouseClickOnUı(int prefabIndex)
    {
        if (onMousePrefab == null)
        {
            onMousePrefab = Instantiate(Palets[prefabIndex], mousePosition, Palets[prefabIndex].transform.rotation);
            id = prefabIndex;
            onMousePrefab.GetComponent<BoxCollider>().enabled = false;
            ClickToMove.Instance.agent.enabled = false;
        }
    }

    public void OnPaleteClick()
    {
        OnMouseClickOnUı(0);

    }
    public void OnTopClick()
    {
        OnMouseClickOnUı(1);

    }
    public void OnYelkenliClick()
    {
        OnMouseClickOnUı(2);

    }
    public void OnHookClick()
    {
        OnMouseClickOnUı(3);

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
                obj.localPosition = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);

                nodes[i + 7, j + 7] = new Node(true, obj.position, obj); // Yeni hücre oluştur
                name++;

            }
        }
    }
    private Node GetMiddleCell()
    {
        int middleX = (width / 2);
        int middleY = (height / 2);

        Node middleCell = nodes[middleX, middleY];

        Debug.Log("Orta Nokta: " + middleCell.obj.name);

        return middleCell;
    }

    public bool IsGridPlacable(int i, int j)
    {
        int numRows = nodes.GetLength(0);
        int numCols = nodes.GetLength(1);

        if (i < 0 || i >= numRows || j < 0 || j >= numCols)
        {
            // Geçersiz indeksler, yerleştirme mümkün değil.
            return false;
        }

        if ((i == 7 && j == 7) ||
            (i < numRows - 1 && !nodes[i + 1, j].isEmpty) ||
            (i > 0 && !nodes[i - 1, j].isEmpty) ||
            (j < numCols - 1 && !nodes[i, j + 1].isEmpty) ||
            (j > 0 && !nodes[i, j - 1].isEmpty))
        {
            return true;
        }

        return false;
    }

}


public class Node
{
    public bool isEmpty;
    public Vector3 cellPosition;
    public Transform obj;

    public Node(bool isEmpty, Vector3 cellPosition, Transform obj)
    {
        this.isEmpty = isEmpty;
        this.cellPosition = cellPosition;
        this.obj = obj;
    }
}