using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidControllerBase : MonoBehaviour, IControllerData, IBoidController
{
    public static IControllerData Instance;

    [SerializeField] private int _boidAmount;

    [SerializeField] float _boidSpeed;
    [SerializeField] float _boidPerceptionRadius;
    [SerializeField] float _cageSize;

    [SerializeField] float _separationWeight;
    [SerializeField] float _cohesionWeight;
    [SerializeField] float _alignmentWeight;

    [SerializeField] float _avoidWallsWeight;
    [SerializeField] float _avoidWallsTurnDist;

    public int BoidAmount
    {
        get => _boidAmount;
        set => _boidAmount = value;
    }

    public float BoidSpeed
    {
        get => _boidSpeed;
        set => _boidSpeed = value;
    }

    public float BoidPerceptionRadius
    {
        get => _boidPerceptionRadius;
        set => _boidPerceptionRadius = value;
    }

    public float CageSize
    {
        get => _cageSize;
        set => _cageSize = value;
    }

    public float SeparationWeight
    {
        get => _separationWeight;
        set => _separationWeight = value;
    }

    public float CohesionWeight
    {
        get => _cohesionWeight;
        set => _cohesionWeight = value;
    }

    public float AlignmentWeight
    {
        get => _alignmentWeight;
        set => _alignmentWeight = value;
    }

    public float AvoidWallsWeight
    {
        get => _avoidWallsWeight;
        set => _avoidWallsWeight = value;
    }

    public float AvoidWallsTurnDist
    {
        get => _avoidWallsTurnDist;
        set => _avoidWallsTurnDist = value;
    }

    public virtual void CreateBoids()
    {
        
    }

    public virtual void Dispose()
    {
        
    }
}
