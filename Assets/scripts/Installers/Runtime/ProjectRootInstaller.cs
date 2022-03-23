using Meta;
using Zenject;

namespace Installers
{
    public class ProjectRootInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle().NonLazy();
        }
    }
}