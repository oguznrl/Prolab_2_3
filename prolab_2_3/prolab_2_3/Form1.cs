using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Prolab2_3Spo
{
    public partial class Form1 : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader dr;
        private string server;
        private string database;
        private string uid;
        private string password;
        MySqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        bool top10click = false;
        bool popclicked = false;
        bool jazzclicked = false;
        bool klasikclicked = false;
        public Form1()
        {
            server = "localhost";
            database = "proje3";
            uid = "root";
            password = "lts3dp45*ll1";

            string connString;
            connString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";


            InitializeComponent();
            conn = new MySqlConnection(connString);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            logPanel.Show();
            adminPanel.Hide();
            adminProscPanel.Hide();
            registerPanel.Hide();
            userloginPanel.Hide();
            singerProscPanel.Hide();
            songProcsPanel.Hide();
            albumProscPanel.Hide();
            normalUserPanel.Hide();
            jazzPanel.Hide();
            popPanel.Hide();
            classicPanel.Hide();
            searchPanel.Hide();
            preSearchPanel.Hide();
            topTenPanel.Hide();
            preSongListPanel.Hide();
            followPanel.Hide();
            albumUpdatePanel.Hide();
            songUpdatePanel.Hide();
            singerUpdatePanel.Hide();

            listTopTen.Columns.Add("Sarki", 140);
            listTopTen.Columns.Add("Sanatci", 110, HorizontalAlignment.Center);
            listTopTen.Columns.Add("Album", 140, HorizontalAlignment.Center);
            listTopTen.Columns.Add("Sure", 70, HorizontalAlignment.Center);
            listTopTen.Columns.Add("Dinlenme", 70, HorizontalAlignment.Center);           
            listTopTen.Columns.Add("Tur", 70, HorizontalAlignment.Center);
            listTopTen.View = View.Details;

            listPreFounded.Columns.Add("Kullanici", 70);
            listPreFounded.Columns.Add("Ulke", 70, HorizontalAlignment.Center);
            listPreFounded.Columns.Add("Uyelik", 70, HorizontalAlignment.Center);
            listPreFounded.Columns.Add("KullaniciID", 0, HorizontalAlignment.Center);
            listPreFounded.View = View.Details;

            listSongFounded.Columns.Add("Sarki", 70);
            listSongFounded.Columns.Add("Sanatci", 70, HorizontalAlignment.Center);
            listSongFounded.Columns.Add("Album", 70, HorizontalAlignment.Center);
            listSongFounded.Columns.Add("SarkiID", 0, HorizontalAlignment.Center);
            listSongFounded.Columns.Add("Sure", 70, HorizontalAlignment.Center);
            listSongFounded.View = View.Details;

            listPop.Columns.Add("Sarki", 100);
            listPop.Columns.Add("Sanatci", 100, HorizontalAlignment.Center);
            listPop.Columns.Add("Sure", 70, HorizontalAlignment.Center);
            listPop.Columns.Add("Tur", 70, HorizontalAlignment.Center);
            listPop.Columns.Add("Dinlenme", 70, HorizontalAlignment.Center);
            listPop.View = View.Details;

            listClassic.Columns.Add("Sarki", 100);
            listClassic.Columns.Add("Sanatci", 100, HorizontalAlignment.Center);
            listClassic.Columns.Add("Sure", 70, HorizontalAlignment.Center);
            listClassic.Columns.Add("Tur", 70, HorizontalAlignment.Center);
            listClassic.Columns.Add("Dinlenme", 70, HorizontalAlignment.Center);
            listClassic.View = View.Details;

            listJazz.Columns.Add("Sarki", 100);
            listJazz.Columns.Add("Sanatci", 100, HorizontalAlignment.Center);
            listJazz.Columns.Add("Sure", 70, HorizontalAlignment.Center);
            listJazz.Columns.Add("Tur", 70, HorizontalAlignment.Center);
            listJazz.Columns.Add("Dinlenme", 70, HorizontalAlignment.Center);
            listJazz.View = View.Details;

            listFollowing.Columns.Add("Kullanıcı", 70);
            listFollowing.Columns.Add("KullanıcıID", 00);
            listFollowing.View = View.Details;

            listView1.Columns.Add("Sarki", 70);
            listView1.Columns.Add("Sanatci", 70, HorizontalAlignment.Center);           
            listView1.Columns.Add("Sure", 70, HorizontalAlignment.Center);
            listView1.Columns.Add("SarkıID", 0, HorizontalAlignment.Center);
            listView1.View = View.Details;
        }

        public bool Register(string user, string pass, string mail, string country)
        {
            string query = $"INSERT INTO kullanicilar (kullaniciAd, kullaniciSifre, kullaniciMail, kullaniciUlke, kullaniciTur) VALUES ('{user}','{pass}','{mail}','{country}','Normal');";


            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }
        }

        public bool IsLogin(string user, string pass)
        {
            string query = $"SELECT * FROM kullanicilar WHERE kullaniciAd='{user}' AND kullaniciSifre='{pass}';";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        reader.Close();
                        conn.Close();
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }
        }

        public bool IsPremium(string user)
        {
            string query = $"SELECT kullaniciTur FROM kullanicilar WHERE kullaniciAd = '{user}';";
            string kulTur = string.Empty;
            string pre = "Premium";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            kulTur = (string)reader["kullaniciTur"];
                        }
                    }
                    conn.Close();
                    if (string.Equals(kulTur, pre))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }
        }


        public bool calmaListesiCreate(string user, string tur)
        {
            string query = $"CREATE TABLE {user}{tur} (sarkiID INT NOT NULL, PRIMARY KEY (sarkiID), CONSTRAINT f_key_{user}{tur}f FOREIGN KEY (sarkiID) REFERENCES sarkilar (sarkiID) ON DELETE CASCADE ON UPDATE CASCADE);";
            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                    catch
                    {
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }

        }

        public bool takipListCreate(string user)
        {
            string query = $"CREATE TABLE {user}takip (kullaniciID INT NOT NULL, PRIMARY KEY (kullaniciID), CONSTRAINT f_key_{user}takip FOREIGN KEY (kullaniciID) REFERENCES kullanicilar (kullaniciID) ON DELETE CASCADE ON UPDATE CASCADE);";
            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                    catch
                    {
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }
        }

        private bool OpenConnection()
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Sunucu ile baglanti basarisiz oldu!");
                        break;
                    case 1045:
                        MessageBox.Show("Sunucu adi veya sifresi hatali!");
                        break;
                }
                return false;
            }

        }

        public bool IsAdminLogin(string user, string pass)
        {
            string query = $"SELECT * FROM kullanicilar WHERE kullaniciAd='{user}' AND kullaniciSifre='{pass}' AND kullaniciTur='Admin';";
            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        reader.Close();
                        conn.Close();
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }

        }

        public bool addSanatci(string ad, string ulke)
        {
            string query = $"INSERT INTO sanatcilar (sanatciAd,sanatciUlke) SELECT '{ad}','{ulke}' WHERE NOT EXISTS (SELECT sanatciID FROM sanatcilar WHERE sanatciAd = '{ad}' AND sanatciUlke = '{ulke}');";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }
        }
        public bool searchSanatci(string sanatciAd, string sanatciUlke)
        {
            string query = $"SELECT sanatciAd FROM sanatcilar WHERE sanatciAd = '{sanatciAd}' AND  sanatciUlke = '{sanatciUlke}';";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        reader.Close();
                        conn.Close();
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }
        }

        public bool deleteSanatci(string sanatciAd, string sanatciUlke)
        {
            string query = $"DELETE FROM sanatcilar WHERE (sanatciAd='{sanatciAd}') AND (sanatciUlke='{sanatciUlke}');";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }

        }

        string albumTur;
        public void getAlbumTur(string album)
        {
            string query = $"SELECT albumTur FROM albumler WHERE albumAd = '{album}';";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            albumTur = (string)reader["albumTur"];
                        }
                    }
                    conn.Close();

                }
                else
                {
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                conn.Close();

            }


        }

        string sarkiTuru;
        public void getSarkiTur(int sarkiID)
        {
            string query = $"SELECT sarkiTur FROM sarkilar WHERE sarkiID = '{sarkiID}';";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sarkiTuru = (string)reader["sarkiTur"];
                        }
                    }
                    conn.Close();
                }
                else
                {
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                conn.Close();

            }
        }

        public string getSarkiSanatci(int sarkiID)
        {
            string query = $"SELECT sarkiSanatci FROM sarkilar WHERE sarkiID = '{sarkiID}';";
            string sarkiSanatcisi = String.Empty;

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sarkiSanatcisi = (string)reader["sarkiSanatci"];
                        }
                    }
                    conn.Close();
                    return sarkiSanatcisi;
                }
                else
                {
                    conn.Close();
                    return sarkiSanatcisi;

                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return sarkiSanatcisi;
            }


        }
       
        public string getSarkiSure(int sarkiID)
        {
            string query = $"SELECT sarkiSure FROM sarkilar WHERE sarkiID = '{sarkiID}';";
            string sarkiSuresi = String.Empty;
            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sarkiSuresi = (string)reader["sarkiSure"];
                        }
                    }
                    conn.Close();
                    return sarkiSuresi;
                }
                else
                {
                    conn.Close();
                    return sarkiSuresi;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return sarkiSuresi;
            }

        }

        public bool addSarki(string sarkiAd, string muzisyen, string album, string sarkiUzunluk, string tur)
        {
            getAlbumTur(album);
            if (String.Equals(albumTur, tur))
            {
                string query = $"INSERT INTO sarkilar (sarkiAd, sarkiSanatci, sarkiSure, sarkiDinlenmeSayi, sarkiTur) SELECT '{sarkiAd}','{muzisyen}', '{sarkiUzunluk}','0','{tur}' WHERE NOT EXISTS (SELECT sarkiID FROM sarkilar WHERE sarkiAd = '{sarkiAd}' AND sarkiSanatci ='{muzisyen}')";

                try
                {
                    if (OpenConnection())
                    {
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        try
                        {
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            conn.Close();
                            return false;
                        }
                    }
                    else
                    {
                        conn.Close();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool searchSarki(string sarkiAd, string muzisyen)
        {
            string query = $"SELECT sarkiAd, sarkiSanatci FROM sarkilar WHERE sarkiAd = '{sarkiAd}' AND sarkiSanatci='{muzisyen}';";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        reader.Close();
                        conn.Close();
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }

        }

        public bool deleteSarki(string sarkiAd, string muzisyen)
        {
            string query = $"DELETE FROM sarkilar WHERE (sarkiAd='{sarkiAd}' AND sarkiSanatci='{muzisyen}');";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }

        }

        public bool add_to_albumsarki(int albumID, int sarkiID)
        {
            string query = $"INSERT INTO album_sarki (albumID,sarkiID) VALUES('{albumID}','{sarkiID}');";
            MessageBox.Show($"{albumID}-{sarkiID}");

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }
        }

        public bool search_albumsarki(int albumID, int sarkiID)
        {
            string query = $"SELECT albumID, sarkiID FROM album_sarki WHERE albumID='{albumID}' AND sarkiID='{sarkiID}';";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        reader.Close();
                        conn.Close();
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }
        }

        public bool del_albumsarki(int albumID, int sarkiID)
        {
            string query = $"DELETE FROM album_sarki WHERE (albumID='{albumID}') AND (sarkiID='{sarkiID}');";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }

        }

        int sarkiID = 0;
        public int searchsarkiIDfromsarkilar(string sarkiAd, string sarkiSanatci)
        {
            string query = $"SELECT sarkiID FROM sarkilar WHERE (sarkiAd = '{sarkiAd}') AND (sarkiSanatci='{sarkiSanatci}');";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sarkiID = (int)reader["sarkiID"];
                        }
                    }
                    conn.Close();
                    return sarkiID;
                }
                else
                {
                    conn.Close();
                    return -1;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return -1;
            }

        }

        int albumID = 0;
        public int searchalbumIDfromalbumler(string albumAd)
        {
            string query = $"SELECT albumID FROM albumler WHERE (albumAd = '{albumAd}');";
            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            albumID = (int)reader["albumID"];
                        }
                    }
                    conn.Close();
                    return albumID;
                }
                else
                {
                    conn.Close();
                    return -1;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return -1;
            }

        }

        public bool updateSanatci(string sanatciEskiAd, string sanatciEskiUlke, string sanatciYeniAd, string sanatciYeniUlke)
        {
            string query = $"UPDATE sanatcilar SET sanatciAd = '{sanatciYeniAd}', sanatciUlke = '{sanatciYeniUlke}' WHERE (sanatciAd = '{sanatciEskiAd}') AND (sanatciUlke = '{sanatciEskiUlke}');";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }

        }

        public bool searchAlbum(string albumAd, string albumSanatci)
        {
            string query = $"SELECT albumAd FROM albumler WHERE albumAd='{albumAd}' AND albumSanatci='{albumSanatci}';";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        reader.Close();
                        conn.Close();
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }

        }

        public bool addAlbum(string albumAd, string albumTur, string albumTarih, string albumSanatci)
        {
            string query = $"INSERT INTO albumler (albumAd,albumSanatci,albumTarih,albumTur) SELECT '{albumAd}','{albumSanatci}','{albumTarih}','{albumTur}' WHERE NOT EXISTS (SELECT albumID FROM albumler WHERE albumAd = '{albumAd}' AND albumSanatci='{albumSanatci}')";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }

        }

        public bool deleteAlbum(string albumAd, string albumSanatci)
        {
            string query = $"DELETE FROM albumler WHERE (`albumAd` = '{albumAd}') AND (`albumSanatci` = '{albumSanatci}');";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }
        }

        public bool updateAlbum(string albumEskiAd, string albumEskiSanatci, string albumYeniAd, string albumYeniSanatci, string albumYeniTarih, string albumYeniTur)
        {
            string query = $"UPDATE albumler SET albumAd = '{albumYeniAd}', albumSanatci = '{albumYeniSanatci}', albumTarih = '{albumYeniTarih}', albumTur = '{albumYeniTur}' WHERE (albumAd = '{albumEskiAd}') AND (albumSanatci = '{albumEskiSanatci}');";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }
        }

        int sanatciID = 0;
        public int search_sanatciID_fromSanatcilar(string sanatciAd)
        {
            string query = $"SELECT sanatciID FROM sanatcilar WHERE (sanatciAd = '{sanatciAd}');";


            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sanatciID = (int)reader["sanatciID"];
                        }
                    }
                    conn.Close();
                    return sanatciID;
                }
                else
                {
                    conn.Close();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return 0;
            }
        }

        public bool add_to_albumSanatci(int albumID, int sanatciID)
        {
            string query = $"INSERT INTO album_sanatci (albumID,sanatciID) VALUES('{albumID}','{sanatciID}');";
            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }
        }

        public bool updateSong(string songOldName, string songOldSinger, string songNewName, string songNewSinger, string songNewAlbum, string songNewLength, string songNewDate, string songNewTur)
        {
            getAlbumTur(songNewAlbum);
            if (String.Equals(albumTur, songNewTur))
            {
                string query = $"UPDATE sarkilar SET sarkiAd = '{songNewName}', sarkiSanatci = '{songNewSinger}', sarkiSure = '{songNewLength}', sarkiTur = '{songNewTur}' WHERE (sarkiAd = '{songOldName}') AND (sarkiSanatci = '{songOldSinger}');";

                try
                {
                    if (OpenConnection())
                    {
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        try
                        {
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            conn.Close();
                            return false;
                        }
                    }
                    else
                    {
                        conn.Close();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        public bool changekulTur(string kulAd, string tur)
        {
            string query = $"UPDATE kullanicilar SET kullaniciTur = '{tur}' WHERE (kullaniciAd = '{kulAd}');";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }

        }
        public void top10()
        {
            string query = "SELECT sarkiAd, sarkiSanatci, albumAd, sarkiSure, sarkiDinlenmeSayi, sarkiTur FROM sarkilar,albumler,album_sarki WHERE sarkilar.sarkiID=album_sarki.sarkiID AND albumler.albumID=album_sarki.albumID order by sarkiDinlenmeSayi+0 desc;";


            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    da = new MySqlDataAdapter(cmd);
                    ds = new DataSet();                  
                    listTopTen.Items.Clear();
                    ds.Clear();

                    try
                    {
                        da.Fill(ds, "testTable");
                        dt = ds.Tables["testTable"];
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            listTopTen.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                            listTopTen.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                            listTopTen.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                            listTopTen.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                            listTopTen.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                            listTopTen.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                            if (i == 9)
                            {
                                break;
                            }

                        }
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        top10click = true;

                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                    }
                }
                else
                {
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                conn.Close();

            }

        }

        public void searchkullanici(string kulAd)
        {
            string query = $"SELECT kullaniciAd, kullaniciUlke, kullaniciTur,kullaniciID FROM kullanicilar WHERE kullaniciAd='{kulAd}'AND kullaniciTur='Premium' AND kullaniciAd <> '{textUserName.Text}';";


            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    da = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    listPreFounded.Items.Clear();
                    ds.Clear();

                    try
                    {
                        da.Fill(ds, "testTable");
                        dt = ds.Tables["testTable"];

                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            listPreFounded.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                            listPreFounded.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                            listPreFounded.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                            listPreFounded.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                        }
                        cmd.ExecuteNonQuery();
                        conn.Close();

                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                    }
                }
                else
                {
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                conn.Close();

            }

        }

        public void searchSong(string songName)
        {
            string query = $"SELECT sarkiAd, sarkiSanatci, albumAd, sarkilar.sarkiID, sarkiSure FROM sarkilar,albumler,album_sarki WHERE sarkiAd='{songName}' AND sarkilar.sarkiID=album_sarki.sarkiID AND albumler.albumID=album_sarki.albumID;";


            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    da = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    listSongFounded.Items.Clear();
                    ds.Clear();

                    try
                    {
                        da.Fill(ds, "testTable");
                        dt = ds.Tables["testTable"];

                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            listSongFounded.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                            listSongFounded.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                            listSongFounded.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                            listSongFounded.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                            listSongFounded.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                        }
                        cmd.ExecuteNonQuery();
                        conn.Close();

                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                    }
                }
                else
                {
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                conn.Close();

            }

        }

        public void kulJazzList(string kulAd)
        {
            string query = $"SELECT sarkilar.sarkiAd,sarkilar.sarkiSanatci,sarkilar.sarkiSure,sarkilar.sarkiTur,sarkilar.sarkiDinlenmeSayi FROM {kulAd}jazz,sarkilar WHERE {kulAd}jazz.sarkiID=sarkilar.sarkiID;";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    da = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    listJazz.Items.Clear();
                    ds.Clear();

                    try
                    {
                        da.Fill(ds, "testTable");
                        dt = ds.Tables["testTable"];
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            listJazz.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                            listJazz.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                            listJazz.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                            listJazz.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                            listJazz.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                        }
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                    }
                }
                else
                {
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                conn.Close();

            }
        }

        public void kulPopList(string kulAd)
        {
            string query = $"SELECT sarkilar.sarkiAd,sarkilar.sarkiSanatci,sarkilar.sarkiSure,sarkilar.sarkiTur,sarkilar.sarkiDinlenmeSayi FROM {kulAd}pop,sarkilar WHERE {kulAd}pop.sarkiID=sarkilar.sarkiID;";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    da = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    listPop.Items.Clear();
                    ds.Clear();

                    try
                    {
                        da.Fill(ds, "testTable");
                        dt = ds.Tables["testTable"];
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            listPop.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                            listPop.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                            listPop.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                            listPop.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                            listPop.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                        }
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                    }
                }
                else
                {
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                conn.Close();

            }
        }

        public void kulKlasikList(string kulAd)
        {
            string query = $"SELECT sarkilar.sarkiAd,sarkilar.sarkiSanatci,sarkilar.sarkiSure,sarkilar.sarkiTur,sarkilar.sarkiDinlenmeSayi FROM {kulAd}klasik,sarkilar WHERE {kulAd}klasik.sarkiID=sarkilar.sarkiID;";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    da = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    listClassic.Items.Clear();
                    ds.Clear();

                    try
                    {
                        da.Fill(ds, "testTable");
                        dt = ds.Tables["testTable"];
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            listClassic.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                            listClassic.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                            listClassic.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                            listClassic.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                            listClassic.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                        }
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                    }
                }
                else
                {
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                conn.Close();

            }
        }

        public void kulTakipList(string kulAd)
        {
            string query = $"SELECT kullanicilar.kullaniciAd,{kulAd}takip.kullaniciID FROM {kulAd}takip,kullanicilar WHERE {kulAd}takip.kullaniciID=kullanicilar.kullaniciID;";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    da = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    listFollowing.Items.Clear();
                    ds.Clear();                   

                    try
                    {
                        da.Fill(ds, "testTable");
                        dt = ds.Tables["testTable"];
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            listFollowing.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                            listFollowing.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                        }
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                    }
                }
                else
                {
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                conn.Close();

            }
        }

        string sarkidin = String.Empty;
        public bool addtoKullList(string user, int sarkiID)
        {        
            getSarkiTur(sarkiID);
            string query = $"INSERT INTO {user}{sarkiTuru} (sarkiID) VALUES ({sarkiID});";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }

        }

        public bool addtoKullListfromPre(string user, int sarkiID)
        {
            getSarkiTur(sarkiID);
            string query = $"INSERT INTO {user}{sarkiTuru} (sarkiID) VALUES ('{sarkiID}');";          

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }

        }

        public void showList(string kulAd, string tur)
        {
            string query = $"SELECT sarkiAd, sarkiSanatci, sarkiSure, {kulAd}{tur}.sarkiID FROM {kulAd}{tur},sarkilar WHERE {kulAd}{tur}.sarkiID=sarkilar.sarkiID ;";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    da = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    listView1.Items.Clear();
                    ds.Clear();

                    try
                    {
                        da.Fill(ds, "testTable");
                        dt = ds.Tables["testTable"];
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            listView1.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                            listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                            listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                            listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                        }
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                    }
                }
                else
                {
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                conn.Close();

            }
        }

        public bool follow(string user, int followuserID)
        {
            string query = $"INSERT INTO {user}takip (kullaniciID) SELECT '{followuserID}' WHERE NOT EXISTS (SELECT kullaniciID FROM {user}takip WHERE kullaniciID = '{followuserID}');";
            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }

        }

        public void deleteSongfromalbum_sarki(int songID)
        {
            string query = $"DELETE FROM album_sarki WHERE (sarkiID = '{songID}');";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                    }
                }
                else
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                conn.Close();
            }
        }

        public void deleteAlbumfromalbum_sanatci(int albumID)
        {
            string query = $"DELETE FROM album_sanatci WHERE (albumID = '{albumID}');";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                    }
                }
                else
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                conn.Close();
            }
        }

        public void topList(string Tur)
        {
            string query = $"SELECT sarkiAd,sarkiSanatci,sarkiSure,sarkiDinlenmeSayi,sarkiTur FROM sarkilar WHERE sarkiTur='{Tur}' ORDER BY sarkiDinlenmeSayi+0 desc;";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    da = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    listClassic.Items.Clear();
                    listPop.Items.Clear();
                    listJazz.Items.Clear();
                    ds.Clear();

                    try
                    {
                        da.Fill(ds, "testTable");
                        dt = ds.Tables["testTable"];
                        if (String.Equals(Tur, "Pop"))
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                listPop.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                                listPop.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                                listPop.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                                listPop.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                                listPop.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                                if (i == 9)
                                {
                                    break;
                                }
                            }
                        }
                        else if (String.Equals(Tur, "Klasik"))
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                listClassic.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                                listClassic.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                                listClassic.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                                listClassic.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                                listClassic.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                                if (i == 9)
                                {
                                    break;
                                }
                            }
                        }
                        else if (String.Equals(Tur, "Jazz"))
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                listJazz.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                                listJazz.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                                listJazz.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                                listJazz.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                                listJazz.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                                if (i == 9)
                                {
                                    break;
                                }

                            }
                        }
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                    }
                }
                else
                {
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                conn.Close();

            }
        }

        string kullaniciTuru;
        public void getKulTur(string kulAd)
        {
            string query = $"SELECT kullaniciTur FROM kullanicilar WHERE kullaniciAd = '{kulAd}';";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            kullaniciTuru = (string)reader["kullaniciTur"];
                        }
                    }
                    conn.Close();

                }
                else
                {
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                conn.Close();

            }


        }
       
        public string getSarkiAd(int sarkiID)
        {
            string query = $"SELECT sarkiAd FROM sarkilar WHERE sarkiID= '{sarkiID}';";
            string sarkiAdi = String.Empty;

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sarkiAdi = (string)reader["sarkiAdi"];
                        }
                    }
                    conn.Close();
                    return sarkiAdi;
                }
                else
                {
                    conn.Close();
                    return sarkiAdi;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return sarkiAdi;
            }

        }

        public bool addtokulllistall(string prekul,string user,string tur)
        {
            string query = $"INSERT INTO {user}{tur} SELECT * FROM {prekul}{tur} WHERE {prekul}{tur}.sarkiID NOT IN(SELECT {user}{tur}.sarkiID FROM {user}{tur})";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }

        }

        public bool searchSonginUserList(string user,string Tur,int sarkiID)
        {
            string query = $"SELECT sarkiID FROM {user}{Tur} WHERE sarkiID={sarkiID};";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        reader.Close();
                        conn.Close();
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }
        }

        string sanatciUlkesi = String.Empty;
        public void addUlketoComboBox()
        {
            string query = $"SELECT DISTINCT(sanatciUlke) FROM sanatcilar;";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    da = new MySqlDataAdapter(cmd);
                    ds = new DataSet();                    
                    ds.Clear();

                    try
                    {
                        da.Fill(ds, "testTable");
                        dt = ds.Tables["testTable"];
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {                          
                            comboBoxCountry.Items.Add(dt.Rows[i].ItemArray[0].ToString());                          
                        }
                        cmd.ExecuteNonQuery();
                        conn.Close();                        

                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                    }
                }
                else
                {
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                conn.Close();

            }
        }

        public void top10ulke(string country)
        {
           string query = $"SELECT sarkiAd, sarkiSanatci, albumAd, sarkiSure, sarkiDinlenmeSayi, sarkiTur FROM sarkilar,albumler,album_sarki,sanatcilar,album_sanatci WHERE sanatciUlke='{country}' AND sarkilar.sarkiID=album_sarki.sarkiID AND albumler.albumID=album_sarki.albumID AND albumler.albumID=album_sanatci.albumID AND album_sanatci.sanatciID=sanatcilar.sanatciID order by sarkiDinlenmeSayi+0 desc;";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    da = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    listTopTen.Items.Clear();
                    ds.Clear();

                    try
                    {
                        da.Fill(ds, "testTable");
                        dt = ds.Tables["testTable"];
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            listTopTen.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                            listTopTen.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                            listTopTen.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                            listTopTen.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                            listTopTen.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                            listTopTen.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                            if (i == 9)
                            {
                                break;
                            }

                        }
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        top10click = true;

                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                    }
                }
                else
                {
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                conn.Close();

            }
        }

        public bool searchUserinTakipList(string user,int preUserID)
        {
            string query = $"SELECT kullaniciID FROM {user}takip WHERE kullaniciID='{preUserID}';";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        reader.Close();
                        conn.Close();
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }
        }



        /*
         * 
         * Admin işlemleri
         * 
         */

        //Admin işlemleri yapacağı paneli açılır
        private void buttonAdminLog_Click(object sender, EventArgs e)
        {
            string user = textAdminName.Text;
            string pass = textAdminPass.Text;

            if (IsAdminLogin(user, pass))
            {
                MessageBox.Show($"Hosgeldiniz {user}!");
                adminProscPanel.Show();
                adminPanel.Hide();
            }
            else
            {
                MessageBox.Show($"Yanlis kullanici adi veya sifre girdiniz!");
            }
        }
        //Admin giriş paneline giriş yapar
        private void buttonLogAd_Click(object sender, EventArgs e)
        {
            logPanel.Hide();
            adminPanel.Show();
        }
        //Admin panelinden ayrılır
        private void buttonBack_Click(object sender, EventArgs e)
        {
            logPanel.Show();
            adminPanel.Hide();
        }
        //Admin çıkış yapar
        private void buttonExitAdmin_Click(object sender, EventArgs e)
        {
            adminPanel.Show();
            adminProscPanel.Hide();
        }

        /*
         *Sanatçı Güncelleme Paneli İşlemleri 
         */

        //Sanatçı düzeleme paneli açılır
        private void buttonSingerProsc_Click(object sender, EventArgs e)
        {
            singerProscPanel.Show();
            songProcsPanel.Hide();
            albumProscPanel.Hide();

        }
        //Admin şarkı  güncelleme panelini kapatır
        private void buttonBackSing_Click(object sender, EventArgs e)
        {
            singerProscPanel.Hide();
        }
        //Sanatçı bilgisini günceller
        private void buttonUpdateSing_Click(object sender, EventArgs e)
        {
            String singerName = textSingerNameAdd.Text;
            String singerCountry = textSingerCountryAdd.Text;
            if (searchSanatci(singerName, singerCountry))
            {
                singerUpdatePanel.Show();
            }
            else
            {
                MessageBox.Show($"{singerName}-{singerCountry} sanatcilarda bulunamadi!");
            }

        }
        //Sanatçı bilgisini siler
        private void buttonDeleteSing_Click(object sender, EventArgs e)
        {
            String singerName = textSingerNameAdd.Text;
            String singerCountry = textSingerCountryAdd.Text;
            if (searchSanatci(singerName, singerCountry))
            {
                if (deleteSanatci(singerName, singerCountry))
                {
                    MessageBox.Show($"{singerName} sanatcilardan basariyla silindi!");
                }
                else
                {
                    MessageBox.Show($"{singerName} sanatcilardan silinemedi!");
                }
            }
            else
            {
                MessageBox.Show($"{singerName} sanatcilarda bulunamadi!");
            }
        }
        //Sanatçı ekler
        private void buttonAppendSing_Click(object sender, EventArgs e)
        {
            String singerName = textSingerNameAdd.Text;
            String singerCountry = textSingerCountryAdd.Text;
            if(String.IsNullOrEmpty(singerName)||String.IsNullOrEmpty(singerCountry))
            {
                MessageBox.Show("Sanatci bilgileri bos birakilamaz!");
            }
            else if (addSanatci(singerName, singerCountry))
            {
                MessageBox.Show($"{singerName} sanatcilara basariyla eklendi!");
            }
            else
            {
                MessageBox.Show($"{singerName} sanatcilara eklenemedi!");

            }
        }

        /*
         * Şarkı Güncelleme Paneli İşlemleri 
         */

        //Admin şarkı düzenlemesi yaptığı panel gözkür
        private void buttonSongProsc_Click(object sender, EventArgs e)
        {
            songProcsPanel.Show();
            singerProscPanel.Hide();
            albumProscPanel.Hide();
        }
        //Admin şarkı düzenlemesi yaptığı panel görünmez olur
        private void buttonBackSong_Click(object sender, EventArgs e)
        {
            songProcsPanel.Hide();
        }
        //Şarkı ekler
        private void buttonAppendSong_Click(object sender, EventArgs e)
        {
            String songName = textSongSingerAdd.Text;
            String songAlbum = textSongAlbumAdd.Text;
            String songSinger = textSongNameAdd.Text;
            String songLong = textSongLongAdd.Text;
            String date = textSongDateAdd.Text;
            String songType = textSongTypeAdd.Text;
            if(String.IsNullOrEmpty(songName)|| String.IsNullOrEmpty(songAlbum)|| String.IsNullOrEmpty(songSinger)|| String.IsNullOrEmpty(songLong)|| String.IsNullOrEmpty(date)|| String.IsNullOrEmpty(songType))
            {
                MessageBox.Show("Sarki bilgileri bos birakilamaz!");
            }
            else if (addSarki(songName, songSinger, songAlbum, songLong, songType))
            {
                MessageBox.Show($"{songName}-{songSinger} sarkilara basariyla eklendi!");
                add_to_albumsarki(searchalbumIDfromalbumler(songAlbum), searchsarkiIDfromsarkilar(songName, songSinger));
            }
            else
            {
                MessageBox.Show($"{songName}-{songSinger} sarkilara eklenemedi!");
            }

        }
        //Şarkı siler
        private void buttonDeleteSong_Click(object sender, EventArgs e)
        {
            String songName = textSongNameAdd.Text;
            String songAlbum = textSongAlbumAdd.Text;
            String songSinger = textSongSingerAdd.Text;
            String songLong = textSongLongAdd.Text;
            String date = textSongDateAdd.Text;
            String songType = textSongTypeAdd.Text;

            if (searchSarki(songName, songSinger))
            {
                if (deleteSarki(songName, songSinger))
                {
                    deleteSongfromalbum_sarki(searchsarkiIDfromsarkilar(songName, songSinger));
                    MessageBox.Show($"{songName}-{songSinger} sarkilardan basariyla silindi!");
                }
                else
                {
                    MessageBox.Show($"{songName}-{songSinger} sarkilardan silinemedi!");
                }
            }
            else
            {
                MessageBox.Show($"{songName}-{songSinger} sarkilarda bulunamadi!");
            }
        }
        //Şarkı günceller
        private void buttonUpdateSong_Click(object sender, EventArgs e)
        {
            String songName = textSongNameAdd.Text;
            String songSinger = textSongSingerAdd.Text;

            if (searchSarki(songName, songSinger))
            {
                songUpdatePanel.Show();
            }
            else
            {
                MessageBox.Show($"{songName}-{songSinger} sarkilarda bulunamadi!");
            }
        }



        /* 
         *Albüm Güncelleme Paneli İşlemleri 
         */

        //Albüm Güncelleme paneli gözükür
        private void buttonAlbumProsc_Click(object sender, EventArgs e)
        {
            albumProscPanel.Show();
            singerProscPanel.Hide();
            songProcsPanel.Hide();
        }
        //Albüm Güncelleme paneli görünmez olur
        private void buttonBackAlbum_Click(object sender, EventArgs e)
        {
            albumProscPanel.Hide();
        }
        //Album günceller
        private void buttonUpdateAlbum_Click(object sender, EventArgs e)
        {
            String albumName = textAlbumNameAdd.Text;
            String albumSinger = textAlbumSingerAdd.Text;

            if (searchAlbum(albumName, albumSinger))
            {
                albumUpdatePanel.Show();

            }
            else
            {
                MessageBox.Show($"{albumName}-{albumSinger} albumlerde bulunamadi!");
            }
        }
        //Album siler
        private void buttonDeleteAlbum_Click(object sender, EventArgs e)
        {
            String albumName = textAlbumNameAdd.Text;
            String albumDate = textAlbumDateAdd.Text;
            String albumType = textAlbumTypeAdd.Text;
            String albumSinger = textAlbumSingerAdd.Text;

            if (searchAlbum(albumName, albumSinger))
            {
                deleteAlbumfromalbum_sanatci(searchalbumIDfromalbumler(albumName));
                if (deleteAlbum(albumName, albumSinger))
                {                   
                    MessageBox.Show($"{albumName}-{albumSinger} albumlerden basariyla silindi!");                    
                }
                else
                {
                    MessageBox.Show($"{albumName}-{albumSinger} albumlerden silinemedi!");
                }
            }
            else
            {
                MessageBox.Show($"{albumName}-{albumSinger} albumlerde bulunamadi!");
            }
        }
        //Albüm ekler
        private void buttonAppendAlbum_Click(object sender, EventArgs e)
        {
            String albumName = textAlbumNameAdd.Text;
            String albumDate = textAlbumDateAdd.Text;
            String albumType = textAlbumTypeAdd.Text;
            String albumSinger = textAlbumSingerAdd.Text;
            if(String.IsNullOrEmpty(albumName)||String.IsNullOrEmpty(albumDate)|| String.IsNullOrEmpty(albumSinger))
            {
                MessageBox.Show("Album bilgileri bos birakilamaz!");
            }           
            else if (addAlbum(albumName, albumType, albumDate, albumSinger))
            {
                MessageBox.Show($"{albumName} albumlere basariyla eklendi!");
                add_to_albumSanatci(searchalbumIDfromalbumler(albumName), search_sanatciID_fromSanatcilar(albumSinger));
            }
            else
            {
                MessageBox.Show($"{albumName} albumlere eklenemedi!");
            }
        }


        /*
         *
         *Kullanıcı İşlemleri
         *
         */

        //Kullanıcı giriş paneline giriş yapar
        private void buttonLogUser_Click(object sender, EventArgs e)
        {
            logPanel.Hide();
            userloginPanel.Show();
        }

        /*
         * Kayıt paneli işlemleri
         */
        //Yeni kullanıcıyı kaydeder Login paneline döner
        private void buttonRegConf_Click(object sender, EventArgs e)
        {
            String userEmail = textNewUsMail.Text;
            String userName = textNewUser.Text;
            String userCountry = textNewCountry.Text;
            String userPassword = string.Empty;
            if (textNewPass.Text.Equals(textNewPassConf.Text))
            {
                userPassword = textNewPass.Text;
            }
            else
            {
                MessageBox.Show("Sifreler uyusmamaktadir");
            }

            if (Register(userName, userPassword, userEmail, userCountry))
            {
                MessageBox.Show($"Kullanici {userName} basariyla olusturuldu!");
                calmaListesiCreate(userName, "jazz");
                calmaListesiCreate(userName, "pop");
                calmaListesiCreate(userName, "klasik");
                takipListCreate(userName);
                userloginPanel.Show();
                registerPanel.Hide();
            }
            else
            {
                MessageBox.Show($"Kullanici {userName} olusturulamadi!");
            }
        }

        //Kayıt ekranından bir önceki panele(userLogPanel) döner
        private void buttonRegBack_Click(object sender, EventArgs e)
        {
            userloginPanel.Show();
            registerPanel.Hide();
        }

        /*
         *Kullanıcı giriş paneli işlemleri 
         */

        //Kullanıcı ekranından bir önceki panele(logPanel) döner
        private void buttonUserLogBack_Click(object sender, EventArgs e)
        {
            logPanel.Show();
            userloginPanel.Hide();
        }

        //Kullanıcı kaydı oluşturma panelini gösterir
        private void buttonRegister_Click(object sender, EventArgs e)
        {
            userloginPanel.Hide();
            registerPanel.Show();

        }
        //Kullanıcı giriş yapar
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            String user = textUserName.Text;
            String pass = textUserPass.Text;
            Boolean isPremium = false;
            if (IsLogin(user, pass))
            {
                if (IsPremium(user))
                {
                    MessageBox.Show($"Hosgeldin premium {user}!");
                    userloginPanel.Hide();
                    normalUserPanel.Show();
                    buttonBePre.Text = "Premiumdan ayrıl";
                }
                else
                {
                    MessageBox.Show($"Hosgeldin {user}!");
                    normalUserPanel.Show();
                    userloginPanel.Hide();
                    buttonBePre.Text = "Premium uyelige gecis yap";
                }
            }
            else
            {
                MessageBox.Show($"Kullanici adi veya sifre yanlis!");
            }          
        }



        /*
         * 
         *Premium kullanıcı paneli işlemleri 
         *
         */

        private void buttonPremiumExit_Click(object sender, EventArgs e)
        {
            searchPanel.Hide();
            topTenPanel.Hide();
            classicPanel.Hide();
            popPanel.Hide();
            jazzPanel.Hide();
            userloginPanel.Show();
        }
        //Premium kullanıcı premiumdan ayrılır
        private void buttonBeNormal_Click(object sender, EventArgs e)
        {
            String user = textUserName.Text;
            String tur = "Normal";
            if (changekulTur(user, tur))
            {
                MessageBox.Show("Premiumdan Ayrıldınız!");
                normalUserPanel.Show();

            }
            else
            {
                MessageBox.Show("Premiumdan ayrilma islemi gerceklesemedi!");
            }
        }
        //Jazz şarkı listesini premium kullanıcı için açar
        private void buttonPremiumJazz_Click(object sender, EventArgs e)
        {
            String user = textUserName.Text;
            jazzPanel.Show();
            searchPanel.Hide();
            topTenPanel.Hide();
            classicPanel.Hide();
            popPanel.Hide();
            kulJazzList(user);

        }
        //Pop şarkı listesini premium kullanıcı için açar
        private void buttonPremiumPop_Click(object sender, EventArgs e)
        {
            String user = textUserName.Text;
            popPanel.Show();
            searchPanel.Hide();
            topTenPanel.Hide();
            classicPanel.Hide();
            jazzPanel.Hide();
            kulPopList(user);

        }
        //Klasik şarkı listesini premium kullanıcı için açar
        private void buttonPremiumClassic_Click(object sender, EventArgs e)
        {
            String user = textUserName.Text;
            classicPanel.Show();
            searchPanel.Hide();
            topTenPanel.Hide();
            popPanel.Hide();
            jazzPanel.Hide();
            kulKlasikList(user);
            
        }
        //Şarkı arama panelini premium kullanıcı için açar
        private void buttonPremiumSearch_Click(object sender, EventArgs e)
        {
            searchPanel.Show();
            topTenPanel.Hide();
            classicPanel.Hide();
            popPanel.Hide();
            jazzPanel.Hide();
            
        }
        //TOP10 panelini premium kullanıcı için açar
        private void buttonPreTop_Click(object sender, EventArgs e)
        {
            topTenPanel.Show();
            searchPanel.Hide();
            classicPanel.Hide();
            popPanel.Hide();
            jazzPanel.Hide();
            if (!top10click)
            {
                top10();
            }

        }

        /*
         * 
         *Normal kullanıcı paneli işlemleri 
         *
         */
        
        private void buttonNormalExit_Click(object sender, EventArgs e)
        {
            userloginPanel.Show();
            searchPanel.Hide();
            topTenPanel.Hide();
            classicPanel.Hide();
            popPanel.Hide();
            jazzPanel.Hide();
            preSearchPanel.Hide();
            followPanel.Hide();
            normalUserPanel.Hide();
            preSongListPanel.Hide();
        }
        //Jazz şarkı listesini normal kullanıcı için gösterir
        private void buttonNormalJazz_Click(object sender, EventArgs e)
        {
            String user = textUserName.Text;
            jazzPanel.Show();
            searchPanel.Hide();
            topTenPanel.Hide();
            classicPanel.Hide();
            popPanel.Hide();
            preSearchPanel.Hide();
            followPanel.Hide();
            preSongListPanel.Hide();
            kulJazzList(user);
            
            

        }
        //Pop şarkı listesini normal kullanıcı için gösterir
        private void buttonNormalPop_Click(object sender, EventArgs e)
        {
            String user = textUserName.Text;
            popPanel.Show();
            searchPanel.Hide();
            topTenPanel.Hide();
            classicPanel.Hide();
            jazzPanel.Hide();
            preSearchPanel.Hide();
            followPanel.Hide();
            preSongListPanel.Hide();
            kulPopList(user);
            
        }
        //Klasik şarkı listesini normal kullanıcı için gösterir
        private void buttonNormalClassic_Click(object sender, EventArgs e)
        {
            String user = textUserName.Text;
            classicPanel.Show();
            searchPanel.Hide();
            topTenPanel.Hide();
            popPanel.Hide();
            jazzPanel.Hide();
            preSearchPanel.Hide();
            followPanel.Hide();
            preSongListPanel.Hide();

            kulKlasikList(user);

        }
        //Şarkı arama panelini normal kullanıcı için açar
        private void buttonNormalSearchSong_Click(object sender, EventArgs e)
        {
            searchPanel.Show();
            topTenPanel.Hide();
            classicPanel.Hide();
            popPanel.Hide();
            jazzPanel.Hide();
            preSearchPanel.Hide();
            preSongListPanel.Hide();
            followPanel.Hide();
            
        }
        //TOP10 panelini normal kullanıcı için açar
        private void buttonNormalTop_Click(object sender, EventArgs e)
        {
            topTenPanel.Show();
            searchPanel.Hide();
            classicPanel.Hide();
            popPanel.Hide();
            jazzPanel.Hide();
            preSearchPanel.Hide();
            followPanel.Hide();
            preSongListPanel.Hide();
            comboBoxCountry.Visible = false;
            buttonUpdateTopTenList.Visible = false;

            if (!top10click)
            {
                top10();
            }
            comboBoxCountry.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCountry.Items.Clear();
            addUlketoComboBox();
        }
        //Premium kullanıcı arama panelini normal kullanıcı için açar
        private void buttonNormalSearchPre_Click(object sender, EventArgs e)
        {
            preSearchPanel.Show();
            searchPanel.Hide();
            topTenPanel.Hide();
            classicPanel.Hide();
            popPanel.Hide();
            jazzPanel.Hide();
            followPanel.Hide();
            preSongListPanel.Hide();

        }
        //Normal kullanıcı premium olur
        private void buttonBePre_Click(object sender, EventArgs e)
        {
            String user = textUserName.Text;

            getKulTur(user);
            if (string.Equals(kullaniciTuru, "Normal"))
            {
                changekulTur(user, "Premium");
                buttonBePre.Text = "Premiumdan ayrıl";
                MessageBox.Show("Premium uyelige hos geldiniz!");
            }
            else
            {
                changekulTur(user, "Normal");
                buttonBePre.Text = "Premiuma gecis yap";
                MessageBox.Show("Premium uyeliginizi iptal ettiniz!");
            }
        }
        /*
         *Jazz paneli işlemleri 
         */

        private void buttonJazzBack_Click(object sender, EventArgs e)
        {
            jazzPanel.Hide();
        }
        //Jazz listesini günceller
        private void buttonJazzUpdate_Click(object sender, EventArgs e)
        {

        }


        /*
         *Pop paneli işlemleri 
         */

        private void buttonPopBack_Click(object sender, EventArgs e)
        {
            popPanel.Hide();
        }
        //Pop listesini günceller
        private void buttonPopUpdate_Click(object sender, EventArgs e)
        {

        }


        /*
         *Klasik paneli işlemleri 
         */

        private void buttonClassicBack_Click(object sender, EventArgs e)
        {
            classicPanel.Hide();
        }
        //Klasik listesini günceller
        private void buttonClassicUpdate_Click(object sender, EventArgs e)
        {

        }


        /*
         *Premium kullanıcı arama paneli işlemleri 
         */

        private void buttonPreSearchBack_Click(object sender, EventArgs e)
        {
            preSearchPanel.Hide();
        }
        //Premium kullanıcıyı takip eder
        private void buttonFollow_Click(object sender, EventArgs e)
        {
            String user = textUserName.Text;
            int userfollowID = Convert.ToInt32(listPreFounded.SelectedItems[0].SubItems[3].Text);
            if (listPreFounded.SelectedItems.Count == 1)
            {
                if (searchUserinTakipList(user, userfollowID))
                {
                    MessageBox.Show($"{listPreFounded.SelectedItems[0].Text} zaten takip ediliyor!");
                }
                else
                {
                    if (follow(user, userfollowID))
                    {
                        MessageBox.Show($"{listPreFounded.SelectedItems[0].Text} takip edildi!");
                    }
                    else
                    {
                        MessageBox.Show($"{listPreFounded.SelectedItems[0].Text} takip edilemedi!");
                    }
                }
                
            }
        }
        //Premium kullanıcı arar
        private void buttonPreSearch_Click(object sender, EventArgs e)
        {
            string preUsername = textPreSearchBox.Text;
            searchkullanici(preUsername);
        }


        /*
         *Şarkı arama paneli işlemleri 
         */

        private void buttonSearchBack_Click(object sender, EventArgs e)
        {
            searchPanel.Hide();
        }
        //Şarkı ekler
        private void buttonUserSongAdd_Click(object sender, EventArgs e)
        {
            String user = textUserName.Text;
            if (listSongFounded.SelectedItems.Count == 1)
            {
                int ID = Convert.ToInt32(listSongFounded.SelectedItems[0].SubItems[3].Text);
                getSarkiTur(sarkiID);
                if (searchSonginUserList(user,sarkiTuru,ID))
                {
                    MessageBox.Show($"{listSongFounded.SelectedItems[0].Text}-{listSongFounded.SelectedItems[0].SubItems[1].Text} calma listenizde zaten var!");
                }
                else if (addtoKullList(user, ID))
                {
                    MessageBox.Show($"{listSongFounded.SelectedItems[0].Text}-{listSongFounded.SelectedItems[0].SubItems[1].Text} calma listenize eklendi!");
                }
                else
                {              
                    MessageBox.Show($"{listSongFounded.SelectedItems[0].Text}-{listSongFounded.SelectedItems[0].SubItems[1].Text} calma listenize eklenemedi!");
                }
            }
        }
        //Şarkı arar
        private void buttonSeach_Click(object sender, EventArgs e)
        {
            String songName = textSearchBox.Text;
            searchSong(songName);
        }
        //Takipçi panelini açar
        private void buttonProcsFollowing_Click(object sender, EventArgs e)
        {
            preSongListPanel.Hide();
            String user = textUserName.Text;
            kulTakipList(user);
            followPanel.Show();

        }

        /*
         * Top 10 paneli işlemleri 
         */
        private void buttonTopBack_Click(object sender, EventArgs e)
        {
            topTenPanel.Hide();
        }
        
        //Top Ten listesini günceller
        private void buttonUpdateTopTenList_Click(object sender, EventArgs e)
        {
            
            if (radioCountryTopPanel.Checked)
            {
                if (comboBoxCountry.SelectedIndex> -1)
                {
                    string country = comboBoxCountry.SelectedItem.ToString();
                    top10ulke(country);
                }
                else
                {
                    MessageBox.Show("Lutfen ulke seciniz!");
                }
            }
            
        }
        
        /*
         * Takipçi paneli işlemleri
         */

        private void buttonFollowBack_Click(object sender, EventArgs e)
        {
            followPanel.Hide();
        }
        //Takip edilenin listesini gösterir
        private void buttonPreSongList_Click(object sender, EventArgs e)
        {
            preSongListPanel.Show();
        }

        /*
         * Takip edilen kullanıcı verileri ile ilgili panel işlemleri
         */
        private void buttonPreSongListBack_Click(object sender, EventArgs e)
        {
            preSongListPanel.Hide();
        }
        //Pre kullanıcının jazz listesini gösterir
        private void buttonPreSongListJazz_Click(object sender, EventArgs e)
        {
            String userr = listFollowing.SelectedItems[0].Text;
            if (listFollowing.SelectedItems.Count == 1)
            {
                showList(userr, "jazz");
            }
            klasikclicked = false;
            popclicked = false;
            jazzclicked = true;
        }
        //Pre kullanıcının pop listesini gösterir
        private void buttonPreSongListPop_Click(object sender, EventArgs e)
        {
            String userr = listFollowing.SelectedItems[0].Text;
            if (listFollowing.SelectedItems.Count == 1)
            {
                showList(userr, "pop");
            }
            klasikclicked = false;
            popclicked = true;
            jazzclicked = false;
        }
        //Pre kullanıcının klasik listesini gösterir
        private void buttonPreSongListClassic_Click(object sender, EventArgs e)
        {
            String userr = listFollowing.SelectedItems[0].Text;
            if (listFollowing.SelectedItems.Count == 1)
            {
                showList(userr, "klasik");
            }
            klasikclicked = true;
            popclicked = false;
            jazzclicked = false;
        }
        //İlgili listedeki tüm şarkıları ekler
        private void buttonPreSongAddAll_Click(object sender, EventArgs e)
        {
            string preKulad = listFollowing.SelectedItems[0].Text;
            String user = textUserName.Text;                     
            string listeturu=String.Empty;
            if (popclicked)
            {
                listeturu = "pop";
            }
            else if (jazzclicked)
            {
                listeturu = "jazz";
            }
            else if (klasikclicked)
            {
                listeturu = "klasik";
            }

            if (addtokulllistall(preKulad, user, listeturu))
            {
                MessageBox.Show("Butun sarkilar calma listenize eklendi!");
            }
            else
            {
                MessageBox.Show("eklenemedi");
            }

        }
        //İlgili listedeki seçilen şarkıları ekler
        private void buttonPreSongAddChoosed_Click(object sender, EventArgs e)
        {
            string preKulad = listFollowing.SelectedItems[0].Text;
            String user = textUserName.Text;
            string listeturu = String.Empty;
            int sarkiID = Int32.Parse(listView1.SelectedItems[0].SubItems[3].Text);
            getSarkiTur(sarkiID);
            if (searchSonginUserList(user, sarkiTuru, sarkiID))
            {
                MessageBox.Show($"{listView1.SelectedItems[0].Text}-{listView1.SelectedItems[0].SubItems[1].Text} calma listenizde zaten var!");
            }
            else if(addtoKullListfromPre(user,sarkiID))
            {
                MessageBox.Show($"{listView1.SelectedItems[0].Text}-{listView1.SelectedItems[0].SubItems[1].Text} calma listenize eklendi!");
            }
            else
            {
                MessageBox.Show($"{listView1.SelectedItems[0].Text}-{listView1.SelectedItems[0].SubItems[1].Text} calma listenize eklenemedi!");
            }
            
            
        }



        /*
         * 
         *Şarkı güncelleme paneli işlemleri 
         *
         */

        //Doldurulan alana göre şarkı günceller
        private void buttonApplyUpdateSong_Click(object sender, EventArgs e)
        {
            String songOldName = textSongNameAdd.Text;
            String songOldSinger = textSongSingerAdd.Text;
            String songNewName = textSongNameUpdate.Text;
            String songNewSinger = textSongSingerUpdate.Text;
            String songNewAlbum = textSongAlbumUpdate.Text;
            String songNewLength = textSongLongUpdate.Text;
            String songNewDate = textSongDateUpdate.Text;
            String songNewTur = textSongType.Text;
            if (updateSong(songOldName, songOldSinger, songNewName, songNewSinger, songNewAlbum, songNewLength, songNewDate, songNewTur))
            {
                MessageBox.Show($"{songOldName}-{songOldSinger} yeni bilgilerle basariyla güncellendi!");
            }
            else
            {
                MessageBox.Show($"{songOldName}-{songOldSinger} güncellenemedi!");
            }
        }
        //Güncelleme paneliden çıkar
        private void buttonBackUpdateSong_Click(object sender, EventArgs e)
        {
            songUpdatePanel.Hide();
        }

       /*
        * 
        *Sanatçı güncelleme paneli işlemleri 
        *
        */

        //Doldurulan alana göre sanatçıyı günceller
        private void buttonApplyUpdateSinger_Click(object sender, EventArgs e)
        {
            String singerName = textSingerNameAdd.Text;
            String singerCountry = textSingerCountryAdd.Text;
            String singerNewName = textSingerNameUpdate.Text;
            String singerNewCountry = textSingerCountryUpdate.Text;
            if (updateSanatci(singerName, singerCountry, singerNewName, singerNewCountry))
            {
                MessageBox.Show($"{singerName} yeni bilgilerle basariyla güncellendi!");
            }
            else
            {
                MessageBox.Show($"{singerName} güncellenemedi!");
            }
        }

        private void buttonBackUpdateSinger_Click(object sender, EventArgs e)
        {
            singerUpdatePanel.Hide();
        }


        /*
         * 
         *Albüm güncelleme paneli işlemleri 
         *
         */

        //Doldurulan alana göre albümü günceller
        private void buttonApplyUpdateAlbum_Click(object sender, EventArgs e)
        {
            String albumOldName = textAlbumNameAdd.Text;
            String albumOldSinger = textAlbumSingerAdd.Text;
            String albumNewName = textAlbumNameUpdate.Text;
            String albumNewSinger = textAlbumSingerUpdate.Text;
            String albumNewDate = textAlbumDateUpdate.Text;
            String albumNewTur = textAlbumTypeUpdate.Text;

            if (updateAlbum(albumOldName, albumOldSinger, albumNewName, albumNewSinger, albumNewDate, albumNewTur))
            {
                MessageBox.Show($"{albumOldName}-{albumOldSinger} yeni bilgilerle basariyla güncellendi!");
            }
            else
            {
                MessageBox.Show($"{albumOldName}-{albumOldSinger} güncellenemedi!");
            }
        }

        private void buttonBackUpdateAlbum_Click(object sender, EventArgs e)
        {
            albumUpdatePanel.Hide();
        }

        int logcheckpass = 1;

        //Login ekranında şifreyi gösterir
        private void showPassLog_CheckedChanged(object sender, EventArgs e)
        {
            if (logcheckpass % 2 == 0)
            {
                textUserPass.UseSystemPasswordChar = true;
                logcheckpass++;
            }
            else
            {
                textUserPass.UseSystemPasswordChar = false;
                logcheckpass++;
            }
            
        }
        int admincheckpass = 1;
        //Admin ekranında şifreyi gösterir
        private void showPassAdmin_CheckedChanged(object sender, EventArgs e)
        {
            if (admincheckpass % 2 == 0)
            {
                textAdminPass.UseSystemPasswordChar = true;
                admincheckpass++;
            }
            else
            {
                textAdminPass.UseSystemPasswordChar = false;
                admincheckpass++;
            }
        }
        //kayıt ekranında şifreyi gösterir
        int regcheckpass = 1;

        private void showRegAdmin_CheckedChanged(object sender, EventArgs e)
        {
            if (regcheckpass % 2 == 0)
            {
                textNewPass.UseSystemPasswordChar = true;
                textNewPassConf.UseSystemPasswordChar = true;
                regcheckpass++;
            }
            else
            {
                textNewPass.UseSystemPasswordChar = false;
                textNewPassConf.UseSystemPasswordChar = false;
                regcheckpass++;
            }
        }

        private void radioGeneralClassic_CheckedChanged(object sender, EventArgs e)
        {
            String user = textUserName.Text;
            kulKlasikList(user);
        }

        private void radioTopClassic_CheckedChanged(object sender, EventArgs e)
        {
            topList("Klasik");
        }

        private void radioGeneralPop_CheckedChanged(object sender, EventArgs e)
        {
            String user = textUserName.Text;
            kulPopList(user);

        }

        private void radioTopPop_CheckedChanged(object sender, EventArgs e)
        {
            topList("Pop");
        }

        private void radioGeneralJazz_CheckedChanged(object sender, EventArgs e)
        {
            String user = textUserName.Text;
            kulJazzList(user);
        }

        private void radioTopJazz_CheckedChanged(object sender, EventArgs e)
        {
            topList("Jazz");
        }

        private void comboBoxCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioGeneralTopPanel_CheckedChanged(object sender, EventArgs e)
        {
            top10();
            comboBoxCountry.Visible = false;
            buttonUpdateTopTenList.Visible = false;
        }

        private void radioCountryTopPanel_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxCountry.Visible = true;
            buttonUpdateTopTenList.Visible = true;
            if (comboBoxCountry.SelectedIndex > -1)
            {
                string country = comboBoxCountry.SelectedItem.ToString();
                top10ulke(country);
            }
        }
    }
}
