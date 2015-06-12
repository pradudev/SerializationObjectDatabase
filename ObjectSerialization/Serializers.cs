using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ObjectSerialization
{
    public class Serializers<T> where T : class
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public static void SaveObjectToDB(T obj)
        {
            try
            {
                var xmlData = GetXMLData(obj);
                var binData = GetBinaryData(obj);
                var strData = GetStringEncodedData(obj);

                using (var conn = new SqlConnection(connectionString))
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "Insert into SerializedData values(@xmldata,@varchardata,@varbinarydata)";

                        cmd.Parameters.Add("@xmldata", SqlDbType.Xml, Int32.MaxValue).Value = xmlData.InnerXml;
                        cmd.Parameters.Add("@varchardata", SqlDbType.VarChar, Int32.MaxValue).Value = strData;
                        cmd.Parameters.Add("@varbinarydata", SqlDbType.VarBinary, Int32.MaxValue).Value = binData;

                        cmd.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("Object serialized and stored successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Method to get XML serialized data
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static XmlDocument GetXMLData(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlDocument doc = new XmlDocument();
            using (var ms = new MemoryStream())
            {
                serializer.Serialize(ms, obj);
                ms.Position = 0;
                doc.Load(ms);
            }

            Console.WriteLine("XML data size: " + doc.InnerXml.Length);

            return doc;
        }

        /// <summary>
        /// Method to get Binanry serialized data
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static byte[] GetBinaryData(T obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            byte[] data = new byte[0];

            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                data = ms.ToArray();
            }

            Console.WriteLine("Binary data size: " + data.Length);

            return data;
        }

        /// <summary>
        /// Method to get Base64 encoded string data
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string GetStringEncodedData(T obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string data = string.Empty;

            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                data = Convert.ToBase64String(ms.ToArray()); //Do not use Encoding.UTF8.GetString() as your binary data is not encoded text data, and shouldn't be treated as such
            }

            Console.WriteLine("Base64 data size: " + data.Length);

            return data;
        }
    }
}