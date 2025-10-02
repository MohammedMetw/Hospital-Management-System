using Hospital.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


//---------------------------Note--------------------------------------
//                      --------------
// API Key in appsetting.json need to intialze before use (from OpenRouter.ai)
//                      --------------
//---------------------------------------------------------------------

namespace Hospital.Infrastructure.Services
{
    public class AIChatService : IAIChatService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _model;

        public AIChatService(IConfiguration configuration, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiKey = configuration["AiSettings:OpenRouterApiKey"]; 
            _model = configuration["AiSettings:Model"] ?? "meta-llama/llama-3-8b-instruct";

            if (string.IsNullOrEmpty(_apiKey))
                throw new ArgumentNullException(nameof(_apiKey), "OpenRouter API key not found in configuration.");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        }

        public async Task<string> GetTriageSuggestion(string patientSymptoms)
        {
            var systemPrompt = "You are a helpful medical triage assistant. Based on the patient's symptoms, suggest the most appropriate doctor specialty they should see. Keep the response short and direct.";

            var payload = new
            {
                model = _model,
                messages = new[]
                {
                    new { role = "system", content = systemPrompt },
                    new { role = "user", content = patientSymptoms }
                },
                max_tokens = 100
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://openrouter.ai/api/v1/chat/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"OpenRouter API call failed: {response.StatusCode} - {responseString}");

            using var doc = JsonDocument.Parse(responseString);
            var choice = doc.RootElement.GetProperty("choices")[0];
            return choice.GetProperty("message").GetProperty("content").GetString();
        }
    }
}
