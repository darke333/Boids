using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;

public class BoidControllerECSBase<T> : BoidControllerBase
{
    [SerializeField] private Mesh _sharedMesh;
    [SerializeField] private Material _sharedMaterial;

    private List<Entity> _boids = new List<Entity>();
    private EntityManager _entityManager;

    public override void CreateBoids()
    {
        Instance = this;

        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype boidArchetype = _entityManager.CreateArchetype(
            typeof(T),
            typeof(RenderMesh),
            typeof(RenderBounds),
            typeof(LocalToWorld)
        );

        NativeArray<Entity> boidArray = new NativeArray<Entity>(BoidAmount, Allocator.Temp);
        _entityManager.CreateEntity(boidArchetype, boidArray);

        for (int i = 0; i < boidArray.Length; i++)
        {
            Unity.Mathematics.Random rand = new Unity.Mathematics.Random((uint) i + 1);
            _entityManager.SetComponentData(boidArray[i], new LocalToWorld
            {
                Value = float4x4.TRS(
                    RandomPosition(),
                    RandomRotation(),
                    new float3(1f))
            });
            _entityManager.SetSharedComponentData(boidArray[i], new RenderMesh
            {
                mesh = _sharedMesh,
                material = _sharedMaterial,
            });
        }

        _boids = boidArray.ToList();
        boidArray.Dispose();
    }

    public override void Dispose()
    {
        if (_boids != null && _boids.Count > 0)
        {
            foreach (Entity boid in _boids)
            {
                _entityManager.DestroyEntity(boid);
            }
        }

        _boids.Clear();
    }

    private float3 RandomPosition()
    {
        return new float3(
            UnityEngine.Random.Range(-CageSize / 2f, CageSize / 2f),
            UnityEngine.Random.Range(-CageSize / 2f, CageSize / 2f),
            UnityEngine.Random.Range(-CageSize / 2f, CageSize / 2f)
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
                CageSize,
                CageSize,
                CageSize
            )
        );
    }
}
