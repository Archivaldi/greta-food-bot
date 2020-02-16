using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using RestSharp.Extensions;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace GretaFoodBot
{
    public static class Program
    {
        private static TelegramBotClient Bot;
        private static readonly AutoResetEvent _appShutDownWaitHandle = new AutoResetEvent(false);

        public static async Task Main(string[] args)
        {
            var telegramToken = args[0];
            Console.CancelKeyPress += ConsoleOnCancelKeyPress;

            Bot = new TelegramBotClient(telegramToken);
            var me = await Bot.GetMeAsync();
            Console.Title = me.Username;

            var cts = new CancellationTokenSource();

            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            Bot.StartReceiving(
                new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync),
                cts.Token
            );

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            // Send cancellation request to stop bot
            _appShutDownWaitHandle.WaitOne();
        }
        
        private static void ConsoleOnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("Application is shutting down...");
            _appShutDownWaitHandle.Set();
        }

        public static async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => BotOnMessageReceived(update.Message),
                UpdateType.EditedMessage => BotOnMessageReceived(update.Message),
                UpdateType.CallbackQuery => BotOnCallbackQueryReceived(update.CallbackQuery),
                UpdateType.InlineQuery => BotOnInlineQueryReceived(update.InlineQuery),
                UpdateType.ChosenInlineResult => BotOnChosenInlineResultReceived(update.ChosenInlineResult),
                // UpdateType.Unknown:
                // UpdateType.ChannelPost:
                // UpdateType.EditedChannelPost:
                // UpdateType.ShippingQuery:
                // UpdateType.PreCheckoutQuery:
                // UpdateType.Poll:
                _ => UnknownUpdateHandlerAsync(update)
            };

            try
            {
                await handler;
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(exception, cancellationToken);
            }
        }

        private static async Task BotOnMessageReceived(Message message)
        {
            Console.WriteLine($"Recevie message type: {message.Type}");
            if (message.Type == MessageType.Location)
            {
                var userGeo = $"{message.Location.Latitude},{message.Location.Longitude}"; 
                var closestFood = (await  new FoodService().FindClosestFoodAsync(userGeo))
                                    .OrderBy(f => f.DistanceInMeters)
                                    .ToList();
                
                await Bot.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    parseMode: ParseMode.Markdown,
                    text: $"I found *{closestFood.Count}* meals 🍲 for you nearby..."
                    //replyMarkup: new ReplyKeyboardRemove()
                );
                foreach (var food in closestFood)
                {

                    await Bot.SendPhotoAsync(chatId: message.Chat.Id,photo:food.Food.ImageUrl);
                    await Bot.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        parseMode: ParseMode.Markdown,
                        text: 
                              $"*{food.Food.Name}* from *{food.Food.RestaurantName}* is in *{food.DistanceInMeters}* meters from you.\n" +
                              $"It will take only {food.ArivalTime.Subtract(DateTime.Now).Minutes} minutes ⏱ for you to get there.\n" +
                              $"Go grab your free food 🥙 😀",
                        replyMarkup: new InlineKeyboardMarkup(new[]
                        {
                            new []
                            {
                                InlineKeyboardButton.WithCallbackData("Grab this food", food.Food.Geo),
                            },
                        })
                    );
                }
                return;
            }
            if (message.Type != MessageType.Text)
                return;

            switch (message.Text.Split(' ').First())
            {
                // send inline keyboard
                case "/start":
                    await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                    var startReplyKeyboard = new ReplyKeyboardMarkup(
                        new[] {
                            new[]{
                                KeyboardButton.WithRequestLocation("Find some free food nearby !"),
                            },
                        });
                    
                    await Bot.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Hi, I'm Greta Food Bot, and I will find some free food for you. Please, send me your location for next steps",
                        replyMarkup: startReplyKeyboard
                    );
                    break;
                
                default:
                    const string usage = "Usage:\n" +
                                         "/start  - start conversation with saint Greta\n" +
                                         "/search - find some food fo you\n";
                    await Bot.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: usage,
                        replyMarkup: new ReplyKeyboardRemove()
                    );
                    break;
            }
        }

        private static async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery)
        {
            await Bot.AnswerCallbackQueryAsync(
                callbackQuery.Id,
                $"Congrats!"
            );
            var lo = float.Parse(callbackQuery.Data.Split(',')[0]);
            var ln = float.Parse(callbackQuery.Data.Split(',')[1]);
            await Bot.SendLocationAsync(
                callbackQuery.Message.Chat.Id, lo, ln);
            
            await Bot.EditMessageTextAsync(
                callbackQuery.Message.Chat.Id, 
                callbackQuery.Message.MessageId, 
                $"Your free promocode to get the food is: *DeveloperWeek2020*. Go ahead and tell at the end location ! 🥙 😀",
                parseMode: ParseMode.Markdown);
        }

        private static async Task BotOnInlineQueryReceived(InlineQuery inlineQuery)
        {
            Console.WriteLine($"Received inline query from: {inlineQuery.From.Id}");

            InlineQueryResultBase[] results = {
                // displayed result
                new InlineQueryResultArticle(
                    id: "3",
                    title: "TgBots",
                    inputMessageContent: new InputTextMessageContent(
                        "hello"
                    )
                )
            };

            await Bot.AnswerInlineQueryAsync(
                inlineQuery.Id,
                results,
                isPersonal: true,
                cacheTime: 0
            );
        }

        private static async Task BotOnChosenInlineResultReceived(ChosenInlineResult chosenInlineResult)
        {
            Console.WriteLine($"Received inline result: {chosenInlineResult.ResultId}");
        }

        private static async Task UnknownUpdateHandlerAsync(Update update)
        {
            Console.WriteLine($"Unknown update type: {update.Type}");
        }

        public static async Task HandleErrorAsync(Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
        }
    }
}