using System;
using System.Text;
using System.Security.Cryptography;

namespace cryptinion
{
    public class AesManaged
    {
        /// <summary>
        /// Функция возвращает сгенерированый ключ для шифрования данных
        /// в случае не удачи возвращает null
        /// </summary>
        /// <param name="string cryptName">название класса щифрования(AES,DES и.д)</param>
        /// <param name="cryptName">Путь к файлу куда запишется сгенерирванный ключ</param>
        /// 
        public static byte[] GenerateKey(string outFileName,string cryptName)
        {
            try
            {
                SymmetricAlgorithm al = SymmetricAlgorithm.Create(cryptName);
                al.GenerateKey();
                using (System.IO.FileStream ss = new System.IO.FileStream(outFileName, System.IO.FileMode.Create))
                {
                    ss.Write(al.Key, 0, al.Key.Length);
                }
                return al.Key;
            }
            catch (Exception ee)
            {
                return null;
            }
        }
        /// <summary>
        /// Зашифровывает запись, в случае не удачи возращает null
        /// </summary>
        /// <param name="keyFromFilemane"></param>
        /// <param name="keyFromFilemane">Путь к файлу, где лежит ключ</param>
        /// <param name="string cryptName">название класса щифрования(AES,DES и.д)</param>
        /// <param name="Str">Строка, которую нужно зашифровать</param>
        /// <param name="Str"></param>
        /// <returns>Зашифрованная строка(byte[])</returns>
        public static byte[] EncryptStringUsingFileKey2(string keyFromFilemane, string cryptName, string Str)
        {
            try
            {
                byte[] ha = Encoding.UTF8.GetBytes(Str);

                SymmetricAlgorithm al2 = SymmetricAlgorithm.Create(cryptName);
                //------------------------------------------
                byte[] key;
                using (System.IO.FileStream ss = new System.IO.FileStream(keyFromFilemane, System.IO.FileMode.Open))
                {
                    key = new byte[ss.Length];
                    ss.Read(key, 0, (int)key.Length);
                }
                al2.Key = key;
                //-------------------------------------------
                System.IO.MemoryStream m = new System.IO.MemoryStream();

                al2.GenerateIV();
                m.Write(al2.IV, 0, al2.IV.Length);
                CryptoStream s = new CryptoStream(m, al2.CreateEncryptor(), CryptoStreamMode.Write);
                s.Write(ha, 0, ha.Length);
                s.FlushFinalBlock();

                return m.ToArray();
            }
            catch (Exception ee)
            {
                return null;
            }
        }
    }
}
