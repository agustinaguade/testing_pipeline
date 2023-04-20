using System.Diagnostics.CodeAnalysis;
using BlogApp.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BlogApp.WebApi.Test;


[ExcludeFromCodeCoverage]
[TestClass]
public class UserControllerTest
{
   
    [TestMethod]
    public void GetUsersReturnsAsExpected()
    {
        // Arrange
        var userController = new UserController();
        
        // Act
        var response = userController.Get();
        var okResponseObject = response as OkObjectResult;
        
        // Assert
        Assert.AreEqual(response, okResponseObject);
    }
}