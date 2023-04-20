using System.Net.Mail;

namespace BlogApp.Domain;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    

    public void VerifyFormat()
    {
        ValidateFields();
        ValidateEmail();
    }
    
    private void ValidateFields()
    {
        if (AreAllTheFieldsCompleted())
        {
            throw new ArgumentException("Some required fields are missing");
        }
    }

    private void ValidateEmail()
    {
       if(IsEmailOk()) EmailAddress = new MailAddress(EmailAddress).Address.Trim();
    }
    

    private bool AreAllTheFieldsCompleted()
    {
        return new [] { Name, LastName, UserName, Password, EmailAddress }.Any(string.IsNullOrEmpty);
    }

    
    private bool IsEmailOk()
    {
        var trimmedEmailAddress = EmailAddress.Trim();
        
        try
        {
            var addr = new MailAddress(trimmedEmailAddress);
            return true;
        }
        catch {
            throw new ArgumentException("Invalid Email");
        }
    }
    
    public void UpdateProperties(User user)
    {
        Name = user.Name;
        LastName = user.LastName;
        UserName = user.UserName;
        EmailAddress = user.EmailAddress;
        Password = user.Password;
    }
    
    public override bool Equals(object obj)
    {
        return obj is User user &&
               EmailAddress.Equals(user.EmailAddress);
    }

  
}
