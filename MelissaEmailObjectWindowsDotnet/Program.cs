using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using MelissaData;

namespace MelissaEmailObjectWindowsDotnet
{
  /// <summary>
  /// Email Object allows your websites and custom applications to update email addresses
  /// in your database files while verifying and correcting misspelled domain names.
  /// </summary>
  /// <remarks>
  /// High-level flow of this sample:
  ///   1. SETUP     - create an mdEmail instance, hand it the license string and the
  ///                  path to the data files, then InitializeDataFiles() (one time).
  ///   2. INPUT     - feed an email address in.
  ///   3. PROCESS   - configure the lookup options, then VerifyEmail() checks and
  ///                  corrects the address.
  ///   4. READ      - pull the parsed pieces back out with the Get* getters
  ///                  (GetMailBoxName, GetDomainName, GetTopLevelDomain, ...).
  ///   5. INTERPRET - GetResults() returns comma-separated result codes describing
  ///                  what the object did/found; each code has a human description.
  ///
  /// The pieces in this file map onto that flow:
  ///   - Program        : console harness (argument parsing + the interactive loop).
  ///   - EmailObject    : thin wrapper around mdEmail that owns setup + the call sequence.
  ///   - DataContainer  : plain holder for one record's input and output.
  ///
  /// Where mdEmail comes from:
  ///   The MelissaData namespace and its mdEmail class live in mdEmail_cSharpCode.cs,
  ///   a generated C# wrapper over mdEmail.dll that the accompanying
  ///   MelissaEmailObjectWindowsDotnet.ps1 script downloads on every run.
  ///
  /// Reference:
  ///   Quickstart    : https://docs.melissa.com/on-premise-api/email-object/email-object-quickstart.html
  ///   Release notes : https://releasenotes.melissa.com/on-premise-api/email-object/
  ///   Result codes  : https://docs.melissa.com/on-premise-api/email-object/result-codes.html
  /// </remarks>
  class Program
  {
    /// <summary>
    /// Entry point. Reads the optional command-line arguments, then hands control to
    /// RunAsConsole, which performs the actual Email Object setup and processing.
    /// </summary>
    /// <param name="args">The raw command-line arguments</param>
    static void Main(string[] args)
    {
      // Populated by ParseArguments below.
      string license = "";
      string testEmail = "";
      string dataPath = "";

      ParseArguments(ref license, ref testEmail, ref dataPath, args);
      RunAsConsole(license, testEmail, dataPath);
    }

    /// <summary>
    /// Reads the supported command-line options into the ref parameters.
    ///
    /// Recognized flags (each followed by its value, e.g. "--email name@example.com"):
    ///   --license / -l   : the Melissa license string
    ///   --email / -e     : an email address to test in one-shot mode
    ///   --dataPath / -d  : path to the Email Object data files
    /// </summary>
    /// <param name="license">Receives the Melissa license string.</param>
    /// <param name="testEmail">Receives the email address to test in one-shot mode.</param>
    /// <param name="dataPath">Receives the path to the Email Object data files.</param>
    /// <param name="args">The raw command-line arguments to parse.</param>
    static void ParseArguments(ref string license, ref string testEmail, ref string dataPath, string[] args)
    {
      for (int i = 0; i < args.Length; i++)
      {
        if (args[i].Equals("--license") || args[i].Equals("-l"))
        {
          if (args[i + 1] != null)
          {
            license = args[i + 1];
          }
        }
        if (args[i].Equals("--email") || args[i].Equals("-e"))
        {
          if (args[i + 1] != null)
          {
            testEmail = args[i + 1];
          }
        }
        if (args[i].Equals("--dataPath") || args[i].Equals("-d"))
        {
          if (args[i + 1] != null)
          {
            dataPath = args[i + 1];
          }
        }
      }
    }

    /// <summary>
    /// Sets up the Email Object once, then drives the input -> process -> output cycle.
    ///
    /// In interactive mode (no --email) it loops, asking for a new email each pass until
    /// the user answers "N". In one-shot mode (--email supplied) it runs a single pass
    /// and exits.
    /// </summary>
    /// <param name="license">The Melissa license string used to initialize the object.</param>
    /// <param name="testEmail">An email address to process in one-shot mode; if empty, the program prompts interactively.</param>
    /// <param name="dataPath">Path to the Email Object data files.</param>
    static void RunAsConsole(string license, string testEmail, string dataPath)
    {
      Console.WriteLine("\n\n=========== WELCOME TO MELISSA EMAIL OBJECT WINDOWS DOTNET =========\n");

      // Construct the wrapper. This is where the object is licensed, pointed at the
      // data files, and initialized (see the EmailObject constructor below).
      EmailObject emailObject = new EmailObject(license, dataPath);

      bool shouldContinueRunning = true;

      // Gate the program on a successful initialization. If the data files could not
      // be loaded (bad/expired license, missing or wrong-path data files, ...),
      // GetInitializeErrorString() returns the reason instead of "No error." and we
      // skip the processing loop entirely.
      if (emailObject.mdEmailObj.GetInitializeErrorString() != "No error.")
      {
        shouldContinueRunning = false;
      }

      while (shouldContinueRunning)
      {
        // Holder for this pass's input and result codes.
        DataContainer dataContainer = new DataContainer();

        if (string.IsNullOrEmpty(testEmail))
        {
          // Interactive mode: prompt the user for an email address.
          Console.WriteLine("\nFill in each value to see the Email Object results");
          Console.WriteLine("Email:");

          Console.CursorTop -= 1;
          Console.CursorLeft = 7;
          dataContainer.Email = Console.ReadLine();
        }
        else
        {
          // One-shot mode: use the email passed on the command line.
          dataContainer.Email = testEmail;
        }

        // Print user input
        Console.WriteLine("\n============================== INPUTS ==============================\n");
        Console.WriteLine($"\t                Email: {dataContainer.Email}");

        // Execute Email Object
        // Runs the configure + verify sequence and stores the result codes on dataContainer.
        emailObject.ExecuteObjectAndResultCodes(ref dataContainer);

        // Print output
        // Each Get* getter below returns one component the object produced for the most
        // recently processed email. These read directly from the mdEmail instance, which
        // still holds the results from the Execute call above.
        Console.WriteLine("\n============================== OUTPUT ==============================\n");
        Console.WriteLine("\n\t     Email Object Information:");

        Console.WriteLine($"\t                       Email: {dataContainer.Email}");
        Console.WriteLine($"\t                Mailbox Name: {emailObject.mdEmailObj.GetMailBoxName()}");
        Console.WriteLine($"\t                 Domain Name: {emailObject.mdEmailObj.GetDomainName()}");
        Console.WriteLine($"\t            Top-Level Domain: {emailObject.mdEmailObj.GetTopLevelDomain()}");
        Console.WriteLine($"\tTop-Level Domain Description: {emailObject.mdEmailObj.GetTopLevelDomainDescription()}");
        Console.WriteLine($"\t                Result Codes: {dataContainer.ResultCodes}");

        // Result codes come back as a single comma-separated string (e.g. "ES01,ES21").
        // Split it and ask the object for a readable description of each code.
        // ResultCodeDescriptionLong requests the long-form text; a short form is also
        // available via ResultCodeDescriptionShort
        String[] rs = dataContainer.ResultCodes.Split(',');
        foreach (String r in rs)
          Console.WriteLine($"        {r}: {emailObject.mdEmailObj.GetResultCodeDescription(r, mdEmail.ResultCdDescOpt.ResultCodeDescriptionLong)}");

        bool isValid = false;

        // In one-shot mode there is nothing more to do after a single pass: mark the
        // input handled and stop the outer loop.
        if (!string.IsNullOrEmpty(testEmail))
        {
          isValid = true;
          shouldContinueRunning = false;
        }

        // Interactive mode: ask whether to process another email. Keep prompting until
        // we get a valid Y/N. "N" ends the program; "Y" falls through to another pass.
        while (!isValid)
        {
          Console.WriteLine("\nTest another email? (Y/N)");
          string testAnotherResponse = Console.ReadLine();

          if (!string.IsNullOrEmpty(testAnotherResponse))
          {
            testAnotherResponse = testAnotherResponse.ToLower();
            if (testAnotherResponse == "y")
            {
              isValid = true;
            }
            else if (testAnotherResponse == "n")
            {
              isValid = true;
              shouldContinueRunning = false;
            }
            else
            {
              Console.Write("Invalid Response, please respond 'Y' or 'N'");
            }
          }
        }
      }
      Console.WriteLine("\n============ THANK YOU FOR USING MELISSA DOTNET OBJECT ===========\n");
    }
  }

  /// <summary>
  /// Wrapper that owns a single Melissa Email Object instance and encapsulates the two
  /// things every Melissa object needs: one-time setup (license + data files) and the
  /// per-record processing sequence. Reuse one instance across many emails; do NOT
  /// re-initialize per email.
  /// </summary>
  class EmailObject
  {
    // Path to the Email Object data files.
    string dataFilePath;

    // The underlying Melissa Email Object instance.
    public mdEmail mdEmailObj = new mdEmail();

    /// <summary>
    /// Performs the mandatory one-time setup, in this required order:
    ///   1. SetLicenseString     - authorize the object.
    ///   2. SetPathToEmailFiles  - tell it where the data files live.
    ///   3. InitializeDataFiles  - load the data into memory.
    /// </summary>
    /// <param name="license">The Melissa license string used to authorize the object.</param>
    /// <param name="dataPath">Path to the folder containing the Email Object data files.</param>
    public EmailObject(string license, string dataPath)
    {
      // Set license string and set path to data files
      mdEmailObj.SetLicenseString(license);
      dataFilePath = dataPath;

      // Point the object at the Email Object data files.
      mdEmailObj.SetPathToEmailFiles(dataFilePath);

      // Load the data files. The returned ProgramStatus reports whether initialization succeeded.
      // If you see a different date than expected, check your license string and either download the new data files
      // or use the Melissa Updater program to update your data files.
      mdEmail.ProgramStatus pStatus = mdEmailObj.InitializeDataFiles();

      // If an issue occurred, please investigate the common causes.
      // Common causes: an invalid/expired license, or missing/wrong-path data files.
      if (pStatus != mdEmail.ProgramStatus.ErrorNone)
      {
        Console.WriteLine("Failed to Initialize Object.");
        Console.WriteLine(pStatus);
        return;
      }
      
      // Diagnostic information, handy for confirming the object loaded the data you expect:

      // Build date of the data files
      Console.WriteLine($"                DataBase Date: {mdEmailObj.GetDatabaseDate()}");

      // When the license stops working
      Console.WriteLine($"              Expiration Date: {mdEmailObj.GetLicenseStringExpirationDate()}");

      // This number should match with the file properties of the Melissa Object binary file.
      // If TEST appears with the build number, there may be a license key issue.
      Console.WriteLine($"               Object Version: {mdEmailObj.GetBuildNumber()}\n");
    }

    /// <summary>
    /// Runs the full Email Object processing sequence for one email and captures its
    /// result codes. This is the canonical per-record call pattern to copy into your
    /// own application:
    ///   configure lookup options -> VerifyEmail -> GetResults
    /// </summary>
    /// <param name="data">
    /// The record to process. Its Email is read as input, and ResultCodes is populated
    /// with this run's result codes.
    /// </param>
    public void ExecuteObjectAndResultCodes(ref DataContainer data)
    {
      // These are the configurable pieces of the Email Object - they control which checks
      // VerifyEmail performs (syntax correction, database & MX lookups, fuzzy matching, ...).
      mdEmailObj.SetCacheUse(1);
      mdEmailObj.SetCorrectSyntax(true);
      mdEmailObj.SetDatabaseLookup(true);
      mdEmailObj.SetFuzzyLookup(true);
      mdEmailObj.SetMXLookup(true);
      mdEmailObj.SetStandardizeCasing(true);
      mdEmailObj.SetWSLookup(false);

      // Validate and correct the email per the options above
      mdEmailObj.VerifyEmail(data.Email);

      // Collect the result codes for this run
      // ResultsCodes explain any issues Email Object has with the object.
      // List of result codes for Email Object
      // https://docs.melissa.com/on-premise-api/email-object/result-codes.html
      data.ResultCodes = mdEmailObj.GetResults();
    }
  }

  /// <summary>
  /// Data holder for a single record: carries the input email in and the result codes out.
  /// </summary>
  public class DataContainer
  {
    // Record identifier. Never set or read by this sample.
    public string RecID { get; set; }

    // Input: the email address to process.
    public string Email { get; set; }

    // Output: comma-separated result codes from GetResults().
    public string ResultCodes { get; set; } = "";
  }
}
