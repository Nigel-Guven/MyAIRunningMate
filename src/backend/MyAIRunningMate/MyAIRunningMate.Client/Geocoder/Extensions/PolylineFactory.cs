using System.Text;

namespace MyAIRunningMate.Client.Geocoder.Extensions;

public static class PolylineFactory
{
    public static string Encode(IEnumerable<(double Lat, double Lng)> points)
    {
        var str = new StringBuilder();
        var lastLat = 0;
        var lastLng = 0;

        foreach (var point in points)
        {
            var lat = (int)Math.Round(point.Lat * 1e5);
            var lng = (int)Math.Round(point.Lng * 1e5);

            var deltaLat = lat - lastLat;
            var deltaLng = lng - lastLng;

            EncodeValue(deltaLat, str);
            EncodeValue(deltaLng, str);

            lastLat = lat;
            lastLng = lng;
        }

        return str.ToString();
    }

    private static void EncodeValue(int value, StringBuilder str)
    {
        var sValue = value << 1;
        if (value < 0)
        {
            sValue = ~sValue;
        }

        while (sValue >= 0x20)
        {
            str.Append((char)((0x20 | (sValue & 0x1f)) + 63));
            sValue >>= 5;
        }
        str.Append((char)(sValue + 63));
    }
}