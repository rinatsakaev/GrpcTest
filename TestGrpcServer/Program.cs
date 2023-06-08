using TestGrpcServer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<SequenceService>();
builder.Services.AddGrpc();
var app = builder.Build();
app.MapGrpcService<RandomNumberService>();
app.Run();