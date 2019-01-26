using UnityEngine;

public class particleTrail : MonoBehaviour
{

    public ParticleSystem thruster;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            thruster.Emit(1); //Emit some particle
        }
    }
}