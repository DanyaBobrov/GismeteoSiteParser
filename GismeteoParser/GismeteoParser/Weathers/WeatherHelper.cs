using System;
using System.Collections.Generic;

namespace GismeteoParser.Weathers
{
    public static class WeatherHelper
    {
        public static Dictionary<Time, string> GetWeaterAsPairs(string[] informationParse) 
        {
            if (informationParse.Length != 8)
                throw new ApplicationException("Warning Length");

            return new Dictionary<Time, string>()
            {
                { Time.One, informationParse[0]?.Trim() },
                { Time.Four, informationParse[1]?.Trim() },
                { Time.Seven, informationParse[2]?.Trim() },
                { Time.Ten, informationParse[3]?.Trim() },
                { Time.Thirteen, informationParse[4]?.Trim() },
                { Time.Sixteen, informationParse[5]?.Trim() },
                { Time.Nineteen, informationParse[6]?.Trim() },
                { Time.TwentyTwo, informationParse[7]?.Trim() },
            };
        }
    }
}
