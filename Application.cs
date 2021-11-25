namespace Testaufgabe_Dynamics_Consulting
{
  public static class Application
  {
    /// <summary>
    /// Creates new Account
    /// </summary>
    /// <param name="organizationServiceProxy"></param>
    /// <returns>New AccountId</returns>
    public static System.Guid GetCreatedAccountId(
      Microsoft.Xrm.Sdk.Client.OrganizationServiceProxy organizationServiceProxy)
    {
      try
      {
        Microsoft.Xrm.Sdk.Entity entity = new Microsoft.Xrm.Sdk.Entity(Constants.Entity.ACCOUNT);

        entity.Attributes[Constants.Account.NAME] = "Software Greve Int. Holding";

        return organizationServiceProxy.Create(entity);
      }
      catch (System.Exception ex)
      {
        System.Console.WriteLine("Software Greve Int. Holding could not be created: " + ex.ToString());

        return System.Guid.Empty;
      }
    }

    /// <summary>
    /// Creates new Contact
    /// </summary>
    /// <param name="organizationServiceProxy"></param>
    /// <param name="accountid"></param>
    /// <returns>New ContactId</returns>
    public static System.Guid GetCreatedContactId(
      Microsoft.Xrm.Sdk.Client.OrganizationServiceProxy organizationServiceProxy, 
      System.Guid accountid)
    {
      try
      {
        Microsoft.Xrm.Sdk.Entity entity = new Microsoft.Xrm.Sdk.Entity(Constants.Entity.CONTACT);

        entity.Attributes[Constants.Contact.FIRSTNAME] = "Sir Ulrich";

        entity.Attributes[Constants.Contact.LASTNAME] = "von Greve";

        entity.Attributes[Constants.Contact.PARENT_CUSTOMER_ID] = new Microsoft.Xrm.Sdk.EntityReference(Constants.Entity.ACCOUNT, accountid);

        return organizationServiceProxy.Create(entity);
      }
      catch (System.Exception ex)
      {
        System.Console.WriteLine("Sir Ulrich von Greve could not be created: " + ex.ToString());

        return System.Guid.Empty;
      }
    }

    /// <summary>
    /// Updates Contact with email and phone
    /// </summary>
    /// <param name="organizationServiceProxy"></param>
    /// <param name="contactid"></param>
    /// <param name="emailAddress"></param>
    /// <param name="phoneNumber"></param>
    public static void UpdateContact(
      Microsoft.Xrm.Sdk.Client.OrganizationServiceProxy organizationServiceProxy, 
      System.Guid contactid, 
      string emailAddress, 
      string phoneNumber)
    {
      try
      {
        Microsoft.Xrm.Sdk.Entity entity = organizationServiceProxy.Retrieve(
          Constants.Entity.CONTACT, 
          contactid, 
          new Microsoft.Xrm.Sdk.Query.ColumnSet(
            new string[] { 
              Constants.Contact.FIRSTNAME, 
              Constants.Contact.LASTNAME, 
              Constants.Contact.PARENT_CUSTOMER_ID }));

        entity.Attributes[Constants.Contact.EMAILADRESS1] = emailAddress;

        entity.Attributes[Constants.Contact.TELEPHONE2] = phoneNumber;

        organizationServiceProxy.Update(entity);
      }
      catch (System.Exception ex)
      {
        System.Console.WriteLine("Sir Ulrich von Greve could not be updated: " + ex.ToString());
      }
    }

    /// <summary>
    /// Displays Contact-Attributes
    /// </summary>
    /// <param name="organizationServiceProxy"></param>
    /// <param name="contactid"></param>
    public static void DisplayContact(
      Microsoft.Xrm.Sdk.Client.OrganizationServiceProxy organizationServiceProxy,
      System.Guid contactid)
    {
      try
      {
        Microsoft.Xrm.Sdk.Entity entity = organizationServiceProxy.Retrieve(
          Constants.Entity.CONTACT,
          contactid,
          new Microsoft.Xrm.Sdk.Query.ColumnSet(
            new string[] { 
              Constants.Contact.FIRSTNAME, 
              Constants.Contact.LASTNAME, 
              Constants.Contact.PARENT_CUSTOMER_ID, 
              Constants.Contact.EMAILADRESS1, 
              Constants.Contact.TELEPHONE2}));

        DisplayContact(organizationServiceProxy, entity);
      }
      catch (System.Exception ex)
      {
        System.Console.WriteLine("Sir Ulrich von Greve could not be displayed: " + ex.ToString());
      }
    }

    /// <summary>
    /// Displays Contact-Attributes
    /// </summary>
    /// <param name="organizationServiceProxy"></param>
    /// <param name="entity"></param>
    public static void DisplayContact(
      Microsoft.Xrm.Sdk.Client.OrganizationServiceProxy organizationServiceProxy,
      Microsoft.Xrm.Sdk.Entity entity)
    {
      try
      {
        System.Console.WriteLine($"Firstname: {entity.GetAttributeValue<string>(Constants.Contact.FIRSTNAME)}");

        System.Console.WriteLine($"Lastname: {entity.GetAttributeValue<string>(Constants.Contact.LASTNAME)}");

        System.Console.WriteLine(
          $"ParentCustomerId: {entity.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Constants.Contact.PARENT_CUSTOMER_ID).Id.ToString()}");

        System.Console.WriteLine($"Emailadress1: {entity.GetAttributeValue<string>(Constants.Contact.EMAILADRESS1)}");

        System.Console.WriteLine($"Telephone2: {entity.GetAttributeValue<string>(Constants.Contact.TELEPHONE2)}");
      }
      catch (System.Exception ex)
      {
        System.Console.WriteLine("Sir Ulrich von Greve could not be displayed: " + ex.ToString());
      }
    }

    /// <summary>
    /// Displays Account- and regarding Contact-Attributes
    /// </summary>
    /// <param name="organizationServiceProxy"></param>
    /// <param name="accountid"></param>
    public static void DisplayAccount(
      Microsoft.Xrm.Sdk.Client.OrganizationServiceProxy organizationServiceProxy,
      System.Guid accountid)
    {
      try
      {
        Microsoft.Xrm.Sdk.Entity entity = organizationServiceProxy.Retrieve(
          Constants.Entity.ACCOUNT,
          accountid,
          new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { Constants.Account.NAME }));

        System.Console.WriteLine($"Name: {entity.GetAttributeValue<string>(Constants.Account.NAME)}");

        Microsoft.Xrm.Sdk.Query.QueryExpression queryExpression = new Microsoft.Xrm.Sdk.Query.QueryExpression(Constants.Entity.CONTACT);

        queryExpression.Criteria.AddCondition(Constants.Contact.PARENT_CUSTOMER_ID, Microsoft.Xrm.Sdk.Query.ConditionOperator.Equal, entity.Id);

        queryExpression.ColumnSet =
          new Microsoft.Xrm.Sdk.Query.ColumnSet(
            new string[] {
              Constants.Contact.FIRSTNAME,
              Constants.Contact.LASTNAME,
              Constants.Contact.PARENT_CUSTOMER_ID,
              Constants.Contact.EMAILADRESS1,
              Constants.Contact.TELEPHONE2});

        Microsoft.Xrm.Sdk.EntityCollection entityCollection = organizationServiceProxy.RetrieveMultiple(queryExpression);

        if (entityCollection == null || entityCollection.Entities == null || entityCollection.Entities.Count < 1)
        {
          throw new System.Exception($"Regarding contact to account {entity.GetAttributeValue<string>(Constants.Account.NAME)} not found.");
        }

        if (entityCollection.Entities.Count > 1)
        {
          throw new System.Exception($"Multiple contacts to account {entity.GetAttributeValue<string>(Constants.Account.NAME)} found.");
        }

        DisplayContact(organizationServiceProxy, entityCollection.Entities[0]);
      }
      catch (System.Exception ex)
      {
        System.Console.WriteLine("Software Greve Int. Holding could not be displayed: " + ex.ToString());
      }
    }
  }
}
