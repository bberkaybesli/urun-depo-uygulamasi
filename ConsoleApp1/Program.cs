// Gerekli kütüphaneyi yüklüyoruz list koleksiyon vb için
using System;
using System.Collections.Generic;

public class Urun // Ürün bilgilerini tutan sınıfı oluşturuyoruz
{
    public int ID; // Ürün kimliği
    public string Ad; // Ürün adı
    public int Miktar; // Ürün miktarı
    public Urun Next; // Sonraki ürüne bağlantı olacak nesnemiz

    public Urun(int id, string ad, int miktar) // Tekrar ürün nesnesi oluşturuyoruz
    {
        ID = id; Ad = ad; Miktar = miktar; // Ürün özelliklerini atıyoruz.
    }
}

public class LinkedList // Bağlı liste yani linkedlist sınıfımızı oluşturuyoruz
{
    private Urun bas; // Listenin başlangıcı

    public void Ekle(int id, string ad, int miktar, bool basaEkle) // Ürün ekleme için metotumuzu oluşturuyoruz
    {
        Urun yeniUrun = new Urun(id, ad, miktar); // Yeni ürün oluştur
        if (basaEkle) // Eğer başa ekleniyorsa
        {
            yeniUrun.Next = bas;  // Buradaki amacımız yeni bir ürün eklediğimizde zaten listede bir ürün varsa onu nexte 
            bas = yeniUrun;       // atıyoruz ve yeni ürünümüz başa eklenmiş oluyor
        }
        else // Sona ekleniyorsa
        {
            if (bas == null) bas = yeniUrun; // Eğer liste boşsa başa ekle
            else // Liste dolu ise
            {
                Urun temp = bas; // Geçici değişken ile baştan başla
                while (temp.Next != null) temp = temp.Next; // Son ürüne kadar git
                temp.Next = yeniUrun; // Yeni ürünü sona ekle
            }
        }
    }

    public void Sil(bool bastanSil) // Ürün silme metotumuz
    {
        if (bas == null) return; // Eğer liste boşsa hiçbir şey yapma
        if (bastanSil) bas = bas.Next; // Baştan sil
        else // Sondan sil
        {
            Urun temp = bas;
            if (temp.Next == null) bas = null; // Tek ürün varsa başı null yap çünkü silinebilecek tek ürün var listeyi sıfırlarız
            else
            {
                while (temp.Next.Next != null) temp = temp.Next; // Sona kadar git
                temp.Next = null; // Son ürünü sil
            }
        }
    }

    public Urun Ara(int id) // Ürün arama metotumuz
    {
        for (Urun temp = bas; temp != null; temp = temp.Next) // Tüm ürünleri kontrol et
            if (temp.ID == id) return temp; // Ürün bulunduysa döndür
        return null; // Ürün bulunamadıysa null dönecektir yani boş
    }

    public void Listele() // Ürünleri listeleme metotumuz
    {
        for (Urun temp = bas; temp != null; temp = temp.Next)
            Console.WriteLine($"ID: {temp.ID}, Ad: {temp.Ad}, Miktar: {temp.Miktar}"); // Ürün bilgilerini yazdır
    }

    public void Sırala() // Miktara göre sıralama metotumuz
    {
        List<Urun> urunListesi = new List<Urun>(); // Ürünleri geçici bir listeye alıyoruz
        for (Urun temp = bas; temp != null; temp = temp.Next) urunListesi.Add(temp); // Listeye ekle
        urunListesi.Sort((x, y) => x.Miktar.CompareTo(y.Miktar)); // Miktara göre sırala
        ListeleSirali(urunListesi); // Sıralı listeyi göster
    }

    private void ListeleSirali(List<Urun> urunListesi) // Sıralı listeyi yazdırma
    {
        foreach (var u in urunListesi)
            Console.WriteLine($"ID: {u.ID}, Ad: {u.Ad}, Miktar: {u.Miktar}"); // Ürün bilgilerini yazdır
    }
}

public class Program
{
    public static void Main()
    {
        LinkedList linkedList = new LinkedList(); // Bağlı liste nesnesini oluşturuyoruz
        Console.Write("Kuyruk (1) mi Stack (2) mi? (1/2): ");
        bool kuyruk = Convert.ToInt32(Console.ReadLine()) == 1; // Kuyruk veya Stack seçimi

        while (true) // Kullanıcı çıkış yapmadığı sürece konsolumuzun açık kalması için sonsuz döngü açıyoruz
        {
            Console.Write("Ekle(1), Sil(2), Ara(3), Listele(4), Sırala(5), Çıkış(6): ");
            int islem = Convert.ToInt32(Console.ReadLine());

            switch (islem) // Kullanıcıdan gelen işlem
            {
                case 1: // Ürün ekleme
                    Console.Write("ID: "); int id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Ad: "); string ad = Console.ReadLine();
                    Console.Write("Miktar: "); int miktar = Convert.ToInt32(Console.ReadLine());
                    linkedList.Ekle(id, ad, miktar, !kuyruk); // Kuyruk için sona Stack için başa ekler
                    break;

                case 2: // Ürün silme
                    linkedList.Sil(kuyruk); // Kuyruk baştan Stack sondan siler
                    break;

                case 3: // Ürün arama
                    Console.Write("Aranacak ID: ");
                    Urun urun = linkedList.Ara(Convert.ToInt32(Console.ReadLine())); // Arama işlemi
                    Console.WriteLine(urun != null ? $"Bulundu: ID: {urun.ID}, Ad: {urun.Ad}, Miktar: {urun.Miktar}" : "Ürün bulunamadı."); // Sonucu yazdır
                    break;

                case 4: // Listeleme
                    linkedList.Listele(); // Tüm ürünleri yazdır
                    break;

                case 5: // Sıralama
                    linkedList.Sırala(); // Miktara göre sıralı listeyi göster
                    break;

                case 6: // Çıkış
                    return; // Programdan çık
            }
        }
    }
}

