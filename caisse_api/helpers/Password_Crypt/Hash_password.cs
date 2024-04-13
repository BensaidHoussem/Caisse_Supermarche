using System.Security.Cryptography;

public class Hash_password:IHash_password
    {
        private static RNGCryptoServiceProvider rng =new RNGCryptoServiceProvider();
        private static  int SaltSize = 20;
        private static int HashSise = 16;
        private static int iteration = 10000;


        public async Task<string> hashPassword(string password)
        {
            byte[] salt;
            rng.GetBytes(salt = new byte[SaltSize]);
            var key = new Rfc2898DeriveBytes(password, salt, iteration);
            var hash = key.GetBytes(HashSise);

            var hashByte = new byte[SaltSize + HashSise];
            Array.Copy(salt, 0, hashByte, 0, SaltSize);
            Array.Copy(hash, 0, hashByte, SaltSize, HashSise);

            var crypt = Convert.ToBase64String(hashByte);

            return Task.FromResult(crypt).Result;
        }

        public async Task<bool> VerifyPassword(string password, string crypt)
        {
            var hashbytes = Convert.FromBase64String(crypt);

            var salt = new byte[SaltSize];
            Array.Copy(hashbytes, 0, salt, 0, SaltSize);

            var key = new Rfc2898DeriveBytes(password, salt, iteration);
            byte[] hash = key.GetBytes(HashSise);

            for (var i = 0; i < HashSise; i++)
            {
                if (hashbytes[i + SaltSize] != hash[i])
                    return await Task.FromResult(false);

            }
            return await Task.FromResult(true);


        }

}