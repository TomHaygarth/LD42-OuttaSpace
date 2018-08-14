using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OuttaSpace.Camera {
	public class PlayerTracker : MonoBehaviour {

		public Transform focalTransform;
		public Transform[] players;

		private const float zDepth =-10.0f;
		
		// Update is called once per frame
		void Update () {
			var length = players.Length;
			var focalPoint = Vector3.zero;

			for(int i = 0; i < length; ++i) {
				focalPoint += players[i].position;
			}

			focalPoint.z = zDepth;

			focalTransform.position = focalPoint / length;
		}
	}
}
