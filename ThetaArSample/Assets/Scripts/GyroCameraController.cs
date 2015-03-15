using UnityEngine;
using System.Runtime.InteropServices;

[RequireComponent(typeof(Camera))]
public class GyroCameraController : MonoBehaviour {

	void Start () {
		if (IsSupportsGyro()) {
			StartGyro();
		} else {
			UIController.Instance.MessageBoxText = "Gyroscope is not supported.";
		}
	}

	void Update () {
		this.transform.localRotation = GetGyroQuaternion();
	}

	void OnApplicationQuit() {
		if (IsSupportsGyro()) {
			StopGyro();
		}
	}

#if UNITY_IOS

	[DllImport ("__Internal")]
	private extern static bool IsSupportsGyro();

	[DllImport ("__Internal")]
	private extern static void StartGyro();

	[DllImport ("__Internal")]
	private extern static void StopGyro();

	private Quaternion GetGyroQuaternion() {
		float[] result = new float[4];
		GetGyroQuaternion(result);
		float x = result [0];
		float y = result [1];
		float z = result [2];
		float w = result [3];

		return Quaternion.Euler (90, 0, 0) * (new Quaternion (x, y, z, w)) *  Quaternion.Euler (0, 0, 90);
	}

	[DllImport ("__Internal")]
	private extern static void GetGyroQuaternion(float[] result);

#else

	private bool IsSupportsGyro() {
		return SystemInfo.supportsGyroscope;
	}

	private void StartGyro() {
		// It is necessary to explicitly enable the gyro in Android.
		Input.gyro.enabled = true;
	}

	private void StopGyro() {
		Input.gyro.enabled = false;
	}

	private Quaternion GetGyroQuaternion() {
		Quaternion gyro = Input.gyro.attitude;
		return Quaternion.Euler (90, 0, 0) * (new Quaternion (-gyro.x, -gyro.y, gyro.z, gyro.w));
	}

#endif
}
