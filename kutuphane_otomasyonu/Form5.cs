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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS03;Database=kütüphane;Trusted_Connection=True;");  //Bu veritabanına bağlanmak için gerekli olan bağlantı cümlemiz

        //Burada veri çekip veri ekledim. böylece Kitap ekledim. 
        private void Form5_Load(object sender, EventArgs e)
        {
            VeriÇek();     //fonksiyonu çağırdım
            YVeriÇek();   //fonksiyonu çağırdım
        }
        public void VeriÇek()
        {
            dataGridView1.GridColor = Color.Red;                     //dataGridView1 rengini değiştirdim
            conn.Open();                                            //bağlatıyı aç
            string kayıt = "SELECT *from KitapEkleSil";            //KitapEkleSil tablosundaki verileri seçtim
            SqlCommand komut = new SqlCommand(kayıt, conn);       //kayıttaki sql cümleciğini veritabanımıza uygular
            SqlDataAdapter da = new SqlDataAdapter(komut);       ///Ardından DataAdapter verilerimizi çektik.
            DataTable dt = new DataTable();                     //çekilen verileri barındırmak için DataTable oluşturduk
            da.Fill(dt);                                       ///Son adımda ise bu çekilen verileri Fill komutu ile oluşturulan DataTable a aktardım
            dataGridView1.DataSource = dt;                    //dataGridView1 verileri çağırdım
            conn.Close();                                    //bağlantıyı kapat 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VeriEkle(); //veriyi ekle fonksiyonu çağırdım
            VeriÇek();  //yeni verileri tekrar çağırdım
        }
        public void VeriEkle()
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")  //texboxlar boş ise
            {
                MessageBox.Show("boş alan bırakılamaz");                           //hata mesajını ver

            }
            else                                                                  //dolu ise
            {
                conn.Open();                                                                                  //bağlantıyı aç
                string kayıt = "insert into KitapEkleSil(KitapAdi,yazar,SayfaSayisi) values(@ka,@ya,@ss) ";  //kayıt değikeni ile KitapEkleSil tablosundanki verileri eklemek için değerleri tanımlıyoruz
                SqlCommand komut = new SqlCommand(kayıt, conn);                                             //kayıttaki sql cümleciğini veritabanımıza uygular
                komut.Parameters.AddWithValue("@ka", textBox1.Text);                                       //ka değerine textbox1den alınan değeri ata demek
                komut.Parameters.AddWithValue("@ya", textBox2.Text);                                      //ya eğerine textbox2den alınan değeri ata demek
                komut.Parameters.AddWithValue("@ss", textBox3.Text);                                     //ss değerine textbox3den alınan değeri ata demek
                komut.ExecuteNonQuery();                                                                //komutumuzu çalıştıralım
                conn.Close();                                                                          //bağlantıyı kapat
                MessageBox.Show("Kitap Başarıyla Eklendi");
            }
        }
      
        //Eklenen kitapları silme işlemi
        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)  //datagridviewde her bir satırı okuyo
            {
                string KitapAdi = Convert.ToString(drow.Cells[0].Value);  //o satırlardan 0. indistekini kitap adına atıyo
                KitapSil(KitapAdi);                                      //silme fonksiyonunu çağırdım
            }
        }


        public void KitapSil(string KitapAdi)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu Kitabı Silmek İstiyormusunuz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(dialog==DialogResult.Yes)
            {
                string kayit = "DELETE FROM KitapEkleSil WHERE KitapAdi=@ka";      //kitap adı @ka olan kitabı tablodan siliyo
                SqlCommand komut = new SqlCommand(kayit, conn);                   //kayıttaki sql cümleciğini veritabanımıza uygular
                komut.Parameters.AddWithValue("@ka", KitapAdi);                  //
                conn.Open();                                                    //bağlantıyı aç        
                komut.ExecuteNonQuery();                                       //komutumuzu çalıştıralım
                conn.Close();                                                 //bağlantıyı kapat
                VeriÇek();                                                   //veri çek fonksiyonunu silme işlemini tamamlandıktan sonra yeni verileri çağırmak için  

            }
           
        }




        //TextBoxların temizleme işlemi

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

       
        public void YVeriÇek()
        {
            dataGridView2.GridColor = Color.Purple;                       //dataGridView2 rengini değiştirdim
            conn.Open();                                                 //bağlantıyı aç
            string kayıt = "SELECT *from Yonetici ";                    //yönetici tablosundakileri getiriyor
            SqlCommand komut = new SqlCommand(kayıt, conn);            //komut tanımlıyoruz. bu da kayıttaki cümleciğimizle connetionla balıyor
            SqlDataAdapter da = new SqlDataAdapter(komut);            //yeni bi data adapter tanımlıyo ve bu komutumuzu sql ile bağlıyo
            DataTable dt = new DataTable();                          //çekilen verileri barındırmak için DataTable oluşturduk
            da.Fill(dt);                                            //Son adımda ise bu çekilen verileri Fill komutu ile oluşturulan DataTable a aktardım
            dataGridView2.DataSource = dt;                         ///dataGridView1 verileri çağırdım
            conn.Close();                                         //bağlantıyı kapat
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            YVeriEkle(); //veri ekle fonksiyonunu çağır
            YVeriÇek(); //tekrar veri çekle yeni işlemin verilerini çağırmak için 
        }
        public void YVeriEkle()
        {
            if (textBox9.Text == "" || textBox10.Text == "")  //textboxlar boşsa
            {
                MessageBox.Show("boş alan bırakılamaz");  //hata mesajı ver

            }
            else
            {
                conn.Open();                                                                  //bağlantıyı aç
                string kayıt = "insert into Yonetici(KullaniciAdi,sifre) values(@ka,@sf) ";  //kayıt değikeni ile Yonetici tablosundanki verileri eklemek için değerleri tanımlıyoruz
                SqlCommand komut = new SqlCommand(kayıt, conn);                             //komut tanımlıyoruz. bu da kayıttaki cümleciğimizle connetionla balıyor
                komut.Parameters.AddWithValue("@ka", textBox9.Text);                       //ka değerine textbox9den alınan değeri ata demek
                komut.Parameters.AddWithValue("@sf", textBox10.Text);                     //sf değerine textbox10den alınan değeri ata demek
                komut.ExecuteNonQuery();                                                 //komutumuzu çalıştıralım
                conn.Close();                                                           //bağlantıyı kapat
                MessageBox.Show("Artık Yöneticisiniz");
            }

        }
        ////Yönetici silme işlemi
        private void button6_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drow in dataGridView2.SelectedRows)  //datagridviewde her bir satırı okuyo
            {
                string KullaniciAdi = Convert.ToString(drow.Cells[0].Value);  //o satırlardan 0.indistekini kitap adına atıyo
                YöneticiSil(KullaniciAdi);  //sil fonksiyonunu çağır
            }
        }
        public void YöneticiSil(string KullaniciAdi)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu Yöneticiyi Silmek İstiyormusunuz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(dialog==DialogResult.Yes)
            {
                string kayit = "DELETE FROM Yonetici WHERE KullaniciAdi=@ku"; //kitap adı @ka olan kitabı tablodan siliyo
                SqlCommand komut = new SqlCommand(kayit, conn);              //komut tanımlıyor. bu da kayıttaki cümleciğimizle connetionla balıyor
                komut.Parameters.AddWithValue("@ku", KullaniciAdi);         //
                conn.Open();                                               //bağlantıyı aç
                komut.ExecuteNonQuery();                                  //komutumuzu çalıştıralım
                conn.Close();                                            //bağlantıyı kapat
                YVeriÇek();                                             //veri çek fonksiyonunu silme işlemini tamamlandıktan sonra yeni verileri çağırmak için  
            }
           
        }


    }
}
