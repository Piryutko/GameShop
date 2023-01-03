using GameShopLibrary;
using System;
using System.Collections.Generic;

namespace GameShopConsole
{
    class Program
    {
        static public string GetUserName()
        {
            Console.WriteLine("Введите имя юзера - ");
            return Console.ReadLine();
        }

        static public string GetGameName()
        {
            Console.WriteLine("Введите название игры -> ");
            var name = Console.ReadLine();
            return name;
        }

        static public double GetGameCost()
        {
            Console.Write("Введите цену -> ");
            return double.Parse(Console.ReadLine());
        }

        static public Genre GetGenreGame()
        {
            Console.WriteLine("Выберите жанр игры 1)RPG 2)Shooter 3)Race ");
            var genre = int.Parse(Console.ReadLine());

            switch (genre)
            {
                case 1:
                    return Genre.RPG;
                case 2:
                    return Genre.Shooter;
                case 3:
                    return Genre.Race;
            }

            Console.WriteLine("Не корректное значение -> выбрано значение по умолчанию жанр 'Shooter' ");

            return Genre.Shooter;
        }

        static public  void PrintGames(List<Game> games)
        {
            foreach (var game in games)
            {
                Console.WriteLine($"Название игры : {game.Name} | Цена : {game.Cost} | Жанр : {game.Genre} | Игра продана? : {game.IsBought}");
            }
        }

        static void Main(string[] args)
        {
            var gameStorage = new GameStorage();
            var userStorage = new UserStorage();
            var purshaseStorage = new PurchaseStorage();

            var userFacade = new UserFacade(userStorage);
            var shopFacade = new GameFacade(purshaseStorage, gameStorage, userStorage);

            Console.WriteLine("Добро пожаловать в GameShop!");
            while (true)
            {
                try
                {
                    var user = userFacade.CreateUser(GetUserName());
                    var game = shopFacade.AddGame(GetGameName(), GetGameCost(), GetGenreGame());

                    PrintGames(shopFacade.GetAllAvailableGames());

                    Console.WriteLine($"Совершается покупка: \nUserId - {user}\nGameId - {game}");
                    shopFacade.BuyGame(user, game);

                    PrintGames(shopFacade.GetAllGames());

                    var secondGame = shopFacade.AddGame(GetGameName(), GetGameCost(), GetGenreGame());

                    Console.WriteLine("удаляем игру");

                    shopFacade.RemoveGame(secondGame);

                    PrintGames(shopFacade.GetAllAvailableGames());

                }
                catch
                {
                    Console.WriteLine("не корректные данные,повторите попытку");
                }
            }
        }
    }
}
