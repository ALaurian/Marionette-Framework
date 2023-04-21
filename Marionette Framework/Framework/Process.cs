using Marionette.Orchestrator;
using Marionette.Orchestrator.Enums;
using Marionette.WebBrowser;

namespace Marionette_Framework;

static partial class Framework
{
    private static bool startButtonPressed = false;

    public static void Process(ref QueueItem in_TransactionItem, Dictionary<string, object> in_Config,
        MarionetteWebBrowser in_chromeBrowser)
    {
        Console.WriteLine("Started Process");
        in_TransactionItem.Status = QueueItemStatus.InProgress;
        in_TransactionItem.StartTransactionTime = DateTime.Now.ToString();
        //Invoke steps of processs
        RPAChallenge(in_TransactionItem, in_chromeBrowser);
    }

    public static void RPAChallenge(QueueItem currentTransaction, MarionetteWebBrowser chromeBrowser)
    {
        if (startButtonPressed == false)
        {
            chromeBrowser.Click("(//button[normalize-space()='Start'])[1]");
            startButtonPressed = true;
        }


        var FirstName = currentTransaction.SpecificContent["First Name"].ToString();
        var LastName = currentTransaction.SpecificContent["Last Name "].ToString();
        var CompanyName = currentTransaction.SpecificContent["Company Name"].ToString();
        var roleInCompany = currentTransaction.SpecificContent["Role in Company"].ToString();
        var Address = currentTransaction.SpecificContent["Address"].ToString();
        var Email = currentTransaction.SpecificContent["Email"].ToString();
        var PhoneNumber = currentTransaction.SpecificContent["Phone Number"].ToString();

        chromeBrowser.SetText("//label[normalize-space()='First Name']/following-sibling::*[1]", FirstName);

        chromeBrowser.SetText("//label[normalize-space()='Last Name']/following-sibling::*[1]", LastName);

        chromeBrowser.SetText("//label[normalize-space()='Company Name']/following-sibling::*[1]", CompanyName);

        chromeBrowser.SetText("//label[normalize-space()='Role in Company']/following-sibling::*[1]", roleInCompany);

        chromeBrowser.SetText("//label[normalize-space()='Address']/following-sibling::*[1]", Address);

        chromeBrowser.SetText("//label[normalize-space()='Email']/following-sibling::*[1]", Email);

        chromeBrowser.SetText("//label[normalize-space()='Phone Number']/following-sibling::*[1]", PhoneNumber);

        chromeBrowser.Click("(//input[@value='Submit'])[1]");
    }
}