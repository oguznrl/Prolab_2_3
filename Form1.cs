using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prolab2_3Spo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
            premiumUserPanel.Hide();
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
        }
        
        /*
         * 
         * Admin işlemleri
         * 
         */

        //Admin işlemleri yapacağı paneli açılır
        private void buttonAdminLog_Click(object sender, EventArgs e)
        {
            adminProscPanel.Show();
            adminPanel.Hide();
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
            singerUpdatePanel.Show();
            
        }
        //Sanatçı bilgisini siler
        private void buttonDeleteSing_Click(object sender, EventArgs e)
        {
            String singerName = textSingerNameAdd.Text;
            String singerCountry = textSingerCountryAdd.Text;
        }
        //Sanatçı ekler
        private void buttonAppendSing_Click(object sender, EventArgs e)
        {
            String singerName = textSingerNameAdd.Text;
            String singerCountry = textSingerCountryAdd.Text;
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
            String songName = textSongNameAdd.Text;
            String songAlbum = textSongAlbumAdd.Text;
            String[] songSinger=textSongSingerAdd.Text.Split(';');
            float songLong = float.Parse(textSongLongAdd.Text);
            String date = textSongDateAdd.Text;
            String songType = textSongTypeAdd.Text;

        }
        //Şarkı siler
        private void buttonDeleteSong_Click(object sender, EventArgs e)
        {
            String songName = textSongNameAdd.Text;
            String songAlbum = textSongAlbumAdd.Text;
            String[] songSinger = textSongSingerAdd.Text.Split(';');
            float songLong = float.Parse(textSongLongAdd.Text);
            String date = textSongDateAdd.Text;
            String songType = textSongTypeAdd.Text;
        }
        //Şarkı günceller
        private void buttonUpdateSong_Click(object sender, EventArgs e)
        {
            String songName = textSongNameAdd.Text;
            String songAlbum = textSongAlbumAdd.Text;
            String[] songSinger = textSongSingerAdd.Text.Split(';');
            String songLongTemp = textSongLongAdd.Text;
            //float songLong = float.Parse(songLongTemp);
            String date = textSongDateAdd.Text;
            String songType = textSongTypeAdd.Text;
            songUpdatePanel.Show();
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
            String albumDate = textAlbumDateAdd.Text;
            String albumType = textAlbumTypeAdd.Text;
            albumUpdatePanel.Show();
        }
        //Album siler
        private void buttonDeleteAlbum_Click(object sender, EventArgs e)
        {
            String albumName = textAlbumNameAdd.Text;
            String albumDate = textAlbumDateAdd.Text;
            String albumType = textAlbumTypeAdd.Text;
        }
        //Albüm ekler
        private void buttonAppendAlbum_Click(object sender, EventArgs e)
        {
            String albumName = textAlbumNameAdd.Text;
            String albumDate = textAlbumDateAdd.Text;
            String albumType = textAlbumTypeAdd.Text;
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
            String userPassword;
            if (textNewPass.Text.Equals(textNewPassConf.Text))
            {
                userPassword = textNewPass.Text;
            }
            else
            {
                MessageBox.Show("Şifreler uyuşmamaktadır");
            }
            userloginPanel.Show();
            registerPanel.Hide();
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
            String name = textUserName.Text;
            String password = textUserName.Text;
            Boolean isPremium = false;
            if (isPremium)
            {
                premiumUserPanel.Show();
                userloginPanel.Hide();
            }
            else
            {
                normalUserPanel.Show();
                userloginPanel.Hide();
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
            premiumUserPanel.Hide();
            userloginPanel.Show();
        }
        //Premium kullanıcı premiumdan ayrılır
        private void buttonBeNormal_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Premiumdan Ayrıldınız");
        }
        //Jazz şarkı listesini premium kullanıcı için açar
        private void buttonPremiumJazz_Click(object sender, EventArgs e)
        {
            jazzPanel.Show();
            searchPanel.Hide();
            topTenPanel.Hide();
            classicPanel.Hide();
            popPanel.Hide();
            
        }
        //Pop şarkı listesini premium kullanıcı için açar
        private void buttonPremiumPop_Click(object sender, EventArgs e)
        {
            popPanel.Show();
            searchPanel.Hide();
            topTenPanel.Hide();
            classicPanel.Hide();
            jazzPanel.Hide();
            
        }
        //Klasik şarkı listesini premium kullanıcı için açar
        private void buttonPremiumClassic_Click(object sender, EventArgs e)
        {
            classicPanel.Show();
            searchPanel.Hide();
            topTenPanel.Hide();
            popPanel.Hide();
            jazzPanel.Hide();
            
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
            premiumUserPanel.Hide();
            preSearchPanel.Hide();
            followPanel.Hide();
            normalUserPanel.Hide();
        }
        //Jazz şarkı listesini normal kullanıcı için gösterir
        private void buttonNormalJazz_Click(object sender, EventArgs e)
        {
            jazzPanel.Show();
            searchPanel.Hide();
            topTenPanel.Hide();
            classicPanel.Hide();
            popPanel.Hide();
            premiumUserPanel.Hide();
            preSearchPanel.Hide();
            followPanel.Hide();
            
        }
        //Pop şarkı listesini normal kullanıcı için gösterir
        private void buttonNormalPop_Click(object sender, EventArgs e)
        {
            popPanel.Show();
            searchPanel.Hide();
            topTenPanel.Hide();
            classicPanel.Hide();
            jazzPanel.Hide();
            premiumUserPanel.Hide();
            preSearchPanel.Hide();
            followPanel.Hide();
            
        }
        //Klasik şarkı listesini normal kullanıcı için gösterir
        private void buttonNormalClassic_Click(object sender, EventArgs e)
        {
            classicPanel.Show();
            searchPanel.Hide();
            topTenPanel.Hide();
            popPanel.Hide();
            jazzPanel.Hide();
            premiumUserPanel.Hide();
            preSearchPanel.Hide();
            followPanel.Hide();
            
        }
        //Şarkı arama panelini normal kullanıcı için açar
        private void buttonNormalSearchSong_Click(object sender, EventArgs e)
        {
            searchPanel.Show();
            topTenPanel.Hide();
            classicPanel.Hide();
            popPanel.Hide();
            jazzPanel.Hide();
            premiumUserPanel.Hide();
            preSearchPanel.Hide();
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
            premiumUserPanel.Hide();
            preSearchPanel.Hide();
            followPanel.Hide();
            
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
            premiumUserPanel.Hide();
            followPanel.Hide();
            
        }
        //Normal kullanıcı premium olur
        private void buttonBePre_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Premium üyeliğe Hoşgeldiniz!!!");
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
            
        }
        //Premium kullanıcı arar
        private void buttonPreSearch_Click(object sender, EventArgs e)
        {
            string preUsername = textPreSearchBox.Text;
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

        }
        //Şarkı arar
        private void buttonSeach_Click(object sender, EventArgs e)
        {
            String songName = textSearchBox.Text;
        }
        //Takipçi panelini açar
        private void buttonProcsFollowing_Click(object sender, EventArgs e)
        {
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

        }
        //Pre kullanıcının pop listesini gösterir
        private void buttonPreSongListPop_Click(object sender, EventArgs e)
        {

        }
        //Pre kullanıcının klasik listesini gösterir
        private void buttonPreSongListClassic_Click(object sender, EventArgs e)
        {

        }
        //İlgili listedeki tüm şarkıları ekler
        private void buttonPreSongAddAll_Click(object sender, EventArgs e)
        {

        }
        //İlgili listedeki seçilen şarkıları ekler
        private void buttonPreSongAddChoosed_Click(object sender, EventArgs e)
        {

        }



        /*
         * 
         *Şarkı güncelleme paneli işlemleri 
         *
         */

        //Doldurulan alana göre şarkı günceller
        private void buttonApplyUpdateSong_Click(object sender, EventArgs e)
        {

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
    }
}
