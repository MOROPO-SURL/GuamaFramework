using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace MOROPO.Framework.Guama.Core.Persistence.Helpers
{
    public static class DataIntegrityHelper
    {
        private static readonly Guid SignatureProcessGuid = new Guid("9e119871-4956-4fcc-925a-dc5d641e71a9");
        private static readonly byte[] OBFUSTCATED_SECRET_KEY = { 115, 224, 50, 55, 246, 145, 123, 99, 33, 133, 10, 245, 45, 110, 160, 160, 155, 10, 98, 11, 5, 33, 67, 89, 26, 127, 44, 10, 31, 18, 77, 161, 79, 174, 52, 182, 16, 121, 105, 7, 250, 32, 160, 231, 35, 185, 170, 222 };

        public static string GetEntitySignature(Entities.Contract.IPersistenceSignable entity)
        {
            var signableEntityModel = entity.GetSignableEntityModel();

            var dataSerialization = JsonConvert.SerializeObject(signableEntityModel);

            var encoding = new ASCIIEncoding();

            var clientIDDigest = SignatureProcessGuid.ToByteArray();

            var hmacSha384 = new HMACSHA384(new byte[] { 114, 9, 91 })
            {
                ProduceLegacyHmacValues = false
            };

            var ofuscatorDigest = hmacSha384.ComputeHash(clientIDDigest);

            var secretKey = new byte[48];

            for (int i = 0; i < 48; i++)
            {
                secretKey[i] = (byte)(OBFUSTCATED_SECRET_KEY[i] ^ ofuscatorDigest[i]);
            }

            var firstHmacSha512 = new HMACSHA512(secretKey)
            {
                ProduceLegacyHmacValues = false
            };

            var partialDigest = firstHmacSha512.ComputeHash(encoding.GetBytes(dataSerialization));

            var secoudHmacSha512 = new HMACSHA512(clientIDDigest)
            {
                ProduceLegacyHmacValues = false
            };

            var finalDigest = secoudHmacSha512.ComputeHash(partialDigest);

            var rawResult = BitConverter.ToString(finalDigest);

            return rawResult.Replace("-", string.Empty).ToLowerInvariant();
        }
    }
}
