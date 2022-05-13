using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _controllers = new List<GameObject>();
    private int _curIndex;
    private IBoidController _boidController;

    private void Awake()
    {
        ChangeController(0);
    }

    public void Build()
    {
        Dispose();
        GetController();
        _boidController.CreateBoids();
    }

    public void ChangeValues()
    {
    }

    public void ChangeController(int index)
    {
        _curIndex = index;
    }

    public void Dispose()
    {
        if (_boidController!=null)
        {
            _boidController.Dispose();
        }
    }

    private void GetController()
    {
        switch (_curIndex)
        {
            case 0:
            {
                _boidController = _controllers[_curIndex].GetComponent<BoidsController>();
                break;
            }
            case 1:
            {
                _boidController = _controllers[_curIndex].GetComponent<BoidControllerECS>();
                break;
            }
            case 2:
            {
                _boidController = _controllers[_curIndex].GetComponent<BoidControllerECSJobs>();
                break;
            }
            case 3:
            {
                _boidController = _controllers[_curIndex].GetComponent<BoidControllerECSJobsFast>();
                break;
            }
        }
    }
}
