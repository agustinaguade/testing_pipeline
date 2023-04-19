using BlogApp.Domain;

namespace BlogApp.IDataAccess.Repositories;

public interface IUserRepository: IRepository<User>
{
    User Get(Guid id);
}