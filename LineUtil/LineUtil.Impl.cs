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
            //CreateLine();
            Extrude();

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
    private void Extrude()
    {
        Part workPart = theSession.Parts.Work;

        Feature nullFeature = null;
        ExtrudeBuilder builder = workPart.Features.CreateExtrudeBuilder(nullFeature);

        TaggedObject[] selectedArray = this.curveSelection1.GetSelectedObjects();
        Section section = workPart.Sections.CreateSection();

        //Curve[] curves = Array.ConvertAll<TaggedObject, Curve>(selected, item => item as Curve);
        //SelectionIntentRule rule = workPart.ScRuleFactory.CreateRuleCurveDumb(curves);
        //section.AddToSection(new SelectionIntentRule[] { rule }, null, null, null, new Point3d(), Section.Mode.Create);
        
        foreach(TaggedObject selected in selectedArray)
        {
            SelectionIntentRule rule = null;
            if (selected is Edge)
            {
                Edge edge = selected as Edge;
                rule = workPart.ScRuleFactory.CreateRuleEdgeDumb(new Edge[] { edge });
            }
            else if (selected is Curve)
            {
                Curve curve = selected as Curve;
                rule = workPart.ScRuleFactory.CreateRuleCurveDumb(new Curve[] { curve });
            }

            section.AddToSection(new SelectionIntentRule[] { rule }, null, null, null, new Point3d(), Section.Mode.Create);
        }
        
        builder.Section = section;

        builder.Limits.StartExtend.Value.Value = 10.0;
        builder.Limits.EndExtend.Value.RightHandSide = "10.0*2";

        Point3d origin = new Point3d(0, 0, 0);
        Vector3d vector = new Vector3d(1, 0, 0);
        Direction direction = workPart.Directions.CreateDirection(origin, vector, Direction.UpdateOption.WithinModeling);

        builder.Direction = direction;

        Extrude feature = builder.CommitFeature() as Extrude;
        Body[] bodies = feature.GetBodies();
        foreach(Body body in bodies)
        {
            body.SetName("Body by Extrude by Open API");
        }

        builder.Destroy();
        section.Destroy();
    }

    private void CreateLine()
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

        //
        result.SetName(this.nameInput.Value);

        NXObject[] items = result.GetEntities();
        if (items.Length > 0)
        {
            NXObject item = items[0];
            Line line = item as Line;
            line.SetName(this.nameInput.Value);
        }
    }
}
