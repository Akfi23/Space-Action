using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akfi
{
    public class RocketComponent : MonoBehaviour
    {
        [SerializeField] private ParticleSystem landingFX;
        [SerializeField] private ParticleSystem startFX;

        public ParticleSystem LandingFX => landingFX;
        public ParticleSystem StartFX => startFX;
    }
}
