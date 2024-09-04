using System;
using System.Collections.Generic;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;

public class JWTGenerator
{
    public static string CreateToken(Dictionary<string, object> payload, string secretKey)
    {
        var algorithm = new HMACSHA256Algorithm();
        var serializer = new JsonNetSerializer();
        var urlEncoder = new JwtBase64UrlEncoder();

        var encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

        payload["exp"] = DateTimeOffset.UtcNow.AddSeconds(5).ToUnixTimeSeconds(); // token expiry

        try
        {
            return encoder.Encode(payload, secretKey);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error encoding JWT token: " + ex.Message);
            return null; 
        }
    }
}