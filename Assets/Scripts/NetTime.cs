using System;
using System.Net;
using System.Net.Sockets;

public static class NetTime
{
    private const string ntpServer1 = "pool.ntp.org";
    private const string ntpServer2 = "ntp1.stratum2.ru";

    private static DateTime dateTime1;

    public static DateTime GetNetworkTime()
    {
        try
        {
            dateTime1 = GetNetworkTime2(ntpServer1);
        }
        catch
        {
            dateTime1 = GetNetworkTime2(ntpServer2);
        }

        return dateTime1;
    }

    private static DateTime GetNetworkTime2(string ntpServer)
    {
        var ntpData = new byte[48];

        ntpData[0] = 0x1B;

        var addresses = Dns.GetHostEntry(ntpServer).AddressList;
        var ipEndPoint = new IPEndPoint(addresses[0], 123);
        using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
        {
            socket.Connect(ipEndPoint);

            socket.ReceiveTimeout = 3000;

            socket.Send(ntpData);
            socket.Receive(ntpData);
        }

        const byte serverReplyTime = 40;

        ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);

        ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

        intPart = SwapEndianness(intPart);
        fractPart = SwapEndianness(fractPart);

        var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

        var networkDateTime = new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds((long)milliseconds);

        return networkDateTime.ToLocalTime();
    }

    private static uint SwapEndianness(ulong x)
    {
        return (uint)(((x & 0x000000ff) << 24) +
                       ((x & 0x0000ff00) << 8) +
                       ((x & 0x00ff0000) >> 8) +
                       ((x & 0xff000000) >> 24));
    }
}
