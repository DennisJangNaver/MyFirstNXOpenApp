using NXOpen;
using NXOpen.Features;
using NXOpen.UF;
using System;
using System.Diagnostics;

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
        Debugger.Launch();

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
            UI.GetUI().NXMessageBox.Show("Message", NXMessageBox.DialogType.Error, ex.Message);
        }
    }


    //------------------------------------------------------------------------------
    //  Explicit Activation
    //      This entry point is used to activate the application explicitly
    //------------------------------------------------------------------------------
    public static int Main(string[] args)
    {
        int retValue = 0;
        try
        {

            theProgram = new Program();
            theProgram.Run2();
            
        }
        catch (NXOpen.NXException ex)
        {
            UI.GetUI().NXMessageBox.Show("Message", NXMessageBox.DialogType.Error, ex.Message);
            retValue = 1;
        }
        finally
        {
            if(theProgram != null)
                theProgram.Dispose();
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


    public int GetUnloadOption(string arg)
    {
        //Unloads the image explicitly, via an unload dialog
        return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);

        //Unloads the image immediately after execution within NX
        //return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);

        //Unloads the image when the NX session terminates
        // return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);


    }


    private void Run()
    {
        View selectedView = null;
        Point3d start, end;

        

        theUI.SelectionManager.SelectScreenPosition("Select Start Point.", out selectedView, out start);
        theUI.SelectionManager.SelectScreenPosition("Select End Point.", out selectedView, out end);

        var workPart = theSession.Parts.Work;
        //var dispPart = theSession.Parts.Display;

        Line newLine = workPart.Curves.CreateLine(start, end);

        newLine.SetName("MyLine");
    }

    private void Run2()
    {
        var workPart = theSession.Parts.Work;

        //Create Line Builder
        AssociativeLine lineFeature = null;
        var lineBuilder = workPart.BaseFeatures.CreateAssociativeLineBuilder(lineFeature);



        //Prepare two Points
        Point3d startCoord = new Point3d(10, 20, 30);
        Point startPt  = workPart.Points.CreatePoint(startCoord);

        Point3d endCoord = new Point3d(10, 30, 30);
        Point endPt = workPart.Points.CreatePoint(endCoord);



        //Set parameter
        lineBuilder.Associative = true;
        lineBuilder.StartPointOptions = AssociativeLineBuilder.StartOption.Point;
        lineBuilder.StartPoint.Value = startPt;

        lineBuilder.EndPointOptions = AssociativeLineBuilder.EndOption.Point;
        lineBuilder.EndPoint.Value = endPt;

        



        //Commit and Clear Resource
        var result = lineBuilder.Commit();
        lineBuilder.Destroy();

        NXObject[] items = lineFeature.GetEntities();

        if (items.Length > 0)
        {
            NXObject item = items[0];
            Line line = (Line)item;
        }
    }

}