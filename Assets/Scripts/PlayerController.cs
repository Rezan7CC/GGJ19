using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerController : MonoBehaviour, IResetable
{
	private int Mine = Animator.StringToHash("Mine");
	private int Land = Animator.StringToHash("Land");
	private int Launch = Animator.StringToHash("Launch");
	private int Soldier = Animator.StringToHash("Soldier");
	
	public Shield Shield;
	public shipManeuverController Ship;
	public MammothViewController MammothViewController;
	public Animator MammothAnimator;
    public Rigidbody ShipRigidbody;

    public AudioSource PlayerAudioSource;
    public AudioClip LaunchAudioClip;
    public AudioClip LandAudioClip;
    public AudioSource DrillingAudioSource;

    private TriggerCollisionType _currentTriggerCollisionType;
	private float collectFrequency;
	private float currentCollectFrequency;

	private readonly Dictionary<ControlMode, Action> _controlsMapping = new Dictionary<ControlMode, Action>();
	private readonly Dictionary<TriggerCollisionType, ControlMode> _collisionTypeMapping = new Dictionary<TriggerCollisionType, ControlMode>()
	{
		{ TriggerCollisionType.HomeStation, ControlMode.ShieldMovement },
		{ TriggerCollisionType.ResourcePlanet, ControlMode.ResourceGathering }
	};

	private int _requiredScoreToWin;

	private void Start()
	{
		_controlsMapping[ControlMode.ResourceGathering] = HandleResourceGathering;
		_controlsMapping[ControlMode.ShipMovement] = HandleShipMovement;
		_controlsMapping[ControlMode.ShieldMovement] = HandleShieldMovement;

		Game.Instance.GameSignals.OnTriggerCollisionEnter += OnTriggerCollisionEnter;
		Game.Instance.GameSignals.OnTriggerCollisionExit += OnTriggerCollisionExit;
		Game.Instance.GameSignals.OnWin += OnWin;

		Game.Instance.GameModel.OnControlModeChanged += OnControlModeChanged;

		var segmentScores = Game.Instance.GameSettings.segmentScores;
		_requiredScoreToWin = segmentScores[segmentScores.Length - 1];
		
		var gameSettings = Game.Instance.GameSettings;
		collectFrequency = gameSettings.totalCollectDuration / gameSettings.resourceCapacity;
	}

	private void OnWin()
	{
		Game.Instance.GameModel.SetControlMode(ControlMode.ShipMovement);
	}

	private void OnControlModeChanged(ControlMode controlmode)
	{
        if (controlmode == ControlMode.ShipMovement)
        {
            ShipRigidbody.isKinematic = false;
	        Ship.ThrustForward(15);
            PlayerAudioSource.PlayOneShot(LaunchAudioClip);
        }

        if (controlmode != ControlMode.ShipMovement)
        {
            ShipRigidbody.isKinematic = true;
            Ship.StopMovement();
        }

        if (controlmode == ControlMode.ShieldMovement)
        {
            PlayerAudioSource.PlayOneShot(LandAudioClip);
	        
	        if (Game.Instance.GameModel.GetScore() + Game.Instance.GameModel.GetResources() <
	            _requiredScoreToWin)
	        {
				MammothViewController.DockToHomebase(Shield.ShieldObject.transform.rotation.eulerAngles);
	        }
            Game.Instance.GameModel.DeliverResources();
		}

		if (controlmode == ControlMode.ResourceGathering)
		{
            PlayerAudioSource.PlayOneShot(LandAudioClip);
            MammothViewController.DockToResourcePlanet();
            DrillingAudioSource.Play();
        }
        else
        {
            DrillingAudioSource.Stop();
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

	private void Update()
	{
		HandleControlMode();
		var controlMode = Game.Instance.GameModel.GetControlMode();
		if (controlMode == ControlMode.ResourceGathering)
		{
			_controlsMapping[controlMode]();
		}
	}

	private void FixedUpdate()
	{
		var controlMode = Game.Instance.GameModel.GetControlMode();
		if (controlMode == ControlMode.ShieldMovement ||
		    controlMode == ControlMode.ShipMovement)
		{
			_controlsMapping[controlMode]();
		}
	}

	private void HandleControlMode()
	{
		if (Input.GetKeyDown(KeyCode.Space) && _currentTriggerCollisionType != TriggerCollisionType.None)
		{
			if (Game.Instance.GameModel.GetControlMode() == ControlMode.ShipMovement)
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
		if (Input.GetKeyDown(KeyCode.F) && Game.Instance.GameModel.InGameTimeScale > 0)
		{
			MammothAnimator.SetBool(Mine, true);
		}
		
		if (Input.GetKey(KeyCode.F))
		{
			currentCollectFrequency -= Time.deltaTime * Game.Instance.GameModel.InGameTimeScale;
			if (currentCollectFrequency <= 0)
			{
				currentCollectFrequency = collectFrequency;
				Game.Instance.GameModel.IncreaseResourceAmount(1);
			}
		}
		
		if (Input.GetKeyUp(KeyCode.F))
		{
			MammothAnimator.SetBool(Mine, false);
		}
	}
}
