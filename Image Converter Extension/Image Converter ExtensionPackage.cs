using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using System.Text;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using GuusBeltman.Image_Converter_Extension.Forms;
using GuusBeltman.Image_Converter_Extension.Helpers;
using GuusBeltman.Image_Converter_Extension.Resize;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;

namespace GuusBeltman.Image_Converter_Extension
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidImage_Converter_ExtensionPkgString)]
    [ProvideAutoLoad("f1123ef8-92ec-443c-9ed7-fdadf150da82")]
    public sealed class Image_Converter_ExtensionPackage : Package
    {
        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public Image_Converter_ExtensionPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }

        private DTE2 dte;

        // For logging to Pane window:
        private IVsOutputWindowPane pane;
        double allBefore, allAfter, processed;
        
        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteLine (string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();
            dte = (EnvDTE.DTE)GetService(typeof(EnvDTE.DTE)) as DTE2;

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if ( null != mcs )
            {
                // Create the command for the menu item.
                /*
                CommandID menuCommandID = new CommandID(GuidList.guidImage_Converter_ExtensionCmdSet, (int)PkgCmdIDList.cmdidImageConverter);
                MenuCommand menuItem = new MenuCommand(MenuItemCallback, menuCommandID );
                mcs.AddCommand( menuItem );*/
                
                /*
                //Main
                CommandID queryStatusCommandID = new CommandID(GuidList.guidDynamicMenuDevelopmentCmdSetPart2, (int)PkgCmdIDList.cmdImageOptimizerQueryStatus);
                OleMenuCommand queryStatusMenuCommand = new OleMenuCommand(RecompressCallback, queryStatusCommandID);
                mcs.AddCommand(queryStatusMenuCommand);
                queryStatusMenuCommand.BeforeQueryStatus += new EventHandler(createThumbMenuCommand_BeforeQueryStatus);
                 */

                // Create thumbnail
                CommandID createThumbCommandID = new CommandID(GuidList.guidCreateThumbnail, (int)PkgCmdIDList.cmdCreateThumbnail);
                OleMenuCommand createThumbMenuCommand = new OleMenuCommand(CopyAsThumbnailCallback, createThumbCommandID);
                createThumbMenuCommand.BeforeQueryStatus += createThumbMenuCommand_BeforeQueryStatus;
                mcs.AddCommand(createThumbMenuCommand);
                

                // Recompress image
                CommandID compressImageID = new CommandID(GuidList.guidRecompress, (int)PkgCmdIDList.cmdRecompressImg);
                OleMenuCommand compressImageMenuCommand = new OleMenuCommand(RecompressCallback, compressImageID);
                compressImageMenuCommand.BeforeQueryStatus += createThumbMenuCommand_BeforeQueryStatus;
                mcs.AddCommand(compressImageMenuCommand);

                // Convert to JPEG
                CommandID convertToJpegID = new CommandID(GuidList.guidConvertToJpeg, (int)PkgCmdIDList.cmdConvertToJpeg);
                OleMenuCommand convertToJpegMenuCommand = new OleMenuCommand(ConvertToJpegCallback, convertToJpegID);
                convertToJpegMenuCommand.BeforeQueryStatus += checkIsNotJpeg_BeforeQueryStatus;
                mcs.AddCommand(convertToJpegMenuCommand);

                // Rotate left
                CommandID rotateLeftID = new CommandID(GuidList.guidRotateLeft, (int)PkgCmdIDList.cmdRotateLeft);
                OleMenuCommand rotateLeftMenuCommand = new OleMenuCommand(RotateLeftCallback, rotateLeftID);
                rotateLeftMenuCommand.BeforeQueryStatus += createThumbMenuCommand_BeforeQueryStatus;
                mcs.AddCommand(rotateLeftMenuCommand);

                // Rotate right
                CommandID rotateRightID = new CommandID(GuidList.guidRotateRight, (int)PkgCmdIDList.cmdRotateRight);
                OleMenuCommand rotateRightMenuCommand = new OleMenuCommand(RotateRightCallback, rotateRightID);
                rotateRightMenuCommand.BeforeQueryStatus += createThumbMenuCommand_BeforeQueryStatus;
                mcs.AddCommand(rotateRightMenuCommand);

                // Advanced resize
                CommandID advResizeID = new CommandID(GuidList.guidAdvancedResize, (int)PkgCmdIDList.cmdAdvancedResize);
                OleMenuCommand advResizeMenuCommand = new OleMenuCommand(AdvancedResizeCallback, advResizeID);
                advResizeMenuCommand.BeforeQueryStatus += createThumbMenuCommand_BeforeQueryStatus;
                mcs.AddCommand(advResizeMenuCommand);

                // Convert to PNG
                CommandID convertToPngID = new CommandID(GuidList.guidConvertToPng, (int)PkgCmdIDList.cmdConvertToPng);
                OleMenuCommand convertToPngMenuCommand = new OleMenuCommand(ConvertToPngCallback, convertToPngID);
                convertToPngMenuCommand.BeforeQueryStatus += checkIsNotPng_BeforeQueryStatus;
                mcs.AddCommand(convertToPngMenuCommand);

                // Convert to GIF
                CommandID convertToGifID = new CommandID(GuidList.guidConvertToGif, (int)PkgCmdIDList.cmdConvertToGif);
                OleMenuCommand convertToGifMenuCommand = new OleMenuCommand(ConvertToGifCallback, convertToGifID);
                convertToGifMenuCommand.BeforeQueryStatus += checkIsNotGif_BeforeQueryStatus;
                mcs.AddCommand(convertToGifMenuCommand);

            }
        }

        private void ConvertToGifCallback(object sender, EventArgs e)
        {
            ShowInitPane();

            var res = new Resizer(new ProcessingCommand(0, 0, Mode.KeepSize, ManipulateAction.ConvertToGif, 85));
            res.Progress += resizer_Progress;
            res.Completed += resizer_Completed;
            res.BeforeWritingFile += resizer_BeforeWritingFile;
            res.ConvertImages(GetSelectedItemPaths().ToArray());
        }

        private void ConvertToPngCallback(object sender, EventArgs e)
        {
            ShowInitPane();

            var res = new Resizer(new ProcessingCommand(0, 0, Mode.KeepSize, ManipulateAction.ConvertToPng, 85));
            res.Progress += resizer_Progress;
            res.Completed += resizer_Completed;
            res.BeforeWritingFile += resizer_BeforeWritingFile;
            res.ConvertImages(GetSelectedItemPaths().ToArray());
        }

        private void AdvancedResizeCallback(object sender, EventArgs e)
        {
            var form = new ResizeForm(1024, 768);
            form.passResult += PerformResize;
            form.ShowDialog();

        }

        private void PerformResize(ProcessingCommand cmd)
        {
            ShowInitPane();

            var res = new Resizer(cmd);
            res.Progress += resizer_Progress;
            res.Completed += resizer_Completed;
            if (cmd.CreateBackup)
                res.BeforeWritingFile += resizer_BeforeWritingFileMakeBackup;
            else
                res.BeforeWritingFile += resizer_BeforeWritingFile;
            res.ResizeImages(GetSelectedItemPaths().ToArray());
        }

        #endregion

        private void CopyAsThumbnailCallback(object sender, EventArgs e)
        {
            ShowInitPane();
            var cmd = new ProcessingCommand(100, 100, Mode.Stretch, ManipulateAction.CopyAsThumbnail, 85);
            cmd.AddToSolution = true;
            cmd.CreateNewFile = true;

            var res = new Resizer(cmd);
            res.Progress += resizer_Progress;
            res.Completed += resizer_Completed;
            res.BeforeWritingFile += resizer_BeforeWritingFile;
            res.ResizeImages(GetSelectedItemPaths().ToArray());
        }

        private void RecompressCallback(object sender, EventArgs e)
        {
            ShowInitPane();

            var res = new Resizer(new ProcessingCommand(0, 0, Mode.KeepSize, ManipulateAction.ReCompressWithQuality, 85));
            res.Progress += resizer_Progress;
            res.Completed += resizer_Completed;
            if (DialogResult.Yes ==
                MessageBox.Show("Create a backup of original file?", "Backup?", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question))
            {
                res.BeforeWritingFile += resizer_BeforeWritingFileMakeBackup;
            }
            else
            {
                res.BeforeWritingFile += resizer_BeforeWritingFile;   
            }
            res.ResizeImages(GetSelectedItemPaths().ToArray());
        }

        private void ConvertToJpegCallback(object sender, EventArgs e)
        {
            ShowInitPane();

            var res = new Resizer(new ProcessingCommand(0, 0, Mode.KeepSize, ManipulateAction.ConvertToJpeg, 85));
            res.Progress += resizer_Progress;
            res.Completed += resizer_Completed;
            res.BeforeWritingFile += resizer_BeforeWritingFile;
            res.ConvertImages(GetSelectedItemPaths().ToArray());
        }

        private void RotateLeftCallback(object sender, EventArgs e)
        {
            ShowInitPane();

            var res = new Resizer(new ProcessingCommand(0, 0, Mode.KeepSize, ManipulateAction.Rotate, 85));
            res.Progress += resizer_Progress;
            res.Completed += resizer_Completed;
            res.BeforeWritingFile += resizer_BeforeWritingFile;

            res.RotateImages(false, GetSelectedItemPaths().ToArray());
        }

        private void RotateRightCallback(object sender, EventArgs e)
        {
            ShowInitPane();

            var res = new Resizer(new ProcessingCommand(0, 0, Mode.KeepSize, ManipulateAction.Rotate, 85));
            res.Progress += resizer_Progress;
            res.Completed += resizer_Completed;
            res.BeforeWritingFile += resizer_BeforeWritingFile;
   
            res.RotateImages(true, GetSelectedItemPaths().ToArray());
        }
        
        private void ResetCounters()
        {
            processed = allBefore = allAfter = 0;
        }

        private void createThumbMenuCommand_BeforeQueryStatus(object sender, EventArgs e)
        {
            OleMenuCommand menuCommand = sender as OleMenuCommand;
            var items = GetSelectedItemPaths();
            menuCommand.Enabled = items.Count(f => Resizer.IsAllowed(f)) > 0;
        }

        private void checkIsNotJpeg_BeforeQueryStatus(object sender, EventArgs e)
        {
            OleMenuCommand menuCommand = sender as OleMenuCommand;
            var items = GetSelectedItemPaths();
            menuCommand.Enabled = items.Count(f => Resizer.IsAllowedToConvertToJpeg(f)) > 0;
        }
        private void checkIsNotPng_BeforeQueryStatus(object sender, EventArgs e)
        {
            OleMenuCommand menuCommand = sender as OleMenuCommand;
            var items = GetSelectedItemPaths();
            menuCommand.Enabled = items.Count(f => Resizer.IsAllowedToConvertToPng(f)) > 0;
        }
        private void checkIsNotGif_BeforeQueryStatus(object sender, EventArgs e)
        {
            OleMenuCommand menuCommand = sender as OleMenuCommand;
            var items = GetSelectedItemPaths();
            menuCommand.Enabled = items.Count(f => Resizer.IsAllowedToConvertToGif(f)) > 0;
        }
        #region utilities
        private IEnumerable<string> GetSelectedItemPaths()
        {
            var items = (Array)dte.ToolWindows.SolutionExplorer.SelectedItems;
            foreach (UIHierarchyItem selItem in items)
            {
                var item = selItem.Object as ProjectItem;
                if (item != null)
                {
                    yield return item.Properties.Item("FullPath").Value.ToString();
                }
            }
        }

        private void AddToSolution(string sourceFile, string newFile)
        {
            try
            {
                ProjectItem sf = dte.Solution.FindProjectItem(sourceFile);
                if (sf != null && !sourceFile.Equals(newFile, StringComparison.OrdinalIgnoreCase))
                {
                    ProjectItem folder = sf.Properties.Parent as ProjectItem;
                    if (folder != null)
                    {
                        dte.ItemOperations.AddExistingItem(newFile);
                            //folderfolder.sf.ProjectItems.Item())).Parent as ProjectItem;
                        //folder.ProjectItems.AddFromFile(newFile);
                    }
                }
            }
            catch
            {
            }
        }

        private void ShowInitPane()
        {
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindOutput).Visible = true;
            dte.StatusBar.Animate(true, vsStatusAnimation.vsStatusAnimationGeneral);

            pane = base.GetOutputPane(VSConstants.GUID_OutWindowDebugPane, "Debug");
            pane.Activate();

            ResetCounters();
            WriteToPaneLog("\n------ Image operations started -----");
        }

        private void WriteToPaneLog(string text)
        {
            pane.OutputString(text);
        }
        #endregion



        #region events

        void resizer_BeforeWritingFile(object sender, ResizerEventArgs e)
        {
            try
            {
                if (dte.SourceControl.IsItemUnderSCC(e.Result.DestFileName) && !dte.SourceControl.IsItemCheckedOut(e.Result.DestFileName))
                    dte.SourceControl.CheckOutItem(e.Result.DestFileName);
                //Check if file is part of solution
                if (dte.Solution.FindProjectItem(e.Result.DestFileName) == null)
                {
                    var proj = dte.Solution.FindProjectItem(e.Result.SourceFileName).ContainingProject;    
                    if (proj != null && dte.SourceControl.IsItemUnderSCC(proj.FileName) && !dte.SourceControl.IsItemCheckedOut(proj.FileName))
                    {
                         dte.SourceControl.CheckOutItem(proj.FileName);
                    }

                }
            }
            catch
            {
                // Do nothing
            }
        }

        void resizer_BeforeWritingFileMakeBackup(object sender, ResizerEventArgs e)
        {
            resizer_BeforeWritingFile(sender, e);
            string backupFile = FileUtil.GenerateNewFileName(e.Result.SourceFileName, "_backup");
            if (File.Exists(backupFile))
                File.Delete(backupFile);
            File.Copy(e.Result.SourceFileName, backupFile);
            WriteToPaneLog("\nCreated backup: " + backupFile);
        }

        private void resizer_Progress(object sender, ResizerEventArgs e)
        {
            Resizer resizer = (Resizer)sender;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(Environment.NewLine + Path.GetFileName(e.Result.DestFileName) + " - " + e.Result.ConversionMsg);

            if (e.Result.HasError)
            {
                WriteToPaneLog(e.Result.ErrorMessage);
            }
            else
            {
                 switch (resizer.Command.ActionToPerform)
                 {
                     case ManipulateAction.AdvancedConvert:
                         if (resizer.Command.CreateNewFile)
                             sb.AppendLine("Created new image: " + e.Result.DestFileName);
                         sb.AppendLine("Resized image: " + FileUtil.ConvertSize(e.Result.SizeAfter));
                         sb.AppendLine("Difference: " + e.Result.PercentSaved + "%");
                         
                         resizer.Command.AddToSolution = true;
                         break;                    
                     case ManipulateAction.Rotate:
                         sb.AppendLine("Rotate: " + e.Result.DestFileName);
                         break;
                     case ManipulateAction.CopyAsThumbnail:
                         sb.AppendLine("Thumbnail: " + FileUtil.ConvertSize(e.Result.SizeAfter));
                         resizer.Command.AddToSolution = true;
                         break;
                     case ManipulateAction.ConvertToJpeg:
                         sb.AppendLine("Original: " + FileUtil.ConvertSize(e.Result.SizeBefore));
                         sb.AppendLine("New JPG: " + FileUtil.ConvertSize(e.Result.SizeAfter));
                         sb.AppendLine("Difference: " + e.Result.PercentSaved + "%");
                         resizer.Command.AddToSolution = true;
                         break;
                     case ManipulateAction.ConvertToPng:
                         sb.AppendLine("Original: " + FileUtil.ConvertSize(e.Result.SizeBefore));
                         sb.AppendLine("New PNG: " + FileUtil.ConvertSize(e.Result.SizeAfter));
                         sb.AppendLine("Difference: " + e.Result.PercentSaved + "%");
                         resizer.Command.AddToSolution = true;
                         break;
                     case ManipulateAction.ConvertToGif:
                         sb.AppendLine("Original: " + FileUtil.ConvertSize(e.Result.SizeBefore));
                         sb.AppendLine("New GIF: " + FileUtil.ConvertSize(e.Result.SizeAfter));
                         sb.AppendLine("Difference: " + e.Result.PercentSaved + "%");
                         resizer.Command.AddToSolution = true;
                         break;
                     case ManipulateAction.ReCompressWithQuality:
                        sb.AppendLine("Original: " + FileUtil.ConvertSize(e.Result.SizeBefore));
                        sb.AppendLine("Recompressed: " + FileUtil.ConvertSize(e.Result.SizeAfter));
                        sb.AppendLine("Savings: " + e.Result.PercentSaved + "%");
                        break;
                     default:
                         throw new ArgumentOutOfRangeException();
                 }
                if (resizer.Command.AddToSolution)
                    AddToSolution(e.Result.SourceFileName, e.Result.DestFileName);
 

                processed++;
                allBefore += e.Result.SizeBefore;
                allAfter += e.Result.SizeAfter;
            }

            WriteToPaneLog(sb.ToString());
            dte.StatusBar.Progress(true, e.Result.DestFileName, resizer.Optimized, resizer.Count);
        }


        private void resizer_Completed(object sender, EventArgs e)
        {
            Resizer resizer = (Resizer)sender;
            
            if (resizer.Optimized > 0)
            {
                double percentConvert;
                switch (resizer.Command.ActionToPerform)
                {
                    case ManipulateAction.CopyAsThumbnail:
                        WriteToPaneLog("\n" + processed + " thumbnail files created. " + (resizer.Count - processed) + " skipped. Total size of thumbs " + FileUtil.ConvertSize(allAfter));
                        break;
                    case ManipulateAction.Rotate:
                        WriteToPaneLog("\n" + processed + " rotated. " + (resizer.Count - processed) + " skipped.");
                        break;
                    case ManipulateAction.ConvertToJpeg:
                        percentConvert = allBefore > 0 ? Math.Round((allBefore - allAfter) / allBefore * 100, 2) : 0;
                        WriteToPaneLog("\n" + processed + " converted to JPEG. " + (resizer.Count - processed) + " skipped. Total savings " + percentConvert + "% = " + FileUtil.ConvertSize(allBefore - allAfter));
                        break;
                    case ManipulateAction.ConvertToGif:
                        percentConvert = allBefore > 0 ? Math.Round((allBefore - allAfter) / allBefore * 100, 2) : 0;
                        WriteToPaneLog("\n" + processed + " converted to GIF. " + (resizer.Count - processed) + " skipped. Total savings " + percentConvert + "% = " + FileUtil.ConvertSize(allBefore - allAfter));
                        break;
                    case ManipulateAction.ConvertToPng:
                        percentConvert = allBefore > 0 ? Math.Round((allBefore - allAfter) / allBefore * 100, 2) : 0;
                        WriteToPaneLog("\n" + processed + " converted to PNG. " + (resizer.Count - processed) + " skipped. Total savings " + percentConvert + "% = " + FileUtil.ConvertSize(allBefore - allAfter));
                        break;
                    case ManipulateAction.ReCompressWithQuality:
                        percentConvert = allBefore > 0 ? Math.Round((allBefore - allAfter) / allBefore * 100, 2) : 0;
                        WriteToPaneLog("\n" + processed + " processed. " + (resizer.Count - processed) + " skipped. Total savings " + percentConvert + "% = " + FileUtil.ConvertSize(allBefore - allAfter));
                        break;
                }
            }
            else
            {
                WriteToPaneLog("\nNo PNG or JPG images in selected folder(s)\n");
            }

            WriteToPaneLog("\n------ Image operations finished -----\n");
            dte.StatusBar.Progress(false);
            dte.StatusBar.Animate(false, vsStatusAnimation.vsStatusAnimationGeneral);
        }
        #endregion


    }
}
