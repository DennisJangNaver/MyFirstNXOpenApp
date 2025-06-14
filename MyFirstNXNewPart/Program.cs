﻿using NXOpen;
using System;

public class Program
{
    // class members
    private static Session theSession;


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


            isDisposeCalled = false;
        }
        catch (NXOpen.NXException ex)
        {
            // ---- Enter your exception handling code here -----
            // UI.GetUI().NXMessageBox.Show("Message", NXMessageBox.DialogType.Error, ex.Message);
        }
    }


    //------------------------------------------------------------------------------
    //  New Part
    //      This user exit is invoked after the following menu item is activated:
    //      "File->New"  

    //Will work when complete path of the dll is provided to Environment Variable 
    //USER_CREATE or USER_DEFAULT
    //------------------------------------------------------------------------------
    public static int ufcre(string[] args)
    {
        int retValue = 0;
        try
        {
            theProgram = new Program();

            //TODO: Add your application code here 
            if (theSession.ListingWindow.IsOpen == false)
            {
                theSession.ListingWindow.Open();
            }

            theSession.ListingWindow.WriteLine("Do Something to Create New Part");

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