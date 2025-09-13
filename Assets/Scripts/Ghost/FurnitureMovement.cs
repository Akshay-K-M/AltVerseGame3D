using Unity.VisualScripting;
using UnityEngine;

public class FurnitureMovement : MonoBehaviour
{
    [SerializeField] private GameObject furniture;

    [Header("Toggle Axes")]
    [SerializeField] private bool CanYaw = true;
    [SerializeField] private bool CanPitch = true;
    [SerializeField] private bool CanRoll = true;

    [Header("Speeds (deg/sec)")]
    [SerializeField] private float YawSpeed = 80f;
    [SerializeField] private float PitchSpeed = 80f;
    [SerializeField] private float RollSpeed = 80f;

    [Header("Clamp (degrees)")]
    [SerializeField] private float YawClamp = 5f;
    [SerializeField] private float PitchClamp = 5f;
    [SerializeField] private float RollClamp = 5f;

    private Vector3 defaultPos;
    private Quaternion defaultRot;

    private float yawAngle, pitchAngle, rollAngle;

    public bool moveFurniture = false;

    void Start()
    {
        furniture = this.GameObject();
        defaultPos = furniture.transform.position;
        defaultRot = furniture.transform.rotation;
    }

    void Update()
    {
        if (moveFurniture)
        {
            float dt = Time.deltaTime;

            if (CanYaw)
            {
                yawAngle += YawSpeed * dt;
                if (Mathf.Abs(yawAngle) > YawClamp) {
                    YawSpeed = -YawSpeed;
                    yawAngle = Mathf.Clamp(yawAngle, -YawClamp, YawClamp);
                }
            }

            if (CanPitch)
            {
                pitchAngle += PitchSpeed * dt;
                if (Mathf.Abs(pitchAngle) > PitchClamp) {
                    PitchSpeed = -PitchSpeed;
                    pitchAngle = Mathf.Clamp(pitchAngle, -PitchClamp, PitchClamp);
                }
            }

            if (CanRoll)
            {
                rollAngle += RollSpeed * dt;
                if (Mathf.Abs(rollAngle) > RollClamp) {
                    RollSpeed = -RollSpeed;
                    rollAngle = Mathf.Clamp(rollAngle, -RollClamp, RollClamp);
                }
            }

            // Apply combined rotation relative to default
            furniture.transform.SetPositionAndRotation(
                defaultPos,
                defaultRot * Quaternion.Euler(pitchAngle, yawAngle, rollAngle)
            );
        }
        else
        {
            // Reset to default smoothly
            furniture.transform.SetPositionAndRotation(
                Vector3.Lerp(furniture.transform.position, defaultPos, Time.deltaTime * 5f),
                Quaternion.Slerp(furniture.transform.rotation, defaultRot, Time.deltaTime * 5f)
            );
        }
    }
}
