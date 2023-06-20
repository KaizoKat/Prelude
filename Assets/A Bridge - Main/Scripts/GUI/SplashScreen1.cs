using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen1 : MonoBehaviour
{
    ParticleSystem _prtS;
    ParticleSystem.ForceOverLifetimeModule _folt;
    void Start()
    {
        _prtS = GetComponent<ParticleSystem>();
        _folt = _prtS.forceOverLifetime;
    }

    float _a;
    float _Timer;
    void Update()
    {
        _a += Time.deltaTime;
        if (_a > 360)
            _a = 0;
        if (_Timer >= 1)
        {
            _Timer = 0;
            _folt.x = Mathf.Sin(_a);
            _folt.y = Mathf.Cos(_a);
        }
        else _Timer += Time.deltaTime;

    }
}
