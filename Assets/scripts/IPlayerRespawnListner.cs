using System;
using System.Collections;
using UnityEngine;

public interface IPlayerRespawnListner
{
	void OnPlayerRespawnListnerInThisCheckpoint(Checkpoint checkpoint, Player player);

}