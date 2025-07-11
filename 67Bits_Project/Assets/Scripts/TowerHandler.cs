using System.Collections.Generic;
using UnityEngine;

public class TowerHandler : MonoBehaviour
{
    [SerializeField] List<Transform> noodleSegments;

    [Header("// SIMPLE")]
    [SerializeField] float followSpeed = 10f;
    [SerializeField] float segmentHeight = 1f;
    [SerializeField] float wobbleSpeed = 5f;
    [SerializeField] float wobbleAmount = 0.025f;

    [Header("// WITH ROTATION")]
    [SerializeField] float segmentSpacing = 1f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float waveOffset = 1f;

    [Header("// WITH INERTIA")]
    [SerializeField] float followSmoothness = 10f;
    [SerializeField] float inertia = 0.9f;
    [SerializeField] float rotationSmoothness = 5f;

    [Header("// GENERATOR")]
    [SerializeField] List<Transform> _prefabs = null;
    [SerializeField] int _initialCount = 10;

    private readonly List<NoodleSegmentData> noodleData = new();

    private void Start()
    {
        for (int i = 0; i < _initialCount; i++)
        {
            var _prefab = _prefabs[Random.Range(0, _prefabs.Count)];
            var _instance = Instantiate(_prefab);
            noodleSegments.Add(_instance);
        }

        int _count = noodleSegments.Count;

        for (int i = 0; i < _count; i++)
        {
            noodleData.Add(new NoodleSegmentData
            {
                position = noodleSegments[i].position,
                rotation = noodleSegments[i].rotation,
                velocity = Vector3.zero
            });
        }
    }

    private void LateUpdate()
    {
        //Simple();
        //WithRotation();
        WithInertia();
    }

    private void Simple()
    {
        for (int i = 1; i < noodleSegments.Count; i++)
        {
            var segment = noodleSegments[i];

            Vector3 targetPos = noodleSegments[i - 1].position + Vector3.up * segmentHeight;
            segment.position = Vector3.Lerp(noodleSegments[i].position, targetPos, Time.deltaTime * followSpeed);

            Vector3 sideOffset = Mathf.Sin(Time.time * wobbleSpeed) * wobbleAmount * transform.right;
            segment.position += sideOffset;
        }
    }

    private void WithRotation()
    {
        for (int i = 1; i < noodleSegments.Count; i++)
        {
            Transform prev = noodleSegments[i - 1];
            Transform current = noodleSegments[i];

            // Position follow
            Vector3 targetPos = prev.position + Vector3.up * segmentSpacing;
            current.position = Vector3.Lerp(current.position, targetPos, Time.deltaTime * followSpeed);

            // Rotation follow
            Vector3 direction = prev.position - current.position;
            if (direction != Vector3.zero)
            {
                //float wave = Mathf.Sin(Time.time * wobbleSpeed + i * waveOffset) * wobbleAmount;
                //current.localRotation *= Quaternion.Euler(0f, wave, 0f);

                Quaternion targetRot = Quaternion.LookRotation(direction);
                current.rotation = Quaternion.Slerp(current.rotation, targetRot, Time.deltaTime * rotationSpeed);
            }
        }
    }

    private void WithInertia()
    {
        for (int i = 0; i < noodleSegments.Count; i++)
        {
            var segment = noodleSegments[i];
            var data = noodleData[i];

            if (i == 0)
            {
                // First segment follows player directly
                data.position = noodleSegments[0].position;
                data.rotation = noodleSegments[0].rotation;
            }
            else
            {
                var prev = noodleData[i - 1];

                // Target position based on previous
                Vector3 targetPos = prev.position + Vector3.up * segmentSpacing;

                // Inertia movement
                Vector3 desiredVelocity = (targetPos - data.position) * followSmoothness;
                data.velocity = Vector3.Lerp(data.velocity, desiredVelocity, Time.deltaTime * inertia);

                // Apply movement
                data.position += data.velocity * Time.deltaTime;

                // Smooth rotation toward previous
                Vector3 direction = prev.position - data.position;
                if (direction.sqrMagnitude > 0.001f)
                {
                    Quaternion targetRot = Quaternion.LookRotation(direction);
                    data.rotation = Quaternion.Slerp(data.rotation, targetRot, Time.deltaTime * rotationSmoothness);
                }
            }

            //float wave = Mathf.Sin(Time.time * wobbleSpeed + i * waveOffset) * wobbleAmount;
            //segment.localRotation *= Quaternion.Euler(0f, wave, 0f);

            // Apply to segment
            segment.SetPositionAndRotation(data.position, data.rotation);

            //float wave = Mathf.Sin(Time.time * wobbleSpeed + i * waveOffset) * wobbleAmount;
            //segment.localRotation *= Quaternion.Euler(0f, wave, 0f);
        }
    }
}

public class NoodleSegmentData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 velocity;
}
