# IO Intelligence API Wrapper
**IO Intelligence API Wrapper** is a lightweight and intuitive .NET library that simplifies access to IO Intelligence’s AI models and agents. Instead of manually handling HTTP requests, this wrapper abstracts the underlying communication so you can quickly integrate advanced AI capabilities into your applications.

## Features
- **Easy Integration:** Seamlessly connect to IO Intelligence’s API using your API key.
- **Model & Agent Management:** Retrieve available models, start chat sessions, and manage agents.
- **Workflow Automation:** Execute various AI workflows such as translation, summarization, sentiment analysis, and content moderation.
- **Robust Error Handling:** Built-in support for catching API exceptions and displaying detailed error messages.
- **Clean Response Processing:** Utility methods to sanitize and clean up API responses.

## Installation
Install the package via the .NET CLI

```
dotnet add package IONET.IOIntelligence
```
Or search for IONET.IOIntelligence in Visual Studio's NuGet Package Manager.

## Getting Started

### Prerequisites

- Get your API key from [IO Intelligence Portal](https://ai.io.net/ai/api-keys)
- [Review API Documentation](https://docs.io.net/reference/get-started-with-io-intelligence-api) for usage limits and details

## Basic Setup
Replace **"YOUR_API_KEY_HERE"** with your actual API key. This initializes the IoIntelligenceClient.


```
var apiKey = "YOUR_API_KEY_HERE";
if (string.IsNullOrEmpty(apiKey))
{
    Console.WriteLine("Please set the IOINTELLIGENCE_API_KEY environment variable.");
    return;
}

IIoIntelligenceClient client = new IoIntelligenceClient(apiKey);
```

# Chat Model
The chat model lets you engage in interactive conversations with an AI assistant. You provide a sequence of messages (including system instructions and user inputs), and the API returns a reply from the selected AI model.

### Retrieving Available Models
Call the API to retrieve the available models. Each model's Id actually represents its model name.

```
var models = await client.Models.GetModelsAsync();
```

### Example output:
```
- deepseek-ai/DeepSeek-R1
- deepseek-ai/DeepSeek-R1-Distill-Llama-70B
- deepseek-ai/DeepSeek-R1-Distill-Qwen-32B
- meta-llama/Llama-3.3-70B-Instruct
- databricks/dbrx-instruct
- Qwen/QwQ-32B-Preview
- mistralai/Ministral-8B-Instruct-2410
- deepseek-ai/DeepSeek-R1-Distill-Qwen-1.5B
- deepseek-ai/DeepSeek-R1-Distill-Qwen-7B
- deepseek-ai/DeepSeek-R1-Distill-Qwen-14B
- deepseek-ai/DeepSeek-R1-Distill-Llama-8B
- netease-youdao/Confucius-o1-14B
- nvidia/AceMath-7B-Instruct
- google/gemma-2-9b-it
- neuralmagic/Llama-3.1-Nemotron-70B-Instruct-HF-FP8-dynamic
- mistralai/Mistral-Large-Instruct-2411
- microsoft/phi-4
- bespokelabs/Bespoke-Stratos-32B
- NovaSky-AI/Sky-T1-32B-Preview
- tiiuae/Falcon3-10B-Instruct
- CohereForAI/c4ai-command-r-plus-08-2024
- THUDM/glm-4-9b-chat
- Qwen/Qwen2.5-Coder-32B-Instruct
- CohereForAI/aya-expanse-32b
- jinaai/ReaderLM-v2
- openbmb/MiniCPM3-4B
- Qwen/Qwen2.5-1.5B-Instruct
- ozone-ai/0x-lite
- microsoft/Phi-3.5-mini-instruct
- ibm-granite/granite-3.1-8b-instruct
```

Since these model names are the IDs, you must include the model ID in your chat request.

### Select a Model and Send a Chat Request
Here, we select the first available model and use it in the chat completion request:

```
// Select a model (here we simply choose the first one)
string modelId = models.First().Id;

// Send the chat request using the selected model ID
var chatRequest = new ChatCompletionRequest
{
    Model = modelId, // e.g., "deepseek-ai/DeepSeek-R1"
    Messages = new List<ChatCompletionMessage>
    {
        new ChatCompletionMessage { Role = "system", Content = "You are a helpful assistant." },
        new ChatCompletionMessage { Role = "user", Content = "Hello, how are you?" }
    }
};

var chatResponse = await client.Models.CreateChatCompletionAsync(chatRequest);
string chatReply = SanitizeResponse(chatResponse.Choices.First().Message.Content);
Console.WriteLine("Assistant: " + chatReply);
```

### Utility Function for Sanitizing Responses
If you need to remove internal metadata (like **<think>** blocks), use this helper function:

```
static string SanitizeResponse(string response)
{
    // Remove any <think> blocks and trim extra characters.
    var cleaned = Regex.Replace(response, @"<think>.*?</think>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
    cleaned = Regex.Replace(cleaned, @"^[\s\u200B\p{C}\|%-]+|[\s\u200B\p{C}\|%-]+$", "");
    return cleaned.Trim();
}
```

# AI AGENTS
In addition to the chat model, IO Intelligence provides several specialized agents to handle different tasks. After retrieving the available agents, choose the one that fits your use case.

## Get Available Agents
Retrieve and list available agents:

```
     var agents = await client.Agents.GetAgentsAsync();
     foreach (var agent in agents.Agents)
     {
         Console.WriteLine($"- {agent.Value.Name}");
     }
```

### Example output:
```
- Reasoning Agent
- Summary Agent
- Sentiment Analysis Agent
- Named Entity Recognizer
- Custom Agent
- Moderation Agent
- Classification Agent
- Translation Agent
```

> Note: The **Reasoning Agent** has not been added to the NuGet library yet.

## Specific Agent Examples

### Summary Agent
This snippet summarizes a longer piece of text. The **SummarizeAgentArgs** lets you specify parameters like maximum words.

```
var summaryRequest = new WorkflowRequest
{
    Text = "In the rapidly evolving landscape of artificial intelligence, the ability to condense vast amounts of information into concise and meaningful summaries is crucial. From research papers and business reports to legal documents and news articles, professionals across industries rely on summarization to extract key insights efficiently. Traditional summarization techniques often struggle with maintaining coherence and contextual relevance. However, advanced AI models now leverage natural language understanding to identify core ideas, eliminate redundancy, and generate human-like summaries. As organizations continue to deal with an ever-growing influx of data, the demand for intelligent summarization tools will only increase. Whether enhancing productivity, improving decision-making, or streamlining workflows, AI-powered summarization is set to become an indispensable asset in the digital age.",
    Args = new SummarizeAgentArgs { MaxWords = 60 }
};
var summaryResult = await client.Workflows.ExecuteAsync<SummaryAgentResponse>(summaryRequest);
Console.WriteLine("Summary: " + summaryResult.Summary);
```

### Sentiment Analysis Agent
Analyze the sentiment of a given text and display the sentiment along with a confidence score.

```
var sentimentRequest = new WorkflowRequest
{
    Text = "I absolutely love this new feature!",
    Args = new SentimentAgentArgs() // Default parameters used here.
};
var sentimentResult = await client.Workflows.ExecuteAsync<SentimentAgentResponse>(sentimentRequest);
Console.WriteLine($"Sentiment: {sentimentResult.Sentiment} ({sentimentResult.Confidence:P0})");
```

### Named Entity Recognition Agent
Extract key entities such as persons, locations, and organizations from text.

```
var nerRequest = new WorkflowRequest
{
    Text = "A leading technology company recently announced the launch of its latest smartphone, the Nova X, at an event in Tech Valley. The company’s CEO, Jordan Lane, highlighted the device’s improved battery life, advanced camera system, and AI-powered enhancements. To achieve higher performance and energy efficiency, the company partnered with Coretron Systems to develop the new Zenith chipset.Pre-orders will begin on October 10, and the device will be available in global markets by October 20. Industry analysts predict strong demand across multiple regions, driven by innovation and evolving consumer expectations.",
    Args = new NameEntityRecognizerAgentArgs() // Uses default settings.
};
var nerResult = await client.Workflows.ExecuteAsync<NameEntityRecognizerAgentResponse>(nerRequest);
Console.WriteLine("Persons: " + string.Join(", ", nerResult.Persons));
Console.WriteLine("Locations: " + string.Join(", ", nerResult.Locations));
Console.WriteLine("Organizations: " + string.Join(", ", nerResult.Organizations));
```

### Custom Agent
Execute a custom task by specifying a name, objective, and instructions. In this example, the agent calculates 2+2.

```
var customRequest = new WorkflowRequest
{
    Text = "Calculate 2+2",
    Args = new CustomAgentArgs
    {
        Name = "calc 2+2",
        Objective = "Calculate 2+2",
        Instructions = "Return the result of 2+2"
    }
};
var customResult = await client.Workflows.ExecuteAsync<CustomAgentResult>(customRequest);
Console.WriteLine("Custom Agent Result: " + customResult.RawData);
```

### Moderation Agent
This snippet checks text for inappropriate content.

 > Note: The standard ModerationAgent currently has issues, so use **ModerationAgentArgsWithWorkaround** for a more reliable result.

**Standard Moderation Agent:**
```
var moderationRequest = new WorkflowRequest
{
    Text = "I absolutely hate this service! It’s a total scam, and the customer support is useless. Anyone who buys from them is getting ripped off. I swear, if they don’t fix this issue, I’m going to make sure no one ever buys from them again! Also, I’ve seen people spreading false information about their competitors—this is unethical business practice",
    Args = new ModerationAgentArgs
    {
        Threshold = 0.5
    }
};
var moderationResult = await client.Workflows.ExecuteAsync<ModerationAgentResult>(moderationRequest);
Console.WriteLine($"Flagged: {moderationResult.IsFlagged}");
Console.WriteLine($"Hate Speech Score: {moderationResult.HateSpeech:P0}");
Console.WriteLine($"Profanity Score: {moderationResult.ExtremeProfanity:P0}");
Console.WriteLine($"Harassment Score: {moderationResult.Harassment:P0}");
Console.WriteLine($"SelfHarm Score: {moderationResult.SelfHarm:P0}");
Console.WriteLine($"DangerousContent Score: {moderationResult.DangerousContent:P0}");
Console.WriteLine($"SexuallyExplicit: {moderationResult.SexuallyExplicit:P0}");
 ```

**Using ModerationAgentArgsWithWorkaround:**
``` 
var moderationRequest = new WorkflowRequest
{
    Text = "I absolutely hate this service! It’s a total scam, and the customer support is useless. Anyone who buys from them is getting ripped off. I swear, if they don’t fix this issue, I’m going to make sure no one ever buys from them again! Also, I’ve seen people spreading false information about their competitors—this is unethical business practice",
    Args = new ModerationAgentArgsWithWorkaround
    {
        Threshold = 0.5,
        Name = "content_moderator",
        Objective = "Identify and flag inappropriate content",
        Instructions = "You are an agent that moderates content."
    }
};
var moderationResult = await client.Workflows.ExecuteAsync<ModerationAgentWithWorkaroundResult>(moderationRequest);
Console.WriteLine("Moderation Result: " + moderationResult.RawData);
```
 
### Classification Agent
Classify text into one of the specified categories.

```
var classificationRequest = new WorkflowRequest
{
    Text = "A major tech company has announced a breakthrough in battery technology that significantly enhances energy density and reduces charging time. This innovation is expected to accelerate the adoption of electric vehicles, making them more practical for everyday use. Industry experts predict that this advancement could drive increased competition in the market and attract further investment in sustainable energy solutions.",
    Args = new ClassificationAgentArgs
    {
        ClassifyBy = new List<string> { "fact", "fiction", "sci-fi", "fantasy" }
    }
};
var classificationResult = await client.Workflows.ExecuteAsync<ClassificationAgentResult>(classificationRequest);
Console.WriteLine("Classification Category: " + classificationResult.Category);
```

### Translation Agent
Translate text into English. For TargetLanguage, you can provide either a language code (e.g., "en") or the language name (e.g., "english").

```
var translationRequest = new WorkflowRequest
{
    Text = "Votre texte en français ici",
    Args = new TranslateAgentArgs { TargetLanguage = "en" }
};
var translationResult = await client.Workflows.ExecuteAsync<TranslationAgentResponse>(translationRequest);
Console.WriteLine("Translated Text: " + translationResult.TranslatedText);
```
For the *TargetLanguage* property, you can specify the language using either its **ISO 639-1** code (e.g., "en" for English or "es" for Spanish) or its full English name (e.g., "English" or "Spanish").

# Vision Capabilities

IO Intelligence now supports image analysis using advanced vision models. This feature allows you to upload images via URL and get AI-powered descriptions and analyses.

## Supported Vision Models

Currently, two vision models are available:
- `meta-llama/Llama-3.2-90B-Vision-Instruct` - Meta's powerful vision model
- `Qwen/Qwen2-VL-7B-Instruct` - Qwen's vision-language model

For convenience, these are accessible through the `VisionModel` enum:
```csharp
VisionModel.Llama32Vision  // Uses meta-llama/Llama-3.2-90B-Vision-Instruct
VisionModel.Qwen2Vision    // Uses Qwen/Qwen2-VL-7B-Instruct
```

## Basic Image Analysis Example

Here's a simple example of analyzing an image:

```csharp
// Initialize the client
var apiKey = "YOUR_API_KEY_HERE";
IIoIntelligenceClient client = new IoIntelligenceClient(apiKey);

// Create a vision request using the factory
var request = VisionModelFactory.CreateVisionRequest(
    VisionModel.Llama32Vision,  // Select the vision model
    "https://example.com/your-image.jpg",  // URL to your image
    "What is in this image?"  // Optional prompt (default)
);

// Send the request to the API
var response = await client.Models.CreateChatCompletionAsync(request);

// Get the AI's analysis
string imageDescription = SanitizeResponse(response.Choices[0].Message.Content);
Console.WriteLine("Image Analysis: " + imageDescription);
```
## Advanced Usage: Image Conversation

You can also incorporate images into a back-and-forth conversation:

```csharp
// Start a conversation
var request = new ChatCompletionRequest
{
    Model = VisionModelFactory.GetModelString(VisionModel.Llama32Vision),
    Messages = new List<ChatCompletionMessage>
    {
        new ChatCompletionMessage { Role = "system", Content = "You are a visual analysis assistant." },
        new ChatCompletionMessage { Role = "user", Content = "I have some photos to analyze." }
    }
};

// Get initial response
var initialResponse = await client.Models.CreateChatCompletionAsync(request);
request.Messages.Add(initialResponse.Choices[0].Message);

// Add an image to the conversation
request.Messages.Add(VisionChatMessageExtensions.CreateImageMessage(
    "What can you tell me about this photo?",
    "https://example.com/vacation-photo.jpg"
));

// Get the image analysis
var imageResponse = await client.Models.CreateChatCompletionAsync(request);
request.Messages.Add(imageResponse.Choices[0].Message);

// Continue the conversation with a follow-up question
request.Messages.Add(new ChatCompletionMessage { 
    Role = "user", 
    Content = "What time of day do you think this was taken?" 
});

// Get the final response
var finalResponse = await client.Models.CreateChatCompletionAsync(request);
Console.WriteLine(SanitizeResponse(finalResponse.Choices[0].Message.Content));
```

# Final Notes
I've created a Telegram bot using the IO Chat Model as an example. You can use and extend it as needed (for instance, by implementing additional agents).

For more details, check out the related repository:
[IOIntelligence Telegram Bot](https://github.com/ertan2002/IOIntellegence_TelegramBot)
