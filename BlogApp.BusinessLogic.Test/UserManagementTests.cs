using System.Diagnostics.CodeAnalysis;
using BlogApp.Domain;
using BlogApp.IDataAccess.Repositories;
using Moq;

namespace BlogApp.BusinessLogic.Test;

[ExcludeFromCodeCoverage]
[TestClass]
public class UserManagementTests
{
    private Mock<IUserRepository> _repositoryMock;
    
    [TestInitialize]
    public void Setup()
    {
        _repositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _repositoryMock.VerifyAll();
    }
    
    [TestMethod]
        public void CreateUserSuccess()
        {
            User user = CreateDefaultUser();
            _repositoryMock.Setup(m => m.Add(It.IsAny<User>()));
            UserManagement userLogic = new UserManagement(_repositoryMock.Object);
            User result = userLogic.Create(user);
            
            User userToCompare = new User()
            {
                Id = result.Id,
                Name = result.Name,
                LastName = result.LastName,
                UserName = result.UserName,
                EmailAddress = result.EmailAddress,
                Password = result.Password
            };

            Assert.AreEqual(result, userToCompare);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserFailure()
        {
            User user = CreateDefaultUser();
            _repositoryMock.Setup(m => m.Add(It.IsAny<User>())).Throws(new ArgumentException());
            UserManagement userLogic = new UserManagement(_repositoryMock.Object);
            
            User result = userLogic.Create(user);
        }
        
        [TestMethod]
        public void UpdateUserSuccess()
        {

            User user = CreateDefaultUser();
            user.Name = "John";
            _repositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(user);
            _repositoryMock.Setup(m => m.Update(It.IsAny<User>()));
            var userLogic = new UserManagement(_repositoryMock.Object);
            
            var result = userLogic.UpdateUser(user.Id, user);

            Assert.AreEqual("John", user.Name);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserNullFailure()
        {

            User user = CreateDefaultUser();
            user.Name = "John";
            _repositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ArgumentException());
            _repositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(value: null);
            var userLogic = new UserManagement(_repositoryMock.Object);
            
            var result = userLogic.UpdateUser(user.Id, user);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserBlankNameFailure()
        {

            User user = CreateDefaultUser();
            user.Name = "";
            _repositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(value: null);
            var userLogic = new UserManagement(_repositoryMock.Object);
            
            var result = userLogic.UpdateUser(user.Id, user);
        }
        
        [TestMethod]
        public void RemoveUserSuccess()
        {
            User user = CreateDefaultUser();
            _repositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(user);
            _repositoryMock.Setup(m => m.Remove(It.IsAny<User>()));
            var userLogic = new UserManagement(_repositoryMock.Object);
            
            userLogic.RemoveUser(user.Id);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveUserFailure()
        {
            User user = CreateDefaultUser();
            _repositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ArgumentException());
            var userLogic = new UserManagement(_repositoryMock.Object);
            
            userLogic.RemoveUser(user.Id);
        }
        
        [TestMethod]
        public void GetUserSuccessful()
        {
            var user = CreateDefaultUser();
            _repositoryMock.Setup(u => u.Get(user.Id)).Returns(user);
            UserManagement m = new UserManagement(_repositoryMock.Object);

            var userRespond = m.GetUser(user.Id);
            
            Assert.AreEqual(userRespond, user);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUserFailure()
        {
            var user = CreateDefaultUser();
            _repositoryMock.Setup(u => u.Get(user.Id)).Throws(new ArgumentException());
            UserManagement m = new UserManagement(_repositoryMock.Object);

            m.GetUser(user.Id);
        }
        
        [TestMethod]
        public void GetAllUsersSuccess()
        {
            User user = CreateDefaultUser();
            User user2 = new User()
            {
                Id = Guid.NewGuid(),
                Name = "John",
                LastName = "Wick",
                UserName = "jWick2023",
                EmailAddress = "j_wick_2023@gun.com",
                Password = "wannabefree"
            };
            List<User> expectedResult = new List<User>() { user, user2 };
            _repositoryMock.Setup(m => m.GetAll()).Returns(expectedResult);
            UserManagement userLogic = new UserManagement(_repositoryMock.Object);

            List<User> obtainedResult = userLogic.GetAll().ToList();
            
            CollectionAssert.AreEqual(expectedResult, obtainedResult);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAllUsersFailure()
        {
            _repositoryMock.Setup(m => m.GetAll()).Throws(new ArgumentException());
            UserManagement userLogic = new UserManagement(_repositoryMock.Object);

            userLogic.GetAll();
        }


        private  User CreateDefaultUser()
        {
           return  new User()
            {
                Id = Guid.NewGuid(),
                Name = "James",
                LastName = "Bond",
                UserName = "jamesb007",
                EmailAddress = "jamesb_007@mi6.com",
                Password = "shakenplease"
            };
        }

}