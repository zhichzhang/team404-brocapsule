using UnityEngine;

public class RadialAbsorb : MonoBehaviour
{
    public ParticleSystem ps;
    public Transform centerPoint;
    public float pullSpeed = 1.5f;

    ParticleSystem.Particle[] particles;

    void LateUpdate()
    {
        if (ps == null || centerPoint == null) return;

        if (particles == null || particles.Length < ps.main.maxParticles)
            particles = new ParticleSystem.Particle[ps.main.maxParticles];

        int count = ps.GetParticles(particles);

        Vector3 center = centerPoint.position;

        for (int i = 0; i < count; i++)
        {
            Vector3 dir = (center - particles[i].position).normalized;
            particles[i].velocity = dir * pullSpeed;
        }

        ps.SetParticles(particles, count);
    }
}