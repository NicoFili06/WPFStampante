using System.Resources;
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
using filippini.nicolò._4i.stampante.Models;


namespace filippini.nicolò._4i.stampante
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Models.stampante stampante = new();

        public MainWindow()
        {
            InitializeComponent();

            MessageBoxResult result = MessageBox.Show("Vuoi riprendere da dove eri rimasto?", "ATTENZIONE", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    LoadStatus();
                    Updater();
                    break;
                case MessageBoxResult.No:
                    break;
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Vuoi salvare lo stato della stampante?", "ATTENZIONE", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    SaveStatus();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void cyan_Click(object sender, RoutedEventArgs e)
        {
            stampante.SostituisiciColore(1);

            Updater();
        }

        private void magenta_Click(object sender, RoutedEventArgs e)
        {
            stampante.SostituisiciColore(2);

            Updater();
        }

        private void yellow_Click(object sender, RoutedEventArgs e)
        {
            stampante.SostituisiciColore(3);

            Updater();
        }

        private void black_Click(object sender, RoutedEventArgs e)
        {
            stampante.SostituisiciColore(4);

            Updater();
        }

        private void paper_Click(object sender, RoutedEventArgs e)
        {
            int p = 0;
            try
            {
                p = Convert.ToInt32(qtaPaper.Text);
            }
            catch
            {
                MessageBox.Show("Hai inserito un valore non valido");
                qtaPaper.Text = p.ToString();
            }

            qtaPaper.Text = stampante.AggiungiCarta(p).ToString();

            Updater();
        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
            Pagina p = new Pagina();

            qtaCyan.Text = p.C.ToString();
            qtaMagenta.Text = p.M.ToString();
            qtaYellow.Text = p.Y.ToString();
            qtaBlack.Text = p.K.ToString();

        }

        private void print_Click(object sender, RoutedEventArgs e)
        {

            int c = Convert.ToInt32(qtaCyan.Text);
            int m = Convert.ToInt32(qtaMagenta.Text);
            int y = Convert.ToInt32(qtaYellow.Text);
            int b = Convert.ToInt32(qtaBlack.Text);

            try
            {
                Pagina p = new Pagina(c, m, y, b);

                if (stampante.Stampa(p))
                {
                    statusPrint.Background = Brushes.Green;
                    statusPrint.Foreground = Brushes.White;
                }
                else
                {
                    statusPrint.Background = Brushes.Red;
                    statusPrint.Foreground = Brushes.White;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Updater();
        }



        private void PaperMinus_Click(object sender, RoutedEventArgs e)
        {
            int value = Convert.ToInt32(qtaPaper.Text);

            if (value > 0)
                value--;

            qtaPaper.Text = value.ToString();

        }

        private void PaperAdd_Click(object sender, RoutedEventArgs e)
        {
            int value = Convert.ToInt32(qtaPaper.Text);

            value++;

            qtaPaper.Text = value.ToString();
        }


        private void CyanMinus_Click(object sender, RoutedEventArgs e)
        {
            int value = Convert.ToInt32(qtaCyan.Text);

            if (value > 0)
                value--;

            qtaCyan.Text = value.ToString();

        }

        private void CyanAdd_Click(object sender, RoutedEventArgs e)
        {
            int value = Convert.ToInt32(qtaCyan.Text);

            if (value < 3)
                value++;

            qtaCyan.Text = value.ToString();
        }

        private void MagentaMinus_Click(object sender, RoutedEventArgs e)
        {
            int value = Convert.ToInt32(qtaMagenta.Text);

            if (value > 0)
                value--;

            qtaMagenta.Text = value.ToString();

        }

        private void MagentaAdd_Click(object sender, RoutedEventArgs e)
        {
            int value = Convert.ToInt32(qtaMagenta.Text);

            if (value < 3)
                value++;

            qtaMagenta.Text = value.ToString();
        }

        private void YellowMinus_Click(object sender, RoutedEventArgs e)
        {
            int value = Convert.ToInt32(qtaYellow.Text);

            if (value > 0)
                value--;

            qtaYellow.Text = value.ToString();

        }

        private void YellowAdd_Click(object sender, RoutedEventArgs e)
        {
            int value = Convert.ToInt32(qtaYellow.Text);

            if (value < 3)
                value++;

            qtaYellow.Text = value.ToString();
        }

        private void BlackMinus_Click(object sender, RoutedEventArgs e)
        {
            int value = Convert.ToInt32(qtaBlack.Text);

            if (value > 0)
                value--;

            qtaBlack.Text = value.ToString();

        }

        private void BlackAdd_Click(object sender, RoutedEventArgs e)
        {
            int value = Convert.ToInt32(qtaBlack.Text);

            if (value < 3)
                value++;

            qtaBlack.Text = value.ToString();
        }

        private void SaveStatus()
        {
            using (StreamWriter sw = new StreamWriter("saves.csv"))
            {
                sw.Write(stampante.ToString());
            }
        }
        private void LoadStatus()
        {
            using (StreamReader sr = new StreamReader("saves.csv"))
            {
                string s = sr.ReadToEnd();

                try
                {
                    stampante = new Models.Stampante(s);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    stampante = new Models.Stampante();
                }
            }
        }
        private void Updater()
        {
            resources.Text = "Livello inchiostro: \n\n" +
                             $"Ciano {stampante.C}% \n" +
                             $"Magenta {stampante.M}% \n" +
                             $"Giallo {stampante.Y}% \n" +
                             $"Nero {stampante.K}% \n\n\n" +
                             $"Nel cassetto ci sono {stampante.Fogli}/200 fogli";
        }

    }
}