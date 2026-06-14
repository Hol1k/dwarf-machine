using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Loot;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using Random = UnityEngine.Random;

namespace Level
{
    public class SecondaryLootSpawnManager : MonoBehaviour
    {
        public IReadOnlyList<Transform> OreVeinsCollection => _oreVeins;
        private List<Transform> _oreVeins;
        
        public IReadOnlyList<Transform> WoodCollection => _woods;
        private List<Transform> _woods;
        
        [SerializeField] private int oreVeinCount;
        [SerializeField] private int rareWoodCount;
        
        [Space]
        [SerializeField] private AssetReference oreVeinPrefab;
        [SerializeField] private AssetReference rareWoodPrefab;
        
        [Inject] private DiContainer _diContainer;
        
        [Space]
        [SerializeField] private bool drawLootSpawnRadius;
        [SerializeField] [Min(3)] private int countOfLootSpawnRadiusLines;
        [SerializeField] private Vector2 centerOfLootSpawn;
        [SerializeField] private float radiusOfLootSpawn;
        
        public Transform ClosestOreVeinTransform(Transform target) => 
            OreVeinsCollection.OrderBy(ore => Vector3.Distance(target.position, ore.position)).First();

        public void DestroyClosestOreVein(Transform target)
        {
            var oreVein = ClosestOreVeinTransform(target);
            _oreVeins.Remove(oreVein);
            Destroy(oreVein.gameObject);
        }
        
        public Transform ClosestWoodTransform(Transform target) => 
            WoodCollection.OrderBy(ore => Vector3.Distance(target.position, ore.position)).First();

        public async UniTask SpawnLoot()
        {
            try
            {
                var oreVeinSpawnTask = SpawnOreVein();
                var rareWoodSpawnTask = SpawnRareWood();
                await UniTask.WhenAll(
                    oreVeinSpawnTask,
                    rareWoodSpawnTask);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        
        private async UniTask SpawnOreVein()
        {
            var groundLayer = LayerMask.NameToLayer("Ground");
            _oreVeins = new List<Transform>();
            
            for (int i = 0; i < oreVeinCount; i++)
            {
                var spawnTask = Addressables.InstantiateAsync(oreVeinPrefab, parent: transform).ToUniTask();
                
                var rayCastPosVector2 = Random.insideUnitCircle * radiusOfLootSpawn;
                var rayCastPos = new Vector3(centerOfLootSpawn.x + rayCastPosVector2.x, 500f, centerOfLootSpawn.y + rayCastPosVector2.y);
                RaycastHit hitInfo;
                while (!Physics.Raycast(rayCastPos, Vector3.down, out hitInfo, float.PositiveInfinity) &&
                       hitInfo.transform.gameObject.layer == groundLayer) {}
                
                var oreVeinObject = await spawnTask;
                oreVeinObject.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y-0.03f, hitInfo.point.z);
                oreVeinObject.transform.rotation = Quaternion.Euler(hitInfo.normal);
                oreVeinObject.transform.Rotate(Vector3.right, Random.Range(-20f, 20f));
                oreVeinObject.transform.Rotate(Vector3.forward, Random.Range(-20f, 20f));
                oreVeinObject.transform.Rotate(Vector3.up, Random.Range(0f, 360f));
                
                _diContainer.InjectGameObject(oreVeinObject);
                _oreVeins.Add(oreVeinObject.transform);

                if (oreVeinObject.TryGetComponent(out BreakableLootComponent oreComponent))
                {
                    oreComponent.OnDeath += obj =>
                    {
                        _oreVeins.Remove(obj.transform);
                    };
                }
            }
        }
        
        private async UniTask SpawnRareWood()
        {
            var groundLayer = LayerMask.NameToLayer("Ground");
            _woods = new List<Transform>();
            
            for (int i = 0; i < rareWoodCount; i++)
            {
                var spawnTask = Addressables.InstantiateAsync(rareWoodPrefab, parent: transform).ToUniTask();
                
                var rayCastPosVector2 = Random.insideUnitCircle * radiusOfLootSpawn;
                var rayCastPos = new Vector3(centerOfLootSpawn.x + rayCastPosVector2.x, 500f, centerOfLootSpawn.y + rayCastPosVector2.y);
                RaycastHit hitInfo;
                while (!Physics.Raycast(rayCastPos, Vector3.down, out hitInfo, float.PositiveInfinity) &&
                       hitInfo.transform.gameObject.layer == groundLayer) {}
                
                var woodObject = await spawnTask;
                woodObject.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y-0.1f, hitInfo.point.z);
                woodObject.transform.Rotate(Vector3.up, Random.Range(0f, 360f));
                
                _diContainer.InjectGameObject(woodObject);
                _woods.Add(woodObject.transform);

                if (woodObject.TryGetComponent(out BreakableLootComponent woodComponent))
                {
                    woodComponent.OnDeath += obj =>
                    {
                        _woods.Remove(obj.transform);
                    };
                }
            }
        }
        
        private void OnDrawGizmos()
        {
            if (drawLootSpawnRadius)
            {
                var middleOfRadius = new Vector3(centerOfLootSpawn.x, 500f, centerOfLootSpawn.y);
                List<Vector3> linesPositions = new();

                for (int i = 0; i < countOfLootSpawnRadiusLines; i++)
                {
                    var linePos =
                        middleOfRadius +
                        Quaternion.Euler(0, 360 / countOfLootSpawnRadiusLines * i, 0) * Vector3.forward *
                        radiusOfLootSpawn;

                    linesPositions.Add(linePos);
                    linesPositions.Add(new Vector3(linePos.x, -100f, linePos.z));
                }

                Gizmos.DrawLineList(new ReadOnlySpan<Vector3>(linesPositions.ToArray()));
            }
        }
    }
}