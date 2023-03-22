
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(ParticleSystem))]
public class EmitParticlesOnKartBounce : MonoBehaviour 
{
    ParticleSystem effect;
    GoKartBounce vehicleBounce;

    void Awake() 
    {
        effect = GetComponent<ParticleSystem>();
        vehicleBounce = GetComponentInParent<GoKartBounce>();

        Assert.IsNotNull(vehicleBounce, "This particle should be a child of a VehicleBounce!");
    }

    void Update()
    {
        if (vehicleBounce.BounceFlag)
        {
            effect.Play();
        }
    }
}
