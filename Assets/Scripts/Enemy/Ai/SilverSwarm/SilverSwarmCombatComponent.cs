using System;
using Entities;
using UnityEngine;

namespace Enemy.Ai.SilverSwarm
{
    public class SilverSwarmCombatComponent : EnemyCombatComponent
    {
        private float _lastAttackTime;

        public override bool CanAttackTarget => throw new NotImplementedException();

        public override bool CanAttackTargetFrom(Vector3 position) => throw new NotImplementedException();
        public override void AttackTarget(StatsComponent target)
        {
            throw new NotImplementedException();
        }
    }
}