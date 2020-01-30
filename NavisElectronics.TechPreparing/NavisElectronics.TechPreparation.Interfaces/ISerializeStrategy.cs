using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace NavisElectronics.TechPreparation.Interfaces
{
    public interface ISerializeStrategy<T>
    {
        /// <summary>
        /// The serialize.
        /// </summary>
        /// <param name="parameterToSerialize">
        /// The parameter to serialize.
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        byte[] Serialize(T parameterToSerialize);
    }

    public class SerializeStrategyDefault<T>: ISerializeStrategy<T>
    {
        public byte[] Serialize(T parameterToSerialize)
        {
            // сериализуем
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            byte[] bytes;
            using (MemoryStream ms = new MemoryStream())
            {
                binaryFormatter.Serialize(ms, parameterToSerialize);
                bytes = ms.ToArray();
            }

            return bytes;
        }
    }

    public class SerializeStrategyBson<T> : ISerializeStrategy<T>
    {
        public byte[] Serialize(T parameterToSerialize)
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            jsonSerializer.NullValueHandling = NullValueHandling.Ignore;
            byte[] bytes;
            using (MemoryStream ms = new MemoryStream())
            {
                using (JsonWriter jsonWriter = new BsonWriter(ms))
                {
                    jsonSerializer.Serialize(jsonWriter, parameterToSerialize);
                }

                bytes = ms.ToArray();
            }

            return bytes;
        }
    }

}