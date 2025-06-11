using NXOpen;
using NXOpen.UF;
using System;

public class Program
{
    // class members
    private static Session theSession;
    private static UI theUI;
    private static UFSession theUfSession;
    public static Program theProgram;
    public static bool isDisposeCalled;
    //------------------------------------------------------------------------------
    // Constructor
    //------------------------------------------------------------------------------
    public Program()
    {
        try
        {
            theSession = Session.GetSession();
            theUI = UI.GetUI();
            theUfSession = UFSession.GetUFSession();
            isDisposeCalled = false;
        }
        catch (NXOpen.NXException ex)
        {
            // ---- Enter your exception handling code here -----
            // UI.GetUI().NXMessageBox.Show("Message", NXMessageBox.DialogType.Error, ex.Message);
        }
    }


    //------------------------------------------------------------------------------
    //  Save Part
    //      This user exit is invoked after the following menu item is activated:
    //      "File->Save"  

    //Will work when complete path of the dll is provided to Environment Variable 
    //USER_FILE or USER_DEFAULT
    //------------------------------------------------------------------------------
    public static int ufput(string[] args)
    {
        int retValue = 0;
        try
        {
            theProgram = new Program();

            //TODO: Add your application code here
            theSession.ListingWindow.WriteLine("Are you Saving Part?");

            theProgram.Dispose();
        }
        catch (NXOpen.NXException ex)
        {
            // ---- Enter your exception handling code here -----

        }
        return retValue;
    }


    //------------------------------------------------------------------------------
    // Following method disposes all the class members
    //------------------------------------------------------------------------------
    public void Dispose()
    {
        try
        {
            if (isDisposeCalled == false)
            {
                //TODO: Add your application code here 
            }
            isDisposeCalled = true;
        }
        catch (NXOpen.NXException ex)
        {
            // ---- Enter your exception handling code here -----

        }
    }


    public static int GetUnloadOption(string arg)
    {
        //Unloads the image explicitly, via an unload dialog
        return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);
        //Unloads the image immediately after execution within NX
        // return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);
        //Unloads the image when the NX session terminates
        //return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);

    }

}