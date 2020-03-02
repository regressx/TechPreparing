using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace NavisElectronics.TechPreparation.Interfaces
{
    /// <summary>
    /// The DeserializeStrategy interface.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public interface IDeserializeStrategy<T>
    {
        T Deserialize(byte[] bytes);
    }

    public class DeserializeStrategyBinDefault<T> : IDeserializeStrategy<T>
    {
        public T Deserialize(byte[] bytes)
        {
            BinaryFormatter binFormatter = new BinaryFormatter();
            T deserializedObject;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                deserializedObject = (T)binFormatter.Deserialize(ms);
            }

            return deserializedObject;
        }
    }

    public class DeserializeStrategyBson<T> : IDeserializeStrategy<T>
    {
        public T Deserialize(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                // десериализуем
                JsonSerializer jsonSerializer = new JsonSerializer();
                T deserializedObject;
                using (JsonReader jsonReader = new BsonReader(ms))
                {
                    deserializedObject = (T)jsonSerializer.Deserialize(jsonReader, typeof(T));
                }

                return deserializedObject;
            }
        }
    }
}