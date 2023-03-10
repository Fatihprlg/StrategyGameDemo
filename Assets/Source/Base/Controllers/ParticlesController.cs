using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : ControllerBase
{
    private static ParticlesController instance;
    [SerializeField] private ParticlePoolModel[] particlePools;

    public override void Initialize()
    {
        base.Initialize();

        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }

    public static void SetParticle(int index, Vector3 pos)
    {
        instance.particlePools[index].SetParticle(pos);
    }

    public static void SetParticle(int index, Vector3 pos, Quaternion rotation)
    {
        instance.particlePools[index].SetParticle(pos, rotation);
    }

}