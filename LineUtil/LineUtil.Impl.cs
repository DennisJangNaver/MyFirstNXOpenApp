using System;
using System.Diagnostics;
using System.Linq;
using NXOpen;
using NXOpen.BlockStyler;
using static NXOpen.Session;

public partial class LineUtil
{

    public bool enableOKButton_Impl()
    {
        try
        {
            //---- Enter your callback code here -----
            if (this.nameInput.Value.Trim().Equals(String.Empty))
                return false;

            //

            ///

            //

        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        return true;
    }

    public int apply_Impl()
    {
        int errorCode = 0;
        UndoMarkId undoMark = theSession.SetUndoMark(Session.MarkVisibility.Visible, "LineUtil");

        try
        {
            Point start = startPointSelection.GetSelectedObjects().FirstOrDefault() as Point;
            Point end = endPointSelection.GetSelectedObjects().FirstOrDefault() as Point;

            Line newLine = theSession.Parts.Work.Curves.CreateLine(start, end);
            newLine.SetName(this.nameInput.Value);

            

            theSession.UpdateManager.DoUpdate(undoMark);
            //theSession.DeleteUndoMark(undoMark, "LineUtil");
        }
        catch (Exception ex)
        {

            //---- Enter your exception handling code here -----
            theSession.UndoToMark(undoMark, "LineUtil");

            errorCode = 1;
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        return errorCode;
    }
}
