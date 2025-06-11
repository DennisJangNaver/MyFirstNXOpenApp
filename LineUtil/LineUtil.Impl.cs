using System;
using System.Diagnostics;
using System.Linq;
using NXOpen;
using NXOpen.BlockStyler;
using NXOpen.Features;
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
            

            Point startPt = startPointSelection.GetSelectedObjects().FirstOrDefault() as Point;
            Point endPt = endPointSelection.GetSelectedObjects().FirstOrDefault() as Point;

            //Line newLine = theSession.Parts.Work.Curves.CreateLine(start, end);
            //newLine.SetName(this.nameInput.Value);
            //newLine.RedisplayObject();
            //Create Line Builder
            var workPart = theSession.Parts.Work;
            AssociativeLine lineFeature = null;
            var lineBuilder = workPart.BaseFeatures.CreateAssociativeLineBuilder(lineFeature);

            //Set parameter
            lineBuilder.Associative = true;
            lineBuilder.StartPointOptions = AssociativeLineBuilder.StartOption.Point;
            lineBuilder.StartPoint.Value = startPt;

            lineBuilder.EndPointOptions = AssociativeLineBuilder.EndOption.Point;
            lineBuilder.EndPoint.Value = endPt;

            //Commit and Clear Resource
            AssociativeLine result = lineBuilder.Commit() as AssociativeLine;
            lineBuilder.Destroy();

            result.SetName(this.nameInput.Value);

            NXObject[] items = result.GetEntities();
            if (items.Length > 0)
            {
                NXObject item = items[0];
                Line line = (Line)item;
                line.SetName(this.nameInput.Value);
            }

            theSession.UpdateManager.DoUpdate(undoMark);
            //theSession.DeleteUndoMark(undoMark, "LineUtil");
            //theSession.DisplayManager.MakeUpToDate();
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
