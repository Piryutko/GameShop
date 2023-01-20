using GameShop.Domains;
using GameShop.Facades;
using GameShop.StorageInMemory;

var gameStorage = new GameInMemoryStorage();
var userStorage = new UserInMemoryStorage();
var purchaseStorage = new PurchaseInMemoryStorage();

var gameFacade = new GameShopFacade(gameStorage, purchaseStorage, userStorage);

var gameId = gameFacade.AddGame("Doom", 1000, Genre.Shooter);
var userId = gameFacade.AddUser("John");

gameFacade.BuyGame(userId, gameId);
