namespace Testaufgabe_Dynamics_Consulting
{
  public class Program
  {
    static void Main(string[] args)
    {
      try
      {
        System.Uri organizationUri = new System.Uri(Constants.Organization.URI);

        System.Net.NetworkCredential networkCredentials = new System.Net.NetworkCredential();

        networkCredentials.UserName = Constants.Organization.USERNAME;

        networkCredentials.Password = Constants.Organization.PASSWORD;

        Microsoft.Xrm.Sdk.Client.IServiceManagement<Microsoft.Xrm.Sdk.IOrganizationService> serviceManagement =
          Microsoft.Xrm.Sdk.Client.ServiceConfigurationFactory.CreateManagement<Microsoft.Xrm.Sdk.IOrganizationService>(organizationUri);

        Microsoft.Xrm.Sdk.Client.AuthenticationProviderType authenticationProviderType = serviceManagement.AuthenticationType;

        Microsoft.Xrm.Sdk.Client.AuthenticationCredentials authenticationCredentials =
          new Microsoft.Xrm.Sdk.Client.AuthenticationCredentials();

        authenticationCredentials.ClientCredentials.Windows.ClientCredential = networkCredentials;

        using (Microsoft.Xrm.Sdk.Client.OrganizationServiceProxy organizationServiceProxy = 
          new Microsoft.Xrm.Sdk.Client.OrganizationServiceProxy(serviceManagement, authenticationCredentials.ClientCredentials))
        {
          System.Guid accountId = Application.GetCreatedAccountId(organizationServiceProxy);

          System.Console.WriteLine($"Account with Id: {accountId.ToString()} created.");

          System.Guid contactId = Application.GetCreatedContactId(organizationServiceProxy, accountId);

          System.Console.WriteLine($"Contact with Id: {contactId.ToString()} created.");

          Application.UpdateContact(
            organizationServiceProxy, 
            contactId, 
            "greve@software-greve.de", 
            System.Text.RegularExpressions.Regex.Replace(System.Guid.NewGuid().ToString(), Constants.NO_NUMBER, string.Empty));

          System.Console.WriteLine($"Contact with Id: {contactId.ToString()} updated.");

          Application.DisplayAccount(organizationServiceProxy, accountId);
        }

        System.Console.ReadLine();
      }
      catch (System.Exception ex)
      {
        System.Console.WriteLine(ex.ToString());
      }
      
      System.Console.ReadLine();
    }
  }
}
