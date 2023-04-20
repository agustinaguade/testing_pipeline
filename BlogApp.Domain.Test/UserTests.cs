using System.Diagnostics.CodeAnalysis;
using BlogApp.IDataAccess.Repositories;
using Moq;

namespace BlogApp.Domain.Test;

[ExcludeFromCodeCoverage]
[TestClass]
    public class UserTests
    {
        
        
        [TestMethod]
        public void CreateValidMovie()
        {
            var user = CreateUser();
            
            user.VerifyFormat();
            
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateFieldsThrowException()
        {
            var user = new User();
            user.VerifyFormat();
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateEmailFailsAndThrowException()
        {
            var user = new User()
            {
                Name = "Juan",
                EmailAddress = "lalal"
            };
            
            user.VerifyFormat();
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateEmailValidationFails()
        {
            var user = new User()
            {
                Name = "Agustin",
                LastName = "Aguade",
                UserName = "agusagua",
                Id  = Guid.NewGuid(),
                Password = "DA2",
                EmailAddress = "agus.com"
            };
            
            user.VerifyFormat();
        }
        
        [TestMethod]
        public void ValidateIdAreNotEQuals()
        {
            var user = CreateUser();
            var toCompareUser = CreateUser();
            
           Assert.AreNotEqual(user.Id, toCompareUser.Id);
        }

        [TestMethod]
        public void ValidateAreBothUsersEquals()
        {
            var expectedUser = CreateUser();
            var expectedToBeEqualUser = CreateUser();

            Assert.AreEqual(expectedUser, expectedToBeEqualUser);

        }

        private User CreateUser()
        {
            return  new User() {
                Name = "Agustin",
                LastName = "Aguade",
                UserName = "agusagua",
                Id  = Guid.NewGuid(),
                Password = "DA2",
                EmailAddress = "agusagua@hotmail.com"};
        }

    }