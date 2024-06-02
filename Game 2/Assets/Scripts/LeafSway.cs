using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafSway : MonoBehaviour
{
    [SerializeField] private float swayStrength = 0.1f;  // Controls the amplitude of the sway
    [SerializeField] private float swaySpeed = 1.0f;     // Controls the frequency of the sway

    private List<Transform> leaves = new List<Transform>();
    private Vector3[] initialPositions;

    void Start()
    {
        // Find all the leaves under this GameObject
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "Leaf") // Assuming each leaf is tagged as "Leaf"
            {
                leaves.Add(child);
            }
        }

        // Store initial positions of all leaves
        initialPositions = new Vector3[leaves.Count];
        for (int i = 0; i < leaves.Count; i++)
        {
            initialPositions[i] = leaves[i].localPosition;
        }
    }

    void Update()
    {
        // Apply wind effect to each leaf
        for (int i = 0; i < leaves.Count; i++)
        {
            // Calculate a sway offset using only the X-axis for horizontal sway
            float swayOffset = Mathf.Sin(Time.time * swaySpeed + i) * swayStrength;

            // Apply the sway effect to the local position along the X-axis
            Vector3 newPosition = initialPositions[i];
            newPosition.x += swayOffset;
            leaves[i].localPosition = newPosition;
        }
    }
}