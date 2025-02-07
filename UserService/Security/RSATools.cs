﻿using System.Security.Cryptography;

namespace UserService.Security
{
    public static class RSATools
    {
        public static RSA GetPrivateKey()
        {
            var f = File.ReadAllText("RSA/private_key.pem");
            var rsa = RSA.Create();
            rsa.ImportFromPem(f);
            return rsa;
        }

        public static RSA GetPublicKey()
        {
            var f = File.ReadAllText("RSA/public_key.pem");
            var rsa = RSA.Create();
            rsa.ImportFromPem(f);
            return rsa;
        }
    }
}