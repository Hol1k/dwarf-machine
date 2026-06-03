using System;
using Character;
using Mech;
using UnityEngine;

namespace ScenesManagement
{
    public class GameplayBootstrap : Bootstrap
    {
        [SerializeField] private GameObject flamethrowerMech;
        [SerializeField] private GameObject hummerMech;
        [SerializeField] private GameObject swordMech;
        
        [Space]
        [SerializeField] private EquipmentHandlerComponent equipmentHandler;
        [SerializeField] private CharacterAbilitiesController characterAbilitiesController;
        
        public override void Init(IBootstrapArgs args)
        {
            if (args is not GameplayArgs gameplayArgs)
                throw new ArgumentException("Invalid args type");
            
            InitLevel(gameplayArgs);
        }

        private void InitLevel(GameplayArgs args)
        {
            switch (args.ChosenMechType)
            {
                case MechType.Flamethrower:
                    Destroy(hummerMech);
                    Destroy(swordMech);
                    break;
                case MechType.Hummer:
                    Destroy(flamethrowerMech);
                    Destroy(swordMech);
                    break;
                case MechType.Sword:
                    Destroy(flamethrowerMech);
                    Destroy(hummerMech);
                    break;
            }

            equipmentHandler.defaultEquipment = args.DefaultCharacterEquipment;
            equipmentHandler.equipmentSlot1 = args.Slot1CharacterEquipment;
            equipmentHandler.equipmentSlot2 = args.Slot2CharacterEquipment;
            equipmentHandler.equipmentSlot3 = args.Slot3CharacterEquipment;
            
            characterAbilitiesController.ability1 = args.Slot1CharacterAbility;
            characterAbilitiesController.ability2 = args.Slot2CharacterAbility;
        }
    }
}