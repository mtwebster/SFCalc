using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409


namespace SFCalc
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private double current_rpm;
        private double current_sfm;
        private double current_dia;

        private double current_in;
        private double current_mm;

        public MainPage()
        {
            this.InitializeComponent();
            this.current_rpm = 0.0;
            this.current_sfm = 0.0;
            this.current_dia = 0.0;

            this.current_in = 0.0;
            this.current_mm = 0.0;
        }

        private double get_rpm(double sfm, double dia)
        {
            return sfm * 3.82 / dia;
        }

        private double get_sfm(double rpm, double dia)
        {
            return rpm * dia * .262;
        }

        private void rpm_entry_TextChanging(object sender, TextBoxTextChangingEventArgs e)
        {
            if (dia_entry.Text.Equals(""))
            {
                return;
            }

            double sfm = 0.0;
            double rpm = 0.0;

            try
            {
                rpm = Double.Parse(rpm_entry.Text);
                this.current_rpm = rpm;
            }
            catch (FormatException)
            {
                rpm_entry.TextChanging -= rpm_entry_TextChanging;
                rpm_entry.Text = this.current_rpm.ToString("N0");
                rpm_entry.TextChanging += rpm_entry_TextChanging;

                return;
            }

            try
            {
                double dia = Double.Parse(dia_entry.Text);

                sfm = get_sfm(rpm, dia);
            }
            catch (FormatException)
            {
                return;
            }

            sfm_entry.TextChanging -= sfm_entry_TextChanging;
            sfm_entry.Text = sfm.ToString("N0");
            sfm_entry.TextChanging += sfm_entry_TextChanging;

            this.current_sfm = sfm;
        }

        private void dia_entry_TextChanging(object sender, TextBoxTextChangingEventArgs e)
        {
            if (dia_entry.Text.Equals("") && sfm_entry.Text.Equals(""))
            {
                return;
            }

            double rpm = 0.0;
            double dia = 0.0;

            try
            {
                dia = Double.Parse(dia_entry.Text);
                this.current_dia = dia;
            }
            catch (FormatException)
            {
                if (dia_entry.Text.Equals("."))
                {
                    return;
                }
                dia_entry.TextChanging -= dia_entry_TextChanging;
                dia_entry.Text = this.current_dia.ToString("N0");
                dia_entry.TextChanging += dia_entry_TextChanging;

                return;
            }
            try
            {
                double sfm = Double.Parse(sfm_entry.Text);
                dia = Double.Parse(dia_entry.Text);

                rpm = get_rpm(sfm, dia);
            }
            catch (FormatException)
            {

                return;
            }

            rpm_entry.TextChanging -= rpm_entry_TextChanging;
            rpm_entry.Text = rpm.ToString("N0");
            rpm_entry.TextChanging += rpm_entry_TextChanging;

            this.current_rpm = rpm;
        }

        private void sfm_entry_TextChanging(object sender, TextBoxTextChangingEventArgs e)
        {
            if (dia_entry.Text.Equals(""))
            {
                return;
            }

            double rpm = 0.0;

            try
            {
                double sfm = Double.Parse(sfm_entry.Text);
                double dia = Double.Parse(dia_entry.Text);

                rpm = get_rpm(sfm, dia);
            }
            catch (FormatException)
            {
                return;
            }

            rpm_entry.TextChanging -= rpm_entry_TextChanging;
            rpm_entry.Text = rpm.ToString("N0");
            rpm_entry.TextChanging += rpm_entry_TextChanging;

            this.current_rpm = rpm;
        }

        private void entry_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox box = (TextBox)sender;

            box.SelectAll();
        }

        private void entry_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
        }

        private void inch_entry_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (inch_entry.Text.Equals(""))
            {
                return;
            }

            double inches = 0.0;
            double mm = 0.0;

            try
            {
                inches = Double.Parse(inch_entry.Text);
                this.current_in = inches;
            }
            catch (FormatException)
            {
                if (inch_entry.Text.Equals("."))
                {
                    return;
                }
                inch_entry.TextChanging -= inch_entry_TextChanging;
                inch_entry.Text = this.current_in.ToString("F4");
                inch_entry.TextChanging += inch_entry_TextChanging;

                return;
            }

            mm = inches * 25.4;

            mm_entry.TextChanging -= mm_entry_TextChanging;
            mm_entry.Text = mm.ToString("F3");
            mm_entry.TextChanging += mm_entry_TextChanging;

            this.current_mm = mm;
        }

        private void mm_entry_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (mm_entry.Text.Equals(""))
            {
                return;
            }

            double mm = 0.0;
            double inches = 0.0;

            try
            {
                mm = Double.Parse(mm_entry.Text);
                this.current_mm = mm;
            }
            catch (FormatException)
            {
                if (mm_entry.Text.Equals("."))
                {
                    return;
                }
                mm_entry.TextChanging -= mm_entry_TextChanging;
                mm_entry.Text = this.current_mm.ToString("F3");
                mm_entry.TextChanging += mm_entry_TextChanging;

                return;
            }

            inches = mm / 25.4;

            inch_entry.TextChanging -= inch_entry_TextChanging;
            inch_entry.Text = inches.ToString("F4");
            inch_entry.TextChanging += inch_entry_TextChanging;

            this.current_in = inches;
        }


    }
}
