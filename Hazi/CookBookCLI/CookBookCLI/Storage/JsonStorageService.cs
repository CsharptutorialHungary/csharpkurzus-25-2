using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CookBookCLI.Storage
{
    internal static class JsonStorageService
    {
        private static readonly JsonSerializerOptions _jsonOptions = new() 
        { 
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
        };

        public static async Task Serialize<T>(string filePath, T data, CancellationToken token = default)
        {
            await using FileStream stream = File.Create(filePath);
            await JsonSerializer.SerializeAsync<T>(stream, data, _jsonOptions, token);
        }

        public static async Task<T?> Deserialize<T>(string filePath, CancellationToken token = default)
        {
            if (!File.Exists(filePath))
                return default;
            await using FileStream stream = File.OpenRead(filePath);
            T? data = await JsonSerializer.DeserializeAsync<T>(stream, _jsonOptions, token);
            return data;
        }
    }
}
