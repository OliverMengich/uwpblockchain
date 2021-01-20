using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace BL
{
    public class Block
    {
        private readonly DateTime dateTime;
        private long nonce;
        public string PreviousHash { get; set; }
        public List<Transaction> Transactions { get; set; }
        public string Hash { get; set; }
        public Block(DateTime timestamp, List<Transaction> transactions, string previousHash="")
        {
            dateTime = timestamp;
            nonce = 0;
            Transactions = transactions;
            PreviousHash = previousHash;
            Hash = CreateHash();
        }
        public string CreateHash()
        {

            string rawData = dateTime+ PreviousHash + Transactions  + nonce;
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
            var _privatekey = csp.ExportParameters(true);
            byte[] bytes = Encoding.UTF8.GetBytes(rawData);
            var signed = HashAndSign(bytes, _privatekey);
            return Encoding.UTF8.GetString(signed);
        }
        public byte[] HashAndSign(byte[] data, RSAParameters _privateKey)
        {
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
            csp.ImportParameters(_privateKey);
            var returnedBytessigned = csp.SignData(data, SHA256.Create());

            return returnedBytessigned;
        }
        public void MineBlock(int _difficulty)
        {
            string hashValidationTemplate = new String('0', _difficulty);
            while(Hash.Substring(0,_difficulty) != hashValidationTemplate)
            {
                nonce++;
                Hash = CreateHash();
            }
        }
    }
    
}
