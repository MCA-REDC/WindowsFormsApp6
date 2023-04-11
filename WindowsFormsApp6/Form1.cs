using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFile();
        }
        private void openFile()
        {
           
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFile.FileName);
            }
        }

        private void asciitizeEm()
        {
            // recitrieving and asciitizing the selected image
            BitmapAsciis secPicture = new BitmapAsciis();
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            string temp = secPicture.Asciitize(bmp);
            richTextBox2.Text = temp;

        }
        private void button2_Click(object sender, EventArgs e)
        {
            asciitizeEm();
        }



        private int[,] kernel; // declare a 2D integer array to represent the kernel matrix


        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            BitmapAsciis thiskernel= new BitmapAsciis();
            Bitmap filteredImage = new Bitmap(richTextBox1.Text);
            BitmapAsciis red = new BitmapAsciis();
            string str = red.Kernel(filteredImage);
            richTextBox1.Text = str;
            // Update the height of the kernel matrix
            int newWidth= (int)numericUpDown1.Value;
            int[,] newKernel = new int[kernel.GetLength(0), newWidth];
            for (int i = 0; i < kernel.GetLength(0); i++)
            {
                for (int j = 0; j < newWidth && j < kernel.GetLength(1); j++)
                {
                    newKernel[i, j] = kernel[i, j];
                }
            }
            kernel = newKernel;

        }

        public void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            BitmapData bitmapData = new BitmapData();
            Bitmap filteredImage = new Bitmap(richTextBox2.Text);
            BitmapAsciis red = new BitmapAsciis();  
            string str = red.Kernel(filteredImage);
            richTextBox2.Text = str;

            // Update the height of the kernel matrix
            int newHeight = (int)numericUpDown2.Value;
            int[,] newKernel = new int[kernel.GetLength(0), newHeight];
            for (int i = 0; i < kernel.GetLength(0); i++)
            {
                for (int j = 0; j < newHeight && j < kernel.GetLength(1); j++)
                {
                    newKernel[i, j] = kernel[i, j];
                }
            }
            kernel = newKernel;

        }

       
        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
