using System;

namespace XOX
{
    class Program
    {
        static char[] tahta = { '0', '1', '2', '3', '4', '5', '6', '7', '8' };
        static char oyuncu = 'X';

        static void Main(string[] args)
        {
            while (true)
            {
                OyunBaslat();
                Console.WriteLine("Yeni bir oyun başlatılıyor...");
                System.Threading.Thread.Sleep(2000); // 2 saniye beklet
                SifirlaTahta(); // Tahtayı sıfırla ve yeni oyun başlat
            }
        }

        static void OyunBaslat()
        {
            int oyunDurumu;
            do
            {
                Console.Clear();
                TahtaGoster();
                Console.WriteLine($"Oyuncu {oyuncu}'in sırası. Lütfen 0-8 arası bir pozisyon seçin:");

                int hamle;
                while (!int.TryParse(Console.ReadLine(), out hamle) || hamle < 0 || hamle > 8 || tahta[hamle] == 'X' || tahta[hamle] == 'O')
                {
                    Console.WriteLine("Geçersiz hamle! Lütfen tekrar deneyin.");
                }

                tahta[hamle] = oyuncu;
                oyunDurumu = KazanmaKontrol();

                if (oyunDurumu == 0)
                {
                    oyuncu = oyuncu == 'X' ? 'O' : 'X';
                }
            } while (oyunDurumu == 0);

            Console.Clear();
            TahtaGoster();

            if (oyunDurumu == 1)
            {
                Console.WriteLine($"Tebrikler! Oyuncu {oyuncu} kazandı!");
            }
            else
            {
                Console.WriteLine("Berabere! Hamle yapılacak yer kalmadı.");
            }
        }

        static void TahtaGoster()
        {
            Console.WriteLine(" {0} | {1} | {2} ", tahta[0], tahta[1], tahta[2]);
            Console.WriteLine("---|---|---");
            Console.WriteLine(" {0} | {1} | {2} ", tahta[3], tahta[4], tahta[5]);
            Console.WriteLine("---|---|---");
            Console.WriteLine(" {0} | {1} | {2} ", tahta[6], tahta[7], tahta[8]);
        }

        static int KazanmaKontrol()
        {
            int[,] kazanmaKosullari = new int[,]
            {
                { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, // Yatay
                { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, // Dikey
                { 0, 4, 8 }, { 2, 4, 6 }              // Çapraz
            };

            for (int i = 0; i < 8; i++)
            {
                int a = kazanmaKosullari[i, 0];
                int b = kazanmaKosullari[i, 1];
                int c = kazanmaKosullari[i, 2];

                if (tahta[a] == tahta[b] && tahta[b] == tahta[c])
                {
                    return 1; // Kazanan var
                }
            }

            foreach (char yer in tahta)
            {
                if (yer != 'X' && yer != 'O')
                {
                    return 0; // Oyun devam ediyor
                }
            }

            return -1; // Beraberlik
        }

        static void SifirlaTahta()
        {
            for (int i = 0; i < tahta.Length; i++)
            {
                tahta[i] = char.Parse(i.ToString()); // Tahtayı başlangıç haline döndür
            }
            oyuncu = 'X'; // İlk oyuncu olarak 'X' atandı
        }
    }
}
