using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ShipController : MonoBehaviour, IResetable
{
	public Shield Shield;
	public shipManeuverController Ship;

	private Dictionary<ControlMode, Action> controlsMapping = new Dictionary<ControlMode, Action>()
	{
		{ ControlMode.ResourceGathering, HandleResourceGathering },
		{ ControlMode.ShipMovement, HandleShipMovement },
		{ ControlMode.ShieldMovement, HandleShieldMovement }
	};

	public void Reset()
	{
		Game.Instance.GameModel.SetControlMode(ControlMode.ShipMovement);
	}

	private void Update()
	{
		controlsMapping[Game.Instance.GameModel.GetControlMode()]();
	}
	
	private static void HandleShieldMovement()
	{
	}

	private static void HandleShipMovement()
	{
	}

	private static void HandleResourceGathering()
	{
	}
}
