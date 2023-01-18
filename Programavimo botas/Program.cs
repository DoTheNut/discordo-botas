using System;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Linq;
using System.Data;

class Program
{
    static void Main(string[] args)
    => new Program().StartAsync().GetAwaiter().GetResult();

    private DiscordSocketClient _client;
    private string _prefix = "!";

    public async Task StartAsync()
    {
        _client = new DiscordSocketClient();
        _client.Log += Log;
        await _client.LoginAsync(TokenType.Bot, "MTA2NTE3OTIwNzU3NzExMjYxNg.GPoXRd.9CdfE6gDui20ew7OXaHN7KLY4dpbrusM9k_MEQ");
        await _client.StartAsync();
        _client.MessageReceived += MessageReceived;
        await Task.Delay(-1);
    }

    private async Task MessageReceived(SocketMessage message)
    {
        if (!message.Content.StartsWith(_prefix)) return;

        var input = message.Content.Substring(_prefix.Length).Trim().Split(' ');

        if (input[0] == "ping")
        {
            await message.Channel.SendMessageAsync("pong!");
        }
        else if (input[0] == "calc")
        {
            var result = Calculate(input[1]);
            await message.Channel.SendMessageAsync(result);
        }
        else if (input[0] == "time")
        {
            var result = TimeAndDate();
            await message.Channel.SendMessageAsync(result);
        }
    }

    private string Calculate(string expression)
    {
        var table = new DataTable();
        try
        {
            var result = table.Compute(expression, "");
            return result.ToString();
        }
        catch (EvaluateException)
        {
            return "Invalid expression.";
        }
    }

    private string TimeAndDate()
    {
        var time = DateTime.Now.ToString("hh:mm");
        var date = DateTime.Now.ToString("yyyy/MM/dd");
        var result = $"Siuo metu yra {time} data: {date}.";
        return result;
    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}
