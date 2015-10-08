using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
/*
 POPULATE TABLE
echo 'USE [pagesystem]<br/>';
echo '<br/>';
echo 'GO<br/>';
	
for($i = 0; $i < 50; $i++) {
	$fn = substr(md5(microtime()),rand(0,26),rand(3,10));
	$vn = substr(md5(microtime()),rand(0,26),rand(3,10));
	
	$d = rand(0,28);
	$m = rand(0,12);
	$y = rand(1900,2014);
	
	$d = ($d < 10) ? '0'.$d : $d;
	$m = ($m < 10) ? '0'.$m : $m;
	
	echo 'INSERT INTO [dbo].[gasten] ([familienaam], [voornaam], [geboortedatum]) VALUES (\''.$fn.'\',\''.$vn.'\',\''.$d.'-'.$m.'-'.$y.'\')<br/>';
}
echo 'GO';  
 */

namespace Pagination
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _nextPageItem = 1;
        private int _prevPageItem = 1;
        private int _listSize = 0;

        public MainWindow()
        {
            InitializeComponent();

            populateRow();
            //_startedItem = populateRow(1);
        }

        //private int populateRow(int id)
        private void populateRow(string familienaam = "", string voornaam = "", string geboortedatum = "")
        {
            /*SELECT
LAG(p.id) OVER (ORDER BY p.id) PreviousValue,
p.id,
p.cat,
p.title,
LEAD(p.id) OVER (ORDER BY p.id) NextValue
FROM dbo.media3 p
WHERE p.id = 50
GO*/
            //List<Media> media = new List<Media>();
            List<Persoon> personen = new List<Persoon>();
            using (SqlConnection con = new SqlConnection(Pagination.Properties.Settings.Default.DefaultConnection))
            {
                con.Open();
                //using (SqlCommand command = new SqlCommand("SELECT LAG(p.id) OVER (ORDER BY p.title) PreviousValue, p.id, p.cat, p.title, LEAD(p.id) OVER (ORDER BY p.title) NextValue FROM dbo.media3 p", con))
                using (SqlCommand command = new SqlCommand("SELECT familienaam, voornaam, geboortedatum FROM dbo.gasten WHERE ((familienaam=@fn) AND (voornaam=@vn) AND (geboortedatum>@gd)) OR ((familienaam=@fn) AND (voornaam>@vn)) OR ((familienaam>@fn)) ORDER BY familienaam, voornaam, geboortedatum", con))
                {
                    command.Parameters.Add("@fn", SqlDbType.NVarChar);
                    command.Parameters["@fn"].Value = familienaam;
                    //
                    command.Parameters.Add("@vn", SqlDbType.NVarChar);
                    command.Parameters["@vn"].Value = voornaam;
                    //
                    command.Parameters.Add("@gd", SqlDbType.NVarChar);
                    command.Parameters["@gd"].Value = geboortedatum;

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        /*int prevId = (reader.GetValue(0).ToString().Length == 0) ? 0 : reader.GetInt32(0);
                        int id = reader.GetInt32(1);    // id int
                        string cat = reader.GetString(2);  // cat string
                        string title = reader.GetString(3); // title string
                        int nextID = (reader.GetValue(4).ToString().Length == 0) ? 0 : reader.GetInt32(4);
                        media.Add(new Media() { ID = id, Title = title, Category = cat, PrevID = prevId, NextID = nextID });*/
                        string fn = reader.GetString(0);
                        string vn = reader.GetString(1);
                        string gd = reader.GetString(2);
                        personen.Add(new Persoon() { Familienaam = fn, Voornaam = vn, Geboortedatum = gd });
                    }
                }
                /*
                using (SqlCommand command = new SqlCommand("SELECT LAG(p.id) OVER (ORDER BY p.id) PreviousValue, p.id, p.cat, p.title, LEAD(p.id) OVER (ORDER BY p.id) NextValue FROM dbo.media3 p", con))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);    // id int
                        string cat = reader.GetString(1);  // cat string
                        string title = reader.GetString(2); // title string
                        media.Add(new Media() { ID = id, Title = title, Category = cat });
                    }
                }*/
                con.Close();
            }
            /*
            var i = (index == 0) ? personen.GetRange(0, 10) : personen.GetRange(index, 10);
            _prevPageItem = personen.IndexOf(personen.Find(x => x.ID == i.First().PrevID)) - 9;
            _nextPageItem = personen.IndexOf(personen.Find(x => x.ID == i.Last().NextID));
            _listSize = personen.Count - 10;*/
            data.ItemsSource = personen;
        }

        /*
        private int _page { get; set; }
        private int _maxPages { get; set; }
        private int _itemsPP { get; set; }
        private List<Media> _media { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            //_media = GetMedia();
            _page = 0;
            _itemsPP = 10;
            double n = countItems() / _itemsPP;// _media.Count / _itemsPP;
            _maxPages = (int)Math.Ceiling(n);
            data.ItemsSource = GetMedia(); // _media.GetRange(_page, _itemsPP);
            page.Content = (_page + 1) + "/" + (_maxPages + 1); 
        }
        
        private int countItems()
        {
            int max = 0;
            using (SqlConnection con = new SqlConnection(Pagination.Properties.Settings.Default.DefaultConnection))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT count(id) as count FROM media3", con))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        max = reader.GetInt32(0);    // count int
                    }
                }
                con.Close();
            }
            return max;
        }
        
        public List<Media> GetMedia()
        {
            List<Media> media = new List<Media>();
            using (SqlConnection con = new SqlConnection(Pagination.Properties.Settings.Default.DefaultConnection))
            {
                con.Open();
                int start = (_page * _itemsPP);
                int end = (_page == _maxPages) ? start + countItems() - (_page * _itemsPP) : start + _itemsPP;
                using (SqlCommand command = new SqlCommand("SELECT TOP "+end+" id, cat, title FROM media3 EXCEPT SELECT TOP "+start+" id, cat, title FROM media3", con))
                {
                    Console.WriteLine(command);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);    // id int
                        string cat = reader.GetString(1);  // cat string
                        string title = reader.GetString(2); // title string
                        media.Add(new Media() { ID = id, Title = title, Category = cat });
                    }
                }
                con.Close();
            }
            return media;
        }
        */
        private void first_Click(object sender, RoutedEventArgs e)
        {
            //_page = 0;
            //gotoPage();
            //populateRow(0);
        }
        
        private void prev_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (_page > 0)
            {
                _page--;
                gotoPage();
            }*/
            if (_prevPageItem >= 0)
            {
                //populateRow(_prevPageItem);
            }
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (_page < _maxPages)
            {
                _page++;
                gotoPage();
            }
             * */
            if (_nextPageItem >= 0)
            {
                //populateRow(_nextPageItem);
            }
        }
        
        private void last_Click(object sender, RoutedEventArgs e)
        {
            //_page = _maxPages;
            //gotoPage();
            //populateRow(_listSize);
        }
        /*
        private void gotoPage_Click(object sender, RoutedEventArgs e)
        {
            if (selectPage.Text.Count() > 0)
            {
                int number;
                bool result = Int32.TryParse(selectPage.Text, out number);
                if (result && number <= _maxPages+1 && number > 0)
                {
                    _page = number-1;
                    gotoPage();
                }
                else
                {
                    Console.WriteLine("Attempted page failed.");
                }
            }
        }

        private void gotoPage()
        {
            page.Content = (_page + 1) + "/" + (_maxPages + 1);
            data.ItemsSource = GetMedia();
            //int end = (_page == _maxPages) ? _media.Count() - (_page * _itemsPP) : _itemsPP;
            //data.ItemsSource = _media.GetRange(_page * _itemsPP, end);
        }*/
    }
}
