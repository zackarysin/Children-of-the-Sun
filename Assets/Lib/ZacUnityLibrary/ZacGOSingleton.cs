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

	public abstract class ZacGOSingleton<T> : ZacGObj where T : UnityEngine.Object{

		/*
		 * 	Note:	
		 * 		1	It has been found that IOS cannot differentiate nested generic class. e.g. class A:B<T> class C:B<T>  class B<T>:ZacSingleton<B<T>> . IOS will consider A and C as the same
		 * 		2	It is suggested to establishSingleton at Awake()
		 */

		private static T inst = null; // If use public Instance, class should declare itself

		protected static T instance{
			get{
				return inst;
			}
		}

		#region Configuration Methods

		protected bool establishZacSingleton(T _singleton, bool _isForce = false){

			/*
			 * 	return:
			 *  	true: establish sucess,  false: establish fail
			 */

			if(instance != null){
                UnityEngine.Debug.Log("Critical Error " + _singleton.name + " asked for singleton establishment dispite instance is not null!");
				return false;
			}

			inst = _singleton;

//			if(isAutoSet){
//				ReportSet();
//			}

			return true;
		}

		#endregion

	}

}

