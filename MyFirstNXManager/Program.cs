using NXOpen;
using NXOpen.PDM;
using NXOpen.UF;
using NXOpenUI;
using System;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;
using NXOpen.Utilities;

namespace MyFirstNXManager
{

    public class Program
    {
        // class members
        internal static Session theSession;
        internal static UI theUI;
        internal static UFSession theUfSession;
        internal static PdmSession thePdmSession;
        public static Program theProgram;
        public static bool isDisposeCalled;

        // The active WinForm
        //private static NXManagerDemo manager;
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
                thePdmSession = theSession.PdmSession;

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
            Debugger.Launch();

            int retValue = 0;
            try
            {
                theProgram = new Program();

                //TODO: Add your application code here 
                //manager = new NXManagerDemo();
                //FormUtilities.SetApplicationIcon(manager);
                //FormUtilities.ReparentForm(manager);

                //manager.Show();
                theProgram.Run();


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
            //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);

            //Unloads the image immediately after execution within NX
            return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);

            //Unloads the image when the NX session terminates
            //return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);


        }

        public static int UnloadLibrary(string arg)
        {
            //if (manager != null)
            //{
            //    manager.Close();
            //    manager.Dispose();
            //    manager = null;
            //}
            return 0;
        }

        private void Run()
        {
            this.QueryItem();
        }

        private void GenerateNewId()
        {
            //PartNameGenerator nameGenerator = thePdmSession.PartNameGenerator;
            //PartNameGenerator.PartAssignInputInfo info = new PartNameGenerator.PartAssignInputInfo();
            //nameGenerator.AutoAssignPartParametersForCreate(info);

            //this.PartIdInput.Text = info.BasePartNumber;
            //this.PartRevInput.Text = info.BasePartRevision;
            //this.PartNameInput.Text = info.BasePartName;

            UFUgmgr.NewPartNoFnT newPartNo = new UFUgmgr.NewPartNoFnT(PartNoGenerated);
            theUfSession.Ugmgr.AskNewPartNo(ref newPartNo);
        }


        public int PartNoGenerated(IntPtr partNoPtr)
        {
            string partNo = Marshal.PtrToStringAnsi(partNoPtr);  
            return 0; // Success
        }

        private void CreateNewPart()
        {
            try
            {
                String partNo = "001999";
                String partRev = "A";
                String partFileType = "master";

                String encodedName;
                theUfSession.Ugmgr.EncodePartFilename(partNo, partRev, partFileType, "", out encodedName);

                Tag partTag;
                theUfSession.Ugmgr.NewPartFromTemplate(encodedName, "UGMASTER", "", out partTag);

                Part part = NXObjectManager.Get(partTag) as Part;
                theUfSession.Part.Save();
            }
            catch(Exception ex)
            {
                Program.theUI.NXMessageBox.Show("NX", NXMessageBox.DialogType.Error, ex.ToString());
            }
        }


        private void CreateNewPartNew()
        {
            try
            {
                FileNew fileNew = Program.theSession.Parts.FileNew();
                fileNew.UseBlankTemplate = true;
                fileNew.TemplateType = FileNewTemplateType.Item;

                PartOperationCreateBuilder builder = thePdmSession.CreateCreateOperationBuilder(PartOperationBuilder.OperationType.Create);
                fileNew.SetPartOperationCreateBuilder(builder);
                builder.SetOperationSubType(PartOperationCreateBuilder.OperationSubType.FromTemplate);
                builder.SetModelType("master");
                builder.SetItemType("Item");

                LogicalObject[] logicalObjects = null;
                builder.CreateLogicalObjects(out logicalObjects);

                logicalObjects[0].SetAttribute("item_id", "001388");
                logicalObjects[0].SetAttribute("item_revision_id", "A");
                logicalObjects[0].SetAttribute("object_name", "Some Item");

                NXObject[] objects = new NXObject[] { logicalObjects[0] };
                builder.AutoAssignAttributes(objects);

                builder.Validate();
                builder.CreateSpecificationsForLogicalObjects(logicalObjects);
                NXObject newFile = fileNew.Commit();
            }
            catch (Exception ex)
            {
                Program.theUI.NXMessageBox.Show("NX", NXMessageBox.DialogType.Error, ex.ToString());
            }
        }

        private void OpenItem()
        {
            //String pdmPartName = "@DB/" + this.PartNoInput.Text.Trim() + "/" + this.PartRevInput.Text.Trim();
            String encodedName;
            theUfSession.Ugmgr.EncodePartFilename("001302", "A", "", "", out encodedName);

            //PartLoadStatus loadStatus;
            //BasePart part = theSession.Parts.OpenBase(encodedName, out loadStatus);
            //PdmPart pdmPart = part.PDMPart;

            Tag partTag;
            UFPart.LoadStatus loadStatus;
            theUfSession.Part.Open(encodedName, out partTag, out loadStatus);

            if (!loadStatus.failed)
            {
                Part part = NXObjectManager.Get(partTag) as Part;
                PdmPart pdmPart = part.PDMPart;

                String owner, group;
                pdmPart.GetOwnerAndGroup(out owner, out group);
            }

            string ItemUid;
            string ItemRevUid;
            parseUIDsfromEncodedPartName(encodedName, out ItemUid, out ItemRevUid);
        }

        static void parseUIDsfromEncodedPartName(string encodedName, out string itemUid, out string itemRevUid)
        {
            int start = encodedName.IndexOf("PH=") + 4;
            int end = encodedName.IndexOf("PRH=") - 1;
            itemUid = encodedName.Substring(start, end - start);

            start = end + 5;
            end = encodedName.IndexOf("PN=") - 1;
            itemRevUid = encodedName.Substring(start, end - start);
        }

        private void GetTCInfo()
        {
            var lw = Program.theSession.ListingWindow;
            lw.Open();

            bool isActive;
            Program.theUfSession.UF.IsUgmanagerActive(out isActive);
            if (!isActive)
            {
                Program.theUI.NXMessageBox.Show("NX", NXMessageBox.DialogType.Warning, "NXManager Not Active.");
                return;
            }

            lw.WriteLine(thePdmSession.GetUserName());
            lw.WriteLine(thePdmSession.GetUserRole());
            lw.WriteLine(thePdmSession.GetUserGroup());

            // Tag folderTag;
            String[] itemTypes = thePdmSession.GetItemTypes();
            foreach (String itemType in itemTypes)
            {
                lw.WriteLine(itemType);
            }

            Tag partTag;
            theUfSession.Ugmgr.AskPartTag("001302", out partTag);

            Tag[] revTags;
            int revCount = 0;
            theUfSession.Ugmgr.ListPartRevisions(partTag, out revCount, out revTags);

            Tag lastRev = revTags.Last();
            String revId;
            theUfSession.Ugmgr.AskPartRevisionId(lastRev, out revId);

            //thePdmSession.CreateFolder("Hello", "NewStuff");

            Tag rootFolder;
            theUfSession.Ugmgr.AskRootFolder(out rootFolder);

            String rootFolderName;
            theUfSession.Ugmgr.AskFolderName(rootFolder, out rootFolderName);

            lw.WriteLine("Root Folder = " + rootFolderName);

            theUfSession.Ugmgr.AddToFolder(partTag, rootFolder);

            int contentCount;
            Tag[] contentTags;
            theUfSession.Ugmgr.ListFolderContents(rootFolder, out contentCount, out contentTags);

            foreach (Tag contentTag in contentTags)
            {
                UFUgmgr.ObjectType objectType;
                theUfSession.Ugmgr.AskObjectType(contentTag, out objectType);
                if (objectType == UFUgmgr.ObjectType.TypePart)
                {
                    String partName, partDesc;
                    theUfSession.Ugmgr.AskPartNameDesc(contentTag, out partName, out partDesc);
                    lw.WriteLine("*" + partName);
                }
                else if (objectType == UFUgmgr.ObjectType.TypeFolder)
                {
                    String folderName;
                    theUfSession.Ugmgr.AskFolderName(contentTag, out folderName);
                    lw.WriteLine(folderName);
                }
            }

            //TaggedObject revTag = NXObjectManager.Get(lastRev);



            //String partAsString = "@DB/Name/Id";

        }

        private void QueryItem()
        {
            var lw = Program.theSession.ListingWindow;
            lw.Open();

            PdmSearchManager searchManager = theSession.PdmSearchManager;
            PdmSearch search = searchManager.NewPdmSearch();

            String[] entries = new String[] { "object_name", "object_type" };
            String[] values = new string[] { "*00*", "ItemRevision" };
            SearchResult result = search.Advanced(entries, values);

            var names = result.GetResultObjectNames();
            foreach (String name in names)
            {
                lw.WriteLine(name);
            }
        }

    }

}