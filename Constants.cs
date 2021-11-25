namespace Testaufgabe_Dynamics_Consulting
{
  public static class Constants
  {
    public const string NO_NUMBER = "[^0-9]";

    public static class Organization
    {
      public const string URI = @"https://crm90aio.dcscloud.de/testtask/XRMServices/2011/Organization.svc";

      public const string USERNAME = "testuser";

      public const string PASSWORD = "j9OAV85qYh7Karq";

      public const string DOMAIN = "crm90aio";
    }

    public static class Entity
    {
      public const string ACCOUNT = "account";

      public const string CONTACT = "contact";
    }

    public static class Account
    {
      public const string NAME = "name";
    }

    public static class Contact
    {
      public const string FIRSTNAME = "firstname";

      public const string LASTNAME = "lastname";

      public const string PARENT_CUSTOMER_ID = "parentcustomerid";

      public const string EMAILADRESS1 = "emailaddress1";

      public const string TELEPHONE2 = "telephone2";
    }
  }
}
