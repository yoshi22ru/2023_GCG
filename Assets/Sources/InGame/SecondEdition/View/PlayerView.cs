using System;
using R3;
using R3.Triggers;
using UnityEngine;

namespace Sources.InGame.SecondEdition.View
{
    public class PlayerView: MonoBehaviour
    {
        public Transform ModelTransform => modelTransform;
        public Rigidbody ModelRigidbody => modelRigidbody;
        public Observable<Collider> TriggerEnter => _triggerEnter;
        
        [SerializeField] private Transform modelTransform;
        [SerializeField] private Rigidbody modelRigidbody;
        private Observable<Collider> _triggerEnter; 

        private void Awake()
        {
            _triggerEnter = this.OnTriggerEnterAsObservable();
        }
    }
}