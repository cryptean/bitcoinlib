using System;
using System.Security.Cryptography.X509Certificates;

namespace BitcoinLib.Auxiliary
{
    public static class Troubleshooting
    {
        //  This is a temporary workaround for new installations where the SSL certificates are not yet properly installed, please do not use this in production enviroment! Using an invalid SSL certificate can put your application in a huge risk!
        public static Boolean IgnoreSslCertificateErrors(Object sender, X509Certificate certification, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
