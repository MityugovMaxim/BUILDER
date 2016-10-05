using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CityBlock : CityItem
{
	private List<CityBuilding> buildings;

	private void Awake()
	{
		buildings = new List<CityBuilding>();
	}
}
