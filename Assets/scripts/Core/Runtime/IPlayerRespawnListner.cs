namespace Core
{
	public interface IPlayerRespawnListner
	{
		void OnPlayerRespawnListnerInThisCheckpoint(Checkpoint checkpoint, Player player);

	}
}