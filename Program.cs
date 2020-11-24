using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace Bot
{
    public class Program
    {
        //invite link https://discord.com/oauth2/authorize?client_id=779682049865809951&permissions=8&scope=bot
        static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

       
        private DiscordSocketClient _client;
        private CommandHandler _handler;

        public async Task StartAsync()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Starting bot...");

            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info
            });

            _client.Log += Log;

            

            

            await _client.LoginAsync(TokenType.Bot, "sike");
            await _client.StartAsync();

            _handler = new CommandHandler(_client);

            while (_client.ConnectionState.ToString() != "Connected") { }
            await Task.Delay(700);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Bot connected!");

            while (_client.ConnectionState.ToString() == "Connected")
            {
                var delay = Task.Delay(60000);
                Console.WriteLine($"\nClient latency: {_client.Latency}ms");
                await delay;
            }

            await Task.Delay(-1);
        }

        private Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }
    }
}

