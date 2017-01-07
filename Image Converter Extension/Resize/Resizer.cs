using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GuusBeltman.Image_Converter_Extension.Forms;
using GuusBeltman.Image_Converter_Extension.Helpers;
using Color = System.Drawing.Color;

namespace GuusBeltman.Image_Converter_Extension.Resize
{
    public class Resizer
    {
        private static readonly Type DefaultJpegEncoderType = typeof(JpegBitmapEncoder);
        private static readonly Type DefaultPngEncoderType = typeof(PngBitmapEncoder);
        private static readonly Type DefaultGifEncoderType = typeof(GifBitmapEncoder);
        private static readonly List<string> _allowedExtensions = new List<string>() { ".PNG", ".JPG", ".JPEG", ".GIF" };
        private static readonly List<string> _allowedToConvertToJpegExtensions = new List<string>() { ".PNG", ".BMP", ".TIFF", ".GIF" };
        private static readonly List<string> _allowedToConvertToPngExtensions = new List<string>() { ".JPG", ".JPEG", ".BMP", ".TIFF", ".GIF" };
        private static readonly List<string> _allowedToConvertToGifExtensions = new List<string>() { ".PNG", ".BMP", ".TIFF", ".JPG", ".JPEG" };
        
        private readonly bool _shrinkOnly;
        private readonly bool _ignoreRotations;
        private readonly ProcessingCommand _processingCmd;

        public int Count { get; set; }
        public int Optimized { get; set; }
        public ProcessingCommand Command
        { 
            get
            {
                return _processingCmd;
            }
        }

        public Resizer(ProcessingCommand cmd)
        {
            _processingCmd = cmd;
            Optimized = 0;
            Count = 0;
        }

        public static bool IsAllowed(string fileName)
        {
            return _allowedExtensions.Contains(Path.GetExtension(fileName.ToUpperInvariant()));
        }

        public static bool IsAllowedToConvertToJpeg(string fileName)
        {
            return _allowedToConvertToJpegExtensions.Contains(Path.GetExtension(fileName.ToUpperInvariant()));
        }

        public static bool IsAllowedToConvertToPng(string fileName)
        {
            return _allowedToConvertToPngExtensions.Contains(Path.GetExtension(fileName.ToUpperInvariant()));
        }
        public static bool IsAllowedToConvertToGif(string fileName)
        {
            return _allowedToConvertToGifExtensions.Contains(Path.GetExtension(fileName.ToUpperInvariant()));
        }

        public void ConvertImages(params string[] foldersAndFiles)
        {
            List<string> images = new List<string>();
            BitmapEncoder enc;
            switch (_processingCmd.ActionToPerform)
            {
                case ManipulateAction.ConvertToJpeg:
                    _allowedToConvertToJpegExtensions.ForEach(ext =>
                    {
                        Array.ForEach(foldersAndFiles, f => images.AddRange(GetImages(f, ext)));
                    });
                    enc = CreateEncoder(DefaultJpegEncoderType);
                    break;
                case ManipulateAction.ConvertToPng:
                    _allowedToConvertToPngExtensions.ForEach(ext =>
                    {
                        Array.ForEach(foldersAndFiles, f => images.AddRange(GetImages(f, ext)));
                    });
                    enc = CreateEncoder(DefaultPngEncoderType);
                    break;
                case ManipulateAction.ConvertToGif:
                    _allowedToConvertToGifExtensions.ForEach(ext =>
                    {
                        Array.ForEach(foldersAndFiles, f => images.AddRange(GetImages(f, ext)));
                    });
                    enc = CreateEncoder(DefaultGifEncoderType);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            this.Count = images.Count;
            this.Optimized = 0;

            if (this.Count > 0)
            {
                ThreadPool.QueueUserWorkItem((state) =>
                    {
                        Parallel.ForEach(images,
                                         f => { this.ConvertToSpecificType(f); });
                        this.OnCompleted();
                    });
            }
        }

        public void ResizeImages(params string[] foldersAndFiles)
        {
            List<string> images = new List<string>();
            _allowedExtensions.ForEach(ext =>
            {
                Array.ForEach(foldersAndFiles, f => images.AddRange(GetImages(f, ext)));
            });

            this.Count = images.Count;
            this.Optimized = 0;
            if (this.Count > 0)
            {
                ThreadPool.QueueUserWorkItem((state) =>
                    {
                        Parallel.ForEach(images, f => { this.Resize(f); });
                        this.OnCompleted();
                    });
            }
        }

        public void RotateImages(bool rotateRight, params string[] foldersAndFiles)
        {
            List<string> images = new List<string>();
            _allowedExtensions.ForEach(ext =>
            {
                Array.ForEach(foldersAndFiles, f => images.AddRange(GetImages(f, ext)));
            });

            this.Count = images.Count;
            this.Optimized = 0;
            if (this.Count > 0)
            {
                ThreadPool.QueueUserWorkItem((state) =>
                {
                    Parallel.ForEach(images, f => { this.Rotate(f, rotateRight); });
                    this.OnCompleted();
                });
            }
        }

        private static string[] GetImages(string folderOrFile, string extension)
        {
            if (File.Exists(folderOrFile) && folderOrFile.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
            {
                return new[] { folderOrFile };
            }
            else if (Directory.Exists(folderOrFile))
            {
                var files = Directory.GetFiles(folderOrFile, "*" + extension, SearchOption.AllDirectories);
                return Array.FindAll(files, f => !f.Contains("\\obj\\") && !f.Contains("\\bin\\"));
            }

            return new string[0];
        }

        private void Rotate(string sourcePath, bool doRotateRight)
        {
            Contract.Requires(!String.IsNullOrWhiteSpace(sourcePath));
            Contract.Ensures(!String.IsNullOrWhiteSpace(Contract.Result<string>()));

            Image img = Image.FromFile(sourcePath);
            if (doRotateRight)
            {
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            else
            {
                img.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }

            string destinationPath = sourcePath;

            var fileExists = File.Exists(destinationPath);
            var finalPath = destinationPath;
            var result = new ResizeResult(finalPath, FileUtil.CreateActionMessage(finalPath, _processingCmd));
            result.SourceFileName = sourcePath;
            result.SizeBefore = new FileInfo(sourcePath).Length;


            if (fileExists)
            {
                destinationPath = Path.GetTempFileName();
            }

            img.Save(destinationPath);

            // Move any existing file to the Recycle Bin
            if (fileExists)
            {
                OnBeforeWritingFile(result);
                // TODO: Is there a better way to do this without a reference to Microsoft.VisualBasic?
                //FileSystem.DeleteFile(finalPath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                File.Delete(finalPath);
                File.Move(destinationPath, finalPath);
                //if (_processingCmd.AddToSolution)
                 //   Ima
            }

            result.SizeAfter = new FileInfo(finalPath).Length;
            result.PercentSaved = result.SizeBefore > 0 ? Math.Round((result.SizeBefore - result.SizeAfter) / result.SizeBefore * 100, 2) : 0;

            OnProgress(new ResizerEventArgs(result));
        }

        private void ConvertToSpecificType(string sourcePath)
        {
            string destinationPath = sourcePath;
            Bitmap b;
            switch (_processingCmd.ActionToPerform)
            {
                case ManipulateAction.ConvertToJpeg:
                    //Resize(sourcePath, encoder);
                    destinationPath = sourcePath.Replace(Path.GetExtension(sourcePath), ".jpg");
                    b = new Bitmap(Image.FromFile(sourcePath));
                    b.Save(destinationPath, ImageFormat.Jpeg);
                    break;
                case ManipulateAction.ConvertToPng:
                    destinationPath = sourcePath.Replace(Path.GetExtension(sourcePath), ".png");
                    b = new Bitmap(Image.FromFile(sourcePath));
                    b.Save(destinationPath, ImageFormat.Png);
                    break;
                case ManipulateAction.ConvertToGif:
                    destinationPath = sourcePath.Replace(Path.GetExtension(sourcePath), ".gif");
                    b = new Bitmap(Image.FromFile(sourcePath));
                    b.Save(destinationPath, ImageFormat.Gif);
                    break;
            }

            var result = new ResizeResult(destinationPath, FileUtil.CreateActionMessage(destinationPath, _processingCmd));
            result.SourceFileName = sourcePath;
            result.SizeBefore = new FileInfo(sourcePath).Length;
            result.SizeAfter = new FileInfo(destinationPath).Length;
            result.PercentSaved = result.SizeBefore > 0 ? Math.Round((result.SizeBefore - result.SizeAfter) / result.SizeBefore * 100, 2) : 0;

            OnProgress(new ResizerEventArgs(result));
        }

        private void Resize(string sourcePath, BitmapEncoder overrideEncoder = null)
        {
            Contract.Requires(!String.IsNullOrWhiteSpace(sourcePath));
            Contract.Ensures(!String.IsNullOrWhiteSpace(Contract.Result<string>()));

            //string extension = Path.GetExtension(sourcePath).ToUpperInvariant();
            string destinationPath = sourcePath;
            switch (_processingCmd.ActionToPerform)
            {
                case ManipulateAction.CopyAsThumbnail:
                    _processingCmd.CreateNewFile = true;
                    _processingCmd.PostFixFile = "_thumb";
                    break;
            }

            if (_processingCmd.CreateNewFile)
            {
                destinationPath = FileUtil.GenerateNewFileName(sourcePath, _processingCmd.PostFixFile);
            }

            BitmapDecoder decoder;
            BitmapEncoder encoder;

            using (var sourceStream = File.OpenRead(sourcePath))
            {
                // NOTE: Using BitmapCacheOption.OnLoad here will read the entire file into
                //       memory which allows us to dispose of the file stream immediately
                decoder = BitmapDecoder.Create(sourceStream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }

            //See if a destination file type is specified
            if (overrideEncoder == null)
            {
                encoder = BitmapEncoder.Create(decoder.CodecInfo.ContainerFormat);

                try
                {
                    // NOTE: This will throw if the codec dose not support encoding
                    var _ = encoder.CodecInfo;
                }
                catch (NotSupportedException)
                {
                    // Fallback to JPEG encoder
                    encoder = CreateEncoder(DefaultJpegEncoderType);
                }
            }
            else
            {
                encoder = overrideEncoder;
            }

            // TODO: Copy container-level metadata if codec supports it
            SetEncoderSettings(encoder);

            
            // NOTE: Only TIFF and GIF images support multiple frames
            foreach (var sourceFrame in decoder.Frames)
            {
                // Apply the transform
                var transform = GetTransform(sourceFrame);
                var transformedBitmap = new TransformedBitmap(sourceFrame, transform);

                // TODO: Optionally copy metadata
                // Create the destination frame
                var thumbnail = sourceFrame.Thumbnail;
                var metadata = sourceFrame.Metadata as BitmapMetadata;
                var colorContexts = sourceFrame.ColorContexts;
                var destinationFrame = BitmapFrame.Create(transformedBitmap, thumbnail, metadata, colorContexts);

                encoder.Frames.Add(destinationFrame);

            }
            

            var fileExists = File.Exists(destinationPath);
            var finalPath = destinationPath;
            var result = new ResizeResult(finalPath, FileUtil.CreateActionMessage(finalPath, _processingCmd));
            result.SourceFileName = sourcePath;
            result.SizeBefore = new FileInfo(sourcePath).Length;
            

            if (fileExists)
            {
                destinationPath = Path.GetTempFileName();
            }

            using (var destinationStream = File.OpenWrite(destinationPath))
            {
                // Save the final image
                encoder.Save(destinationStream);
            }

  

            // Move any existing file to the Recycle Bin
            if (fileExists)
            {
                OnBeforeWritingFile(result);
                // TODO: Is there a better way to do this without a reference to Microsoft.VisualBasic?
                //FileSystem.DeleteFile(finalPath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                File.Delete(finalPath);
                File.Move(destinationPath, finalPath);
            }
            
            result.SizeAfter = new FileInfo(finalPath).Length;
            result.PercentSaved = result.SizeBefore > 0 ? Math.Round((result.SizeBefore - result.SizeAfter) / result.SizeBefore * 100, 2) : 0;
                    
            OnProgress(new ResizerEventArgs(result));
        }

        private void SetEncoderSettings(BitmapEncoder encoder)
        {
            Contract.Requires(encoder != null);

            var jpegEncoder = encoder as JpegBitmapEncoder;

            if (jpegEncoder != null)
            {
                jpegEncoder.QualityLevel = _processingCmd.QualityLevel;
            }
        }

        private BitmapEncoder CreateEncoder(Type encoderType)
        {
            return (BitmapEncoder)Activator.CreateInstance(encoderType);
        }
        // NOTE: This could be changed to return a TransformGroup which would allow a
        //       combination of transforms to be performed on the image
        private Transform GetTransform(BitmapSource source)
        {
            Contract.Requires(source != null);

            if (_processingCmd.Mode == Mode.KeepSize)
                return new ScaleTransform(1,1);

            var desiredWidth = _processingCmd.Width;
            var desiredHeight = _processingCmd.Height;

            if (_processingCmd.Mode != Mode.Scale)
            {
                 if (desiredWidth == 0)
                {
                    desiredWidth = desiredHeight * source.PixelWidth / source.PixelHeight;
                }
                else if (desiredHeight == 0)
                {
                    desiredHeight = desiredWidth * source.PixelHeight / source.PixelWidth;
                }
                else if (_ignoreRotations)
                {
                    if ((desiredWidth > desiredHeight) != (source.PixelWidth > source.PixelHeight))
                    {
                        var temp = desiredWidth;
                        desiredWidth = desiredHeight;
                        desiredHeight = temp;
                    }
                }
            }

            double scaleX = (double)desiredWidth / (double)source.PixelWidth;
            double scaleY = (double)desiredHeight / (double)source.PixelHeight;

            if (_processingCmd.Mode == Mode.Scale)
            {
                var minScale = Math.Min(scaleX, scaleY);

                scaleX = minScale;
                scaleY = minScale;
            }
            else if (_processingCmd.Mode != Mode.Stretch && _processingCmd.Mode != Mode.KeepSize)
            {
                throw new NotSupportedException(String.Format(CultureInfo.InvariantCulture, "The mode '{0}' is not yet supported.", _processingCmd.Mode));
            }

            if (_shrinkOnly)
            {
                var maxScale = Math.Max(scaleX, scaleY);

                if (maxScale > 1.0)
                {
                    scaleX = 1.0;
                    scaleY = 1.0;
                }
            }

            return new ScaleTransform(scaleX, scaleY);
        }

        public event EventHandler<ResizerEventArgs> Progress;
        protected void OnProgress(ResizerEventArgs eventArgs)
        {
            Optimized++;
            if (Progress != null)
           {
                Progress(this, eventArgs);
            }
        }

        public event EventHandler<EventArgs> Completed;
        protected void OnCompleted()
        {
            if (Completed != null)
            {
                Completed(this, EventArgs.Empty);
            }
        }

        public event EventHandler<ResizerEventArgs> BeforeWritingFile;
        protected void OnBeforeWritingFile(ResizeResult result)
        {
            if (BeforeWritingFile != null)
            {
                BeforeWritingFile(this, new ResizerEventArgs(result));
            }
        }



    }


}
