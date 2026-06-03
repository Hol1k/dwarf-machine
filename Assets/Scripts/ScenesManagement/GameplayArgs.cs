using Abilities;
using Equipment;
using Mech;

namespace ScenesManagement
{
    public class GameplayArgs : IBootstrapArgs
    {
        public GameplayArgs(
            MechType chosenMechType, 
            PlayersEquipment defaultCharacterEquipment, 
            PlayersEquipment slot1CharacterEquipment, 
            PlayersEquipment slot2CharacterEquipment, 
            PlayersEquipment slot3CharacterEquipment, 
            Ability slot1CharacterAbility, 
            Ability slot2CharacterAbility)
        {
            ChosenMechType = chosenMechType;
            DefaultCharacterEquipment = defaultCharacterEquipment;
            Slot1CharacterEquipment = slot1CharacterEquipment;
            Slot2CharacterEquipment = slot2CharacterEquipment;
            Slot3CharacterEquipment = slot3CharacterEquipment;
            Slot1CharacterAbility = slot1CharacterAbility;
            Slot2CharacterAbility = slot2CharacterAbility;
        }
        
        public MechType ChosenMechType { get; }
        
        public PlayersEquipment DefaultCharacterEquipment { get; }
        public PlayersEquipment Slot1CharacterEquipment { get; }
        public PlayersEquipment Slot2CharacterEquipment { get; }
        public PlayersEquipment Slot3CharacterEquipment { get; }
        
        public Ability Slot1CharacterAbility { get; }
        public Ability Slot2CharacterAbility { get; }
    }
}