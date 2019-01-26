using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerController : MonoBehaviour, IResetable
{
	public Shield Shield;
	public shipManeuverController Ship;
	public MammothViewController MammothViewController;

	private TriggerCollisionType _currentTriggerCollisionType;

	private readonly Dictionary<ControlMode, Action> _controlsMapping = new Dictionary<ControlMode, Action>();
	private readonly Dictionary<TriggerCollisionType, ControlMode> _collisionTypeMapping = new Dictionary<TriggerCollisionType, ControlMode>()
	{
		{ TriggerCollisionType.HomeStation, ControlMode.ShieldMovement },
		{ TriggerCollisionType.ResourcePlanet, ControlMode.ResourceGathering }
	};

	private void Start()
	{
		_controlsMapping[ControlMode.ResourceGathering] = HandleResourceGathering;
		_controlsMapping[ControlMode.ShipMovement] = HandleShipMovement;
		_controlsMapping[ControlMode.ShieldMovement] = HandleShieldMovement;

		Game.Instance.GameSignals.OnTriggerCollisionEnter += OnTriggerCollisionEnter;
		Game.Instance.GameSignals.OnTriggerCollisionExit += OnTriggerCollisionExit;

		Game.Instance.GameModel.OnControlModeChanged += OnControlModeChanged;
	}

	private void OnControlModeChanged(ControlMode controlmode)
	{
		if (controlmode != ControlMode.ShipMovement)
		{
			Ship.StopMovement();
		}

		if (controlmode == ControlMode.ShieldMovement)
		{
			Game.Instance.GameModel.DeliverResources();
		}

		if (controlmode == ControlMode.ResourceGathering)
		{
			MammothViewController.DockToResourcePlanet();
		}
	}

	private void OnTriggerCollisionExit(TriggerCollisionType type)
	{
		_currentTriggerCollisionType = TriggerCollisionType.None;
	}

	private void OnTriggerCollisionEnter(TriggerCollisionType type)
	{
		_currentTriggerCollisionType = type;
	}

	public void Reset()
	{
		Game.Instance.GameModel.SetControlMode(ControlMode.ShipMovement);
	}

	private void FixedUpdate()
	{
		HandleControlMode();
		_controlsMapping[Game.Instance.GameModel.GetControlMode()]();
	}

	private void HandleControlMode()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (_currentTriggerCollisionType != TriggerCollisionType.None &&
			    Game.Instance.GameModel.GetControlMode() == ControlMode.ShipMovement)
			{
				var controlMode = _collisionTypeMapping[_currentTriggerCollisionType];
				Game.Instance.GameModel.SetControlMode(controlMode);
			}
			else if (Game.Instance.GameModel.GetControlMode() != ControlMode.ShipMovement)
			{
				Game.Instance.GameModel.SetControlMode(ControlMode.ShipMovement);
			}
		}
	}

	private void HandleShieldMovement()
	{
		Shield.HandleMovement();
	}

	private void HandleShipMovement()
	{
		Ship.HandleMovement();
	}

	private void HandleResourceGathering()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			Game.Instance.GameModel.IncreaseResourceAmount(Game.Instance.GameSettings.collectResouceSpeed);
		}
	}
}
