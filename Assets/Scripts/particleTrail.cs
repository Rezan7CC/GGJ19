﻿using UnityEngine;

public class particleTrail : MonoBehaviour
{
    public int particleAmount;
    public ParticleSystem thruster;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && !(Game.Instance.GameModel.GetControlMode() == ControlMode.ResourceGathering))
        {
            thruster.Emit(particleAmount); //Emit some particle
        }
    }
}