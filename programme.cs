using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;


namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------------\n" +
                              "          PROGRAM BAŞLATILDI          \n" +
                              "--------------------------------------");
            Console.Write(" Değerleri girmek için 'enter' tuşuna basınız.");
            Console.ReadLine();
            Console.WriteLine("___________________________________________");

            // Kullanıcıdan değerleri alıyoruz
            Console.Write(" K değerini giriniz: ");
            int k = Convert.ToInt32(Console.ReadLine());
            Console.Write(" Varyans değerini giriniz: ");
            double varyans = Convert.ToDouble(Console.ReadLine());
            Console.Write(" Çarpıklık değerini giriniz: ");
            double carpiklik = Convert.ToDouble(Console.ReadLine());
            Console.Write(" Basıklık değerini giriniz: ");
            double basiklik = Convert.ToDouble(Console.ReadLine());
            Console.Write(" Entropi değerini giriniz: ");
            double entropi = Convert.ToDouble(Console.ReadLine());

            // kNN metodunda bu değerleri kullanıyoruz
            double[,] matris4 = kNN(k,varyans, carpiklik, basiklik, entropi,veriOku());
            
            Console.ReadLine();
            Console.WriteLine(" No      Varyans      Çarpıklık    Basıklık     Entropi     Tür     Uzaklık");
            Console.WriteLine(" __      _______      _________    ________     ________    ___     _______");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            
            // kNN metodundan dönen matrisi döngü yardımı ile bastırıyoruz
            for (int d = 0; d < matris4.GetLength(0); d++)
            {

                string txt0 = String.Format("{0:0.00000}", Math.Round(matris4[d, 0], 5).ToString());
                if (matris4[d, 0] > 0) { txt0 = "+" + txt0; }
                if (txt0.Length == 7) { txt0 = txt0 + "0"; }
                else if (txt0.Length == 6) { txt0 = txt0 + "00"; }

                string txt1 = String.Format("{0:0.00000}", Math.Round(matris4[d, 1], 5).ToString());
                if (matris4[d, 1] > 0) { txt1 = "+" + txt1; }
                if (txt1.Length == 7) { txt1 = txt1 + "0"; }
                else if (txt1.Length == 6) { txt1 = txt1 + "00"; }

                string txt2 = String.Format("{0:0.00000}", Math.Round(matris4[d, 2], 5).ToString());
                if (matris4[d, 2] > 0) { txt2 = "+" + txt2; }
                if (txt2.Length == 7) { txt2 = txt2 + "0"; }
                else if (txt2.Length == 6) { txt2 = txt2 + "00"; }

                string txt3 = String.Format("{0:0.00000}", Math.Round(matris4[d, 3], 5).ToString());
                if (matris4[d, 3] > 0) { txt3 = "+" + txt3; }
                if (txt3.Length == 7) { txt3 = txt3 + "0"; }
                else if (txt3.Length == 6) { txt3 = txt3 + "00"; }

                string txt4 = String.Format("{0:0.00000}", Math.Round(matris4[d, 4], 5).ToString());

                double dist = Math.Sqrt(Math.Pow((varyans - matris4[d, 0]), 2) + Math.Pow((carpiklik - matris4[d, 1]), 2) + Math.Pow((basiklik - matris4[d, 2]), 2) +
                                  Math.Pow((entropi - matris4[d, 3]), 2));

                string txt5 = String.Format("{0:0.00000}", Math.Round(dist, 5).ToString());
                if (dist > 0) { txt5 = "+" + txt5; }
                if (txt5.Length == 7) { txt5 = txt5 + "0"; }
                else if (txt5.Length == 6) { txt5 = txt5 + "00"; }

                Console.WriteLine(" " + (d + 1) + "       " + txt0 + "     " + txt1 + "     " + txt2 + "     " + txt3 + "     " + txt4 + "      " + txt5);
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------");


            }
            Console.ReadLine();
            Console.WriteLine("___________________________________________");
            Console.WriteLine("___________________________________________");
            Console.WriteLine("  İstatistikleri yazdırmak için: 'enter'");
            Console.WriteLine("___________________________________________");
            Console.WriteLine("___________________________________________");
            Console.ReadLine();

            // Tahmin edilen türü bastırıyoruz
            int oy = 0;
            for (int z = 0; z < k; z++)
            {
                if (matris4[z, 4] == 1) { oy++; }
                else { oy--; }
            }
            if (oy > 0) { Console.WriteLine(" Tahmin edilen: 1"); }
            else if (oy < 0) { Console.WriteLine(" Tahmin edilen: 0"); }
            else
            {
                Console.WriteLine(" Tahmin edilen: " + matris4[0, 4]);
            }

            Console.WriteLine("___________________________________________");
            Console.WriteLine("___________________________________________");
            Console.ReadLine();

            tahminMetodu(veriOku(), k);
            Console.WriteLine("___________________________________________");
            Console.WriteLine("___________________________________________");
            Console.WriteLine("\n\nTüm veri setini yazdırmak için: 'enter'\n\n");
            Console.ReadLine();

            Listele(veriOku());
            
            Console.WriteLine(" \n\nÇıkış için 'enter' tuşuna basınız.\n");
            Console.ReadLine();
        }



        public static double[,] veriOku()
        {
            // Veri listesini lines'a atıyoruz
            string[] lines = File.ReadAllLines(@"C:\Users\C.Cesur\Desktop\data_banknote_authentication.txt");
            
            // 1372 banknotun tüm özelliklerini daha kolay erişebilmek için tekrar lines'ı matrise atıyoruz
            double[,] matris0 = new double[lines.Length, 5];
            
            // İçeri atarken virgüllere göre parçalıyoruz ve ardından '.' yerine ',' atıyoruz
            for (int a = 0; a < lines.Length; a++)
            {
                string[] line = lines[a].Split(',');

                line[0] = line[0].Replace(".", ",");
                matris0[a, 0] = Convert.ToDouble(line[0]);
                line[1] = line[1].Replace(".", ",");
                matris0[a, 1] = Convert.ToDouble(line[1]);
                line[2] = line[2].Replace(".", ",");
                matris0[a, 2] = Convert.ToDouble(line[2]);
                line[3] = line[3].Replace(".", ",");
                matris0[a, 3] = Convert.ToDouble(line[3]);
                line[4] = line[4].Replace(".", ",");
                matris0[a, 4] = Convert.ToDouble(line[4]);

            }
           
            return matris0;

        }



        public static double[,] kNN(int k, double varyans, double carpiklik, double basiklik, double entropi, double[,] matris0)
        {

            // Döndüreceğimiz matrisi oluşturduk
            double[,] matris1 = new double[k, 5];

            // while döngüsünü kullandığımızdan dolayı matrisin kaçıncı satırında olduğumuzu öğrenmek amacıyla sayacımızı oluşturduk
            int sayac = 0;
            while (k != 0)
            {
                // minimum distance değerini ilk banknot alıyoruz ki karşılaştırma yapabilelim
                double minDistance = Math.Sqrt(Math.Pow((varyans - matris0[0, 0]), 2) + Math.Pow((carpiklik - matris0[0, 1]), 2) + Math.Pow((basiklik - matris0[0, 2]), 2) +
                                  Math.Pow((entropi - matris0[0, 3]), 2));

                // min distance'ın indexini tutuyor
                int minI = 0;

                // döngüde min distance değerini bulmak için banknotları karşılaştırıyoruz
                for (int i = 1; i < matris0.GetLength(0); i++)
                {
                    double distance = Math.Sqrt(Math.Pow((varyans - matris0[i, 0]), 2) + Math.Pow((carpiklik - matris0[i, 1]), 2) + Math.Pow((basiklik - matris0[i, 2]), 2) +
                                        Math.Pow((entropi - matris0[i, 3]), 2));

                    if (minDistance > distance) { minDistance = distance; minI = i; }

                }

                // Bulduğumuz min distance banknotunu kNN için oluşturduğumuz matrise atıyoruz
                matris1[sayac, 0] = matris0[minI, 0];
                matris1[sayac, 1] = matris0[minI, 1];
                matris1[sayac, 2] = matris0[minI, 2];
                matris1[sayac, 3] = matris0[minI, 3];
                matris1[sayac, 4] = matris0[minI, 4];
                
                k--;
                sayac++;

                // the most min distance değeri ile tekrar karşılaşmamak için absürt değerler atıyoruz
                matris0[minI, 0] = 9999999;
                matris0[minI, 1] = 9999999;
                matris0[minI, 2] = 9999999;
                matris0[minI, 3] = 9999999;
                matris0[minI, 4] = 9999999;

                

            }

            return matris1;
            
        }

        
        
        public static void tahminMetodu(double[,] matris0, int k)
        {
            
            // banknotların sayısını tutuyor
            int uzunluk = matris0.GetLength(0);

            // gerekli matrisler oluşturuldu(veri seti bilindiği için bu şekilde yaptık)
            double[,] matris2 = new double[200,5];
            double[,] matris3 = new double[1172, 5];
            
            // işlemleri sondan başa yaptığımız için atama işlemini yaparken kolaylık olması için bir diğer sayacımız
            int sayac2 = 199;

            // türü 1 olan banknotların sayısını buluyoruz
            int birsayac = 0;
            for (int g = 0; g < matris0.GetLength(0); g++)
            {
                if (matris0[g, 4] == 1) { birsayac++; }
            }
            

            // türü 1 olan banknotları sondan başa doğru olacak şekilde 200 satırlık matrisimize atıyoruz
            for (int e = uzunluk-1; e>=0; e--)
            {

                matris2[sayac2, 0] = matris0[e, 0];
                matris2[sayac2, 1] = matris0[e, 1];
                matris2[sayac2, 2] = matris0[e, 2];
                matris2[sayac2, 3] = matris0[e, 3];
                matris2[sayac2, 4] = matris0[e, 4];

                sayac2--;
                if (sayac2==99) { break; }
            }

            // türü 0 olan banknotların sayısını buluyoruz
            int sifirsayac = 0;
            for (int g = 0; g<matris0.GetLength(0);g++)
            {
                if (matris0[g,4]==0) { sifirsayac++; }
            }

            // türü 0 olan banknotları sondan başa doğru olacak şekilde 200 satırlık matrisimize atıyoruz
            for (int l = sifirsayac-1; l >= 0; l--)
            {
                matris2[sayac2, 0] = matris0[l, 0];
                matris2[sayac2, 1] = matris0[l, 1];
                matris2[sayac2, 2] = matris0[l, 2];
                matris2[sayac2, 3] = matris0[l, 3];
                matris2[sayac2, 4] = matris0[l, 4];

                sayac2--;
                if (sayac2 == -1) { break; }
            }

            // geriye kalan türü 0 olan banknotları 1172 satırlık matrisimize atıyoruz
            for (int n = 0; n<sifirsayac-100; n++)
            {
                matris3[n, 0] = matris0[n, 0];
                matris3[n, 1] = matris0[n, 1];
                matris3[n, 2] = matris0[n, 2];
                matris3[n, 3] = matris0[n, 3];
                matris3[n, 4] = matris0[n, 4];
            }

            // geriye kalan türü 1 olan banknotları 1172 satırlık matrisimize atıyoruz
            for (int x = 0; x<birsayac-100; x++)
            {
                matris3[sifirsayac - 100 + x, 0] = matris0[sifirsayac + x, 0];
                matris3[sifirsayac - 100 + x, 1] = matris0[sifirsayac + x, 1];
                matris3[sifirsayac - 100 + x, 2] = matris0[sifirsayac + x, 2];
                matris3[sifirsayac - 100 + x, 3] = matris0[sifirsayac + x, 3];
                matris3[sifirsayac - 100 + x, 4] = matris0[sifirsayac + x, 4];
            }

            // doğru tahmin sayısını tutacak değişkenimizi oluşturduk
            int dogrutahminler = 0;

            // test için ayrılan banknotları döndüren kod satırları ve sonuca göre dogrutahminler değişkeni artıyor
            for (int y = 0; y<200; y++)
            {
                double[,] matris5 = kNN(k, matris2[y, 0], matris2[y, 1], matris2[y, 2], matris2[y, 3], matris3);
                int oy = 0;
                for (int w = 0; w < k; w++)
                {
                    if (matris5[w, 4] == 1) { oy++; }
                    else { oy--; }
                }
                

                double tahmindegeri;
                if (oy > 0) { tahmindegeri=1; }
                else if (oy < 0) {tahmindegeri=0; }
                else
                {
                    tahmindegeri = matris5[0, 4];
                }
                if(tahmindegeri == matris2[y, 4]) { dogrutahminler++; }

            }
            // çıktımız
            Console.WriteLine(" Başarı oranı: %" + (dogrutahminler / 2));
            

        }


        // Banknotlarımızın saklandığı metodu( veriOku() ) parametre olarak alan metodumuz banknotları yazdırıyor
        public static void Listele(double[,] matris0)
        {
            Console.WriteLine(" No      Varyans      Çarpıklık    Basıklık     Entropi     Tür");
            Console.WriteLine(" __      _______      _________    ________     ________    ___");
            Console.WriteLine("----------------------------------------------------------------");
            
            
            for (int d = 0; d < matris0.GetLength(0); d++)
            {

                string txt0 = String.Format("{0:0.00000}", Math.Round(matris0[d, 0], 5).ToString());
                if (matris0[d, 0] > 0) { txt0 = "+" + txt0; }
                if (txt0.Length == 7) { txt0 = txt0 + "0"; }
                else if (txt0.Length == 6) { txt0 = txt0 + "00"; }
                else if (txt0.Length == 5) { txt0 = txt0 + "000"; }
                else if (txt0.Length == 4) { txt0 = txt0 + "0000"; }
                else if (txt0.Length == 3) { txt0 = txt0 + "00000"; }

                string txt1 = String.Format("{0:0.00000}", Math.Round(matris0[d, 1], 5).ToString());
                if (matris0[d, 1] > 0) { txt1 = "+" + txt1; }
                if (txt1.Length == 7) { txt1 = txt1 + "0"; }
                else if (txt1.Length == 6) { txt1 = txt1 + "00"; }
                else if (txt1.Length == 5) { txt1 = txt1 + "000"; }
                else if (txt1.Length == 4) { txt1 = txt1 + "0000"; }
                else if (txt1.Length == 3) { txt1 = txt1 + "00000"; }

                string txt2 = String.Format("{0:0.00000}", Math.Round(matris0[d, 2], 5).ToString());
                if (matris0[d, 2] > 0) { txt2 = "+" + txt2; }
                if (txt2.Length == 7) { txt2 = txt2 + "0"; }
                else if (txt2.Length == 6) { txt2 = txt2 + "00"; }
                else if (txt2.Length == 5) { txt2 = txt2 + "000"; }
                else if (txt2.Length == 4) { txt2 = txt2 + "0000"; }
                else if (txt2.Length == 3) { txt2 = txt2 + "00000"; }

                string txt3 = String.Format("{0:0.00000}", Math.Round(matris0[d, 3], 5).ToString());
                if (matris0[d, 3] > 0) { txt3 = "+" + txt3; }
                if (txt3.Length == 7) { txt3 = txt3 + "0"; }
                else if (txt3.Length == 6) { txt3 = txt3 + "00"; }
                else if (txt3.Length == 5) { txt3 = txt3 + "000"; }
                else if (txt3.Length == 4) { txt3 = txt3 + "0000"; }
                else if (txt3.Length == 3) { txt3 = txt3 + "00000"; }

                string txt4 = String.Format("{0:0.00000}", Math.Round(matris0[d, 4], 5).ToString());

                Console.WriteLine(" " + (d + 1) + "       " + txt0 + "     " + txt1 + "     " + txt2 + "     " + txt3 + "     " + txt4);
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            }
        }

    }

}
