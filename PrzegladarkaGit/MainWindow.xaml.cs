using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrzegladarkaGit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> imagePaths = new List<string>();
        private BitmapImage displayedImage;
        private int displayedImageIndex = 0;
        private int size = 100;
        private Rotation rotation = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFileDialog(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Wybierz folder";
            dialog.UseDescriptionForTitle = true;

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                imagePaths.Clear();
                string[] extensions = new string[] { ".png", ".jpg", ".jpeg", ".gif", ".bmp" };
                foreach (string extension in extensions)
                    imagePaths.AddRange(Directory.GetFiles(dialog.SelectedPath, "*" + extension));


                if (imagePaths.Count > 0)
                {
                    rotation = 0;
                    DisplayImage(0);
                }
                else
                {
                    displayedImageIndex = 0;
                    Image.Source = null;
                }
            }
        }

        private void DisplayImage(int index)
        {
            displayedImageIndex = index;
            Name.Content = imagePaths[index].Split("\\").Last();
            displayedImage = new BitmapImage();
            displayedImage.BeginInit();
            displayedImage.UriSource = new Uri(imagePaths[index]);
            displayedImage.Rotation = rotation;
            displayedImage.EndInit();
            Image.Source = displayedImage;
        }


        private void Rotate(object sender, RoutedEventArgs e)
        {
            if (imagePaths.Count == 0) return;
            rotation = (Rotation)((int)(rotation + 1) % 4);
            DisplayImage(displayedImageIndex);
        }

        private void PreviousPhoto(object sender, RoutedEventArgs e)
        {
            if (displayedImageIndex > 0)
            {
                rotation = 0;
                DisplayImage(displayedImageIndex - 1);
            }
        }

        private void NextPhoto(object sender, RoutedEventArgs e)
        {
            if (displayedImageIndex < imagePaths.Count - 1)
            {
                rotation = 0;
                DisplayImage(displayedImageIndex + 1);
            }
        }
    }
}