using Character;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Abilities
{
    [CreateAssetMenu(fileName = "NewGunDroneAbility", menuName = "Abilities/GunDroneAbility", order = 0)]
    public class GunDroneAbility : Ability
    {
        [SerializeField] AssetReference dronePrefab;

        [SerializeField] private float droneLifeTime;
        [SerializeField] [Min(0)] private float cooldown;
        public override float Cooldown => cooldown;
        
        [Space]
        [SerializeField] private float attackSpeed;
        [SerializeField] private float damage;

        public override void Cast(CharacterAbilitiesController handler)
        {
            CastDroneAsync(handler).Forget();
        }

        private async UniTask CastDroneAsync(CharacterAbilitiesController handler)
        {
            var drone = await dronePrefab.InstantiateAsync(handler.transform.position, new Quaternion()).ToUniTask();
            var droneComponent = drone.GetComponent<GunDroneComponent>();
            droneComponent.droneHandler = handler.transform;
            droneComponent.attackSpeed = attackSpeed;
            droneComponent.damage = damage;
            await UniTask.WaitForSeconds(droneLifeTime);
            Addressables.Release(drone);
        }
    }
}