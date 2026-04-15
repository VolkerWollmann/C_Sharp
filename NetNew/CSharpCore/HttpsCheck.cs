using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpNew
{
    public class HttpsCheck
    {
        public static void CheckHttps()
        {
            X509Certificate2 certFromServer = null;

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    (request, cert, chain, errors) =>
                    {
                        Console.WriteLine("=== Certificate Chain ===");

                        foreach (var element in chain.ChainElements)
                        {
                            var c = element.Certificate;
                            Console.WriteLine("------------------------");
                            Console.WriteLine("Subject: " + c.Subject);
                            Console.WriteLine("Issuer : " + c.Issuer);
                            Console.WriteLine("Expires: " + c.GetExpirationDateString());
                        }

                        certFromServer = new X509Certificate2(cert);
                        return true; // allow request (IMPORTANT: don't do this in production)
                    }
            };

            using (var client = new HttpClient(handler))
            {
                client.GetAsync("https://www.google.com").Wait(); // make request to trigger certificate validation
            }

            Console.WriteLine("Issuer: " + certFromServer?.Issuer);
            Console.WriteLine("Subject: " + certFromServer?.Subject);
            string expireDate = certFromServer?.GetExpirationDateString();
            Console.WriteLine("Expires: " + expireDate);
        }
    }
}
