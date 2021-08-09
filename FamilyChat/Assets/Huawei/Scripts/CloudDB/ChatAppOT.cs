using HuaweiMobileServices.CloudDB;
using HuaweiMobileServices.Utils;
using UnityEngine;

using System;

namespace HmsPlugin
{
	public class ChatAppOT : JavaObjectWrapper, ICloudDBZoneObject
	{
		public ChatAppOT() : base("com.clouddbdemo.kb.huawei.ChatAppOT") { }
		public ChatAppOT(AndroidJavaObject javaObject) : base(javaObject) { }
		private string id;
		private string userId;
		private string message;
		private DateTime date;
		private bool shadowFlag;

		public string Id
		{
			get { return Call<string>("getId"); }
			set { Call("setId", value); }
		}

		public string UserId
		{
			get { return Call<string>("getUserId"); }
			set { Call("setUserId", value); }
		}

		public string Message
		{
			get { return Call<string>("getMessage"); }
			set { Call("setMessage", value); }
		}

		public DateTime Date
		{
			get { return new DateTime(Call<AndroidJavaObject>("getDate").Call<long>("getTime")); }
			set { Call("setDate", new AndroidJavaObject("java.util.Date", value.Ticks)); }
		}

		public bool ShadowFlag
		{
			get { return Call<bool>("getShadowFlag"); }
			set { Call("setShadowFlag", value); }
		}

		public AndroidJavaObject GetObj() => base.JavaObject;
		public void SetObj(AndroidJavaObject arg0) => base.JavaObject = arg0;
	}
}
