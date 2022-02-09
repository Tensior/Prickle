using Core;
using Core.Interfaces;
using Zenject;

namespace Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Player>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<IPlayerSpawner>().To<PlayerSpawner>().AsSingle().NonLazy();
        }
    }
}