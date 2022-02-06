using UnityEngine;

namespace Core
{
	public interface ITakeDamage
	{
		void TakeDamage(int damage, GameObject instigator);	
	}
}

