using UnityEngine;

public class FurnitureMovement : MonoBehaviour
{
    [SerializeField] private GameObject furniture;

    [Header("Toggle Axes")]
    [SerializeField] private bool CanYaw;
    [SerializeField] private bool CanPitch;
    [SerializeField] private bool CanRoll;

    [Header("Speeds (deg/sec)")]
    [SerializeField] private float YawSpeed = 30f;
    [SerializeField] private float PitchSpeed = 30f;
    [SerializeField] private float RollSpeed = 30f;

    [Header("Clamp (degrees)")]
    [SerializeField] private float YawClamp = 30f;
    [SerializeField] private float PitchClamp = 30f;
    [SerializeField] private float RollClamp = 30f;

    private Vector3 defaultPos;
    private Quaternion defaultRot;

    private float yawAngle, pitchAngle, rollAngle;

    public bool moveFurniture = false;

    void Start()
    {
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
