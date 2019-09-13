using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Listens for touch events and performs an AR raycast from the screen touch point.
/// AR raycasts will only hit detected trackables like feature points and planes.
///
/// If a raycast hits a trackable, the <see cref="placedPrefab"/> is instantiated
/// and moved to the hit position.
/// </summary>

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedPrefab;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }

    void Awake()
    {
        aRPlaneManager = GetComponent<ARPlaneManager>();
        m_RaycastManager = GetComponent<ARRaycastManager>();
        aRPointCloudManager = GetComponent<ARPointCloudManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;
            touchPosition = new Vector2(mousePosition.x, mousePosition.y);
            return true;
        }
#else
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
#endif

        touchPosition = default;
        return false;
    }

    ARPlaneManager aRPlaneManager;
    ARPointCloudManager aRPointCloudManager;

    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        //if (spawnedObject != null)
        //{
        //    aRPlaneManager.enabled = false;
        //    aRPointCloudManager.enabled = false;
        //    foreach (ARPlane plane in aRPlaneManager.trackables)
        //    {
        //        Destroy(plane.gameObject);
        //    }
        //}
        else if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = s_Hits[0].pose;

            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                aRPlaneManager.enabled = false;
                aRPointCloudManager.enabled = false;
                foreach (ARPlane plane in aRPlaneManager.trackables)
                {
                    Destroy(plane.gameObject);
                }
                foreach (ARPointCloud cloud in aRPointCloudManager.trackables)
                {
                    Destroy(cloud.gameObject);
                }
            }
            //else
            //{
            //    spawnedObject.transform.position = hitPose.position;
            //}
        }
    }
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    ARRaycastManager m_RaycastManager;
}