using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace RetroGames.Helpers
{
    public class Utils
    {
        public static string GenerateToken
        {
            get
            {
                var random = new Random();
                var token = string.Empty;

                for (var i = 0; i < 40; i++)
                    token += "abcdefghijklmnopqrstuvwxyz0123456789"[random.Next(36)];

                return token;
            }
        }

        public static string GenerateRc4Key
        {
            get
            {
                var random = new Random();
                var token = string.Empty;

                for (var i = 0; i < 38; i++)
                    token += "abcdefghijklmnopqrstuvwxyz0123456789"[random.Next(36)];

                return token;
            }
        }

        public static int GetSecondsUntilNextMonth
        {
            get
            {
                var now = DateTime.UtcNow;

                if (now.Month != 12)
                    return (int) (new DateTime(now.Year, now.Month + 1, 1, now.Hour,
                                      now.Minute, now.Second) - now).TotalSeconds;

                return (int) (new DateTime(now.Year, 1, 1, now.Hour,
                                  now.Minute, now.Second) - now).TotalSeconds;
            }
        }

        public static bool IsLinux
        {
            get
            {
                var p = (int) Environment.OSVersion.Platform;
                return p == 4 || p == 6 || p == 128;
            }
        }

        public static int GetCurrentTimestamp => (int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

        public static string GetIp4Address()
        {
            var ipAddress = string.Empty;
            var nics = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var nic in nics)
            {
                if (nic.OperationalStatus != OperationalStatus.Up)
                    continue;

                var adapterStat = nic.GetIPv4Statistics();
                var uniCast = nic.GetIPProperties().UnicastAddresses;

                if (uniCast == null) continue;
                if (uniCast.Where(uni => adapterStat.UnicastPacketsReceived > 0
                                         && adapterStat.UnicastPacketsSent > 0
                                         && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback).Any(uni =>
                    uni.Address.AddressFamily == AddressFamily.InterNetwork))
                    ipAddress = nic.GetIPProperties().UnicastAddresses[0].Address.ToString();
            }

            return ipAddress;
        }

        public static int ToTick(TimeSpan duration)
        {
            return (int) (duration.TotalMilliseconds / 50);
        }

        public static int ToTick(int seconds)
        {
            return seconds * 1000 / 50;
        }

        public static double FromTick(int tick)
        {
            return tick * 50 / 1000d;
        }
    }
}