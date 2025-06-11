using NXOpen;
using NXOpen.Assemblies;
using NXOpen.UF;
using System;
using System.Linq;

#if !DEMO

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
            PartLoadStatus loadStatus = null;
            Part part = null;
            try
            {
                part = theSession.Parts.Open(@"C:\Siemens\NX2406\UGOPEN\SampleNXOpenApplications\DotNet\AssemblyViewer\toycar_assy.prt", out loadStatus);
                if (loadStatus.NumberUnloadedParts > 0)
                {
                    foreach (int index in Enumerable.Range(0, loadStatus.NumberUnloadedParts))
                    {
                        String unloadedPart = loadStatus.GetPartName(index);
                        Console.WriteLine(String.Format("Part '{0}' unloaded.", unloadedPart));
                    }

                    return 1;
                }
            }
            finally
            {
                if (loadStatus != null)
                    loadStatus.Dispose();
            }

            if(part != null)
            {
                PrintAssembly(part);

                PartCloseResponses response = theSession.Parts.NewPartCloseResponses();
                theSession.Parts.CloseAll(BasePart.CloseModified.CloseModified, response);
                response.Dispose();
            }
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

    private static void PrintAssembly(Part part)
    {
        Component rootComponent = part.ComponentAssembly.RootComponent;
        if (rootComponent != null)
        {
            PrintComponent(rootComponent);
        }
    }

    private static void PrintComponent(Component component)
    {
        Part part = component.Prototype as Part;
        String componentInfo = String.Format("{0} => {1}", component.Name, part.Name);
        Console.WriteLine(componentInfo);
        PrintExpressions(part);

        Component[] childComponents = component.GetChildren();
        for (int i = 0; i < childComponents.Length; i++)
        {
            Component childComponent = childComponents[i];
            PrintComponent(childComponent);
        }
    }

    private static void PrintExpressions(Part part)
    {
        foreach (Expression expression in part.Expressions)
        {
            String message = expression.Name + " = " + expression.RightHandSide;
            theSession.LogFile.WriteLine(message);
            Console.WriteLine(message);
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