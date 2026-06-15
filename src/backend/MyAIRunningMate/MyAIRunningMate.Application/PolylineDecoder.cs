namespace MyAIRunningMate.Application;

public static class PolylineDecoder
{
    public static (double Latitude, double Longitude)? GetFirstCoordinate(string polyline)
    {
        if (string.IsNullOrEmpty(polyline)) return null;

        int index = 0;
        int len = polyline.Length;
        
        double lat = DecodeNextValue(polyline, ref index);
        double lng = DecodeNextValue(polyline, ref index);

        return (lat, lng);
    }

    private static double DecodeNextValue(string polyline, ref int index)
    {
        int result = 0;
        int shift = 0;
        int b;
        do
        {
            b = polyline[index++] - 63;
            result |= (b & 0x1f) << shift;
            shift += 5;
        } while (b >= 0x20 && index < polyline.Length);

        int delta = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
        return delta * 1e-5;
    }
}