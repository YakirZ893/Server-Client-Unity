using Mirror;
using System;
using UnityEngine;

public enum ControlMode { keyboard = 1, Remote = 2 };

public class CarController : NetworkBehaviour
{
    public ServerData serverData;
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider frontLeftCollider;
    [SerializeField] private WheelCollider frontRightCollider;
    [SerializeField] private WheelCollider rearLeftCollider;
    [SerializeField] private WheelCollider rearRightCollider;

    [Header("Wheel Transforms")]
    [SerializeField] private Transform frontLeftTransform;
    [SerializeField] private Transform frontRightTransform;
    [SerializeField] private Transform rearLeftTransform;
    [SerializeField] private Transform rearRightTransform;

    [Header("Car Handeling")]
    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteeringAngle;

    [Header("Remote Control")]
    [SyncVar(hook = nameof(OnModeChange))]
    private bool IsRemote = false;
    private float Accel;
    private float Steer;
    private bool Brake;
    private GameObject UI;
    public ControlMode inputMode;

    private float _flipValue = 90f;
    [SyncVar]
    private float _horizontalInput;
    [SyncVar]
    private float _verticalInput;
    [SyncVar]
    private float _currentbreakForce;
    [SyncVar]
    private float _currentSteerAngle;
    [SyncVar]
    private bool isBreaking;


    public void AccelInput(float input) { Accel = input; }
    public void SteerInput(float input) { Steer = input; }
    public void BrakeInput(bool input) { Brake = input; }

    public override void OnStartServer()
    {
        UI = serverData.GetUI();
    }

    public override void OnStartLocalPlayer()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        UI = serverData.GetUI();
        UI.SetActive(false);
        serverData.GetPlayer();
        if (isClient)
        {
            serverData.SetCamera();
        }
    }

    private void Start()
    {
        if (inputMode == ControlMode.keyboard)
        {
            IsRemote = false;
        }
        else
        {
            IsRemote = true;
        }
    }

    void FixedUpdate()
    {
        CheckControlMode();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        CheckCar();
    }

    private void CheckControlMode()
    {
        if (IsRemote)
        {
            activateUIOnServer();
            GetRemoteInput();
        }
        else
        {
            GetKeyboardInput();
        }
    }

    [Server]
    private void activateUIOnServer()
    {
        UI.SetActive(true);
        serverData.SetRemote();
        if (isServer)
        {
        serverData.SetCamera();
        }
    }

    private void GetKeyboardInput()
    {
        _horizontalInput = Input.GetAxis(HORIZONTAL);
        _verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    [Server]
    private void GetRemoteInput()
    {
        _verticalInput = Accel;
        _horizontalInput = Steer;
        isBreaking = Brake;
    }

    private void HandleMotor()
    {
        frontLeftCollider.motorTorque = _verticalInput * motorForce;
        frontRightCollider.motorTorque = _verticalInput * motorForce;
        _currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightCollider.brakeTorque = _currentbreakForce;
        frontLeftCollider.brakeTorque = _currentbreakForce;
        rearLeftCollider.brakeTorque = _currentbreakForce;
        rearRightCollider.brakeTorque = _currentbreakForce;
    }

    private void HandleSteering()
    {
        _currentSteerAngle = maxSteeringAngle * _horizontalInput;
        frontLeftCollider.steerAngle = _currentSteerAngle;
        frontRightCollider.steerAngle = _currentSteerAngle;

    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftCollider, frontLeftTransform);
        UpdateSingleWheel(frontRightCollider, frontRightTransform);
        UpdateSingleWheel(rearLeftCollider, rearLeftTransform);
        UpdateSingleWheel(rearRightCollider, rearRightTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelColl, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelColl.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
    
    private void CheckCar()
    {
        float carRot = gameObject.transform.rotation.eulerAngles.z;
        if (carRot >= _flipValue || carRot <= _flipValue)
        {
            FlipCar();
        }
    }

    [Client]
    private void FlipCar()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Vector3 eulerRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
            
        }
    }

    private void OnModeChange(bool oldMode, bool newMode)
    {
        this.IsRemote = newMode;
    }
    
   
    

    
}




