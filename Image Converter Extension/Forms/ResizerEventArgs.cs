using System;
using GuusBeltman.Image_Converter_Extension.Helpers;
using GuusBeltman.Image_Converter_Extension.Resize;

namespace GuusBeltman.Image_Converter_Extension.Forms
{
    public class ProcessingCommand
    {
        public ProcessingCommand(int width, int height, Mode mod, ManipulateAction manipulateAction, int quality)
        {
            Width = width;
            Height = height;
            Mode = mod;
            ActionToPerform = manipulateAction;
            QualityLevel = quality;

        }

        public ProcessingCommand()
        {
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public bool CreateBackup { get; set; }
        public bool CreateNewFile { get; set; }
        public bool AddToSolution { get; set; }
        public string PostFixFile { get; set; }
        public int QualityLevel { get; set; }
        public Mode Mode
        {
            get;
            set;
        }


        public ManipulateAction ActionToPerform
        {
            get;
            set;
        }
    }
}
