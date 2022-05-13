using System.Collections.Generic;
using UnityEngine;

public class BoidsControllerOOP : BoidControllerBase
{
    private List<Boid> _boids = new List<Boid>();
    [SerializeField] private GameObject _boidPrefab;

    public override void CreateBoids()
    {
        Instance = this;
        _boids.Clear();

        for (int i = 0; i < BoidAmount; i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(-CageSize / 2f, CageSize / 2f),
                Random.Range(-CageSize / 2f, CageSize / 2f),
                Random.Range(-CageSize / 2f, CageSize / 2f)
            );
            Quaternion rot = Quaternion.Euler(
                Random.Range(0f, 360f),
                Random.Range(0f, 360f),
                Random.Range(0f, 360f)
            );

            Boid newBoid = Instantiate(_boidPrefab, pos, rot).GetComponent<Boid>();
            newBoid.SetBoids(_boids);
            _boids.Add(newBoid);
        }
    }

    public override void Dispose()
    {
        foreach (Boid boid in _boids)
        {
            Destroy(boid.gameObject);
        }

        _boids.Clear();
    }

    public List<Boid> GetBoids()
    {
        return _boids;
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
