using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;

public class BoidControllerECS : BoidControllerECSBase<BoidECS>, IBoidController
{
}
