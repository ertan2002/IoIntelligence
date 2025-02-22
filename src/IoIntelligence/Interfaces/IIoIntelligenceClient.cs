using IoIntelligence.Client.Clients;

namespace IoIntelligence.Client.Interfaces;

public interface IIoIntelligenceClient
{
    ModelClient Models { get; }
    AgentClient Agents { get; }
    WorkflowClient Workflows { get; }
}