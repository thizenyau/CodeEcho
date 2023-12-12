/*
* UniOSC
* Copyright © 2014-2015 Stefan Schlupek
* All rights reserved
* info@monoflow.org
*/
using UnityEngine;
using OSCsharp.Data;
using System;
using System.Reflection;
using System.Numerics;

namespace UniOSC{


    [AddComponentMenu("UniOSC/TransformSender")]
	public class UniOSCTransformSender : UniOSCEventDispatcher {

		public GameObject head;
        public GameObject leftHand;
        public GameObject rightHand;

		private UnityEngine.Vector3 _headPosition = UnityEngine.Vector3.zero;
        private UnityEngine.Vector3 _leftHandPosition = UnityEngine.Vector3.zero;
        private UnityEngine.Vector3 _rightHandPosition = UnityEngine.Vector3.zero;
        private float _headNote = 60;
        private float _leftHandNote = 57;
        private float _rightHandNote = 64;
        private float _headNote1 = 60;
        private float _leftHandNote1 = 57;
        private float _rightHandNote1 = 64;
        private float _leftHandNote2 = 57;
        private float _rightHandNote2 = 64;
        private float _leftDistance = 0f;
        private float _rightDistance = 0f;
		private float allDistance = 0.7909f;
        private int timer = 0;
        private UnityEngine.Vector3 _currentPosition = UnityEngine.Vector3.zero;


	
		public override void OnEnable ()
		{
			
			//Here we setup our OSC message
			base.OnEnable ();
            ClearData();
			//now we could add data;
			AppendData(0f);//Translation.x
			AppendData(0f);//Translation.y
			AppendData(0f);//Translation.z

			AppendData(0f);//Rotation.x
			AppendData(0f);//Rotation.y
			AppendData(0f);//Rotation.z

			StartSendIntervalTimer();

		}

		public override void OnDisable ()
		{
			base.OnDisable ();
			StopSendIntervalTimer();
		}



		void FixedUpdate(){
			_Update();
		}
		protected override void _Update ()
		{

			base._Update ();

			if(head == null) return;

			_headPosition =head.transform.position;
            _leftHandPosition = leftHand.transform.position;
            _rightHandPosition = rightHand.transform.position;

            _leftDistance = UnityEngine.Vector3.Distance(_headPosition, _leftHandPosition);
            _rightDistance = UnityEngine.Vector3.Distance(_headPosition, _rightHandPosition);

            _leftHandNote1 = _leftHandNote;
            _rightHandNote1 = _rightHandNote;

            _leftHandNote = -(int)Math.Round((_leftDistance / allDistance) * 9) + 60;
            _rightHandNote = (int)Math.Round((_rightDistance / allDistance) * 9) + 60;






			

			OscMessage msg = null;
			if(_OSCeArg.Packet is OscMessage){
				msg = ((OscMessage)_OSCeArg.Packet);
			}else if(_OSCeArg.Packet is OscBundle){
				//bundle version
				msg = ((OscBundle)_OSCeArg.Packet).Messages[0];
			}
			if(msg != null)
			{
				msg.UpdateDataAt(0, 144);
				msg.UpdateDataAt(1, _headNote);
				msg.UpdateDataAt(2, 90);
				
				msg.UpdateDataAt(3, 0f);
				msg.UpdateDataAt(4, 0f);
				msg.UpdateDataAt(5, 0f);
			}
			

			//only send OSC messages at our specified interval
			lock(_mylock){
				if(!_isOSCDirty)return;
				_isOSCDirty = false;
			}

            if((_leftHandNote == _leftHandNote1) && (_rightHandNote == _rightHandNote1))
                timer++;
            else
                timer = 0;
                
            if(timer > 20)
            {

                


                msg.UpdateDataAt(0,128f);
                msg.UpdateDataAt(1,_leftHandNote2);
                msg.UpdateDataAt(2,0f);
                _SendOSCMessage(_OSCeArg);

                msg.UpdateDataAt(0, 128f);
				msg.UpdateDataAt(1, _rightHandNote2);
				msg.UpdateDataAt(2, 0f);
                _SendOSCMessage(_OSCeArg);

                msg.UpdateDataAt(0, 128f);
				msg.UpdateDataAt(1, _headNote);
				msg.UpdateDataAt(2, 0f);
                _SendOSCMessage(_OSCeArg);

                msg.UpdateDataAt(0, 144f);
				msg.UpdateDataAt(1, _leftHandNote);
				msg.UpdateDataAt(2, 90f);
                _SendOSCMessage(_OSCeArg);

                msg.UpdateDataAt(0, 144f);
				msg.UpdateDataAt(1, _rightHandNote);
				msg.UpdateDataAt(2, 90f);
                _SendOSCMessage(_OSCeArg);
                
                msg.UpdateDataAt(0, 144f);
				msg.UpdateDataAt(1, _headNote);
				msg.UpdateDataAt(2, 90f);
                _SendOSCMessage(_OSCeArg);
                
                
                _leftHandNote2 = _leftHandNote;
                _rightHandNote2 = _rightHandNote;
                timer = 0;
            }




			





		}


	}

}