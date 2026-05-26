using Cysharp.Threading.Tasks;
using Unity.AI.Navigation;
using UnityEngine;
using Zenject;

namespace Level
{
    [RequireComponent(typeof(NavMeshSurface))]
    public class NavMeshSurfaceController : MonoBehaviour
    {
        public NavMeshSurface Surface { get; private set; }

        [Inject]
        private void Init(NavMeshSurface surface)
        {
            Surface = surface;
        }

        public async UniTask BuildNavMesh()
        {
            Surface.BuildNavMesh();
            await UniTask.WaitUntil(() => Surface.navMeshData);
        }
    }
}