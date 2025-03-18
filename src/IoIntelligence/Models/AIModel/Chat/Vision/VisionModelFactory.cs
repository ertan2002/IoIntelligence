namespace IoIntelligence.Client.Models.AIModel.Chat.Vision;

public static class VisionModelFactory
{
    /// <summary>
    /// Gets the model string for a VisionModel enum value
    /// </summary>
    public static string GetModelString(VisionModel model)
    {
        return model switch
        {
            VisionModel.Llama32Vision => "meta-llama/Llama-3.2-90B-Vision-Instruct",
            VisionModel.Qwen2Vision => "Qwen/Qwen2-VL-7B-Instruct",
            _ => throw new ArgumentException($"Unsupported vision model: {model}", nameof(model))
        };
    }

    /// <summary>
    /// Creates a vision chat request using the specified vision model
    /// </summary>
    /// <param name="model">The vision model to use</param>
    /// <param name="systemMessage">Optional system message, defaults to "You are an AI assistant."</param>
    /// <returns>A VisionChatRequest configured with the specified model</returns>
    public static VisionChatRequest CreateVisionRequest(VisionModel model, string systemMessage = "You are an AI assistant.")
    {
        return new VisionChatRequest(GetModelString(model), systemMessage);
    }

    /// <summary>
    /// Helper method to quickly create a vision chat request with a specific model, system message and image
    /// </summary>
    /// <param name="model">The vision model to use</param>
    /// <param name="imageUrl">URL of the image to analyze</param>
    /// <param name="text">Optional text prompt, defaults to "What is in this image?"</param>
    /// <param name="systemMessage">Optional system message, defaults to "You are an AI assistant."</param>
    /// <returns>A ready-to-use ChatCompletionRequest with the image and text</returns>
    public static ChatCompletionRequest CreateVisionRequest(
        VisionModel model,
        string imageUrl,
        string text = "What is in this image?",
        string systemMessage = "You are an AI assistant.")
    {
        return new VisionChatRequest(GetModelString(model), systemMessage)
            .AddImageMessage(text, imageUrl)
            .Build();
    }
}