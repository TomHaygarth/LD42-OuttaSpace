using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OuttaSpace.Camera {
	public class PlayerTracker : MonoBehaviour {

		public Transform focalTransform;
		public List<Transform> players;

		private const float zDepth = -10.0f;
		
		// Update is called once per frame
		void Update () {
			var length = players.Count;
			var focalPoint = Vector3.zero;
			var activePlayerCount = 0;

			for(int i = 0; i < length; ++i) {
				if(players[i].gameObject.activeSelf) {
					focalPoint += players[i].position;
					++activePlayerCount;
				}
			}

			if(activePlayerCount > 0) {
				focalPoint.x /= activePlayerCount;
				focalPoint.y /= activePlayerCount;
			}

			focalPoint.z = zDepth;

			focalTransform.position = focalPoint;
		}
	}
}
