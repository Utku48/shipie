using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    [SerializeField] private GameObject bomb;
    [SerializeField] private Transform bomnbInstPos;

    private void Start()
    {

        Instantiate(bomb, new Vector3(bomnbInstPos.position.x, 0.25f, bomnbInstPos.position.z), Quaternion.identity);
    }

}


