using UI;
using Zenject;

namespace Installers
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LivesVM>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}