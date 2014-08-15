﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AxosoftAPI.NET.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AxosoftAPI.NET.Helpers
{
	public class StatusConverter : JsonConverter
	{
		public override bool CanWrite
		{
			get
			{
				// This forces Json.NET to use the default Json serializer
				return false;
			}
		}

		public override bool CanConvert(Type objectType)
		{
			return (objectType == typeof(Status) ||
					objectType == typeof(int));
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			// Load JObject from stream
			var token = JToken.Load(reader);

			// If of type ReleaseType, then return object
			if (token.Type == JTokenType.Object)
			{
				return token.ToObject<Status>();
			}
			// If of type integer then create new ReleaseType and assign to Id
			else if (token.Type == JTokenType.Integer)
			{
				return new Status
				{
					Id = token.ToObject<int>(),
				};
			}

			return null;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			// No implementation needed since CanWrite is hardcoded to return 'false'
			throw new NotImplementedException();
		}
	}
}