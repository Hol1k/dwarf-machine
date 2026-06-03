using System;
using Abilities;
using Cysharp.Threading.Tasks;
using Equipment;
using Mech;
using ScenesManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        private const string GameplayScenePath = "Scenes/Gameplay";
        private const string TestLevelScenePath = "Scenes/TestLevel";
        private const string TestMvpScenePath = "Scenes/MVPLevel";
        
        [SerializeField] private MechType chosenMechType;
        
        [SerializeField] private PlayersEquipment defaultCharacterEquipment;
        [SerializeField] private PlayersEquipment slot1CharacterEquipment;
        [SerializeField] private PlayersEquipment slot2CharacterEquipment;
        [SerializeField] private PlayersEquipment slot3CharacterEquipment;
        
        [SerializeField] private Ability slot1CharacterAbility;
        [SerializeField] private Ability slot2CharacterAbility;
        
        public void OnStartButton()
        {
            StartTestLevel().Forget();
        }

        private async UniTask StartTestLevel()
        {
            try
            {
                DontDestroyOnLoad(this);

                await Addressables.LoadSceneAsync(GameplayScenePath);
                await Addressables.LoadSceneAsync(TestMvpScenePath, LoadSceneMode.Additive);
                
                
                Bootstrap gameplayBootstrap = FindAnyObjectByType<GameplayBootstrap>();
                gameplayBootstrap.Init(new GameplayArgs(
                    chosenMechType,
                    defaultCharacterEquipment,
                    slot1CharacterEquipment,
                    slot2CharacterEquipment,
                    slot3CharacterEquipment,
                    slot1CharacterAbility,
                    slot2CharacterAbility));
                Bootstrap mvpLevelBootstrap = FindAnyObjectByType<MvpLevelBootstrap>();
                mvpLevelBootstrap.Init(new MvpLevelArgs());
            
                Destroy(gameObject);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}