namespace ITFriends.Topic.Core.Services
{
    public interface IBase64Converter
    {
        string EncodeToBase64(string data);
        string DecodeFromBase64(string base64);
    }
}