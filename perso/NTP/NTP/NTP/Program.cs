using System;
using System.Net;
using System.Net.Sockets;

string ntpServer = "time.windows.com";

byte[] ntpData = new byte[48];
ntpData[0] = 0x1B; //LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)
IPEndPoint ntpEndpoint = new IPEndPoint(Dns.GetHostAddresses(ntpServer)[0], 123);

UdpClient ntpClient = new UdpClient();
ntpClient.Connect(ntpEndpoint);

ntpClient.Send(ntpData, ntpData.Length);

ntpData = ntpClient.Receive(ref ntpEndpoint);

DateTime ntpTime = NtpPacket.ToDateTime(ntpData);

//Console.WriteLine("Heure actuelle : " + ntpTime.ToString());
Console.WriteLine("Heure actuelle (format personnalisé) : " + ntpTime.ToString("dddd, dd MMMM yyyy"));
Console.WriteLine("Heure actuelle (format personnalisé) : " + ntpTime.ToString("dd.MM.yyyy HH:mm:ss"));
Console.WriteLine("Heure actuelle (format personnalisé) : " + ntpTime.ToString("dd.MM.yyyy"));

ntpClient.Close();

public class NtpPacket
{
    public static DateTime ToDateTime(byte[] ntpData)
    {
        ulong intPart = (ulong)ntpData[40] << 24 | (ulong)ntpData[41] << 16 | (ulong)ntpData[42] << 8 | (ulong)ntpData[43];
        ulong fractPart = (ulong)ntpData[44] << 24 | (ulong)ntpData[45] << 16 | (ulong)ntpData[46] << 8 | (ulong)ntpData[47];

        var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
        var networkDateTime = (new DateTime(1900, 1, 1)).AddMilliseconds((long)milliseconds);

        return networkDateTime;
    }
}