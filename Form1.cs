using System;
using System.Windows.Forms;

namespace kalkulator
{
    public partial class okno : Form
    {
        double aktualna, wpamieci, poprzedniaaktualna, poprzedniawpamieci, memory;
        char OperacjaWToku = '0';
        bool czyPrzecinekTeraz = false, czyPrzecinek = false, czyRownaSie = false, czyMemoryRecall = false;

        public void WpiszLiczbe(int wpisane)
        {
            czyMemoryRecall = false;
            if (czyRownaSie)
            {
                pomocniczywswietlacz.Text = "";
                wyswietlacz.Text = "";
                aktualna = 0;
                wpamieci = 0;
                czyRownaSie = false;
            }
            if (czyPrzecinekTeraz == false)
            {
                if (czyPrzecinek == false)
                {
                    aktualna = aktualna * 10 + wpisane;
                    wyswietlacz.Text = aktualna.ToString();
                }
                else
                {
                    wyswietlacz.Text += wpisane.ToString();
                    aktualna = double.Parse(wyswietlacz.Text);
                }
            }
            else
            {
                aktualna = aktualna * 10 + wpisane;
                aktualna /= 10;
                wyswietlacz.Text = aktualna.ToString();
                czyPrzecinekTeraz = false;
            }
            
        }
        public void Oblicz(char operacja)
        {

            czyPrzecinek = false;
            czyRownaSie = false;
            if (operacja != '%' && operacja != '√')
            {
                ObliczPoprzednie();
                if (pomocniczywswietlacz.Text != "" && OperacjaWToku != '0')
                {
                    pomocniczywswietlacz.Text = poprzedniawpamieci.ToString() + " " + operacja + " " + poprzedniaaktualna.ToString() + " =";
                }
                else
                {
                    pomocniczywswietlacz.Text = wpamieci.ToString() + " " + operacja;
                }
                wyswietlacz.Text = wpamieci.ToString();
                OperacjaWToku = operacja;
            }
            else
            {
                switch (operacja)
                {
                    case '√':
                        {
                            ObliczPoprzednie();
                            pomocniczywswietlacz.Text = "√(" + wpamieci.ToString() + ") =";
                            wpamieci = Math.Sqrt(wpamieci);
                            aktualna = wpamieci;
                            wyswietlacz.Text = aktualna.ToString();
                            czyRownaSie = true;
                            OperacjaWToku = '0';
                        }
                        break;
                    case '%':
                        {
                            if (pomocniczywswietlacz.Text != "" || OperacjaWToku != '0')
                            {
                                aktualna = aktualna * 0.01 * wpamieci;
                            }
                            else
                            {
                                aktualna *= 0.01;
                            }
                            ObliczPoprzednie();
                            Wynik();
                        }
                        break;
                }
            }
        }

        public void ObliczPoprzednie()
        {
            czyMemoryRecall = false;
            czyPrzecinek = false;
            poprzedniawpamieci = wpamieci;
            poprzedniaaktualna = aktualna;
            if (pomocniczywswietlacz.Text != "" && OperacjaWToku != '0')
            {
                switch(OperacjaWToku)
                {
                    case '*':
                        {
                            wpamieci *= aktualna;
                        }
                        break;
                    case '-':
                        {
                            wpamieci -= aktualna;
                        }
                        break;
                    case '+':
                        {
                            wpamieci += aktualna;
                        }
                        break;
                    case '/':
                        {
                            wpamieci /= aktualna;
                        }
                        break;
                }
            }
            else
            {
                wpamieci = aktualna;
                
            }
            aktualna = 0;
        }

        public void Wynik()
        {
            if (czyRownaSie == false)
            {
                if (pomocniczywswietlacz.Text != "" || OperacjaWToku != '0')
                {
                    pomocniczywswietlacz.Text = poprzedniawpamieci.ToString() + " " + OperacjaWToku +" "+ poprzedniaaktualna.ToString() + " =";
                }
                else
                {
                    pomocniczywswietlacz.Text = wpamieci.ToString() + " = ";
                }
                wyswietlacz.Text = wpamieci.ToString();
                aktualna = wpamieci;
            }
            czyRownaSie = true;
            OperacjaWToku = '0';
        }


        public okno()
        {
            InitializeComponent();
        }

        // ZDARZENIA

        //off, clear, przecinek
        private void off_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void clear_Click(object sender, EventArgs e)
        {
            wyswietlacz.Text = "0";
            pomocniczywswietlacz.Text = "";
            aktualna = 0;
            wpamieci = 0;
            OperacjaWToku = '0';
            czyPrzecinekTeraz = false;
            czyPrzecinek = false;
            czyRownaSie = false;
            czyMemoryRecall = false;
        }
        private void clearentry_Click(object sender, EventArgs e)
        {
            if (czyRownaSie == true)
            {
                pomocniczywswietlacz.Text = "";
                wyswietlacz.Text = "";
                aktualna = 0;
                wpamieci = 0;
                czyRownaSie = false;
            }
            else
            {
                if (czyPrzecinek == true)
                {
                    wyswietlacz.Text.Substring(wyswietlacz.Text.Length - 1);
                    czyPrzecinek = false;
                    czyPrzecinekTeraz = false;
                }    
                aktualna = (aktualna - (aktualna % 10)) / 10;
                wyswietlacz.Text = aktualna.ToString();
            }
        }
        private void przecinek_Click(object sender, EventArgs e)
        {
            if (czyPrzecinek == false)
            {
                if (aktualna == 0)
                {
                    wyswietlacz.Text = aktualna.ToString() + ",";
                    czyPrzecinekTeraz = true;
                }
                else
                {
                    czyPrzecinekTeraz = true;
                    wyswietlacz.Text += ",";
                }

            }
            czyPrzecinek = true;
            
        }


        //memory
        private void mplus_Click(object sender, EventArgs e)
        {
            memory += double.Parse(wyswietlacz.Text);
            m.Visible = true;
        }
        private void mminus_Click(object sender, EventArgs e)
        {
            memory -= double.Parse(wyswietlacz.Text);
            m.Visible = true;
        }
        private void mrc_Click(object sender, EventArgs e)
        {
            if (czyMemoryRecall == false)
            {
                wyswietlacz.Text = memory.ToString();
                aktualna = memory;
                czyMemoryRecall = true;
                pomocniczywswietlacz.Text = "";
            }
            else
            {
                memory = 0;
                m.Visible = false;
                czyMemoryRecall = false;
            }
        }
        


        //liczby
        private void zero_Click(object sender, EventArgs e)
        {
            WpiszLiczbe(0);
        }
        private void jeden_Click(object sender, EventArgs e)
        {
            WpiszLiczbe(1);
        }
        private void dwa_Click(object sender, EventArgs e)
        {
            WpiszLiczbe(2);
        }
        private void trzy_Click(object sender, EventArgs e)
        {
            WpiszLiczbe(3);
        }
        private void cztery_Click(object sender, EventArgs e)
        {
            WpiszLiczbe(4);
        }
        private void piec_Click(object sender, EventArgs e)
        {
            WpiszLiczbe(5);
        }
        private void szesc_Click(object sender, EventArgs e)
        {
            WpiszLiczbe(6);
        }
        private void siedem_Click(object sender, EventArgs e)
        {
            WpiszLiczbe(7);
        }
        private void osiem_Click(object sender, EventArgs e)
        {
            WpiszLiczbe(8);
        }
        private void dziewiec_Click(object sender, EventArgs e)
        {
            WpiszLiczbe(9);
        }


        //działania
        private void mnożenie_Click(object sender, EventArgs e)
        {
            Oblicz('*');
        }
        private void minus_Click(object sender, EventArgs e)
        {
            Oblicz('-');
        }
        private void plus_Click(object sender, EventArgs e)
        {
            Oblicz('+');
        }
        private void dzielenie_Click(object sender, EventArgs e)
        {
            Oblicz('/');
        }

        private void pierwiastek_Click(object sender, EventArgs e)
        {
            Oblicz('√');
        }
        private void procent_Click(object sender, EventArgs e)
        {
            Oblicz('%');
        }
        private void rownasie_Click(object sender, EventArgs e)
        {
            ObliczPoprzednie();
            Wynik();
        }

        //wpisywanie z klawiatury
        private void okno_KeyDown(object sender, KeyEventArgs e)
        {
            

        }
        private void okno_KeyPress(object sender, KeyPressEventArgs e)
        {
            
                switch (e.KeyChar)
                {
                    case '0':
                        WpiszLiczbe(0);
                        break;
                    case '1':
                        WpiszLiczbe(1);
                        break;
                    case '2':
                        WpiszLiczbe(2);
                        break;
                    case '3':
                        WpiszLiczbe(3);
                        break;
                    case '4':
                        WpiszLiczbe(4);
                        break;
                    case '5':
                        WpiszLiczbe(5);
                        break;
                    case '6':
                        WpiszLiczbe(6);
                        break;
                    case '7':
                        WpiszLiczbe(7);
                        break;
                    case '8':
                        WpiszLiczbe(8);
                        break;
                    case '9':
                        WpiszLiczbe(9);
                        break;

                }
            

        }

        //inne
        private void okno_Load(object sender, EventArgs e)
        {

        }

        
    }
}