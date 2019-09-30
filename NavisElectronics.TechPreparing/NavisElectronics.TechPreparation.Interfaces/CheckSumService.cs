namespace NavisElectronics.TechPreparation.Interfaces
{
    using System;
    using System.Security.Authentication;
    using System.Security.Cryptography;
    using System.Text;

    public class CheckSumService
    {
        public string ComputeHash(byte[] bytes, HashAlgorithmType type)
        {
            StringBuilder sBuilder = new StringBuilder();
            switch (type)
            {
                case HashAlgorithmType.Md5:

                    using (MD5 md5Hash = MD5.Create())
                    {
                        byte[] data = md5Hash.ComputeHash(bytes);

                        for (int i = 0; i < data.Length; i++)
                        {
                            sBuilder.Append(data[i].ToString("x2"));
                        }
                    }

                    break;

                case HashAlgorithmType.Sha1:
                    SHA1 sha = new SHA1CryptoServiceProvider();
                    byte[] result = sha.ComputeHash(bytes);
                    for (int i = 0; i < result.Length; i++)
                    {
                        sBuilder.Append(result[i].ToString("x2"));
                    }

                    break;
                default:
                    throw new NotImplementedException("Получение контрольной суммы по выбранному алгоритму еще не реализовано");
            }

            return sBuilder.ToString();
        }

    }
}