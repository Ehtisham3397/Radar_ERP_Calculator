using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Calculator_Radar_Params.UserParams;

namespace Calculator_Radar_Params
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataContext _context = new DataContext();
        private Data _selectedData;
        UserParameters userParameters = new UserParameters();
        public MainWindow()
        {
            InitializeComponent();
            // Clear existing data at the start of the application
            _context.RadarData.RemoveRange(_context.RadarData);
            _context.SaveChanges();
            //LoadData();
            BindRadar();
        }
        private void OnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            NumericValidation.OnlyNumbers(sender, e);
        }
        private void OnlyNumbersWithNegative(object sender, TextCompositionEventArgs e)
        {
            NumericValidation.OnlyNumbersWithNegative(sender, e);
        }
        private void PreventSpace(object sender, KeyEventArgs e)
        {
            NumericValidation.PreventSpace(sender, e);
        }

        private void CalculateRadarParameters(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RadarName.Text))
            {
                MessageBox.Show("Please enter Radar Name.");
                return;
            }
            if (string.IsNullOrWhiteSpace(PeakPower.Text))
            {
                MessageBox.Show("Please fill in the Peak Power of Jammer.");
                return;
            }
            if (string.IsNullOrWhiteSpace(OpFreq.Text))
            {
                MessageBox.Show("Please fill in the Radar Operating Frequency.");
                return;
            }
            if (string.IsNullOrWhiteSpace(AntennaAperture.Text))
            {
                MessageBox.Show("Please fill in the Antenna Aperture.");
                return;
            }
            if (string.IsNullOrWhiteSpace(AntennaEfficiency.Text))
            {
                MessageBox.Show("Please fill in the Antenna Efficiency.");
                return;
            }
            
            double peakPower = double.Parse(PeakPower.Text);
            double log_peakPower = (10 * Math.Log10(peakPower * 1000)) + 30;

            double radarFreq = double.Parse(OpFreq.Text);
            double radarWavelength = (3 * Math.Pow(10, 8)) / (radarFreq * Math.Pow(10, 9));

            double antennaAperture = double.Parse(AntennaAperture.Text);
            double antennaEfficiency = double.Parse(AntennaEfficiency.Text);

            double antennaGain = 4 * Math.PI * antennaAperture * (antennaEfficiency / 100) / Math.Pow(radarWavelength, 2);
            double log_antennaGain = 10 * Math.Log10(antennaGain);
            double radarERP = log_peakPower + log_antennaGain;

            txtAntennaGain.Content = "Antenna Gain (db)  " + log_antennaGain.ToString("F5");
            totalERP.Content = "Total ERP (dbm)  " + radarERP.ToString("F5");

            if (!string.IsNullOrEmpty(RadarName.Text) && !string.IsNullOrEmpty(PeakPower.Text) && !string.IsNullOrEmpty(OpFreq.Text)
                && !string.IsNullOrEmpty(AntennaAperture.Text) && !string.IsNullOrEmpty(AntennaEfficiency.Text))
            {
                var radar = new Data
                {
                    Name = RadarName.Text,
                    Peak_Power = peakPower,
                    Op_Freq = radarFreq,
                    Gain = Math.Round(log_antennaGain, 5),
                    ERP = Math.Round(radarERP, 5),
                    Antenna_Aperture = AntennaAperture.Text,
                    Antenna_Efficiency = AntennaEfficiency.Text
                };

                _context.RadarData.Add(radar);
                _context.SaveChanges();
                LoadData();
                BindRadar();
            }
        }

        private void LoadData()
        {
            DataGrid.ItemsSource = _context.RadarData.ToList();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedData = DataGrid.SelectedItem as Data;
            if (_selectedData != null)
            {
                TxtDelete.Visibility = Visibility.Visible;
                RadarName.Text = _selectedData.Name;
                PeakPower.Text = _selectedData.Peak_Power.ToString();
                OpFreq.Text = _selectedData.Op_Freq.ToString();
                AntennaAperture.Text = _selectedData.Antenna_Aperture;
                AntennaEfficiency.Text = _selectedData.Antenna_Efficiency;
                txtAntennaGain.Content = "Antenna Gain (db)  " +  _selectedData.Gain;
                totalERP.Content = "Total ERP (dbm)  " + _selectedData.ERP;
            }
        }
        private void Delete_Data(object sender, RoutedEventArgs e)
        {
            if (_selectedData != null)
            {
                _context.RadarData.Remove(_selectedData);
                _context.SaveChanges();
                LoadData();

                RadarName.Clear();
                PeakPower.Clear();
                OpFreq.Clear();
                AntennaAperture.Clear();
                AntennaEfficiency.Clear();
                txtAntennaGain.Content = " ";
                totalERP.Content = " ";

                TxtDelete.Visibility = Visibility.Hidden;
            }
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtDelete.Visibility = Visibility.Hidden;
            txtAntennaGain.Content = " ";
            totalERP.Content = " ";
        }

        private void BindRadar()
        {
            // list to hold ComboBox items
            List<string> radarList = new List<string>
            {
                "--SELECT--" // Default first item
            };

            // Fetch data from the database
            var radars = _context.RadarData.ToList();
            foreach (var radar in radars)
            {
                string formattedEntry = $"{radar.Name} | Peak Power {radar.Peak_Power} kW | Operating Frequency {radar.Op_Freq} GHz";
                radarList.Add(formattedEntry);
            }
            // Bind data to ComboBox
            radarSelection.ItemsSource = radarList;
            radarSelection_rcs.ItemsSource = radarList;
            radarSelection.SelectedIndex = 0; // Set default selection
            radarSelection_rcs.SelectedIndex = 0; // Set default selection
        }

        private void radarSelected(object sender, SelectionChangedEventArgs e)
        {
            if (radarSelection.SelectedIndex > 0)
            {
                // Get the selected radar text
                string selectedRadar = radarSelection.SelectedItem.ToString();

                string[] radarParts = selectedRadar.Split('|');

                if (radarParts.Length == 3) // Ensure valid format
                {
                    string radarName = radarParts[0].Trim();
                    double peakPower = double.Parse(radarParts[1].Replace("Peak Power", "").Replace("kW", "").Trim());
                    double opFreq = double.Parse(radarParts[2].Replace("Operating Frequency", "").Replace("GHz", "").Trim());

                    // Fetch radar details from the database using all three parameters
                    var radarData = _context.RadarData.FirstOrDefault(r =>
                        r.Name == radarName &&
                        r.Peak_Power == peakPower &&
                        r.Op_Freq == opFreq);

                    if (radarData != null)
                    {
                        // Store values in variables
                        ERP.Text = radarData.ERP.ToString();
                        AntennaGain.Text = radarData.Gain.ToString();
                        Freq.Text = radarData.Op_Freq.ToString();

                        //// Example: Show values in a message box (You can use them elsewhere)
                        //MessageBox.Show($"Selected Radar: {radarName}\nERP: {selectedERP} dBm\nGain: {selectedGain} dB\nFrequency: {selectedFreq} GHz");
                    }
                    else
                    {
                        MessageBox.Show("Radar data not found!");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid radar format in ComboBox.");
                }
            }
        }
        private void radarSelected_rcs(object sender, SelectionChangedEventArgs e)
        {
            if (radarSelection_rcs.SelectedIndex > 0)
            {
                // Get the selected radar text
                string selectedRadar = radarSelection_rcs.SelectedItem.ToString();

                string[] radarParts = selectedRadar.Split('|');

                if (radarParts.Length == 3) // Ensure valid format
                {
                    string radarName = radarParts[0].Trim();
                    double peakPower = double.Parse(radarParts[1].Replace("Peak Power", "").Replace("kW", "").Trim());
                    double opFreq = double.Parse(radarParts[2].Replace("Operating Frequency", "").Replace("GHz", "").Trim());

                    // Fetch radar details from the database using all three parameters
                    var radarData = _context.RadarData.FirstOrDefault(r =>
                        r.Name == radarName &&
                        r.Peak_Power == peakPower &&
                        r.Op_Freq == opFreq);

                    if (radarData != null)
                    {
                        // Store values in variables
                        ERP_rcs.Text = radarData.ERP.ToString();
                        AntennaGain_rcs.Text = radarData.Gain.ToString();
                        Freq_rcs.Text = radarData.Op_Freq.ToString();

                    }
                    else
                    {
                        MessageBox.Show("Radar data not found!");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid radar format in ComboBox.");
                }
            }
        }
        private void Support_Click(object sender, RoutedEventArgs e)
        {
            range_rcs_params_intruder.Visibility = Visibility.Visible;
            calculate_supportBtn.Visibility = Visibility.Visible;
            
            //range_rcs_params_jammer.Visibility = Visibility.Hidden;
            //calculate_spjBtn.Visibility = Visibility.Hidden;

            //jammer_params.Visibility = Visibility.Hidden;
            //calculate_jammerERP_spj.Visibility = Visibility.Hidden;
            //MainLobe_Pr.Content = "";
            //SideLobe_Pr.Content = "";

            //MainLobe_ERP.Content = "";
            //SideLobe_ERP.Content = "";

            //spjBtn.Visibility = Visibility.Hidden;
            //supportBtn.Visibility = Visibility.Hidden;
        }

        private void SPJ_Click(object sender, RoutedEventArgs e)
        {
            range_rcs_params_jammer.Visibility = Visibility.Visible;
            calculate_spjBtn.Visibility = Visibility.Visible;
            
            //range_rcs_params_intruder.Visibility = Visibility.Hidden;
            //calculate_supportBtn.Visibility = Visibility.Hidden;

            //intruder_params.Visibility = Visibility.Hidden;
            //calculate_jammerERP_support.Visibility = Visibility.Hidden;
            //MainLobe_Pr.Content = "";
            //SideLobe_Pr.Content = "";

            //MainLobe_ERP.Content = "";
            //SideLobe_ERP.Content = "";

            //spjBtn.Visibility = Visibility.Hidden;
            //supportBtn.Visibility = Visibility.Hidden;
        }

        public void Received_Power_MainLobe()
        {
            try
            {
                userParameters.Radar_ERP = double.Parse(ERP.Text);
                userParameters.Radar_Operating_Freq = double.Parse(Freq.Text);
                double wavelength = (3 * Math.Pow(10, 8)) / (userParameters.Radar_Operating_Freq * Math.Pow(10, 9));
                double log_wavelength = 10 * Math.Log10(wavelength);
                userParameters.Radar_Antenna_Gain = double.Parse(AntennaGain.Text);
                userParameters.Radar_Antenna_SLL = double.Parse(TxtSLL.Text);
                userParameters.Radar_Losses = double.Parse(losses.Text);
                userParameters.Intruder_RCS = double.Parse(IntruderRCS.Text);
                double log_RCS = 10 * Math.Log10(userParameters.Intruder_RCS);
                userParameters.Intruder_Range = double.Parse(IntruderRange.Text);
                double log_Range = 10 * Math.Log10(userParameters.Intruder_Range * 1000);

                double Received_Power = userParameters.Radar_ERP + userParameters.Radar_Antenna_Gain + log_RCS + (2 * log_wavelength) - (33 + (4 * log_Range) + userParameters.Radar_Losses);
                MainLobe_Pr.Content = "Rx (Pr) - Main Lobe (dBm): " + Received_Power.ToString("F5");
            }
            catch (FormatException)
            { MessageBox.Show("Please ensure all fields contain valid numbers."); }
        }

        private void CalculatePower_MainLobe(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ERP.Text))
            {
                MessageBox.Show("Please fill in the Radar ERP field.");
                return;
            }
            if (string.IsNullOrWhiteSpace(Freq.Text))
            {
                MessageBox.Show("Please fill in the Radar Operating Frequency field.");
                return;
            }
            if (string.IsNullOrWhiteSpace(AntennaGain.Text))
            {
                MessageBox.Show("Please fill in the Antenna Gain field.");
                return;
            }
            if (string.IsNullOrWhiteSpace(TxtSLL.Text))
            {
                MessageBox.Show("Please fill in the Antenna SLL field.");
                return;
            }
            if (string.IsNullOrWhiteSpace(losses.Text))
            {
                MessageBox.Show("Please fill in the Losses field.");
                return;
            }
            if (string.IsNullOrWhiteSpace(IntruderRCS.Text))
            {
                MessageBox.Show("Please fill in the Intruder RCS field.");
                return;
            }
            if (string.IsNullOrWhiteSpace(IntruderRange.Text))
            {
                MessageBox.Show("Please fill in the Intruder Range field.");
                return;
            }
            MainLobe_Pr.Background = Brushes.DarkKhaki;
            Received_Power_MainLobe();
            intruder_params.Visibility = Visibility.Visible;
            calculate_jammerERP_support.Visibility = Visibility.Visible;
            //spjBtn.Visibility = Visibility.Visible;
            //supportBtn.Visibility = Visibility.Visible;
            //calculate_supportBtn.Visibility = Visibility.Hidden;
        }

        public void Received_Power_SideLobe()
        {
            try
            {
                userParameters.Radar_ERP = double.Parse(ERP.Text);
                userParameters.Radar_Operating_Freq = double.Parse(Freq.Text);
                double wavelength = (3 * Math.Pow(10, 8)) / (userParameters.Radar_Operating_Freq * Math.Pow(10, 9));
                double log_wavelength = 10 * Math.Log10(wavelength);
                userParameters.Radar_Antenna_Gain = double.Parse(AntennaGain.Text);
                userParameters.Radar_Antenna_SLL = double.Parse(TxtSLL.Text);
                userParameters.Radar_Losses = double.Parse(losses.Text);
                userParameters.Jammer_RCS = double.Parse(JammerRCS.Text);
                double log_RCS = 10 * Math.Log10(userParameters.Jammer_RCS);
                userParameters.Jammer_Range_SPJ = double.Parse(JammerRange.Text);
                double log_Range = 10 * Math.Log10(userParameters.Jammer_Range_SPJ * 1000);

                double Received_Power_MainLobe = userParameters.Radar_ERP + userParameters.Radar_Antenna_Gain + log_RCS + (2 * log_wavelength) - (33 + (4 * log_Range) + userParameters.Radar_Losses);
                double Received_Power_SideLobe = userParameters.Radar_ERP + userParameters.Radar_Antenna_Gain + log_RCS + (2 * userParameters.Radar_Antenna_SLL) + (2 * log_wavelength) - (33 + (4 * log_Range) + userParameters.Radar_Losses);
                MainLobe_Pr_SPJ.Content = "Rx (Pr) - Main Lobe (dBm):  " + Received_Power_MainLobe.ToString("F5");
                SideLobe_Pr.Content = "Rx (Pr) - Side Lobe (dBm):  " + Received_Power_SideLobe.ToString("F5");
            }
            catch (FormatException)
            { MessageBox.Show("Please ensure all fields contain valid numbers."); }
        }
        private void CalculatePower_SideLobe(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ERP.Text))
            {
                MessageBox.Show("Please fill in the Radar ERP field.");
                return;
            }
            if (string.IsNullOrWhiteSpace(Freq.Text))
            {
                MessageBox.Show("Please fill in the Radar Operating Frequency field.");
                return;
            }
            if (string.IsNullOrWhiteSpace(AntennaGain.Text))
            {
                MessageBox.Show("Please fill in the Antenna Gain field.");
                return;
            }
            if (string.IsNullOrWhiteSpace(TxtSLL.Text))
            {
                MessageBox.Show("Please fill in the Antenna SLL field.");
                return;
            }
            if (string.IsNullOrWhiteSpace(losses.Text))
            {
                MessageBox.Show("Please fill in the Losses field.");
                return;
            }
            if (string.IsNullOrWhiteSpace(JammerRCS.Text))
            {
                MessageBox.Show("Please fill in the Jammer RCS field.");
                return;
            }
            if (string.IsNullOrWhiteSpace(JammerRange.Text))
            {
                MessageBox.Show("Please fill in the Jammer Range field.");
                return;
            }
            MainLobe_Pr_SPJ.Background = Brushes.DarkOliveGreen;
            SideLobe_Pr.Background = Brushes.DarkOliveGreen;
            Received_Power_SideLobe();
            jammer_params.Visibility = Visibility.Visible;
            calculate_jammerERP_spj.Visibility = Visibility.Visible;
            //spjBtn.Visibility = Visibility.Visible;
            //supportBtn.Visibility = Visibility.Visible;
            //calculate_spjBtn.Visibility = Visibility.Hidden;
        }

        public void ERP_MainLobe()
        {
            userParameters.Radar_ERP = double.Parse(ERP.Text);
            userParameters.Radar_Operating_Freq = double.Parse(Freq.Text);
            double wavelength = (3 * Math.Pow(10, 8)) / (userParameters.Radar_Operating_Freq * Math.Pow(10, 9));
            double log_wavelength = 10 * Math.Log10(wavelength);
            userParameters.Radar_Antenna_Gain = double.Parse(AntennaGain.Text);
            userParameters.Radar_Antenna_SLL = double.Parse(TxtSLL.Text);
            userParameters.Radar_Losses = double.Parse(losses.Text);
            userParameters.Intruder_RCS = double.Parse(IntruderRCS.Text);
            double log_RCS = 10 * Math.Log10(userParameters.Intruder_RCS);
            userParameters.Intruder_Range = double.Parse(IntruderRange.Text);
            double log_Range = 10 * Math.Log10(userParameters.Intruder_Range * 1000);

            double Received_Power = userParameters.Radar_ERP + userParameters.Radar_Antenna_Gain + log_RCS + (2 * log_wavelength) - (33 + (4 * log_Range) + userParameters.Radar_Losses);

            userParameters.Jamming_to_Signal_Ratio = double.Parse(jamming_level_intruder.Text);
            double jammerPowerReceivedByRadar = Received_Power + userParameters.Jamming_to_Signal_Ratio;
            userParameters.Jammer_Range_Support = double.Parse(jammer_range.Text);
            double log_jammerRange = 10 * Math.Log10(userParameters.Jammer_Range_Support * 1000);
            userParameters.Jammer_Losses = double.Parse(jammer_losses_intruder.Text);
            userParameters.AntennaGain_SideLobe = double.Parse(AntennaGain.Text) + double.Parse(TxtSLL.Text);

            double ERP_support = jammerPowerReceivedByRadar + 22 + (2 * log_jammerRange) + userParameters.Jammer_Losses - (2 * log_wavelength + userParameters.AntennaGain_SideLobe);
            MainLobe_ERP.Content = "Required Jammer ERP (dBm):  " + ERP_support.ToString("F5");
        }
        private void CalculateERP_MainLobe(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(jamming_level_intruder.Text))
            {
                MessageBox.Show("Please fill in the jamming to signal ratio.");
                return;
            }
            if (string.IsNullOrWhiteSpace(jammer_range.Text))
            {
                MessageBox.Show("Please fill in the Jammer Range.");
                return;
            }
            if (string.IsNullOrWhiteSpace(jammer_losses_intruder.Text))
            {
                MessageBox.Show("Please fill in the Jammer Losses.");
                return;
            }
            MainLobe_ERP.Background = Brushes.DarkKhaki;
            ERP_MainLobe();
        }
          
        public void ERP_SPJ()
        {
            userParameters.Radar_ERP = double.Parse(ERP.Text);
            userParameters.Radar_Operating_Freq = double.Parse(Freq.Text);
            double wavelength = (3 * Math.Pow(10, 8)) / (userParameters.Radar_Operating_Freq * Math.Pow(10, 9));
            double log_wavelength = 10 * Math.Log10(wavelength);
            userParameters.Radar_Antenna_Gain = double.Parse(AntennaGain.Text);
            userParameters.Radar_Antenna_SLL = double.Parse(TxtSLL.Text);
            userParameters.Radar_Losses = double.Parse(losses.Text);
            userParameters.Jammer_RCS = double.Parse(JammerRCS.Text);
            double log_RCS = 10 * Math.Log10(userParameters.Jammer_RCS);
            userParameters.Jammer_Range_SPJ = double.Parse(JammerRange.Text);
            double log_Range = 10 * Math.Log10(userParameters.Jammer_Range_SPJ * 1000);

            double Received_Power_MainLobe = userParameters.Radar_ERP + userParameters.Radar_Antenna_Gain + log_RCS + (2 * log_wavelength) - (33 + (4 * log_Range) + userParameters.Radar_Losses);
            double Received_Power_SideLobe = userParameters.Radar_ERP + userParameters.Radar_Antenna_Gain + log_RCS + (2 * userParameters.Radar_Antenna_SLL) + (2 * log_wavelength) - (33 + (4 * log_Range) + userParameters.Radar_Losses);

            userParameters.Jamming_to_Signal_Ratio = double.Parse(jamming_level_spj.Text);
            double jammerPowerReceivedByRadar_MainLobe = Received_Power_MainLobe + userParameters.Jamming_to_Signal_Ratio;
            double jammerPowerReceivedByRadar_SideLobe = Received_Power_SideLobe + userParameters.Jamming_to_Signal_Ratio;
            userParameters.Jammer_Losses = double.Parse(jammer_losses_spj.Text);
            userParameters.AntennaGain_SideLobe = double.Parse(AntennaGain.Text) + double.Parse(TxtSLL.Text);

            double ERP_SPJ_MainLobe = jammerPowerReceivedByRadar_MainLobe + 22 + (2 * log_Range) + userParameters.Jammer_Losses - (2 * log_wavelength + userParameters.Radar_Antenna_Gain);
            double ERP_SPJ_SideLobe = jammerPowerReceivedByRadar_SideLobe + 22 + (2 * log_Range) + userParameters.Jammer_Losses - (2 * log_wavelength + userParameters.AntennaGain_SideLobe);

            MainLobe_ERP_SPJ.Content = "In Main Lobe (dBm):  " + ERP_SPJ_MainLobe.ToString("F5");
            SideLobe_ERP_SPJ.Content = "In Side Lobe (dBm):  " + ERP_SPJ_SideLobe.ToString("F5");
        }
        private void CalculateERP_SideLobe(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(jamming_level_spj.Text))
            {
                MessageBox.Show("Please fill in the jamming to signal ratio.");
                return;
            }
            if (string.IsNullOrWhiteSpace(jammer_losses_spj.Text))
            {
                MessageBox.Show("Please fill in the Jammer Losses.");
                return;
            }
            MainLobe_ERP_SPJ.Background = Brushes.DarkOliveGreen;
            SideLobe_ERP_SPJ.Background = Brushes.DarkOliveGreen;
            ERP_SPJ();
        }

        private void maxRange_Click(object sender, RoutedEventArgs e)
        {
            double radarERP = double.Parse(ERP_rcs.Text);
            double radarFreq = double.Parse(Freq_rcs.Text);
            double wavelength = (3 * Math.Pow(10, 8)) / (radarFreq * Math.Pow(10, 9));
            double log_wavelength = 10 * Math.Log10(wavelength);
            double radarGain = double.Parse(AntennaGain_rcs.Text);
            double lossFactor = double.Parse(lossFactorTxt.Text);
            double noise_kT = double.Parse(kT.Text);
            double noiseFigure = double.Parse(noiseFigureTxt.Text);
            double IBW = double.Parse(ibwTxt.Text);
            double log_IBW = 10 * Math.Log10(IBW);
            double SNR_Min = double.Parse(snr_min.Text);
            double RCS = double.Parse(rcsTxt.Text);
            double log_RCS = 10 * Math.Log10(RCS);
            double log_Range = 0.25 * (radarERP + radarGain + log_RCS + (2 * log_wavelength) - (33 + lossFactor + noise_kT + noiseFigure + log_IBW + SNR_Min));
            double range_km = Math.Pow(10, log_Range / 10) / 1000;

            maxRangeKm.Background = Brushes.DarkOliveGreen;
            maxRangeKm.Content = $"Maximum Range of Radar for RCS {RCS} (sq m): {range_km.ToString("F5")} (Km)";
            maxRangeDb.Background = Brushes.DarkOliveGreen;
            maxRangeDb.Content = $"Maximum Range of Radar for RCS {RCS} (sq m): {log_Range.ToString("F5")} (dB)";
        }
    }
}