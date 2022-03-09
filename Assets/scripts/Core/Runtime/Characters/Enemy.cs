namespace Core.Characters
{
	public class Enemy : Character, IPlayerSpawnListener
	{
		public void OnPlayerSpawn()
		{
			HealthSystem?.ModifyHealth(HealthSystem.MaxHealth);
			gameObject.SetActive(true);
		}
	}
}


