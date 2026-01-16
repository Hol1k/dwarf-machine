using UnityEngine;

namespace Entities
{
    public interface IForceDamageReactingComponent
    {
        void AddKnockbackForce(Vector3 force);
    }
}