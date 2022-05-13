using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private List<Boid> _boids;
    private IControllerData _controller;

    private Vector3 _separationForce;
    private Vector3 _cohesionForce;
    private Vector3 _alignmentForce;

    private Vector3 _avoidWallsForce;

    public void SetBoids(List<Boid> boids)
    {
        _boids = boids;
    }

    private void Start() {
        _controller = BoidControllerBase.Instance;
    }

    private void Update() {
        CalculateForces();
        moveForward();
    }

    private void CalculateForces() {

        Vector3 seperationSum = Vector3.zero;
        Vector3 positionSum = Vector3.zero;
        Vector3 headingSum = Vector3.zero;

        int boidsNearby = 0;

        for (int i = 0; i < _boids.Count; i++) {

            if (this != _boids[i]) {

                Vector3 otherBoidPosition = _boids[i].transform.position;
                float distToOtherBoid = (transform.position - otherBoidPosition).magnitude;

                if (distToOtherBoid < _controller.BoidPerceptionRadius) {

                    seperationSum += -(otherBoidPosition - transform.position) * (1f / Mathf.Max(distToOtherBoid, .0001f));
                    positionSum += otherBoidPosition;
                    headingSum += _boids[i].transform.forward;

                    boidsNearby++;
                }
            }
        }

        if (boidsNearby > 0) {
            _separationForce = seperationSum / boidsNearby;
            _cohesionForce   = (positionSum / boidsNearby) - transform.position;
            _alignmentForce  = headingSum / boidsNearby;
        }
        else {
            _separationForce = Vector3.zero;
            _cohesionForce   = Vector3.zero;
            _alignmentForce  = Vector3.zero;
        }

    	if (minDistToBorder(transform.position, _controller.CageSize) < _controller.AvoidWallsTurnDist) {
            // Back to center of cage
            _avoidWallsForce = -transform.position.normalized;
        }
        else {
            _avoidWallsForce = Vector3.zero;
        }
    }
    
    private void moveForward() {
        Vector3 force = 
            _separationForce * _controller.SeparationWeight +
            _cohesionForce   * _controller.CohesionWeight +
            _alignmentForce  * _controller.AlignmentWeight +
            _avoidWallsForce * _controller.AvoidWallsWeight;

        Vector3 velocity = transform.forward * _controller.BoidSpeed;
        velocity += force * Time.deltaTime;
        velocity = velocity.normalized * _controller.BoidSpeed;

        transform.position += velocity * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(velocity);
    }

    private float minDistToBorder(Vector3 pos, float cageSize) {
        float halfCageSize = cageSize / 2f;
        return Mathf.Min(Mathf.Min(
            halfCageSize - Mathf.Abs(pos.x),
            halfCageSize - Mathf.Abs(pos.y)),
            halfCageSize - Mathf.Abs(pos.z)
        );
    }
}
