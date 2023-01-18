using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zamba.PDFSigner;
namespace ZambaTest.Zamba.PDFSinger.Test
{
    [TestClass]
    public class PDFSignTest
    {

        [TestMethod]
        public void SeDebeDevolverMensajeDeFullPathVacioSiElParametroEsVacio()
        {
     
                var sPdf = new PDFSign();
                var fullPath = @"C:\WSASS_como_adherirse.pdf";
                var fileName = "WSASS_como_adherirse.pdf";
                var author = "pepe";
                var certificate = @"MIIHsjCCBZqgAwIBAgIKJFX36gAAAAQg1jANBgkqhkiG9w0BAQUFADCBmDELMAkG
                                    A1UEBhMCQVIxEDAOBgNVBAgMB0NPUkRPQkExFDASBgNVBAoMC0VOQ09ERSBTLkEu
                                    MRkwFwYDVQQFExBDVUlUIDMwNzExMTAzNTM0MUYwRAYDVQQDDD1BdXRvcmlkYWQg
                                    Q2VydGlmaWNhbnRlIEVOQ09ERVNJTiBQZXJzb25hcyBGaXNpY2FzIHkgSnVyaWRp
                                    Y2FzMB4XDTE4MDMwNjE0NTMwOFoXDTIwMDMwNjE1MDMwOFowVDERMA8GA1UEAxMI
                                    TU9ET0MgU0ExGTAXBgNVBAUTEENVSVQgMzA3MTQ0MzkzMDQxFzAVBgNVBAsTDkFE
                                    TUlOSVNUUkFDSU9OMQswCQYDVQQGEwJBUjCCASIwDQYJKoZIhvcNAQEBBQADggEP
                                    ADCCAQoCggEBANd63I5toDKh + 9u1FHQZBDTOYLsGAEx8a64rHk3X5Vs2Z72M2IHb
                                    4hcBXsGv5sx / taOTZP02H40YbPDfp9itednWjka2iFMnNdnx8Lji7S5pBSsFku0k
                                    Ll52WWH + 8OCxFjj / Ok8R3oYFzlO49evGHWRXbVTTyZ48JvYgLFGLPjQdG0ZEhKrx
                                    juTWh / n9jynuw4kCexJY / mzy / +1SqTIL5JMWmrvZeB2XlheX / q / oUYiIznd88Mun
                                    C4e2DR2eYJQAslM73NTHFz9ZioX3pQjv6NfefoCzr8yBSXqNBPS8qBzxrlzAEiQR
                                    Y2j2pi + cKabDBT7n53WBX7TAHsWx9ss5bXsCAwEAAaOCAz8wggM7MA8GA1UdDwEB
                                    / wQFAwMH2YAwDAYDVR0TAQH / BAIwADAVBgNVHSUEDjAMBgorBgEEAYI3CgMMMB0G
                                    A1UdDgQWBBQJMpW8sLbXUfFCUGjq6aAmc + z / 8jAfBgNVHSMEGDAWgBRlN / NvK0sH
                                    VdqIvlleVanVfPLWaTCByQYDVR0fBIHBMIG + MIG7oIG4oIG1hjpodHRwOi8vYWMy
                                    LmVuY29kZXNhLmNvbS5hci9maXJtYS1kaWdpdGFsL2NybC9lbmNvZGVzaW4uY3Js
                                    hjpodHRwOi8vd3d3LmVuY29kZWFjLmNvbS5hci9maXJtYS1kaWdpdGFsL2NybC9l
                                    bmNvZGVzaW4uY3JshjtodHRwOi8vYWNmZC5lbmNvZGVzYS5jb20uYXIvZmlybWEt
                                    ZGlnaXRhbC9jcmwvZW5jb2Rlc2luLmNybDCB6QYIKwYBBQUHAQEEgdwwgdkwRgYI
                                    KwYBBQUHMAKGOmh0dHA6Ly9hYzIuZW5jb2Rlc2EuY29tLmFyL2Zpcm1hLWRpZ2l0
                                    YWwvY2VyL2VuY29kZXNpbi5jcnQwRgYIKwYBBQUHMAKGOmh0dHA6Ly93d3cuZW5j
                                    b2RlYWMuY29tLmFyL2Zpcm1hLWRpZ2l0YWwvY2VyL2VuY29kZXNpbi5jcnQwRwYI
                                    KwYBBQUHMAKGO2h0dHA6Ly9hY2ZkLmVuY29kZXNhLmNvbS5hci9maXJtYS1kaWdp
                                    dGFsL2Nlci9lbmNvZGVzaW4uY3J0MIG0BgNVHSAEgawwgakwgaYGBWAgAQEEMIGc
                                    MFYGCCsGAQUFBwICMEoeSABQAG8AbABpAHQAaQBjAGEAcwAgAGQAZQAgAGMAZQBy
                                    AHQAaQBmAGkAYwBhAGMAaQBvAG4AIABFAE4AQwBPAEQARQBTAEkATjBCBggrBgEF
                                    BQcCARY2aHR0cDovL3d3dy5lbmNvZGVhYy5jb20uYXIvZmlybWEtZGlnaXRhbC9F
                                    TkNPREVTSU4ucGRmMFQGA1UdEQRNMEukSTBHMRIwEAYDVQQMEwlBUE9ERVJBRE8x
                                    GTAXBgNVBAUTEENVSUwgMjAyNTY3MDI2MTUxFjAUBgNVBAMTDU1BUklBTk8gVEFT
                                    SU4wDQYJKoZIhvcNAQEFBQADggIBAKe5ejsC6ufWyKl5kHN3sHX3h1I28K8lNM5e
                                    uS / uuceycOUSvioguT7RkDYUuiB39J5HgzZS + 4PojOBM7t3DTsRW5ivxg / ke8g0h
                                    cB7Rs59IABI1zCMcrNnQkDzFpdP92mYAKpsZb5oDIAYTU0bwKLv + j57mXNSYet5P
                                    AORN1cndg4wFrNsFDBJnBw17CXxidDruhTOY + fGd4hk9PnReyNkmjNRyBLtrYed1
                                    5QgsC7f3grFxiO76d8jGNbrfyf7AIjxtTynqdfaCl1uz5n6RbX5iDm3tshocyk2G
                                    2nwGPnfeq + 46PYXiAbx + qK87OKHP0zqVxyqpIdRBfEdgzVfuc2r093GCHrd2fs / c
                                    QrbPN4XSDs09BiVsbiyRj4XFDI0huFVQHOMHKWX7v3EJr + 8rt5tLg5rXESqk3obF
                                    mKvug2USVuXuG2iDcPYX25ea / Dwkv6I53gRQ5b9g8Ag + 8a + eGpK21jVUmhzTEMRb
                                    tmDsOfch5ipx + 6AIIeQjzkiJXLGuMIJhqpLAj + QdiE5JEn2qgo29NfD + RwqGrExK
                                    m74 + OlO2q6Yj7PB3oKqrx14cqubCxlPlYO / IiU7OjjaHJT8srWofaOaWtMoN5T6c
                                    wzjGJIf5c54qoyZ / kNCvKCNwSrFRNPCNHClYrgFXOAGOyn6lTlooio4OlRfaxj2e
                                    CnI5uf4Y";
                var writePDF = true;
                var title = "titulo";
                var subject = "subject";
                var keywords = "keywords";
                var creator = "creator";
                var producer = "producer";
                var password = "password";
                var reason = "reason";
                var contact = "contact";
                var location = "location";
            try
            {
               sPdf.NewSign(fullPath, fileName, author, title, subject, keywords, creator, producer, certificate, password, reason, contact, location, writePDF);

            }
            catch (Exception e)
            {
                Assert.AreEqual("La ruta del documento es vacia.", e.Message);
            }
        }

    }
}
