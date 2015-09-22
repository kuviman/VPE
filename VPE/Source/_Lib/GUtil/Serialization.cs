using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;
using System.Linq;
using System.Collections;

namespace VitPro {

	public class SerializeAttribute : Attribute {}

    partial class GUtil {

        class DictionaryJsonConverter : JsonConverter {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
                var dictionary = (IDictionary)value;

                writer.WriteStartArray();

                foreach (var key in dictionary.Keys) {
                    writer.WriteStartObject();

                    writer.WritePropertyName("Key");

                    serializer.Serialize(writer, key);

                    writer.WritePropertyName("Value");

                    serializer.Serialize(writer, dictionary[key]);

                    writer.WriteEndObject();
                }

                writer.WriteEndArray();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
                if (!CanConvert(objectType))
                    throw new Exception(string.Format("This converter is not for {0}.", objectType));

                var keyType = objectType.GetGenericArguments()[0];
                var valueType = objectType.GetGenericArguments()[1];
                var dictionaryType = typeof(Dictionary<,>).MakeGenericType(keyType, valueType);
                var result = (IDictionary)Activator.CreateInstance(dictionaryType);

                if (reader.TokenType == JsonToken.Null)
                    return null;

                while (reader.Read()) {
                    if (reader.TokenType == JsonToken.EndArray) {
                        return result;
                    }

                    if (reader.TokenType == JsonToken.StartObject) {
                        AddObjectToDictionary(reader, result, serializer, keyType, valueType);
                    }
                }

                return result;
            }

            public override bool CanConvert(Type objectType) {
                return objectType.IsGenericType && (objectType.GetGenericTypeDefinition() == typeof(IDictionary<,>) || objectType.GetGenericTypeDefinition() == typeof(Dictionary<,>));
            }

            private void AddObjectToDictionary(JsonReader reader, IDictionary result, JsonSerializer serializer, Type keyType, Type valueType) {
                object key = null;
                object value = null;

                while (reader.Read()) {
                    if (reader.TokenType == JsonToken.EndObject && key != null) {
                        result.Add(key, value);
                        return;
                    }

                    var propertyName = reader.Value.ToString();
                    if (propertyName == "Key") {
                        reader.Read();
                        key = serializer.Deserialize(reader, keyType);
                    } else if (propertyName == "Value") {
                        reader.Read();
                        value = serializer.Deserialize(reader, valueType);
                    }
                }
            }
        }

		class SerializationContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver {
			protected override Newtonsoft.Json.Serialization.JsonProperty CreateProperty(
					MemberInfo member, MemberSerialization memberSerialization) {
				var prop = base.CreateProperty(member, memberSerialization);
				prop.Ignored = member.GetCustomAttribute(typeof(SerializeAttribute)) == null;
				prop.Readable = prop.Writable = true;
				return prop;
			}
		}

        static JsonSerializerSettings serializationSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            ObjectCreationHandling = ObjectCreationHandling.Replace,
			ContractResolver = new SerializationContractResolver(),
            Converters = { new DictionaryJsonConverter() },
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
        };

        public static string SerializeJSON(object o) {
            return JsonConvert.SerializeObject(o, serializationSettings);
        }

        public static T DeserializeJSON<T>(string json) {
//			Console.WriteLine(json);
            return JsonConvert.DeserializeObject<T>(json, serializationSettings);
        }

		public static byte[] Serialize(object o) {
			return Compress(GetBytes(SerializeJSON(o)));
        }

		public static T Deserialize<T>(byte[] bytes) {
            return DeserializeJSON<T>(GetString(Decompress(bytes)));
        }

        /// <summary>
        /// Serialize object into a file.
        /// </summary>
        public static void Dump(object o, string path) {
			File.WriteAllBytes(path, Serialize(o));
        }

        /// <summary>
        /// Deserialize object from a file.
        /// </summary>
        public static T Load<T>(string path) {
			return Deserialize<T>(File.ReadAllBytes(path));
        }

    }

}