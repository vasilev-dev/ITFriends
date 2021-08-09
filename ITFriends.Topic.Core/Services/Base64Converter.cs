namespace ITFriends.Topic.Core.Services
{
    public class Base64Converter : IBase64Converter
    {
        public string EncodeToBase64(string data)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(data);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public string DecodeFromBase64(string base64)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}