public interface IControllerData
{
    float BoidSpeed { get; }
    float BoidPerceptionRadius { get; }
    float CageSize { get; }

    float SeparationWeight { get; }
    float CohesionWeight { get; }
    float AlignmentWeight { get; }

    float AvoidWallsWeight { get; }
    float AvoidWallsTurnDist { get; }
}
