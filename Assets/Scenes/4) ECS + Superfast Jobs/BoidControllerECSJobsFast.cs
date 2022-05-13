﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;

public class BoidControllerECSJobsFast : MonoBehaviour, IBoidController
{
    public static BoidControllerECSJobsFast Instance;

    [SerializeField] private int boidAmount;
    [SerializeField] private Mesh sharedMesh;
    [SerializeField] private Material sharedMaterial;

    public float boidSpeed;
    public float boidPerceptionRadius;
    public float cageSize;

    public float separationWeight;
    public float cohesionWeight;
    public float alignmentWeight;

    public float avoidWallsWeight;
    public float avoidWallsTurnDist;

    private List<Entity> boids;
    private EntityManager entityManager;

    public void CreateBoids()
    {
        Instance = this;

        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype boidArchetype = entityManager.CreateArchetype(
            typeof(BoidECSJobsFast),
            typeof(RenderMesh),
            typeof(RenderBounds),
            typeof(LocalToWorld)
        );

        NativeArray<Entity> boidArray = new NativeArray<Entity>(boidAmount, Allocator.Temp);
        entityManager.CreateEntity(boidArchetype, boidArray);

        for (int i = 0; i < boidArray.Length; i++)
        {
            Unity.Mathematics.Random rand = new Unity.Mathematics.Random((uint) i + 1);
            entityManager.SetComponentData(boidArray[i], new LocalToWorld
            {
                Value = float4x4.TRS(
                    RandomPosition(),
                    RandomRotation(),
                    new float3(1f))
            });
            entityManager.SetSharedComponentData(boidArray[i], new RenderMesh
            {
                mesh = sharedMesh,
                material = sharedMaterial,
            });
        }

        boids = boidArray.ToList();
        boidArray.Dispose();
    }

    public void Dispose()
    {
        if (boids != null && boids.Count > 0)
        {
            foreach (Entity boid in boids)
            {
                entityManager.DestroyEntity(boid);
            }
            boids.Clear();
        }
    }

    private float3 RandomPosition()
    {
        return new float3(
            UnityEngine.Random.Range(-cageSize / 2f, cageSize / 2f),
            UnityEngine.Random.Range(-cageSize / 2f, cageSize / 2f),
            UnityEngine.Random.Range(-cageSize / 2f, cageSize / 2f)
        );
    }

    private quaternion RandomRotation()
    {
        return quaternion.Euler(
            UnityEngine.Random.Range(-360f, 360f),
            UnityEngine.Random.Range(-360f, 360f),
            UnityEngine.Random.Range(-360f, 360f)
        );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            Vector3.zero,
            new Vector3(
                cageSize,
                cageSize,
                cageSize
            )
        );
    }
}
