using UnityEngine;
using Cinemachine;

public class DollyCartMover : MonoBehaviour
{
    public CinemachineDollyCart dollyCart;
    public float speed = 5f;

    void Update()
    {
        if (dollyCart != null)
        {
            dollyCart.m_Position += speed * Time.deltaTime;
        }
    }
}