using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GyroCameraController : MonoBehaviour {

	void Start () {
		// It is necessary to explicitly enable the gyro in Android.
		if (SystemInfo.supportsGyroscope) {
			Input.gyro.enabled = true;
		} else {
			Debug.Log("Gyro is not supported.");
		}
	}

	void Update () {
		Quaternion gyro = Input.gyro.attitude;
		this.transform.localRotation = Quaternion.Euler(90, 0, 0) * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));
	}
}
