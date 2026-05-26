using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Enemy;
using Enemy.Ai;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;

namespace PointsOfInterest
{
    public class PointOfInterest : MonoBehaviour
    {
        public PoiType poiType;

        [Min(0)] public int poiValue;
        
        private GameObject _poiPrefab;
        
        private Transform _patrolPointsCollectionParent;
        private Transform _shelterRepositionPointsCollectionParent;
        private Transform _spawnPointsCollectionParent;

        public EnemyPatrolPointsCollection PatrolPointsCollection => new(
            _patrolPointsCollectionParent
                .GetComponentsInChildren<Transform>()
                .Where(point => point != _patrolPointsCollectionParent).ToArray());

        public EnemyRepositionPointsCollection ShelterRepositionPointsCollection => new(
            _shelterRepositionPointsCollectionParent.GetComponentsInChildren<Transform>()
                .Where(point => point != _shelterRepositionPointsCollectionParent).ToArray());

        public EnemySpawnPointsCollection SpawnPointsCollection => new(
            _spawnPointsCollectionParent
                .GetComponentsInChildren<Transform>()
                .Where(point => point != _spawnPointsCollectionParent).ToArray());

        public bool IsOccupied { get; private set; }

        public async UniTask Occupy(EnemyType enemyType = EnemyType.None)
        {
            IsOccupied = true;

            switch (poiType)
            {
                case PoiType.AboriginesCamp:
                    if (enemyType == EnemyType.Aborigine)
                    {
                        _poiPrefab =
                            await Addressables.InstantiateAsync("POIs/AboriginesCamp_AboriginesOccupied").Task;
                    }
                    else
                    {
                        _poiPrefab =
                            await Addressables.InstantiateAsync("POIs/AboriginesCamp_Empty").Task;
                    }
                    break;
                case PoiType.VeinCluster:
                    if (enemyType == EnemyType.Soldier)
                    {
                        _poiPrefab =
                            await Addressables.InstantiateAsync("POIs/VeinCluster_SoldiersOccupied").Task;
                    }
                    else
                    {
                        _poiPrefab =
                            await Addressables.InstantiateAsync("POIs/VeinCluster_Empty").Task;
                    }
                    break;
                case PoiType.Oasis:
                    if (enemyType == EnemyType.Aborigine)
                    {
                        _poiPrefab =
                            await Addressables.InstantiateAsync("POIs/Oasis_AboriginesOccupied").Task;
                    }
                    else
                    {
                        _poiPrefab =
                            await Addressables.InstantiateAsync("POIs/Oasis_Empty").Task;
                    }
                    break;
            }
            
            _poiPrefab.transform.SetParent(transform);

            ApplyPrefabParams();
        }

        private void ApplyPrefabParams()
        {
            if (_poiPrefab.TryGetComponent(out PointOfInterestPrefabComponent poiPrefabComponent))
            {
                _poiPrefab.transform.Rotate(Vector3.up, Random.Range(0f, 360f));
                _poiPrefab.transform.position = _poiPrefab.transform.position + transform.position - poiPrefabComponent.prefabPivot.position;
                    
                _patrolPointsCollectionParent = poiPrefabComponent.patrolPointsCollectionParent;
                _shelterRepositionPointsCollectionParent =  poiPrefabComponent.shelterRepositionPointsCollectionParent;
                _spawnPointsCollectionParent = poiPrefabComponent.spawnPointsCollectionParent;
            }
        }

        private void OnDestroy()
        {
            Addressables.ReleaseInstance(_poiPrefab);
        }
    }

    public enum PoiType
    {
        VeinCluster,
        AboriginesCamp,
        Oasis
    }
}