namespace GameShop.Facades
{
    public interface IUserFacade
    {
        public Guid CreateUser(string name);

        public void DeleteUser(Guid id);


    }
}
