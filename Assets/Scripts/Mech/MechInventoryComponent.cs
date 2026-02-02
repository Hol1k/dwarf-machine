using UnityEngine;

namespace Mech
{
    public class MechInventoryComponent : MonoBehaviour, IMechInventoryData
    {
        public float FillingPercentage => _currentResourcesWeight / _maxResourcesWeight;
        
        [SerializeField] private float _maxResourcesWeight;
        [SerializeField] private float _currentResourcesWeight;
    }
}