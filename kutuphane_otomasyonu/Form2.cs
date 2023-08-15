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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS03;Database=kütüphane;Trusted_Connection=True;"); //Bu veritabanına bağlanmak için gerekli olan bağlantı cümlemiz

        //Burada şifre değiştirdim.Güncelle butonu ile dataGridView yeni şifreyi güncelledim

        private void button1_Click(object sender, EventArgs e)
        {
            bool tckontrol;  //şifre değiştirmek istediğim için şifrenin değişip diğer bilgilerin doğru olması gerek bu yüzden boll ile bilgileri kontrol ettim
            conn.Open();    //bağlantıyı aç
            string kayit = " select Tc from Kullanici where Tc=@tc and ad=@ad and soyad=@sd "; //Kullanici tablosunda tc adı ve soyadı getir  doğru olduğu yerde veri çek
            SqlCommand komut = new SqlCommand(kayit, conn);          //kayıttaki sql cümleciğini veritabanımıza uygular
            komut.Parameters.AddWithValue("@tc", textBox1.Text);    //tc değerine textbox1den alınan değeri ata demek
            komut.Parameters.AddWithValue("@ad", textBox2.Text);   //ad değerine textbox1den alınan değeri ata demek
            komut.Parameters.AddWithValue("@sd", textBox3.Text);  //sd değerine textbox3den alınan değeri ata demek
            SqlDataReader dr = komut.ExecuteReader();            //datareaderr ı çalıştır demek

            if (dr.Read())           //okuyo mu diye kontrol ediyo
            {
                tckontrol = true;  //okuma başarılıysa tc okunan true oluyo

            }
            else
            {
                tckontrol = false;  //başarırsızsa false oluyo
                MessageBox.Show("Kullanıcı Bulunamadı");
            }
            conn.Close();   //bağlantıyı aç
            if(tckontrol==true)   //tckontrol doğruysa
            {
                Guncelle();  //güncelleme fonksiyonu çağrılıyo ve şifre değiştiriliyor
                MessageBox.Show("Şifre Değiştirildi");
                this.Close(); //kapat 
            }
            
        }
     
         public void Guncelle()
        {
  
            conn.Open();   //bağlantıyı aç
            string kayit = "update Kullanici set sifre=@sf where Tc=@tc";    //tc si @tc olanın şifresini @sifre yapıyor
            SqlCommand komut = new SqlCommand(kayit, conn);                 //kayıttaki sql cümleciğini veritabanımıza uygular      
            komut.Parameters.AddWithValue("@tc", textBox1.Text);           //tc değerine textbox1den alınan değeri ata demek
            komut.Parameters.AddWithValue("@sf", textBox4.Text);          //sf değerine textbox4den alınan değeri ata demek
            komut.ExecuteNonQuery();                                     //komutumuzu çalıştıralım
            conn.Close();                                               //bağlantıyı kapat

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();      //formun çerçevesini kaldırdığım için buton ekleyip kendi çıkışımı yaptım
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string metin = textBox1.Text;  //textbox1deki metini metin değişkenine atıyor
            if (metin.Length<11)           //metin 11 den küçükse
            {
                errorProvider1.SetError(textBox1, "Burası 11 haneden küçük olamaz"); //hata veriyor hata ikonu texbox1 11 olmayınca gitmiyor
            }
            else
            {
                errorProvider1.SetError(textBox1, ""); //hata vermiyor 
            }
        }

 
    }
}
