using BlogApp.Domain;
using BlogApp.IDataAccess.Repositories;

namespace BlogApp.BusinessLogic;

public class UserManagement
{

    private readonly IUserRepository _userRepository;

    public UserManagement(IUserRepository userRepository)
    {
        this._userRepository = userRepository;
    }

    public User Create(User user)
    {
        try
        {
            user.Id = Guid.NewGuid();
            user.VerifyFormat();
            _userRepository.Add(user);
            return user;
        }
        catch (Exception e)
        {
            throw new ArgumentException("ERROR: No se pudo crear usuario.",
                e);
        }
    }

    public IEnumerable<User> GetAll()
    {
        try
        {
            return _userRepository.GetAll();
        }
        catch (Exception e)
        {
            throw new ArgumentException("Error al traerlos", e);
        }
    }


    public User UpdateUser(Guid userToModifyId, User user)
    {
        try
        {
            User storedUser = GetUser(userToModifyId);
            storedUser.UpdateProperties(user);
            storedUser.VerifyFormat();
            _userRepository.Update(user);
            return storedUser;
        }
        catch (Exception e)
        {
            throw new ArgumentException("No se pudo updatear", e);
        }
    }

    public void RemoveUser(Guid id)
    {
        try
        {
            User userToDelete = _userRepository.Get(id);
            _userRepository.Remove(userToDelete);
        }
        catch (Exception e)
        {
            throw new ArgumentException("Error: usuario no eliminado", e);
        }
    }

    public User GetUser(Guid userId)
    {
        try
        {
            User userObtained = _userRepository.Get(userId);
            return userObtained;
        }
        catch (Exception e)
        {
            throw new ArgumentException("No se pudo traer al usuario", e);
        }
    }
}