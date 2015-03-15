using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PinchZoomController : MonoBehaviour {
	public float zoomSpeed = 0.5f;

	public float minFov = 30;

	public float maxFov = 100;

	void Update () {
		if (Input.touchCount == 2 && Input.GetTouch (0).phase == TouchPhase.Moved && Input.GetTouch (1).phase == TouchPhase.Moved) {
			Touch touch0 = Input.GetTouch(0);
			Touch touch1 = Input.GetTouch(1);

			Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
			Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

			float prevTouchDeltaMagnitude = (touch0PrevPos - touch1PrevPos).magnitude;
			float touchDeltaMagnitude = (touch0.position - touch1.position).magnitude;

			float deltaMagnitudeDiff = prevTouchDeltaMagnitude - touchDeltaMagnitude;

			Camera camera = GetComponent<Camera>();
			camera.fieldOfView += deltaMagnitudeDiff * zoomSpeed;
			camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, minFov, maxFov);
		}
	}
}
