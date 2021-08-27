using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Png2Jpg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] droppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
            string folderName = Path.GetDirectoryName(droppedFiles[0]);
            string convertFolderName = "convert" + DateTime.Now.ToFileTime();
            foreach (string file in droppedFiles)
            {
                
                //MessageBox.Show(Directory.Exists(Path.Combine(folderName, convertFolderName)).ToString());
                if (!Directory.Exists(Path.Combine(folderName, convertFolderName)))
                {
                    Directory.CreateDirectory(Path.Combine(folderName, convertFolderName));
                }
                string ext = Path.GetExtension(file).ToLower();
                if (ext == ".png")
                {
                    string name = getFileName(file);
                    Image png = Image.FromFile(file);
                    
                    string fullPath = Path.Combine(folderName, convertFolderName);
                    png.Save(fullPath +"/"+ name + ".jpg", ImageFormat.Jpeg);
                    png.Dispose();
                }
                //MessageBox.Show(folderName);
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private string getFileName(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }
    }
}
