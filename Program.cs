using System;
using System.Collections.Generic;

namespace KarStadiumPrediction
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] machine_id = args[0].ToCharArray(); //1つ目のコマンドライン引数を文字の配列に変換

            //1つ目のコマンドライン引数が10文字で1～8,A～Gであるかをチェック
            if (args[0].Length == 10)
            {
                for (int i=0; i<10; i++)
                {
                    if (!(machine_id[i] == '1' || machine_id[i] == '2' || machine_id[i] == '3' || machine_id[i] == '4' || machine_id[i] == '5' || machine_id[i] == '6' || machine_id[i] == '7' || machine_id[i] == '8' || machine_id[i] == 'A' || machine_id[i] == 'B' || machine_id[i] == 'C' || machine_id[i] == 'D' || machine_id[i] == 'E' || machine_id[i] == 'F' || machine_id[i] == 'G'))
                    {
                        Console.WriteLine("Please specify the machine ID using 10 characters.");
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("Please specify the machine ID using 10 characters.");
                return;
            }

            uint[] stadium = { 5, 5, 5, 5, 10, 20, 10, 10, 10, 4, 4, 4, 4, 4, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5 }; //各スタジアムの出現比率

            //2つ目以降のコマンドライン引数からスタジアムの候補を絞る
            for (int i=1; i<args.Length; i++)
            {
                if (args[i] == "-l" || args[i] == "--lock")
                {
                    stadium[3] = 0; stadium[8] = 0; stadium[11] = 0; stadium[12] = 0; stadium[13] = 0; stadium[22] = 0;
                }
                if (args[i] == "dr" || args[i] == "DR")
                {
                    for (int j=4; j<24; j++) stadium[j] = 0;
                }
                if (args[i] == "km" || args[i] == "KM")
                {
                    for (int j=0; j<7; j++) stadium[j] = 0;
                    for (int j=9; j<24; j++) stadium[j] = 0;
                }
                if (args[i] == "dd" || args[i] == "DD")
                {
                    for (int j = 0; j < 9; j++) stadium[j] = 0;
                    for (int j = 14; j < 24; j++) stadium[j] = 0;
                }
                if (args[i] == "sr" || args[i] == "SR")
                {
                    for (int j = 0; j < 14; j++) stadium[j] = 0;
                    stadium[23] = 0;
                }
                if (int.TryParse(args[i], out int result))
                {
                    if ((result>=0) && (result<=24)) stadium[result-1] = 0;
                }
            }

            long seed = DetectSeed(machine_id);
            PrintStadium(seed, stadium);
        }

        static long DetectSeed(char[] machine_id)
        {
            long r0 = 0;
            long r1 = 0;
            int machine_number = 0;
            for (long i=0; i<4294967306; i++)
            {
                r1 = (r0 * 214013 + 2531011) % 4294967296;
                switch (r1 / 286331153)
                {
                    case 0:
                        if (machine_id[machine_number] == '1') machine_number++;
                        else machine_number = 0;
                        break;
                    case 1:
                        if (machine_id[machine_number] == '2') machine_number++;
                        else machine_number = 0;
                        break;
                    case 2:
                        if (machine_id[machine_number] == '3') machine_number++;
                        else machine_number = 0;
                        break;
                    case 3:
                        if (machine_id[machine_number] == '4') machine_number++;
                        else machine_number = 0;
                        break;
                    case 4:
                        if (machine_id[machine_number] == '5') machine_number++;
                        else machine_number = 0;
                        break;
                    case 5:
                        if (machine_id[machine_number] == '6') machine_number++;
                        else machine_number = 0;
                        break;
                    case 6:
                        if (machine_id[machine_number] == '7') machine_number++;
                        else machine_number = 0;
                        break;
                    case 7:
                        if (machine_id[machine_number] == '8') machine_number++;
                        else machine_number = 0;
                        break;
                    case 8:
                        if (machine_id[machine_number] == 'A') machine_number++;
                        else machine_number = 0;
                        break;
                    case 9:
                        if (machine_id[machine_number] == 'B') machine_number++;
                        else machine_number = 0;
                        break;
                    case 10:
                        if (machine_id[machine_number] == 'C') machine_number++;
                        else machine_number = 0;
                        break;
                    case 11:
                        if (machine_id[machine_number] == 'D') machine_number++;
                        else machine_number = 0;
                        break;
                    case 12:
                        if (machine_id[machine_number] == 'E') machine_number++;
                        else machine_number = 0;
                        break;
                    case 13:
                        if (machine_id[machine_number] == 'F') machine_number++;
                        else machine_number = 0;
                        break;
                    default:
                        if (machine_id[machine_number] == 'G') machine_number++;
                        else machine_number = 0;
                        break;
                }
                r0 = r1;
                if (machine_number == 10) break;
            }
            return r0;
        }

        static void PrintStadium(long r0, uint[] stadium)
        {
            long r1 = 0;
            uint rate = 0;
            for (int i=0; i<24; i++) rate += stadium[i]; //スタジアムの出現比率の合計
            uint threshold = 4294967295 / rate; //スタジアムが変化する可能性のある乱数の境界
            int stadium_threshold = 0;
            uint stadium_number = 0; //出現するスタジアムのIDを格納する変数
            uint stadium_last = 0; //出現する可能性のある最後のスタジアムのIDを格納する変数

            for (int i=0; i<31; i++)
            {
                r1 = (r0 * 214013 + 2531011) % 4294967296;
                stadium_threshold = (int)(r1 / threshold);
                for (int j=0; j<24; j++)
                {
                    if (stadium_threshold - stadium[j] >= 0)
                    {
                        stadium_threshold -= (int)stadium[j];
                        //乱数値の剰余を処理
                        if (stadium[j] > 0) stadium_last = (uint)j;
                        if (j == 23) stadium_number = stadium_last;
                    }
                    else
                    {
                        stadium_number = (uint)j;
                        break;
                    }
                }
                Console.WriteLine("{0}\t{1}", i, stadium_number + 1);
                r0 = r1;
            }
        }
    }
}
