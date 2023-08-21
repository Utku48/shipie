using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;

public class AreaFloorBaker : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurface surface;

    [SerializeField]
    private GameObject Player;   //?

    [SerializeField]
    private float updateRate = 0.1f;

    [SerializeField]
    private float MovementThereshold = 3;
    [SerializeField]
    private Vector3 NavMeshhSize = new Vector3(20, 20, 20);//?

    private Vector3 WorldAnchor;
    private NavMeshData NavMeshData;
    private List<NavMeshBuildSource> Sources = new List<NavMeshBuildSource>();

    private void Start()
    {
        NavMeshData = new NavMeshData();
        NavMesh.AddNavMeshData(NavMeshData);
        BuildNavMesh(false);
        StartCoroutine(CheckPlayerMovement());
    }

    private IEnumerator CheckPlayerMovement()
    {
        WaitForSeconds wait = new WaitForSeconds(updateRate);

        while (true)
        {
            if (Vector3.Distance(WorldAnchor, Player.transform.position) > MovementThereshold)
            {
                BuildNavMesh(true);
                WorldAnchor = Player.transform.position;
            }
            yield return null;
        }
    }

    private void BuildNavMesh(bool Async)
    {
        Bounds navMeshBounds = new Bounds(Player.transform.position, NavMeshhSize);
        List<NavMeshBuildMarkup> markups = new List<NavMeshBuildMarkup>();

        List<NavMeshModifier> modifiers;

        if (surface.collectObjects == CollectObjects.Children)
        {
            modifiers = new List<NavMeshModifier>(surface.GetComponentsInChildren<NavMeshModifier>());
        }
        else
        {
            modifiers = NavMeshModifier.activeModifiers;
        }
        for (int i = 0; i < modifiers.Count; i++)
        {
            if (((surface.layerMask & (1 << modifiers[i].gameObject.layer)) == 1) &&
                modifiers[i].AffectsAgentType(surface.agentTypeID))
            {
                markups.Add(new NavMeshBuildMarkup()
                {
                    root = modifiers[i].transform,
                    overrideArea = modifiers[i].overrideArea,
                    area = modifiers[i].area,
                    ignoreFromBuild = modifiers[i].ignoreFromBuild,

                });
            }
        }
        if (surface.collectObjects == CollectObjects.Children)
        {
            NavMeshBuilder.CollectSources(surface.transform, surface.layerMask, surface.useGeometry, surface.defaultArea, markups, Sources);
        }
        else
        {
            NavMeshBuilder.CollectSources(navMeshBounds, surface.layerMask, surface.useGeometry, surface.defaultArea, markups, Sources);
        }

        Sources.RemoveAll(source => source.component != null && source.component.gameObject.GetComponent<NavMeshAgent>() != null);


        if (Async)
        {
            NavMeshBuilder.UpdateNavMeshDataAsync(NavMeshData, surface.GetBuildSettings(), Sources, new Bounds(Player.transform.position, NavMeshhSize));
        }
        else
        {
            NavMeshBuilder.UpdateNavMeshData(NavMeshData, surface.GetBuildSettings(), Sources, new Bounds(Player.transform.position, NavMeshhSize));
        }
    }
}
