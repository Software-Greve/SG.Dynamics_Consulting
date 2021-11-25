;

function CaptureAccountDoublette_OnChangeName(executionContext) {
  var name = Xrm.Page.getAttribute("name");

  if (name == null || name == undefined) {
    return;
  }

  var namevalue = name.getValue();

  var notificationMessage = "Account with Account Name '" + namevalue + "' still exists!";

  var serverUrl = Xrm.Utility.getGlobalContext().getClientUrl();

  var ODataPath = serverUrl;

  var retrieveResult = new XMLHttpRequest();

  var fetchXmlQuery =
    '<fetch version = "1.0" output-format="xml-platform" mapping="logical" distinct="false">' +
    '<entity name="account">' +
    '<attribute name="name" />' +
    '<filter type="and">' +
    '<condition attribute="name" operator="eq" value="' + namevalue + '" />' +
    '</filter>' +
    '</entity>' +
    '</fetch >';

  retrieveResult.open("GET", ODataPath + "/api/data/v9.0/accounts?fetchXml=" + encodeURI(fetchXmlQuery), false, 'crm90aio\\testuser', 'j9OAV85qYh7Karq');

  retrieveResult.setRequestHeader("Prefer", 'odata.include-annotations="*"');

  retrieveResult.onreadystatechange = function () {

    if (this.readyState === 4) {

      retrieveResult.onreadystatechange = null;

      if (this.status === 200) {

        results = JSON.parse(this.responseText).value;

        if (results.length > 0) {
          Xrm.Page.ui.setFormNotification(notificationMessage, "WARNING");
        }
      }
    }
  };

  retrieveResult.send();
}