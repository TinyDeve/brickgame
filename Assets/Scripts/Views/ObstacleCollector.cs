using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using SG;
using UnityEngine;

namespace net.onur.brick.views.obstaclecollector
{
    public class ObstacleCollector : View {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Obstacle")) return;
            ResourceManager.Instance.ReturnObjectToPool(other.gameObject);
        }
    }
}

