using NXOpen;
using NXOpen.Assemblies;
using NXOpen.UF;
using System;

#if DEMO

public class Program
{
    // class members
    private static Session theSession;

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
            Console.WriteLine("Good NX Open External!!!");

            String fileName = @"C:\Siemens\NX2406\UGOPEN\SampleNXOpenApplications\DotNet\AssemblyViewer\toycar_assy.prt";
            PartLoadStatus loadStatus = null;
            Part part = theSession.Parts.Open(fileName, out loadStatus);

            if (loadStatus.NumberUnloadedParts > 0)
            {
                
            }

            Component topAssy = part.ComponentAssembly.RootComponent;
            PrintComponent(topAssy);            

            //

            theProgram.Dispose();
        }
        catch (NXOpen.NXException ex)
        {
            // ---- Enter your exception handling code here -----

        }
        return retValue;
    }

    static void PrintComponent(Component component)
    {
        //Component Info.....
        Part part = component.Prototype as Part;

        Console.WriteLine(component.Name + " ==> " + part.Name);
        PrintExpression(part);

        Component[] children = component.GetChildren();
        foreach (Component child in children)
        {
            PrintComponent(child);
        }
    }

    static void PrintExpression(Part part)
    {
        foreach (Expression expr in part.Expressions)
        {
            Console.WriteLine(expr.Name + " = " + expr.RightHandSide);
        }
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


}


#endif