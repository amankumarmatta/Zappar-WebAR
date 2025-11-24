using UnityEngine;
using Zappar;

public class TapToPlace : MonoBehaviour
{
    public ZapparInstantTrackingTarget tracker;
    private bool placed = false;

    void Update()
    {
        if (Input.touchCount > 0 && !placed)
        {
            tracker.PlaceTrackerAnchor();
            placed = true;
        }
    }
}
