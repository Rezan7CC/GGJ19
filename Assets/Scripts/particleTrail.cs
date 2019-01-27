using UnityEngine;

public class particleTrail : MonoBehaviour
{
    public int particleAmount;
    public ParticleSystem thruster;

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && !(Game.Instance.GameModel.GetControlMode() == ControlMode.ResourceGathering) 
            && !(Game.Instance.GameModel.GetControlMode() == ControlMode.ShieldMovement))
        {
            thruster.Emit(particleAmount); //Emit some particle
        }
    }
}