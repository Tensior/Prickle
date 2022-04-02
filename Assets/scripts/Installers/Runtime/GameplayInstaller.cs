using Core;
using Core.Interactables;
using Core.Interfaces;
using Core.Managers;
using Core.Providers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Projectile _playerProjectile;
        [SerializeField] private Projectile _stoneProjectile;
        [SerializeField] private Projectile _nutProjectile;
        [SerializeField] private Projectile _flowerProjectile;
        
        public override void InstallBindings()
        {
            Container.Bind<PointManager>().AsSingle().NonLazy();
            Container.Bind<UIManager>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<Player>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<IPlayerSpawner>().To<PlayerSpawner>().AsSingle().NonLazy();
            Container.Bind<IPauseController>().To<PauseController>().AsSingle().NonLazy();

            InstallInput();
            InstallPools();
        }

        private void InstallInput()
        {
            Container.Bind<GameplayControls>().FromNew().AsSingle().NonLazy();
            Container.Bind<IInputProvider>().To<InputProvider>().AsSingle().NonLazy();
        }

        private void InstallPools()
        {
            Container.BindFactory<Vector2, float, EntityType, int, Projectile, Projectile.Factory>()
                     .WithId(_playerProjectile.name)
                     .FromMonoPoolableMemoryPool(x =>
                         x.WithInitialSize(5)
                          .FromComponentInNewPrefab(_playerProjectile)
                          .UnderTransformGroup("PlayerProjectilePool"));

            Container.BindFactory<Vector2, float, EntityType, int, Projectile, Projectile.Factory>()
                     .WithId(_stoneProjectile.name)
                     .FromMonoPoolableMemoryPool(x =>
                         x.WithInitialSize(5)
                          .FromComponentInNewPrefab(_stoneProjectile)
                          .UnderTransformGroup("StoneProjectilePool"));

            Container.BindFactory<Vector2, float, EntityType, int, Projectile, Projectile.Factory>()
                     .WithId(_nutProjectile.name)
                     .FromMonoPoolableMemoryPool(x =>
                         x.WithInitialSize(5)
                          .FromComponentInNewPrefab(_nutProjectile)
                          .UnderTransformGroup("NutProjectilePool"));

            Container.BindFactory<Vector2, float, EntityType, int, Projectile, Projectile.Factory>()
                     .WithId(_flowerProjectile.name)
                     .FromMonoPoolableMemoryPool(x =>
                         x.WithInitialSize(5)
                          .FromComponentInNewPrefab(_flowerProjectile)
                          .UnderTransformGroup("FlowerProjectilePool"));
        }
    }
}