// See https://aka.ms/new-console-template for more information

using Console.Trigger.StandardTransaction.Process;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProcessResultMessage;

//setup our DI
var serviceProvider = new ServiceCollection()
    // Add your services here..
    .AddSingleton<ITestTransaction, TestTransaction>()
    .AddSingleton<IProcessResult, ProcessResultsLogic>()
    .BuildServiceProvider();

var demoService = serviceProvider.GetRequiredService<ITestTransaction>();

var result = demoService.EventA(); 

var stringResult = JsonConvert.SerializeObject(result, Formatting.Indented);

System.Console.WriteLine("RESULT: \n");
System.Console.WriteLine(stringResult);