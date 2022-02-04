using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject player;

	public BoxCollider2D Bounds;

	private Vector3
		_min,
		_max;

	public bool IsFollowing { get; set;}

	public static Vector3 targetPos;
	public static float viewX;
	public static float viewY;

	public void Start()
	{
		_min = Bounds.bounds.min;
		_max = Bounds.bounds.max;
	}

	public void Update()
	{
		viewX = player.transform.position.x;
		viewY = player.transform.position.y;
		targetPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

		if (IsFollowing)
		{
			viewX -= (viewX - targetPos.x) * .1f;
			viewY -= (viewY - targetPos.y) * .1f;
		}

		var orthographic = GetComponent<Camera>().orthographicSize;
		var cameraHalfWidth = orthographic * ((float)Screen.width / Screen.height);

		viewX = Mathf.Clamp(viewX, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
		viewY = Mathf.Clamp(viewY, _min.y + orthographic, _max.y - orthographic);

		gameObject.transform.position = new Vector3(viewX, viewY, gameObject.transform.position.z);
	}

}
