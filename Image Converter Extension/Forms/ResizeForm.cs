using System;
using System.Windows.Forms;

namespace GuusBeltman.Image_Converter_Extension.Forms
{
    public partial class ResizeForm : Form
    {
        // Define delegate
        public delegate void PassResult(ProcessingCommand sender);

        private readonly double _proportion;
        private int _initHeight;
        private int _initWidth;
        private int _prevHeight;
        private int _prevWidth;


        // Create instance (null)
        public PassResult passResult;

        public ResizeForm(int initialCustomWidth, int initialCustomHeight)
        {
            InitializeComponent();
            _prevWidth = _initWidth = initialCustomWidth;
            _prevHeight = _initHeight = initialCustomHeight;

            tbCustomHeight.Text = "" + initialCustomHeight;
            tbCustomWidth.Text = "" + initialCustomWidth;
            _proportion = initialCustomHeight/(double) initialCustomWidth;
            cbProportions.Text += " (factor = " + _proportion + ")";
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
            {
                tbCustomHeight.Enabled = true;
                tbCustomWidth.Enabled = true;
            }
            else
            {
                tbCustomHeight.Enabled = false;
                tbCustomWidth.Enabled = false;
            }
        }


        private void tbCustomWidth_Changed(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(tbCustomWidth.Text))
                tbCustomWidth.Text = "0";
            else if (tbCustomWidth.Text.Trim() != "0")
                tbCustomWidth.Text = tbCustomWidth.Text.TrimStart('0');
            int custWidth;
            if (int.TryParse(tbCustomWidth.Text, out custWidth))
            {
                if (cbProportions.Checked && _proportion > 0)
                {
                    tbCustomHeight.Text = "" + Math.Round(custWidth*_proportion, 0);
                }
            }
            else
            {
                tbCustomWidth.Text = "" + _prevWidth;
            }
        }

        private void tbCustomHeight_Changed(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(tbCustomHeight.Text))
                tbCustomHeight.Text = "0";
            else if (tbCustomHeight.Text.Trim() != "0")
                tbCustomHeight.Text = tbCustomHeight.Text.TrimStart('0');
            int custHeight;
            if (int.TryParse(tbCustomHeight.Text, out custHeight))
            {
                if (cbProportions.Checked && _proportion > 0)
                {
                    tbCustomWidth.Text = "" + Math.Round(custHeight*(1 + (1 - _proportion)), 0);
                }
            }
            else
            {
                tbCustomHeight.Text = "" + _prevHeight;
            }
        }

        private void quality_Scroll(object sender, EventArgs e)
        {
            lblQual.Text = quality.Value + "%";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (passResult != null)
            {
                var res = new ProcessingCommand
                    {
                        QualityLevel = quality.Value,
                        CreateNewFile = cbCreateNewFile.Checked,
                        CreateBackup = cbCreateBackup.Checked,
                        PostFixFile = ""
                    };

                //Optional:
                if (res.CreateNewFile)
                {
                    res.AddToSolution = cbAddToSol.Checked;
                    res.PostFixFile = tbNewPostFix.Text;
                }

                //Size:
                if (radioButton1.Checked)
                {
                    res.Width = 100;
                    res.Height = res.Width;
                }
                else if (radioButton2.Checked)
                {
                    res.Width = 320;
                    res.Height = 480;
                }
                else if (radioButton3.Checked)
                {
                    res.Width = 800;
                    res.Height = 600;
                }
                else if (radioButton4.Checked)
                {
                    res.Width = 1024;
                    res.Height = 768;
                }
                else if (radioButton5.Checked)
                {
                    res.Width = 1680;
                    res.Height = 1050;
                }
                else if (radioButton6.Checked)
                {
                    int width, height;
                    int.TryParse(tbCustomWidth.Text, out width);
                    int.TryParse(tbCustomHeight.Text, out height);
                    res.Width = width;
                    res.Height = height;
                }

                passResult(res);
            }
            Close();
        }

        private void tbCustomHeight_PreChange(object sender, KeyEventArgs e)
        {
            int.TryParse(tbCustomHeight.Text, out _prevHeight);
        }

        private void tbCustomWidth_PreChange(object sender, KeyEventArgs e)
        {
            int.TryParse(tbCustomWidth.Text, out _prevWidth);
        }

        private void cbCreateNewFile_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCreateNewFile.Checked)
            {
                cbAddToSol.Enabled = true;
                tbNewPostFix.Enabled = true;
            }
            else
            {
                cbAddToSol.Checked = false;
                cbAddToSol.Enabled = false;
                tbNewPostFix.Enabled = false;
            }
        }
    }
}