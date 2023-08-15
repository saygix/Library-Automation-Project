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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS03;Database=kütüphane;Trusted_Connection=True;"); //Bu veritabanına bağlanmak için gerekli olan bağlantı cümlemiz


        //Burası notifyIcon sayesinde form3 çıkışını form3 gizlemesini yapıyor

        //Göster
        private void gösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();  
        }
        //Gizle
        private void gizleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        //Çıkış yap
        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //Burası Kronometreye ait kodlar

        int sayi = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            sayi = 0;        //başlangıcı 0'a eşitle
            timer1.Start(); //Timer başlat
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sayi =sayi+1;                   //saniye saniye artma
            button3.Text = sayi.ToString(); // sayiyi int den stringe çevirme
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Geçen Süre: "+button3.Text+" Saniye");  //ListBox yazdırma
            button3.Text = "0";                                         // butonun textine 0 yazıp 0'dan başlamasını sağlama*/
            timer1.Stop();                                              //Timer durdurma
        }
         //listBox temizleme
        private void label2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();                                 //listBox1 temizleme
        }


          //Yönetici tarafından eklenen kitapların comboBoxa çekilmesi
        private void Form3_Load(object sender, EventArgs e)
        {
            label10.Text = Form1.Kullanici;  //bu form1 deki kullanıcı tablosundan çekilen kullanıcıya ait tcyi form3 de gösteriyor
            VeriÇek();                      //veri çek fonksiyonunu çağırdım
            KitapGoster();                 //KitapGöster fonksiyonunu çağırdım
            IadeGoster();                 //IadeGöster fonksiyonunu Çağırdım
            dataGridView1.GridColor = Color.LightBlue;  //dataGridView1 rengini değiştirdim
            dataGridView2.GridColor = Color.Yellow;     //dataGridView2 rengini değiştirdim
        }



        //Burada Form5 deki Yönetici tarafından eklenen kitapları comboboxa çektim
        public void VeriÇek()
        {
            SqlConnection conn = new SqlConnection();  //yeni connection tanımlamı
            conn.ConnectionString = "Data Source=localhost\\SQLEXPRESS03;Initial Catalog=kütüphane;Integrated Security=True;";   //tekrar veri tabanına bağlantı
            SqlCommand komut = new SqlCommand();              //yeni  command tanımı adı da komut
            komut.CommandText = "SELECT *FROM KitapEkleSil"; //KitapEkleSil tablosundaki verileri çek
            komut.Connection = conn;                        //veri bağlantısını çağırmak 
            komut.CommandType = CommandType.Text;          // CommandText özelliği saklı yordamın adına ayarlanmalıdır.Komut, Execute yöntemlerinden birini çağırdığınızda bu saklı yordamı yürütür.

            SqlDataReader dr;                             //SqlDataReader tanımlıyoruz adı dr
            conn.Open();                                 //bağlantıyı aç
            dr = komut.ExecuteReader();                 //komudu çalıştırıp dr'ye atıyoruz
            while (dr.Read())                          //read okumaya devam ederse okudukça döngüye giriyor
            {
                comboBox1.Items.Add(dr["kitapadi"]);  //combobox1 kitap adlarını çağır
            }
            conn.Close();                             //bağlantıyı kapat
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex>=0)  //comboboxın içinde tanımlanan kitap varsa
            {
                checkedListBox1.Items.Add(comboBox1.SelectedItem.ToString());  //checkedlistboxa aktar
            }
            else  //yoksa
            {
                MessageBox.Show("Böyle bir kitap yok"); //hata mesajını ver
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            checkedListBox1.Items.Remove(checkedListBox1.SelectedItem);  //checkedListBox1 içindeki seçili itemi sil
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            label1.ForeColor = SystemColors.MenuHighlight; //fare label1 üzerine geldiğinde rengini değiştir
            label1.Font = new Font(label1.Font.Name, label1.Font.SizeInPoints, FontStyle.Underline); //fare label1in üzerine geldiğinde altına çizgi koyar
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = SystemColors.ControlText; //Fare üzerinden gittiğinde rengine geri dön
            label1.Font = new Font(label1.Font.Name, label1.Font.SizeInPoints, FontStyle.Regular); //Fare üzerinden gittiğinde Çizgiyi kaldır

        }



        private void button6_Click(object sender, EventArgs e)
        {
            KitapAl();                          //KitapAl fonksiyonunu çağır 
            KitapGoster();                     //KitapGöster Fonsiyonunu çağır
            MessageBox.Show("Kitap Alındı");
            textBox2.Clear();                 //Texboxı temizle

        }
        public void KitapAl()
        {
            conn.Open();                                                                                              //bağlantıyı aç
            string kayit = "insert into UyeKitapAl (KullaniciAdi,KitapAdi,AlinanTarih) values (@ku,@ka,@at)";        //kayıt değikeni ile UyeKitapAl tablosundanki verileri eklemek için değerleri tanımlıyoruz
            SqlCommand komut = new SqlCommand(kayit,conn);                                                          //komut tanımlıyoruz. bu da kayıttaki cümleciğimizle connetionla balıyor
            komut.Parameters.AddWithValue("@ku", label10.Text);                                                    //ku değerine textbox9den alınan değeri ata demek
            komut.Parameters.AddWithValue("@ka", textBox2.Text);                                                  //ka değerine textbox9den alınan değeri ata demek
            komut.Parameters.AddWithValue("@at", this.dateTimePicker1.Text.ToString());                          //at değerine dateTimerPicker1'den alınan değeri ata demek
            komut.ExecuteNonQuery();                                                                            //komudu çalıştır
            conn.Close();                                                                                      //bağlantıyı kapat
        }


        //Kullanici adini Kullanıcı tablosundan çekerek herkesin kendi adına giriş yaptığında aldığı kitapları ve iadeleri gösteriyor
        //yani Her kullanici için yeni bir sayfa gibi girdiğinde görebilir
        public void KitapGoster()
        {
            conn.Open();                                                                                          //bağlantıyı aç
            string kayit = "SELECT KullaniciAdi,KitapAdi,AlinanTarih From UyeKitapAl where KullaniciAdi=@ku";    //kayıt değikeni ile UyeKitapAl tablosundanki verileri eklemek için değerleri tanımlıyoruz 
            SqlCommand komut = new SqlCommand(kayit, conn);                                                     //komut tanımlıyoruz. bu da kayıttaki cümleciğimizle connetionla balıyor
            komut.Parameters.AddWithValue("@ku", label10.Text);                                                //giren kullanıcını tcsini label10'a atıyor.
            SqlDataAdapter dr = new SqlDataAdapter(komut);                                                    //Ardından DataAdapter verilerimizi çektik.
            DataTable dt = new DataTable();                                                                  //çekilen verileri barındırmak için DataTable oluşturduk
            dr.Fill(dt);                                                                                    //Son adımda ise bu çekilen verileri Fill komutu ile oluşturulan DataTable a aktardım
            dataGridView1.DataSource = dt;                                                                 //aldığım kitaplarrı görmek için dataGridView1 verileri çağırdım
            conn.Close();                                                                                 //bağlantıyı kapat
        }
        private void checkedListBox1_Click(object sender, EventArgs e)
        {
            textBox2.Text = checkedListBox1.SelectedItem.ToString();  //checkedlistboxta seçileni texboxa yazdır 
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["KitapAdi"].Value.ToString(); //datagridviewden seçilen kitabı texbox1e yazdır
        }


        //kitabı iade etme aşaması burada datagridview1de bulunan alınan kitapları oradan silip datagridviewe ye aktardım
        private void button7_Click(object sender, EventArgs e)
        {
            conn.Open();                                                                                                  //bağlantıyı aç
            SqlCommand komut = new SqlCommand("delete from UyeKitapAl where KitapAdi=@ka and KullaniciAdi=@kur", conn); //UyeKitapAl tablosundaki kullanıcı ve kitap adi doğruysa veriyi çek
            komut.Parameters.AddWithValue("@ka", textBox1.Text);                                                       //ka değerine tekbox1'den alınan değeri ata
             komut.Parameters.AddWithValue("@Kur", label10.Text);                                                     //aynı şekilde kullanıcı adını label10'dan alınan değere ata
            komut.ExecuteNonQuery();                                                                                 //komudu çalıştır
            conn.Close();                                                                                           //bağlantıyı kapat
            KitapGoster();                                                                                         //kitap göster fonksiyonunu çağırdık

            // burada datagridview1'den sildiğimiz veriyi datagridview2'ye aktarıyoruz

            conn.Open();                                                                                                            //bağlantıyı aç
            SqlCommand komut2 = new SqlCommand("insert into Iade (KullaniciAdi,KitapAdi,IadeTarihi) values(@ku,@kad,@it)",conn);   //Iade tablosundanki verileri eklemek için değerleri tanımlıyoruz
            komut2.Parameters.AddWithValue("@kad", textBox1.Text);                                                                //kad değerine texbox1'den alınan değere ata
            komut2.Parameters.AddWithValue("@it", this.dateTimePicker2.Text.ToString());                                         //it değerine datetimePicker2den alınan değeri ata                        
            komut2.Parameters.AddWithValue("@ku", label10.Text);                                                                //kur değerine label10'dan alınan değeri ata
            komut2.ExecuteNonQuery();                                                                                          //komudu çalıştır
            conn.Close();                                                                                                     //bağlantıyı kapat
            IadeGoster();                                                                                                    //IadeGoster fonsiyonunu çağır
            MessageBox.Show("İade Edildi");

        }
        public void IadeGoster()
        {
            conn.Open();
            string kayit = "SELECT KullaniciAdi,KitapAdi,IadeTarihi From Iade where KullaniciAdi=@ku"; //Iade tablosunda girilen diğer veriler doğru olduğu yerde veri çek
            SqlCommand komut = new SqlCommand(kayit, conn);                                           //kayıttaki sql cümleciğini veritabanımıza uygular
            komut.Parameters.AddWithValue("@ku", label10.Text);                                      //ku değerine label10'dan alınan değeri ata
            SqlDataAdapter dr = new SqlDataAdapter(komut);                                          //Ardından DataAdapter verilerimizi çektik.
            DataTable dt = new DataTable();                                                        //çekilen verileri barındırmak için DataTable oluşturduk
            dr.Fill(dt);                                                                          //Son adımda ise bu çekilen verileri Fill komutu ile oluşturulan DataTable a aktardım
            dataGridView2.DataSource = dt;                                                       //dataGridView2 ye verileri çağırdım
            conn.Close();                                                                       //bağlantıyı kapat
        }

        private void checkedListBox1_MouseHover(object sender, EventArgs e)
        {
            checkedListBox1.BackColor = SystemColors.GradientActiveCaption;  //fare üzerindeyken checkedlistboxın rengini değiştir
            checkedListBox1.Font = new Font(checkedListBox1.Font.Name, checkedListBox1.Font.SizeInPoints, FontStyle.Underline); //içindeki itemlere çizgi ekle
        }

        private void checkedListBox1_MouseLeave(object sender, EventArgs e)
        {
            checkedListBox1.BackColor = SystemColors.Window; //fare üzerinde ayrıldığında eski rengine dön
        }

   
        //toolstrip ile çıkış yaptım
        private void oturumuKapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Çıkış Yapmak İstiyormusunuz?", "Çıkış Yap", MessageBoxButtons.YesNo, MessageBoxIcon.Information); //mesajboxın evet hayır özelliğini kullandım
            if(dialog == DialogResult.Yes) //evetse
            {
                this.Close();  //form3den çık
            }
        }  

        //visibleleri kapalı olan resimlerin visiblelerini açtım.
        private void label29_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;  
            pictureBox2.Visible = true;
            pictureBox3.Visible = true;
            pictureBox4.Visible = true;
            pictureBox5.Visible = true;
            pictureBox6.Visible = true;

        }
    }
}
