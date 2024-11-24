using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        char[] harfler = { 'A', 'A', 'B', 'B', 'C', 'C', 'D', 'D', 'E', 'E', 'F', 'F', 'G', 'G', 'H', 'H' };
        char[] kartlar = new char[16];
        bool[] kartAcikMi = new bool[16];
        Random rnd = new Random();
        int hamleSayisi = 0;
        var sureOlcer = new Stopwatch();

        Karistir(harfler, rnd);

        for (int i = 0; i < harfler.Length; i++)
        {
            kartlar[i] = harfler[i];
            kartAcikMi[i] = false;
        }

        sureOlcer.Start();
        bool oyunBittiMi = false;

        while (!oyunBittiMi)
        {
            Console.Clear();
            KartlariGoster(kartlar, kartAcikMi);

            int birinciSecim = KartSec("Birinci kartı seçin (1-16): ", kartAcikMi);

            kartAcikMi[birinciSecim] = true;
            Console.Clear();
            KartlariGoster(kartlar, kartAcikMi);

            int ikinciSecim = KartSec("İkinci kartı seçin (1-16): ", kartAcikMi);

            kartAcikMi[ikinciSecim] = true;
            Console.Clear();
            KartlariGoster(kartlar, kartAcikMi);
            hamleSayisi++;

            if (kartlar[birinciSecim] == kartlar[ikinciSecim])
            {
                Console.WriteLine("Tebrikler! Eşleşme bulundu.");
            }
            else
            {
                Console.WriteLine("Kartlar eşleşmedi. Kartlar kapatılıyor...");
                kartAcikMi[birinciSecim] = false;
                kartAcikMi[ikinciSecim] = false;
            }

            oyunBittiMi = OyunBittiMi(kartAcikMi);

            Console.WriteLine("Devam etmek için bir tuşa basın...");
            Console.ReadKey();
        }

        sureOlcer.Stop(); 
        Console.Clear();
        KartlariGoster(kartlar, kartAcikMi);

        Console.WriteLine($"\nTebrikler! Oyunu {hamleSayisi} hamlede tamamladınız.");
        Console.WriteLine($"Geçen süre: {sureOlcer.Elapsed.TotalSeconds:F2} saniye.");
    }

    static void Karistir(char[] dizi, Random rnd)
    {
        for (int i = dizi.Length - 1; i > 0; i--)
        {
            int j = rnd.Next(0, i + 1);
            char temp = dizi[i];
            dizi[i] = dizi[j];
            dizi[j] = temp;
        }
    }


    static void KartlariGoster(char[] kartlar, bool[] kartAcikMi)
    {
        Console.WriteLine("Kartlar:");
        for (int i = 0; i < kartlar.Length; i++)
        {
            if (kartAcikMi[i])
            {
                Console.Write($"[{kartlar[i]}] ");
            }
            else
            {
                Console.Write($"[{i + 1}] ");
            }

            if ((i + 1) % 4 == 0)
                Console.WriteLine();
        }
    }

    static int KartSec(string mesaj, bool[] kartAcikMi)
    {
        int secim = -1;
        bool gecerliSecim = false;

        while (!gecerliSecim)
        {
            Console.Write(mesaj);
            string input = Console.ReadLine();

            if (int.TryParse(input, out secim) && secim >= 1 && secim <= 16 && !kartAcikMi[secim - 1])
            {
                gecerliSecim = true;
            }
            else
            {
                Console.WriteLine("Geçersiz seçim! 1-16 arası bir sayı girin ve kapalı bir kart seçin.");
            }
        }

        return secim - 1;
    }

    static bool OyunBittiMi(bool[] kartAcikMi)
    {
        foreach (bool acikMi in kartAcikMi)
        {
            if (!acikMi)
                return false;
        }
        return true;
    }
}