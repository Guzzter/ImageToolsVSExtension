using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuusBeltman.Image_Converter_Extension.Resize
{
    public class ResizerEventArgs : EventArgs
    {
        public ResizerEventArgs(ResizeResult result)
        {
            this.Result = result;
        }

        public ResizeResult Result { get; private set; }
    }

    public class ResizeResult
    {
        public ResizeResult(string fileName, string conversionMsg)
        {
            this.SourceFileName = fileName;
            this.DestFileName = fileName;
            this.ConversionMsg = conversionMsg;
        }

        public double SizeBefore { get; internal set; }
        public double SizeAfter { get; internal set; }
        public double PercentSaved { get; internal set; }
        public string SourceFileName { get; internal set; }
        public string DestFileName { get; internal set; }
        public string ErrorMessage { get; internal set; }
        public string ConversionMsg { get; internal set; }

        public bool HasError
        {
            get { return !string.IsNullOrEmpty(ErrorMessage); }
        }
    }
}
