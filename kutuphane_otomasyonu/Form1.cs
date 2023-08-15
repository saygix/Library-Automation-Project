using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace WindowsFormsApp13
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS03;Database=kütüphane;Trusted_Connection=True;");    //Bu veritabanına bağlanmak için gerekli olan bağlantı cümlemiz
        string Cinsiyet;  //cinsiyet eklemesi için string olarak tanımlama
        public static string Kullanici;     //Form3 kısmına veri çekmek için kullanıcı string olarak tanımlama


        //LinkLabel ile şifremi unuttum ekleyip yeni forma geçiş yaptım.
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.ShowDialog();
        }

       
        //Burada kullanıcı girişi kodu
        private void button1_Click(object sender, EventArgs e)
        {
            Kullanici = maskedTextBox1.Text;  //Form 3 kısmına veri çekmek için nerden aldığımı belirttim
            login();                           //giriş yapmak için Fonksiyonu Çağırdım

        }
        

        public void login()
        {
            conn.Open();                                                           // ile bağlantımızı açtık.
            string kayit = "SELECT *from Kullanici where(Tc=@tc and sifre=@sf) "; //Kullanici tablosunda tc ve şifre getir ikisinin doğru olduğu yerde veri çek
            SqlCommand komut = new SqlCommand(kayit, conn);                     //kayıttaki sql cümleciğini veritabanımıza uygular
            komut.Parameters.AddWithValue("@tc", maskedTextBox1.Text);         //tc değerine maskedtextbox1den alınan değeri ata demek
            komut.Parameters.AddWithValue("@sf", textBox2.Text);              //sf değerine textbox2den alınan değeri ata demek
            SqlDataReader dr = komut.ExecuteReader();                        //datareaderr ı çalıştır demek

            //Eğer veri döndüyse SqlDataReaderimizi(oku’yu) okuyabiliriz ama eğer veri dönmediyse yani SqlDataReaderimiz(oku) boşsa okuyamayız.
            //Bu yüzden if döngüsü kullanacağız.

            if (dr.Read())   //read okuyo mu diye kontrol ediyor
            {

                //Giriş bilgileri doğruysa yeni forma geç
                timer1.Start();

            }
            else
                MessageBox.Show("Giriş Bilgileri Hatalı");
            
            conn.Close();  //Eğer veri döndüyse bağlantımızı kapatıyoruz 
        }

        //Burada Üye eklemesini yaptım
        private void Form1_Load(object sender, EventArgs e)
        {
            VeriÇek();     //VeriÇek fonksiyonunu çağırdım
            progressBar1.Maximum = 50; //maxı 50ye ata
            progressBar1.Minimum = 0;  //mini 0a ata
            progressBar1.Step = 5;     //stepi 5e ata
            progressBar1.Value = 5;   //value 5e ata
            
        }
        public void VeriÇek()
        {
            dataGridView1.GridColor = Color.Green;                //DataGridView rengini değiştirdim
            conn.Open();                                         // Bağlantıyı açtım
            string kayıt = "SELECT *from Kullanici ";           //Kullanici tablosundaki verileri seçtim
            SqlCommand komut = new SqlCommand(kayıt, conn);    //kayıttaki sql cümleciğini veritabanımıza uygular
            SqlDataAdapter da = new SqlDataAdapter(komut);    //Ardından DataAdapter verilerimizi çektik. 
            DataTable dt = new DataTable();                  //çekilen verileri barındırmak için DataTable oluşturduk
            da.Fill(dt);                                    //Son adımda ise bu çekilen verileri Fill komutu ile oluşturulan DataTable a aktardım
            dataGridView1.DataSource = dt;                 //dataGridView1 verileri çağırdım
            conn.Close();                                 // bağlantıyı kapattım
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            VeriEkle();   //fonksiyonu çağırdım ve veri ekledim
            VeriÇek();   //tektar veri çeki çağırdım ki verileri göstersin
           

        }
        
        public void VeriEkle()
        {
            if (textBox3.Text == "" || textBox4.Text == "" ||textBox5.Text=="" ||textBox6.Text=="" || Cinsiyet== "")  //Texboklar boşmu dolumu kontrol etme
            {
                MessageBox.Show("Üye olunamadı.");     //Boşsa işlem gerçekleşmez


            }
            else   //doluysa
            {
                conn.Open();                                                                                       //bağlantıyı aç
                string kayıt = "insert into Kullanici(Tc,ad,soyad,sifre,Cinsiyet) values(@tc,@ad,@sd,@sf,@ci) ";  //kayıt değikeni ile kullanıcı tablosundanki verileri eklemek için değerleri tanımlıyoruz
                SqlCommand komut = new SqlCommand(kayıt, conn);             //kayıttaki sql cümleciğini veritabanımıza uygular
                komut.Parameters.AddWithValue("@tc", textBox3.Text);       //tc değerine textbox3den alınan değeri ata demek
                komut.Parameters.AddWithValue("@ad", textBox4.Text);      //ad değerine textbox4den alınan değeri ata demek
                komut.Parameters.AddWithValue("@sd", textBox5.Text);     //sd değerine textbox5den alınan değeri ata demek
                komut.Parameters.AddWithValue("@sf", textBox6.Text);    //sf değerine textbox6den alınan değeri ata demek
                komut.Parameters.AddWithValue("@ci",Cinsiyet);         //ci değerine radiobuttondan alınan değeri ata demek
                komut.ExecuteNonQuery();                              // komutumuzu çalıştıralım
                conn.Close();                                        //bağlantıyı kapat
                MessageBox.Show("üye oldunuz");                     //Ve işlem tamam
            }

        }

        //CheckBox ile şifreyi gizleme yaptım
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked )               //CheckBox seçili ise
            {
                textBox2.PasswordChar = '\0';   //göster
                
            }
            else                               //değilse
            {
                textBox2.PasswordChar = '*';   //gizle
                
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)        //CheckBox seçili ise
            {
                textBox6.PasswordChar = '\0';   //göster

            }
            else                             //değilse
            {
                textBox6.PasswordChar = '*';  //gizle

            }
        }

       //Bunun sayesinde değişmiş şifreyi dataGridViewde gösterdim

        private void label7_Click(object sender, EventArgs e)
        {
            VeriÇek();    //Şifremi unuttum kısmında şifre ddeğiştirdiğimde datagridviewde görmek için veri çeki çağırdim bunun sayesinde şifremi değiştirip görebiliyorum
            
        }



        //Yönetici Girişi
        private void button3_Click(object sender, EventArgs e)
        {
            Yonetıcılogin();
        }
        public void Yonetıcılogin()
        {
            conn.Open();                                                                       //bağlantıyı açıyoruz
            string kayit = "SELECT *from Yonetici where(KullaniciAdi=@ka and sifre=@sf) ";     //Yonetici tablosunda tc ve şifre getir ikisinin doğru olduğu yerde veri çek
            SqlCommand komut = new SqlCommand(kayit, conn);                                   //kayıttaki sql cümleciğini veritabanımıza uygular
            komut.Parameters.AddWithValue("@ka", textBox7.Text);                             //ka değerine textbox7den alınan değeri ata demek
            komut.Parameters.AddWithValue("@sf", textBox8.Text);                            //sf değerine textbox8den alınan değeri ata demek.
            SqlDataReader dr = komut.ExecuteReader();                                      //SqlDataReader tanımlıyoruz ve komutumuzdan dönen kayıt var mı yok mu onu kontrol ediyoruz ve komudu çalıştırıyoruz
            //Eğer veri döndüyse SqlDataReaderimizi(oku’yu) okuyabiliriz ama eğer veri dönmediyse yani SqlDataReaderimiz(oku) boşsa okuyamayız.
            //Bu yüzden if döngüsü kullanacağız.
            if (dr.Read())
            {   
               // Doğruysa form5 geç
                Form5 frm5 = new Form5();
                frm5.Show();
                MessageBox.Show("Giriş Yapıldı");
            }
            else
            {
                //değilse giriş başarısız 
                MessageBox.Show("Giriş Başarısız");
            }
            conn.Close();                                               //bağlantıyı kapatıyoruz
        }

        //CheckBox ile şifreyi gizleme yaptım

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)              //CheckBox seçili ise
            {
                textBox8.PasswordChar = '\0';  //Göster

            }
            else                              // değilse
            {
                textBox8.PasswordChar = '*';  //Gizle

            }
        }

        
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Cinsiyet = "Bay";     //bunu seçersen cinsiyet için Bay yazdır
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Cinsiyet = "Bayan";   //bunu seçersen cinsiyet için Bayan yazdır
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox1.Image = Properties.Resources.images; //fare üzerinden ayrıldığında bu resim 
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox1.Image = Properties.Resources.kullanici; //üzerinde iken bu resim 

        }


        //Form3 geçişi progressbar ile yaptım 
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value>=progressBar1.Maximum) //progressbar yukarda tanımladığımız max değerden büyük olduğunda
            {
                timer1.Stop();           //timeri durdur 
                progressBar1.Value = progressBar1.Minimum; //value değerini mine yani 0 ata
                Form3 frm3 = new Form3();          //form3'e geç
                frm3.ShowDialog();
            }
            else
            {
                progressBar1.PerformStep(); //değilse değilse step artır
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Giriş yapmak için kullanıcı adı ve şifreyi girin"); //Clik durumunda progress bar hata mesajını ver
        }

        //picturebox'ın clik özelliği ile yeni bir forma geçip fotoğrafı görüntüledim.
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6();
            frm6.Show();
        }
    }
}