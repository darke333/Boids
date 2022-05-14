using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _controllers = new List<GameObject>();

    [SerializeField] private TMP_InputField _boidAmount;
    [SerializeField] private TMP_InputField _boidSpeed;
    [SerializeField] private TMP_InputField _boidPerceptionRadius;
    [SerializeField] private TMP_InputField _cageSize;
    [SerializeField] private TMP_InputField _separationWeight;
    [SerializeField] private TMP_InputField _cohesionWeight;
    [SerializeField] private TMP_InputField _alignmentWeight;
    [SerializeField] private TMP_InputField _avoidWallsWeight;
    [SerializeField] private TMP_InputField _avoidWallsTurnDist;

    private int _curIndex = -1;
    private BoidControllerBase _boidController;

    private void Awake()
    {
        ChangeController(0);
    }

    public void Build()
    {
        Dispose();
        _boidController.CreateBoids();
    }

    public void ChangeValues()
    {
        _boidController.BoidAmount = int.Parse(_boidAmount.text);
        _boidController.BoidSpeed = int.Parse(_boidSpeed.text);
        _boidController.BoidPerceptionRadius = int.Parse(_boidPerceptionRadius.text);
        _boidController.CageSize = int.Parse(_cageSize.text);
        _boidController.SeparationWeight = int.Parse(_separationWeight.text);
        _boidController.CohesionWeight = int.Parse(_cohesionWeight.text);
        _boidController.AlignmentWeight = int.Parse(_alignmentWeight.text);
        _boidController.AvoidWallsWeight = int.Parse(_avoidWallsWeight.text);
        _boidController.AvoidWallsTurnDist = int.Parse(_avoidWallsTurnDist.text);
    }

    public void ChangeController(int index)
    {
        if (_curIndex.Equals(index))
        {
            return;
        }
        _curIndex = index;
        GetController();
        SetInputs();
    }

    public void Dispose()
    {
        if (_boidController != null)
        {
            _boidController.Dispose();
        }
    }

    private void GetController()
    {
        Dispose();
        _boidController = _controllers[_curIndex].GetComponent<BoidControllerBase>();
    }

    private void SetInputs()
    {
        _boidAmount.text = _boidController.BoidAmount.ToString();
        _boidSpeed.text = _boidController.BoidSpeed.ToString();
        _boidPerceptionRadius.text = _boidController.BoidPerceptionRadius.ToString();
        _cageSize.text = _boidController.CageSize.ToString();
        _separationWeight.text = _boidController.SeparationWeight.ToString();
        _cohesionWeight.text = _boidController.CohesionWeight.ToString();
        _alignmentWeight.text = _boidController.AlignmentWeight.ToString();
        _avoidWallsWeight.text = _boidController.AvoidWallsWeight.ToString();
        _avoidWallsTurnDist.text = _boidController.AvoidWallsTurnDist.ToString();
    }
}
