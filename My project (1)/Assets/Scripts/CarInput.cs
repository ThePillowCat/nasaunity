using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarInput : MonoBehaviour
{
    [SerializeField]
    private Transform leftTrackForce;
    [SerializeField] 
    private Transform rightTrackForce;
    [SerializeField]
    private InputAction W;
    [SerializeField]
    private InputAction A;
    [SerializeField]
    private InputAction S;
    [SerializeField]
    private InputAction D;
    [SerializeField]
    private Rigidbody carRigidBody;
    [SerializeField]
    private float trackSpeed;
    [SerializeField]
    private float maxMagVelocity;
    [SerializeField]
    private float maxTurnVelocity;
    [SerializeField]
    private Transform tireFR;
    [SerializeField]
    private Transform tireFL;
    [SerializeField]
    private Transform tireBL;
    [SerializeField]
    private Transform tireBR;
    [SerializeField]
    private float tireSpeed = 0.5f;

    private bool Wpressed, Apressed, Spressed, Dpressed;

    private void OnEnable()
    {
        W.Enable();
        A.Enable();
        S.Enable();
        D.Enable();
    }

    private void OnDisable()
    {
        W.Disable();
        A.Disable();
        S.Disable();
        D.Disable();
    }

    void Update()
    {
        Wpressed = W.ReadValue<float>() == 1f;
        Apressed = A.ReadValue<float>() == 1f;
        Spressed = S.ReadValue<float>() == 1f;
        Dpressed = D.ReadValue<float>() == 1f;

        Vector3 leftTrackWorldPosition = leftTrackForce.TransformPoint(Vector3.zero);
        Vector3 rightTrackWorldPosition = rightTrackForce.TransformPoint(Vector3.zero);

        Vector3 leftForceDirection = leftTrackForce.TransformDirection(Vector3.forward);
        Vector3 rightForceDirection = rightTrackForce.TransformDirection(Vector3.forward);

        float forceMagnitude = trackSpeed;

        // Move forward
        if (Wpressed)
        {
            carRigidBody.AddForceAtPosition(leftForceDirection * forceMagnitude, leftTrackWorldPosition, ForceMode.Force);
            carRigidBody.AddForceAtPosition(rightForceDirection * forceMagnitude, rightTrackWorldPosition, ForceMode.Force);
            tireBL.Rotate(new Vector3(tireSpeed, 0f, 0f));
            tireBR.Rotate(new Vector3(tireSpeed, 0f, 0f));
            tireFL.Rotate(new Vector3(tireSpeed, 0f, 0f));
            tireFR.Rotate(new Vector3(tireSpeed, 0f, 0f));
        }

        // Move backward
        if (Spressed)
        {
            carRigidBody.AddForceAtPosition(-leftForceDirection * forceMagnitude, leftTrackWorldPosition, ForceMode.Force);
            carRigidBody.AddForceAtPosition(-rightForceDirection * forceMagnitude, rightTrackWorldPosition, ForceMode.Force);
            tireBL.Rotate(new Vector3(-tireSpeed, 0f, 0f));
            tireBR.Rotate(new Vector3(-tireSpeed, 0f, 0f));
            tireFL.Rotate(new Vector3(-tireSpeed, 0f, 0f));
            tireFR.Rotate(new Vector3(-tireSpeed, 0f, 0f));
        }

        // Turn left (A pressed, no W/S)
        if (Apressed && !Wpressed && !Spressed)
        {
            // Simulate turning in place: left track moves backward, right track moves forward
            carRigidBody.AddForceAtPosition(-leftForceDirection * forceMagnitude, leftTrackWorldPosition, ForceMode.Force);
            carRigidBody.AddForceAtPosition(rightForceDirection * forceMagnitude, rightTrackWorldPosition, ForceMode.Force);
            tireBL.Rotate(new Vector3(-tireSpeed, 0f, 0f));
            tireBR.Rotate(new Vector3(tireSpeed, 0f, 0f));
            tireFL.Rotate(new Vector3(-tireSpeed, 0f, 0f));
            tireFR.Rotate(new Vector3(tireSpeed, 0f, 0f));
        }

        // Turn right (D pressed, no W/S)
        if (Dpressed && !Wpressed && !Spressed)
        {
            // Simulate turning in place: left track moves forward, right track moves backward
            carRigidBody.AddForceAtPosition(leftForceDirection * forceMagnitude, leftTrackWorldPosition, ForceMode.Force);
            carRigidBody.AddForceAtPosition(-rightForceDirection * forceMagnitude, rightTrackWorldPosition, ForceMode.Force);
            tireBL.Rotate(new Vector3(tireSpeed, 0f, 0f));
            tireBR.Rotate(new Vector3(-tireSpeed, 0f, 0f));
            tireFL.Rotate(new Vector3(tireSpeed, 0f, 0f));
            tireFR.Rotate(new Vector3(-tireSpeed, 0f, 0f));
        }

        // Handle turning while moving forward
        if (Wpressed && Apressed)
        {
            carRigidBody.AddForceAtPosition(leftForceDirection * forceMagnitude * 0.3f, leftTrackWorldPosition, ForceMode.Force);
            carRigidBody.AddForceAtPosition(rightForceDirection * forceMagnitude, rightTrackWorldPosition, ForceMode.Force);
            tireBL.Rotate(new Vector3(tireSpeed, 0f, 0f));
            tireBR.Rotate(new Vector3(tireSpeed, 0f, 0f));
            tireFL.Rotate(new Vector3(tireSpeed, 0f, 0f));
            tireFR.Rotate(new Vector3(tireSpeed, 0f, 0f));
        }
        else if (Wpressed && Dpressed)
        {
            carRigidBody.AddForceAtPosition(leftForceDirection * forceMagnitude, leftTrackWorldPosition, ForceMode.Force);
            carRigidBody.AddForceAtPosition(rightForceDirection * forceMagnitude * 0.3f, rightTrackWorldPosition, ForceMode.Force);
            tireBL.Rotate(new Vector3(tireSpeed, 0f, 0f));
            tireBR.Rotate(new Vector3(tireSpeed, 0f, 0f));
            tireFL.Rotate(new Vector3(tireSpeed, 0f, 0f));
            tireFR.Rotate(new Vector3(tireSpeed, 0f, 0f));
        }

        // Handle turning while moving backward
        if (Spressed && Apressed)
        {
            carRigidBody.AddForceAtPosition(-leftForceDirection * forceMagnitude * 0.3f, leftTrackWorldPosition, ForceMode.Force);
            carRigidBody.AddForceAtPosition(-rightForceDirection * forceMagnitude, rightTrackWorldPosition, ForceMode.Force);
            tireBL.Rotate(new Vector3(-tireSpeed, 0f, 0f));
            tireBR.Rotate(new Vector3(-tireSpeed, 0f, 0f));
            tireFL.Rotate(new Vector3(-tireSpeed, 0f, 0f));
            tireFR.Rotate(new Vector3(-tireSpeed, 0f, 0f));
        }
        else if (Spressed && Dpressed)
        {
            carRigidBody.AddForceAtPosition(-leftForceDirection * forceMagnitude, leftTrackWorldPosition, ForceMode.Force);
            carRigidBody.AddForceAtPosition(-rightForceDirection * forceMagnitude * 0.3f, rightTrackWorldPosition, ForceMode.Force);
            tireBL.Rotate(new Vector3(-tireSpeed, 0f, 0f));
            tireBR.Rotate(new Vector3(-tireSpeed, 0f, 0f));
            tireFL.Rotate(new Vector3(-tireSpeed, 0f, 0f));
            tireFR.Rotate(new Vector3(-tireSpeed, 0f, 0f));
        }

        if (carRigidBody.velocity.magnitude >= maxMagVelocity)
        {
            carRigidBody.velocity = carRigidBody.velocity.normalized * maxMagVelocity;
        }
        if (carRigidBody.angularVelocity.magnitude >= maxTurnVelocity) {
            carRigidBody.angularVelocity = carRigidBody.angularVelocity.normalized * maxTurnVelocity;
        }
    }
}
