using UnityEngine;

namespace PointsOfInterest
{
    public class PointOfInterestPrefabComponent : MonoBehaviour
    {
        public Transform prefabPivot;
        
        [Space]
        public Transform patrolPointsCollectionParent;
        public Transform shelterRepositionPointsCollectionParent;
        public Transform spawnPointsCollectionParent;
    }
}