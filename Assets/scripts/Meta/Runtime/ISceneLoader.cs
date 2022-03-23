using System.Threading.Tasks;

namespace Meta
{
    public interface ISceneLoader
    {
        Task LoadLevelAsync();
        Task LoadMainMenuAsync();
    }
}