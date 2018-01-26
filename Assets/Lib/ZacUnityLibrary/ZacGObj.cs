/*

The MIT License (MIT)

Copyright (c) 2015 Zackary, Sin Ping Tat

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/

using UnityEngine;
using System.Collections;

namespace Zac{

	public class ZacGObj : MonoBehaviour {

		// === Description Variables === //

		[SerializeField]protected bool isAutoSet = true;

		// === State Variables === //

		protected bool isSet = false;
	
		#region Phases


        /// <summary>
        /// A ChildUpdate simply provides an framework for "parents" to call "childs" in a mediator pattern architecture to pass Update call in a orderly manner.
        /// </summary>
        public virtual void ChildUpdate()
        {

        }

        /// <summary>
        /// The ChildUpdate that should not be called when the game is paused by the player of the game
        /// </summary>
        public virtual void GameTimeUpdate()
        {

        }
		
//		protected IEnumerator GenericCoroutine(){
//		
//		
//		}

		#endregion
		#region Configurations

		protected virtual void set(){

		}

		public void ReportSet(){
			if(isSet){
				return;
			}
			else{
				isSet = true;
			}
			set ();
		}

		public virtual void Reset(){
			isSet = false;
			ReportSet ();
		}

		protected virtual void Awake(){
			if (isAutoSet) {
				ReportSet();
			}
		}

		protected virtual void Start(){

		}

		#endregion


	}

}