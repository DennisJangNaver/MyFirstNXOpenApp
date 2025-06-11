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
    //  Explicit Activation
    //      This entry point is used to activate the application explicitly
    //------------------------------------------------------------------------------
    public static int Main(string[] args)
    {
        int retValue = 0;
        try
        {
            theProgram = new Program();

            //TODO: Add your application code here 
            Part workPart = theSession.Parts.Work;

            if(workPart == null)
            {
                UI.GetUI().NXMessageBox.Show("Warning", NXMessageBox.DialogType.Warning, "Please open Part.");
                return retValue;
            }

            //.....

            CurveCollection curves = workPart.Curves;
            BodyCollection bodies = workPart.Bodies;

            int curveCount = 0;
            foreach(Curve cur in curves)
            {
                curveCount++; //curveCount = curveCount + 1;
                Guide.InfoWriteLine(cur.Name);
            }

            curveCount = curves.ToArray().Length;
            int bodyCount = bodies.ToArray().Length;

            Point3d startPoint = new Point3d(0, 0, 0);
            startPoint.Z = 10;
            Point3d endPoint = new Point3d();
            endPoint.X = 10;
            endPoint.Y = 0;
            endPoint.Z = 0;

            Line newLine = curves.CreateLine(startPoint, endPoint);
            double newLineLength = newLine.GetLength();
            newLine.SetName("NewLine");

            var points = workPart.Points;

            Point3d ptCood = new Point3d(10, 20, 30);
            points.CreatePoint(ptCood);

            
        }
        catch (NXOpen.NXException ex)
        {
            // ---- Enter your exception handling code here -----

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


    public static int GetUnloadOption(string arg)
    {
        //Unloads the image explicitly, via an unload dialog
        //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);

        //Unloads the image immediately after execution within NX
        return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);

        //Unloads the image when the NX session terminates
        //return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);


    }

}