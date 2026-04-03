using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace DataLayer
{
    public interface IAddressRepository
    {
        IList<Country> GetAllCountries();
        IList<State> GetStatesByCountryId(int countryid);
    }


    public class board12
    {
        public int boardid { get; set; }
        public string boardname { get; set; }
    }
    public class testincreent
    {
        static int a = 0;
        public static int add()
        {
            return a++;
        }
    }
    public class midcollege
    {
        public int collegeid { get; set; }
        public string collegename { get; set; }
        public string mid { get; set; }
        public string mkey { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Salt { get; set; }
        public string SuMid { get; set; }

    }

    public class CommonMethod
    {

        //public static string IPAddress = "192.185.11.49";
        //public static string varFrom_mail_Address = "nitingarg2016@brsoftech.org";
        //public static string varFrom_mail_password = "ne1uN68#";
        public static string IPAddress = "";
        public static string varFrom_mail_Address = "";
        public static string varFrom_mail_password = "";
        public static string applicationIDAndroid = "AIzaSyCMa1r7MI9dmiJPpWq7pDv0pAg4fzQi5f0";// this is e911 md "AIzaSyBvQ54XWc4FEZlzqBPMh7a6QQcrpRb5Y_Q";
        DataSet ds;
        SqlDataAdapter da;
        public static SqlConnection connect()
        {
            //Reading the connection string from web.config    
            string Name = ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString;
            //Passing the string in sqlconnection.    
            SqlConnection con = new SqlConnection(Name);
            //Check wheather the connection is close or not if open close it else open it    
            if (con.State == ConnectionState.Open)
            {
                con.Close();

            }
            else
            {

                con.Open();
            }
            return con;

        }
        public static string RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max).ToString();
        }

        public static List<board12> Boradtype()
        {
            List<board12> list = new List<board12>
            {
                new board12 { boardid =2,  boardname = "Bihar Board (Compulsory paper 100 marks(2021 year))" },
                new board12 { boardid = 1, boardname = "Bihar Board (If your Subject like(Optional,LL,RBH,MB Subjects and RBH,MB subject is 50 Marks))"},
                new board12 { boardid =2,  boardname = "Bihar Board/Other State Board/CBSC/ICSC/Madarsa Board etc" },
              //  new board12 { boardid =2,  boardname = "Bihar/Other Board (Fixed 5 Subject)" },
                new board12 { boardid =3,  boardname = "Diploma/Equipment " }
              //  ,
                //new board12 { boardid =4,  boardname = "UG Stream" },
                //new board12 { boardid =5,  boardname = "Other Stream" }
                //new board12 { boardid =6,  boardname = "Diploma Holder" },
            };
            return list;
        }
        public static List<board12> BoradtypePrevious()
        {
            List<board12> list = new List<board12>
            {
                new board12 { boardid = 1, boardname = "Bihar Board (BSEB  Board)"},
                new board12 { boardid = 2,  boardname = "Other Board(Like CBSE,ICSE,other State board or etc)" },
            };
            return list;
        }

        public enum SubSubjectType
        {
            CoreSubject = 1,
            Electivesubject = 2
        }
        public static List<SubjectType> GetSubjectType()
        {
            List<SubjectType> list = new List<SubjectType>
            {
                    new SubjectType { ID = 1, Name = "Core Subject"},
                    new SubjectType { ID = 2, Name = "Elective subject" }
            };
            return list;
        }
        public static List<midcollege> MIDcollegewise()
        {
            List<midcollege> list = new List<midcollege>
            {
                // Old Keys
                //    new midcollege { collegeid = 1, collegename="R.D. & D.J. COLLEGE, MUNGER",mid = "1000758" ,mkey="JP/mQn7neouacH9Fygn2fw=="},
                //    new midcollege { collegeid = 2, collegename="J.R.S. COLLEGE, JAMALPUR",mid = "1000745" ,mkey="0jiaSg7dEtr9gIscyJJLFA=="},
                //    new midcollege { collegeid = 3, collegename="KOSHI COLLEGE, KHAGARIA",mid = "1000749" ,mkey="PDuQnU84phxOaMXXGXAwdg=="},
                //    new midcollege { collegeid = 4, collegename="K. K. M. COLLEGE, JAMUI",mid = "1000747" ,mkey="dbGTB1QA0dw4Avj0/zn/bQ=="},
                //    new midcollege { collegeid = 5, collegename="S. K. R. COLLEGE, BARBIGHA",mid = "1000742" ,mkey="NLTUQa6e6qEHpOQx4EYY8A=="},
                //    new midcollege { collegeid = 6, collegename="R. S. COLLEGE, TARAPUR, MUNGER",mid = "1000736" ,mkey="t2uajDj3LHyWVCLPcsOVVg=="},
                //    new midcollege { collegeid = 7, collegename="B R M COLLEGE",mid = "1000735" ,mkey="72IaBQ13d1OFwZEH20vTkw=="},
                //    new midcollege { collegeid = 10, collegename="B N M College",mid = "1000723" ,mkey="MEJxndOTyFfrx38F/hxs0w=="},
                //    new midcollege { collegeid = 11, collegename="R. D. COLLEGE, SHEIKPURA",mid = "1000760" ,mkey="Wobyu5/rpNP1uBZQ/aWV5A=="},
                //    new midcollege { collegeid = 13, collegename="J. M. S. COLLEGE, MUNGER",mid = "1000752" ,mkey="l8Mv0f4EKMF9VMjDJ1QILA=="},
                //    new midcollege { collegeid = 14, collegename="H. S. COLLEGE",mid = "1000761" ,mkey="8lXVt5DxFGSNHRXEX/eKTw=="},
                //    new midcollege { collegeid = 15, collegename="K. S. S. COLLEGE, LAKHISARAI",mid = "1000733" ,mkey="BpIWpi8lfS93pceSzYXxqA=="},
                //    new midcollege { collegeid = 16, collegename="K. D. S. COLLEGE, GOGRI",mid = "1000746" ,mkey="lHkE2/8wQA3CfZ/gBhh/ng=="},
                //    new midcollege { collegeid = 17, collegename="D S M COLLEGE, JHAJHA",mid = "1000756" ,mkey="t2kA9is3Y3TTmWxIxtfvpg=="},
                //    new midcollege { collegeid = 18, collegename="K. M. D. COLLEGE, PARBATTA",mid = "1000748" ,mkey="nnUplbKPF7V0G5MBmZJrbQ=="},
                //    new midcollege { collegeid = 19, collegename="MAHILA COLLEGE, KHAGARIA",mid = "1000734" ,mkey="sk/6jYU6pg2iHWtZogVRnw=="},
                //    new midcollege { collegeid = 20, collegename="JAMALPUR COLLEGE, JAMALPUR",mid = "1000757" ,mkey="Yz4Z7EcItVptdauE/skY/g=="},
                //    new midcollege { collegeid = 21, collegename="S. B. N. COLLEGE, GARHIRAMPUR, MUNGER",mid = "1000773" ,mkey="KkJgYZNF1Re4soD2Cu5l0Q=="},
                //    new midcollege { collegeid = 22, collegename="INTERNATIONAL COLLEGE, GHOSAITH",mid = "1000744" ,mkey="3qUNFClV35YTyN3OApGD4g=="},
                //    new midcollege { collegeid = 23, collegename="R. LAL COLLEGE, LAKHISARAI",mid = "1000738" ,mkey="720a25RTSPaVlnNfgZ0IXQ=="},
                //    new midcollege { collegeid = 24, collegename="MAHILA COLLEGE, BARAHIYA",mid = "1000751" ,mkey="SLuBneRf8FtVakx/az5/kg=="},
                //    new midcollege { collegeid = 25, collegename="SANJAY GANDHI MAHILA COLLEGE, SHEIKHPURA",mid = "1000763" ,mkey="hVz2zTfLUYwZdBJomKqwFg=="},
                //    new midcollege { collegeid = 26, collegename="S. S. COLLEGE, MEHUS, SHEIKHPURA",mid = "1000772" ,mkey="oIX/v9RXPJ9XcaMCp7hnmQ=="},
                //    new midcollege { collegeid = 27, collegename="C N B COLLEGE, HATHIYAMA, SHEIKHPURA",mid = "1000771" ,mkey="ryUMP1icnBVaRrO/0GG2AQ=="},
                //    new midcollege { collegeid = 28, collegename="M. S. COLLEGE, ALOULI, SONIHAR, KHAGARIA",mid = "1000750" ,mkey="fS2rYhJ6oP/JtqX3SHyhKg=="},
                //    new midcollege { collegeid = 29, collegename="DHANRAJ SINGH COLLEGE, SIKANDARA, JAMUI",mid = "1000737" ,mkey="QSErJAceUcy/f8U/7RdifA=="},
                //    new midcollege { collegeid = 30, collegename="S. K. COLLEGE, LOHANDA, SIKANDARA, JAMUI",mid = "1000743" ,mkey="laGnFn30P2vz5mfTVyD2sQ=="},
                //    new midcollege { collegeid = 31, collegename="PHALGUNI PRASAD YADAV COLLEGE, PHALGUNI PRASAD YADAV COLLEGE, CHAKAI",mid = "1000739" ,mkey="20gpumhqQqbjnJ8CHA4hRg=="},
                //    new midcollege { collegeid = 32, collegename="SHYAMA PRASAD SINGH MAHILA COLLEGE, JAMUI",mid = "1000755" ,mkey="qb1zfaN62oNscHLr5lr7vQ=="},
                //    new midcollege { collegeid = 33, collegename="SHARDA GIRDHARI KESHARI COLLEGE",mid = "1000762" ,mkey="olU4dk1Y89KBGhV7YLqONQ=="},
                //    new midcollege { collegeid = 34, collegename="BISHWANATH SINGH INSTITUTE OF LEGAL STUDIES, MUNGER",mid = "1000914" ,mkey="Ori6jFN8//XRihptb1z7BQ=="},// LLB College 
                // New Keys
                    new midcollege { collegeid = 1, collegename="College1",mid = "1000758" ,mkey="0r1g6qm88efgZO3e/emK5Q=="},
                    new midcollege { collegeid = 2, collegename="College2",mid = "1000745" ,mkey="nbTk5VJAv9tiPe/oCrgz/w=="},
                    new midcollege { collegeid = 3, collegename="College3",mid = "1000749" ,mkey="rSBWEbkSprfPLCyg0ItgJw=="},
                    new midcollege { collegeid = 4, collegename="College4",mid = "1000747" ,mkey="jAwxIz9TcFZI32FWddNhjQ=="},
                    new midcollege { collegeid = 5, collegename="College5",mid = "1000742" ,mkey="EJ98u6WDDLX2sGMqANk1nw=="},
                    new midcollege { collegeid = 6, collegename="College6",mid = "1000736" ,mkey="Kml/cKbQNlINskSuD7gbFg=="},
                    new midcollege { collegeid = 7, collegename="College7",mid = "1000735" ,mkey="aLieRVPqlOhEMFFuAfN7MQ=="},
                    new midcollege { collegeid = 10, collegename="College8",mid = "1000723" ,mkey="6rEkatFT0Eaw8qezcs6R3g=="},
                    new midcollege { collegeid = 11, collegename="College9",mid = "1000760" ,mkey="NJI9TyVdj/xLm16qJbBiug=="},
                    new midcollege { collegeid = 13, collegename="College10",mid = "1000752" ,mkey="/YGepWneD/1ik42cKqE5Bw=="},
                    new midcollege { collegeid = 14, collegename="College11",mid = "1000761" ,mkey="9ykIRX5JtneG2oeIotAf9g=="},
                    new midcollege { collegeid = 15, collegename="College12",mid = "1000733" ,mkey="CLRXo64ZH5UkAhynh9bZsg=="},
                    new midcollege { collegeid = 16, collegename="College13",mid = "1000746" ,mkey="JlBw5Cd//ms1V67ZU4JiDA=="},
                    new midcollege { collegeid = 17, collegename="College14",mid = "1000756" ,mkey="9gdGKHCPasIUtZwH/bNXyA=="},
                    new midcollege { collegeid = 18, collegename="College15",mid = "1000748" ,mkey="A1l2t9q2cQ5Ko688VVLSyQ=="},
                    new midcollege { collegeid = 19, collegename="College16",mid = "1000734" ,mkey="31HtgNz2KOW32fYqvzihBA=="},
                    new midcollege { collegeid = 20, collegename="College17",mid = "1000757" ,mkey="NVcZ6CZGeQATO5ZgD2yVNA=="},
                    new midcollege { collegeid = 21, collegename="College18",mid = "1000773" ,mkey="6rIULlnV/bXOcFauPakHfg=="},
                    new midcollege { collegeid = 22, collegename="College19",mid = "1000744" ,mkey="pc6XEweG9btetXy8WaRY2Q=="},
                    new midcollege { collegeid = 23, collegename="College20",mid = "1000738" ,mkey="vS/EHahBSgeXJw7KTgsFCw=="},
                    new midcollege { collegeid = 24, collegename="College21",mid = "1000751" ,mkey="lzT71oh8cdKIAKjeT1xFqQ=="},
                    new midcollege { collegeid = 25, collegename="College22",mid = "1000763" ,mkey="4nVqLsTFV4p8TAOM2phkvw=="},
                    new midcollege { collegeid = 26, collegename="College23",mid = "1000772" ,mkey="09DHpBpIc6sUfIPc71gFAA=="},
                    new midcollege { collegeid = 27, collegename="College24",mid = "1000771" ,mkey="PBhv81ZLqWq/F0m//MFSqQ=="},
                    new midcollege { collegeid = 28, collegename="College25",mid = "1000750" ,mkey="De1GMK4k56BS1xLryDFsdA=="},
                    new midcollege { collegeid = 29, collegename="College26",mid = "1000737" ,mkey="P1UJX2359If2Wo3ablo5oQ=="},
                    new midcollege { collegeid = 30, collegename="College27",mid = "1000743" ,mkey="/jitlUIavm900/XJ3l2d4Q=="},
                    new midcollege { collegeid = 31, collegename="College28",mid = "1000739" ,mkey="GGnr658V4GSsGLOaHe5UQg=="},
                    new midcollege { collegeid = 32, collegename="College29",mid = "1000755" ,mkey="dQl59MtWOOXstF2E3GXPXQ=="},
                    new midcollege { collegeid = 33, collegename="College30",mid = "1000762" ,mkey="1HZQCpuknAkFiquaZQ3ASg=="},
                    new midcollege { collegeid = 34, collegename="College31",mid = "1000914" ,mkey="Zsg/zaGnfdaarsnvO3sF3g=="},// LLB College 
                    new midcollege { collegeid = 40, collegename="College22",mid = "1001129" ,mkey="DrtxE7/4nM63BotyJhZbyw=="},// 


            };
            return list;
        }

        public static List<midcollege> MIDcollegewiseAirPay()
        {
            List<midcollege> list = new List<midcollege>
{
    new midcollege { collegeid = 95, collegename="B. R. M. COLLEGE, MUNGER", mid = "265384", mkey="eZewwNNC9MNtpatD" },
    new midcollege { collegeid = 96, collegename="R. S. COLLEGE, TARAPUR, MUNGER", mid = "265425", mkey="dnQ4kSaysfVArUPX" },
    new midcollege { collegeid = 97, collegename="K. M. D. COLLEGE, PARBATTA", mid = "265385", mkey="MgqS28mc4eH5AhxX" },
    new midcollege { collegeid = 98, collegename="S. K. COLLEGE, LOHANDA, SIKANDARA, JAMUI", mid = "265897", mkey="QWFU3sNw2xhrUMnS" },
    new midcollege { collegeid = 99, collegename="KOSHI COLLEGE, KHAGARIA", mid = "264571", mkey="M6VafvMv47A2qWaG" },
    new midcollege { collegeid = 100, collegename="Smt. Ganga Devi Memorial Mahavidyalay ,Sudamapur,Jamui", mid = "269276", mkey="3S5mqfnDRqJstDdq" },
    new midcollege { collegeid = 101, collegename="B. N. M. COLLEGE", mid = "265374", mkey="sxNyqpxjJnP96d2H" },
    new midcollege { collegeid = 102, collegename="S. S. COLLEGE, MEHUS, SHEIKHPURA", mid = "265668", mkey="dXztTAuuVAuM4af6" },
    new midcollege { collegeid = 103, collegename="INTERNATIONAL COLLEGE, GHOSAITH", mid = "265604", mkey="B677j4NMMfs6vGfz" },
    new midcollege { collegeid = 104, collegename="K. D. S. COLLEGE, GOGRI", mid = "265602", mkey="8dXwFrWteRe778ny" },
    new midcollege { collegeid = 105, collegename="MAHILA COLLEGE, BARAHIYA", mid = "265424", mkey="8w7jAR5PFJchh9Ze" },
    new midcollege { collegeid = 106, collegename="M. S. COLLEGE, ALOULI, SONIHAR, KHAGARIA", mid = "265824", mkey="8HZJ5X3GE6wVq26T" },
    new midcollege { collegeid = 107, collegename="PHALGUNI PRASAD YADAV COLLEGE CHAKAI, JAMUI", mid = "265666", mkey="zQSJYE8d8PSg4ygR" },
    new midcollege { collegeid = 108, collegename="DHANRAJ SINGH COLLEGE, SIKANDARA, JAMUI", mid = "265891", mkey="bBEyQqmKgvS4F7gM" },
    new midcollege { collegeid = 109, collegename="Dr. Arvind Kumar College ,Bishanpur,chakai,jamui", mid = "269687", mkey="fqnrN9T7Z9eBWYmK" },
    new midcollege { collegeid = 110, collegename="H. S. COLLEGE", mid = "265382", mkey="eTuY7WkDPjfwyKVR" },
    new midcollege { collegeid = 111, collegename="R.D. & D.J. COLLEGE, MUNGER", mid = "265280", mkey="rqFH5DRhegNrQAt2" },
    new midcollege { collegeid = 112, collegename="JAMALPUR COLLEGE, JAMALPUR", mid = "265600", mkey="9zYW7G6n6PFPs55P" },
    new midcollege { collegeid = 113, collegename="SHYAMA PRASAD SINGH MAHILA COLLEGE, JAMUI", mid = "265667", mkey="GnSxv7zr2ffvTzRf" },
    new midcollege { collegeid = 114, collegename="D. S. M. COLLEGE, JHAJHA", mid = "265383", mkey="VvDT73KdMDWF4c7B" },
    new midcollege { collegeid = 115, collegename="R. LAL COLLEGE, LAKHISARAI", mid = "265601", mkey="pbCKQsNaFpP6pU8W" },
    new midcollege { collegeid = 116, collegename="K. S. S. COLLEGE, LAKHISARAI", mid = "265605", mkey="ydJb4F2ZVWZvaVqe" },
    new midcollege { collegeid = 117, collegename="C. N. B. COLLEGE, HATHIYAMA, SHEIKHPURA", mid = "265386", mkey="4wRVW89CCbbye9bf" },
    new midcollege { collegeid = 118, collegename="J.R.S. COLLEGE, JAMALPUR", mid = "265823", mkey="amjW4sXshXnyCavC" },
    new midcollege { collegeid = 119, collegename="Ramdhani Bhagat Degree College, Sangrampur Munger", mid = "265892", mkey="y2hYANfVaX52yN7k" },
    new midcollege { collegeid = 120, collegename="S. B. N. COLLEGE, GARHIRAMPUR, MUNGER", mid = "265894", mkey="RsHg3pymxddrEy2B" },
    new midcollege { collegeid = 121, collegename="SANJAY GANDHI SMARAK MAHILA MAHAVIDYALAYA SHEIKHPURA", mid = "265667", mkey="GnSxv7zr2ffvTzRf" },
    new midcollege { collegeid = 122, collegename="R. D. COLLEGE, SHEIKPURA", mid = "265603", mkey="5kK4B8TFaXAr4eAB" },
    new midcollege { collegeid = 123, collegename="K. K. M. COLLEGE, JAMUI", mid = "265295", mkey="7cBM4E3vBevn7X7r" },
    new midcollege { collegeid = 124, collegename="MAHILA COLLEGE, KHAGARIA", mid = "265381", mkey="UBegfK3fztVGjnut" },
    new midcollege { collegeid = 125, collegename="S. K. R. COLLEGE, BARBIGHA", mid = "265821", mkey="z2p6GdVj8Ah4XAPq" },
    new midcollege { collegeid = 126, collegename="J. M. S. COLLEGE, MUNGER", mid = "265822", mkey="U4qdKFAU66quhSdA" },
};
            return list;
        }
        public static List<midcollege> MIDcollegewiseSafex()
        {
            List<midcollege> list = new List<midcollege>
            {
                new midcollege { collegeid = 1, collegename="College1",mid = "1000758" ,mkey="0r1g6qm88efgZO3e/emK5Q=="},
                    new midcollege { collegeid = 2, collegename="College2",mid = "1000745" ,mkey="nbTk5VJAv9tiPe/oCrgz/w=="},
                    new midcollege { collegeid = 3, collegename="College3",mid = "1000749" ,mkey="rSBWEbkSprfPLCyg0ItgJw=="},
                    new midcollege { collegeid = 4, collegename="College4",mid = "1000747" ,mkey="jAwxIz9TcFZI32FWddNhjQ=="},
                    new midcollege { collegeid = 5, collegename="College5",mid = "1000742" ,mkey="EJ98u6WDDLX2sGMqANk1nw=="},
                    new midcollege { collegeid = 6, collegename="College6",mid = "1000736" ,mkey="Kml/cKbQNlINskSuD7gbFg=="},
                    new midcollege { collegeid = 7, collegename="College7",mid = "1000735" ,mkey="aLieRVPqlOhEMFFuAfN7MQ=="},
                    new midcollege { collegeid = 10, collegename="College8",mid = "1000723" ,mkey="6rEkatFT0Eaw8qezcs6R3g=="},
                    new midcollege { collegeid = 11, collegename="College9",mid = "1000760" ,mkey="NJI9TyVdj/xLm16qJbBiug=="},
                    new midcollege { collegeid = 13, collegename="College10",mid = "1000752" ,mkey="/YGepWneD/1ik42cKqE5Bw=="},
                    new midcollege { collegeid = 14, collegename="College11",mid = "1000761" ,mkey="9ykIRX5JtneG2oeIotAf9g=="},
                    new midcollege { collegeid = 15, collegename="College12",mid = "1000733" ,mkey="CLRXo64ZH5UkAhynh9bZsg=="},
                    new midcollege { collegeid = 16, collegename="College13",mid = "1000746" ,mkey="JlBw5Cd//ms1V67ZU4JiDA=="},
                    new midcollege { collegeid = 17, collegename="College14",mid = "1000756" ,mkey="9gdGKHCPasIUtZwH/bNXyA=="},
                    new midcollege { collegeid = 18, collegename="College15",mid = "1000748" ,mkey="A1l2t9q2cQ5Ko688VVLSyQ=="},
                    new midcollege { collegeid = 19, collegename="College16",mid = "1000734" ,mkey="31HtgNz2KOW32fYqvzihBA=="},
                    new midcollege { collegeid = 20, collegename="College17",mid = "1000757" ,mkey="NVcZ6CZGeQATO5ZgD2yVNA=="},
                    new midcollege { collegeid = 21, collegename="College18",mid = "1000773" ,mkey="6rIULlnV/bXOcFauPakHfg=="},
                    new midcollege { collegeid = 22, collegename="College19",mid = "1000744" ,mkey="pc6XEweG9btetXy8WaRY2Q=="},
                    new midcollege { collegeid = 23, collegename="College20",mid = "1000738" ,mkey="vS/EHahBSgeXJw7KTgsFCw=="},
                    new midcollege { collegeid = 24, collegename="College21",mid = "1000751" ,mkey="lzT71oh8cdKIAKjeT1xFqQ=="},
                    new midcollege { collegeid = 25, collegename="College22",mid = "1000763" ,mkey="4nVqLsTFV4p8TAOM2phkvw=="},
                    new midcollege { collegeid = 26, collegename="College23",mid = "1000772" ,mkey="09DHpBpIc6sUfIPc71gFAA=="},
                    new midcollege { collegeid = 27, collegename="College24",mid = "1000771" ,mkey="PBhv81ZLqWq/F0m//MFSqQ=="},
                    new midcollege { collegeid = 28, collegename="College25",mid = "1000750" ,mkey="De1GMK4k56BS1xLryDFsdA=="},
                    new midcollege { collegeid = 29, collegename="College26",mid = "1000737" ,mkey="P1UJX2359If2Wo3ablo5oQ=="},
                    new midcollege { collegeid = 30, collegename="College27",mid = "1000743" ,mkey="/jitlUIavm900/XJ3l2d4Q=="},
                    new midcollege { collegeid = 31, collegename="College28",mid = "1000739" ,mkey="GGnr658V4GSsGLOaHe5UQg=="},
                    new midcollege { collegeid = 32, collegename="College29",mid = "1000755" ,mkey="dQl59MtWOOXstF2E3GXPXQ=="},
                    new midcollege { collegeid = 33, collegename="College30",mid = "1000762" ,mkey="1HZQCpuknAkFiquaZQ3ASg=="},
                    new midcollege { collegeid = 34, collegename="College31",mid = "1000914" ,mkey="Zsg/zaGnfdaarsnvO3sF3g=="},// LLB College 
                    new midcollege { collegeid = 40, collegename="College22",mid = "1001129" ,mkey="DrtxE7/4nM63BotyJhZbyw=="},// y
           
            };
            return list;
        }
        public static List<midcollege> MIDcollegewiseEaseBuzz()
        {
            List<midcollege> list = new List<midcollege>
            {new midcollege { collegeid = 1, collegename="College1",mid = "1000758" ,mkey="0r1g6qm88efgZO3e/emK5Q=="},
                    new midcollege { collegeid = 2, collegename="College2",mid = "1000745" ,mkey="nbTk5VJAv9tiPe/oCrgz/w=="},
                    new midcollege { collegeid = 3, collegename="College3",mid = "1000749" ,mkey="rSBWEbkSprfPLCyg0ItgJw=="},
                    new midcollege { collegeid = 4, collegename="College4",mid = "1000747" ,mkey="jAwxIz9TcFZI32FWddNhjQ=="},
                    new midcollege { collegeid = 5, collegename="College5",mid = "1000742" ,mkey="EJ98u6WDDLX2sGMqANk1nw=="},
                    new midcollege { collegeid = 6, collegename="College6",mid = "1000736" ,mkey="Kml/cKbQNlINskSuD7gbFg=="},
                    new midcollege { collegeid = 7, collegename="College7",mid = "1000735" ,mkey="aLieRVPqlOhEMFFuAfN7MQ=="},
                    new midcollege { collegeid = 10, collegename="College8",mid = "1000723" ,mkey="6rEkatFT0Eaw8qezcs6R3g=="},
                    new midcollege { collegeid = 11, collegename="College9",mid = "1000760" ,mkey="NJI9TyVdj/xLm16qJbBiug=="},
                    new midcollege { collegeid = 13, collegename="College10",mid = "1000752" ,mkey="/YGepWneD/1ik42cKqE5Bw=="},
                    new midcollege { collegeid = 14, collegename="College11",mid = "1000761" ,mkey="9ykIRX5JtneG2oeIotAf9g=="},
                    new midcollege { collegeid = 15, collegename="College12",mid = "1000733" ,mkey="CLRXo64ZH5UkAhynh9bZsg=="},
                    new midcollege { collegeid = 16, collegename="College13",mid = "1000746" ,mkey="JlBw5Cd//ms1V67ZU4JiDA=="},
                    new midcollege { collegeid = 17, collegename="College14",mid = "1000756" ,mkey="9gdGKHCPasIUtZwH/bNXyA=="},
                    new midcollege { collegeid = 18, collegename="College15",mid = "1000748" ,mkey="A1l2t9q2cQ5Ko688VVLSyQ=="},
                    new midcollege { collegeid = 19, collegename="College16",mid = "1000734" ,mkey="31HtgNz2KOW32fYqvzihBA=="},
                    new midcollege { collegeid = 20, collegename="College17",mid = "1000757" ,mkey="NVcZ6CZGeQATO5ZgD2yVNA=="},
                    new midcollege { collegeid = 21, collegename="College18",mid = "1000773" ,mkey="6rIULlnV/bXOcFauPakHfg=="},
                    new midcollege { collegeid = 22, collegename="College19",mid = "1000744" ,mkey="pc6XEweG9btetXy8WaRY2Q=="},
                    new midcollege { collegeid = 23, collegename="College20",mid = "1000738" ,mkey="vS/EHahBSgeXJw7KTgsFCw=="},
                    new midcollege { collegeid = 24, collegename="College21",mid = "1000751" ,mkey="lzT71oh8cdKIAKjeT1xFqQ=="},
                    new midcollege { collegeid = 25, collegename="College22",mid = "1000763" ,mkey="4nVqLsTFV4p8TAOM2phkvw=="},
                    new midcollege { collegeid = 26, collegename="College23",mid = "1000772" ,mkey="09DHpBpIc6sUfIPc71gFAA=="},
                    new midcollege { collegeid = 27, collegename="College24",mid = "1000771" ,mkey="PBhv81ZLqWq/F0m//MFSqQ=="},
                    new midcollege { collegeid = 28, collegename="College25",mid = "1000750" ,mkey="De1GMK4k56BS1xLryDFsdA=="},
                    new midcollege { collegeid = 29, collegename="College26",mid = "1000737" ,mkey="P1UJX2359If2Wo3ablo5oQ=="},
                    new midcollege { collegeid = 30, collegename="College27",mid = "1000743" ,mkey="/jitlUIavm900/XJ3l2d4Q=="},
                    new midcollege { collegeid = 31, collegename="College28",mid = "1000739" ,mkey="GGnr658V4GSsGLOaHe5UQg=="},
                    new midcollege { collegeid = 32, collegename="College29",mid = "1000755" ,mkey="dQl59MtWOOXstF2E3GXPXQ=="},
                    new midcollege { collegeid = 33, collegename="College30",mid = "1000762" ,mkey="1HZQCpuknAkFiquaZQ3ASg=="},
                    new midcollege { collegeid = 34, collegename="College31",mid = "1000914" ,mkey="Zsg/zaGnfdaarsnvO3sF3g=="},// LLB College 
                    new midcollege { collegeid = 40, collegename="College22",mid = "1001129" ,mkey="DrtxE7/4nM63BotyJhZbyw=="},// 

                    //Univercity Department

            };
            return list;
        }
        public static void PrintLog(Exception ex, string url = "", string Remarks = "", string id = "")
        {
            try
            {
                string dir = @"C:\Error.txt";  // folder location
                if (!Directory.Exists(dir))
                {
                    using (StreamWriter writer = new StreamWriter(HttpContext.Current.Server.MapPath("~/Error.txt"), true))
                    {
                        writer.WriteLine("-----------------------------------------------------------------------------");
                        writer.WriteLine("Date : " + System.DateTime.Now.ToString());
                        writer.WriteLine();
                        writer.WriteLine("-----------------------------------------------------------------------------");
                        writer.WriteLine("URl : " + url);
                        writer.WriteLine();
                        writer.WriteLine("-----------------------------------------------------------------------------");
                        writer.WriteLine("Remarks : " + Remarks);
                        writer.WriteLine();
                        writer.WriteLine("-----------------------------------------------------------------------------");
                        writer.WriteLine("ID : " + id);
                        writer.WriteLine();
                        writer.WriteLine("-----------------------------------------------------------------------------");
                        writer.WriteLine();
                        while (ex != null)
                        {
                            writer.WriteLine(ex.GetType().FullName);
                            writer.WriteLine("Message : " + ex.Message);
                            writer.WriteLine("StackTrace : " + ex.StackTrace);
                            ex = ex.InnerException;
                        }
                    }
                }
            }
            catch (Exception ex1)
            {

            }
        }
        public static void WritetoNotepad(Exception ex, string Url, string Remarks = "", string id = "")
        {
            string strErrorContent = string.Empty;
            string strErrorFile = string.Empty;//System.AppDomain.CurrentDomain.BaseDirectory + "ApplicationErrors.txt";
            string strPreContent = string.Empty;


            strErrorFile = System.AppDomain.CurrentDomain.BaseDirectory + "StudentLog/" + "Error_" + System.DateTime.Now.ToString("dd_MM_yyyy") + ".txt";

            if (!File.Exists(strErrorFile.Trim()))
            {
                FileStream fs1 = new FileStream(strErrorFile,
                    FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fs1);
                writer.WriteLine("***************************Start Block***************************" + Environment.NewLine);
                //  writer.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory);
                writer.WriteLine("**ID >>>>=" + id);
                writer.WriteLine("**Path >>>>=" + Url);
                writer.WriteLine("**Remarks >>>>=" + Remarks);
                //writer.WriteLine("**DateTime >>>>" + DateTime.UtcNow.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
                // writer.WriteLine("**DateTime >>>>" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
                //writer.WriteLine("**DateTime >>>>" + DateTime.Now.AddHours(5.5).ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
                writer.WriteLine("**DateTime >>>>" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
                writer.WriteLine("**DateTime  Plus 5.30 hours >>>>" + DateTime.Now.AddHours(5.5).ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));

                writer.WriteLine("==================================================================");
                while (ex != null)
                {
                    writer.WriteLine(ex.GetType().FullName);
                    writer.WriteLine("**Message : >>>>" + ex.Message);
                    writer.WriteLine("**StackTrace :>>>> " + ex.StackTrace);
                    ex = ex.InnerException;
                }
                writer.WriteLine("*************************** END BLOCK***************************" + Environment.NewLine);
                writer.Close();

            }
            else
            {
                FileStream fs1 = new FileStream(strErrorFile,
                FileMode.Append, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fs1);
                writer.WriteLine("***************************Start Block***************************" + Environment.NewLine);
                //writer.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory);
                writer.WriteLine("**ID >>>>=" + id);
                writer.WriteLine("**Path >>>>=" + Url);
                writer.WriteLine("**Remarks >>>>=" + Remarks);
                // writer.WriteLine("**DateTime >>>>" + DateTime.UtcNow.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
                // writer.WriteLine("**DateTime >>>>" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
                writer.WriteLine("**DateTime >>>>" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
                writer.WriteLine("**DateTime  Plus 5.30 hours >>>>" + DateTime.Now.AddHours(5.5).ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));

                //writer.WriteLine("**DateTime >>>>" + DateTime.Now.AddHours(5.5).ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
                writer.WriteLine("=================================================================");
                while (ex != null)
                {
                    writer.WriteLine(ex.GetType().FullName);
                    writer.WriteLine("**Message : >>>>" + ex.Message);
                    writer.WriteLine("**StackTrace :>>>> " + ex.StackTrace);
                    ex = ex.InnerException;
                }
                writer.WriteLine("*************************** END BLOCK***************************" + Environment.NewLine);
                writer.Close();

            }

        }

        public static void TraceLogWritetoNotepad(string TraceMsg, string Url, string Remarks = "", string id = "")
        {
            string strErrorContent = string.Empty;
            string strErrorFile = string.Empty;//System.AppDomain.CurrentDomain.BaseDirectory + "ApplicationErrors.txt";
            string strPreContent = string.Empty;

            strErrorFile = System.AppDomain.CurrentDomain.BaseDirectory + "StudentLog/" + "Error_" + System.DateTime.Now.ToString("dd_MM_yyyy") + ".txt";

            FileStream fs1 = new FileStream(strErrorFile,
                    FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs1);
            writer.WriteLine("***************************Start Block***************************" + Environment.NewLine);
            //  writer.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory);
            writer.WriteLine("**ID >>>>=" + id);
            writer.WriteLine("**Path >>>>=" + Url);
            writer.WriteLine("**Remarks >>>>=" + Remarks);
            //writer.WriteLine("**DateTime >>>>" + DateTime.UtcNow.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
            // writer.WriteLine("**DateTime >>>>" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
            //writer.WriteLine("**DateTime >>>>" + DateTime.Now.AddHours(5.5).ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
            writer.WriteLine("**DateTime >>>>" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
            writer.WriteLine("**DateTime  Plus 5.30 hours >>>>" + DateTime.Now.AddHours(5.5).ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));

            writer.WriteLine("==================================================================");
            writer.WriteLine(TraceMsg);
            writer.WriteLine("*************************** END BLOCK***************************" + Environment.NewLine);
            writer.Close();
        }

        public static void WritetoNotePaymentgateway(string endata, string dendata, string Url, string Remarks = "", string id = "")
        {

            string strErrorContent = string.Empty;
            string strErrorFile = string.Empty;//System.AppDomain.CurrentDomain.BaseDirectory + "ApplicationErrors.txt";
            string strPreContent = string.Empty;


            strErrorFile = System.AppDomain.CurrentDomain.BaseDirectory + "StudentLog/" + "PaymentGateway" + System.DateTime.Now.ToString("dd_MM_yyyy") + ".txt";

            if (!File.Exists(strErrorFile.Trim()))
            {
                FileStream fs1 = new FileStream(strErrorFile,
                    FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fs1);
                writer.WriteLine("***************************Start Block***************************" + Environment.NewLine);
                //  writer.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory);
                writer.WriteLine("**ID >>>>=" + id);
                writer.WriteLine("**Path >>>>=" + Url);
                writer.WriteLine("**Remarks >>>>=" + Remarks);
                writer.WriteLine("**DateTime >>>>" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
                writer.WriteLine("**DateTime  Plus 5.30 hours>>>>" + DateTime.Now.AddHours(5.5).ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));

                writer.WriteLine("==================================================================");
                writer.WriteLine("**endata >>>>=" + endata); // writer.WriteLine(endata);
                writer.WriteLine("**dendata >>>>=" + dendata); //writer.WriteLine(dendata);
                writer.WriteLine("*************************** END BLOCK***************************" + Environment.NewLine);
                writer.Close();
            }
            else
            {
                FileStream fs1 = new FileStream(strErrorFile,
                FileMode.Append, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fs1);
                writer.WriteLine("***************************Start Block***************************" + Environment.NewLine);
                //writer.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory);
                writer.WriteLine("**ID >>>>=" + id);
                writer.WriteLine("**Path >>>>=" + Url);
                writer.WriteLine("**Remarks >>>>=" + Remarks);
                writer.WriteLine("**DateTime >>>>" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
                writer.WriteLine("**DateTime  Plus 5.30 hours >>>>" + DateTime.Now.AddHours(5.5).ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));

                writer.WriteLine("=================================================================");
                writer.WriteLine("**endata >>>>=" + endata); // writer.WriteLine(endata);
                writer.WriteLine("**dendata >>>>=" + dendata); //writer.WriteLine(dendata);
                writer.WriteLine("*************************** END BLOCK***************************" + Environment.NewLine);
                writer.Close();
            }
            //----------------------
        }
        public static void WritetoNotepadtest(string Url, string Remarks = "")
        {
            string strErrorContent = string.Empty;
            string strErrorFile = string.Empty;//System.AppDomain.CurrentDomain.BaseDirectory + "ApplicationErrors.txt";
            string strPreContent = string.Empty;


            strErrorFile = System.AppDomain.CurrentDomain.BaseDirectory + "StudentLog/" + "Errortest_" + System.DateTime.Now.ToString("dd_MM_yyyy") + ".txt";

            if (!File.Exists(strErrorFile.Trim()))
            {
                try
                {
                    FileStream fs1 = new FileStream(strErrorFile,
                    FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter writer = new StreamWriter(fs1);
                    writer.WriteLine("***************************Start Block***************************" + Environment.NewLine);
                    writer.WriteLine("" + Url + "      " + Remarks + Environment.NewLine);

                    writer.WriteLine("**DateTime  Plus 5.30 hours >>>>" + DateTime.UtcNow.AddHours(5.5).ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));


                    writer.WriteLine("*************************** END BLOCK***************************" + Environment.NewLine);
                    writer.Close();
                }
                catch { }

            }
            else
            {
                FileStream fs1 = new FileStream(strErrorFile,
                FileMode.Append, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fs1);
                writer.WriteLine("***************************Start Block***************************" + Environment.NewLine);
                writer.WriteLine("" + Url + Remarks + Environment.NewLine);

                writer.WriteLine("**DateTime  Plus 5.30 hours >>>>" + DateTime.UtcNow.AddHours(5.5).ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));


                writer.WriteLine("*************************** END BLOCK***************************" + Environment.NewLine);

                writer.Close();

            }

        }

        public static void Addnewqulification(Exception ex, string Url, string Remarks = "", string id = "")
        {
            string strErrorContent = string.Empty;
            string strErrorFile = string.Empty;//System.AppDomain.CurrentDomain.BaseDirectory + "ApplicationErrors.txt";
            string strPreContent = string.Empty;


            strErrorFile = System.AppDomain.CurrentDomain.BaseDirectory + "StudentLog/" + "qualification_" + System.DateTime.Now.ToString("dd_MM_yyyy") + ".txt";

            if (!File.Exists(strErrorFile.Trim()))
            {
                FileStream fs1 = new FileStream(strErrorFile,
                    FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fs1);
                writer.WriteLine("***************************Start Block***************************" + Environment.NewLine);
                //  writer.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory);
                writer.WriteLine("**ID >>>>=" + id);
                writer.WriteLine("**Path >>>>=" + Url);
                writer.WriteLine("**Remarks >>>>=" + Remarks);
                //writer.WriteLine("**DateTime >>>>" + DateTime.UtcNow.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
                // writer.WriteLine("**DateTime >>>>" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
                //writer.WriteLine("**DateTime >>>>" + DateTime.Now.AddHours(5.5).ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
                writer.WriteLine("**DateTime >>>>" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
                writer.WriteLine("**DateTime  Plus 5.30 hours >>>>" + DateTime.Now.AddHours(5.5).ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));

                writer.WriteLine("==================================================================");
                while (ex != null)
                {
                    writer.WriteLine(ex.GetType().FullName);
                    writer.WriteLine("**Message : >>>>" + ex.Message);
                    writer.WriteLine("**StackTrace :>>>> " + ex.StackTrace);
                    ex = ex.InnerException;
                }
                writer.WriteLine("*************************** END BLOCK***************************" + Environment.NewLine);
                writer.Close();

            }
            else
            {
                FileStream fs1 = new FileStream(strErrorFile,
                FileMode.Append, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fs1);
                writer.WriteLine("***************************Start Block***************************" + Environment.NewLine);
                //writer.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory);
                writer.WriteLine("**ID >>>>=" + id);
                writer.WriteLine("**Path >>>>=" + Url);
                writer.WriteLine("**Remarks >>>>=" + Remarks);
                // writer.WriteLine("**DateTime >>>>" + DateTime.UtcNow.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
                // writer.WriteLine("**DateTime >>>>" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
                writer.WriteLine("**DateTime >>>>" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
                writer.WriteLine("**DateTime  Plus 5.30 hours >>>>" + DateTime.Now.AddHours(5.5).ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));

                //writer.WriteLine("**DateTime >>>>" + DateTime.Now.AddHours(5.5).ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
                writer.WriteLine("=================================================================");
                while (ex != null)
                {
                    writer.WriteLine(ex.GetType().FullName);
                    writer.WriteLine("**Message : >>>>" + ex.Message);
                    writer.WriteLine("**StackTrace :>>>> " + ex.StackTrace);
                    ex = ex.InnerException;
                }
                writer.WriteLine("*************************** END BLOCK***************************" + Environment.NewLine);
                writer.Close();

            }

        }
        public static string GetIPAddress()
        {
            string ipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            //String strHostName = string.Empty;
            //strHostName = Dns.GetHostName();
            //Console.WriteLine("Local Machine's Host Name: " + strHostName);
            //IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            //IPAddress[] addr = ipEntry.AddressList;
            return ipaddress.ToString();
        }
        //Creating a method which accept any type of query from controller to execute and give result.    
        //result kept in datatable and send back to the controller.    
        public DataTable MyMethod(string Query)
        {
            ds = new DataSet();
            DataTable dt = new DataTable();
            da = new SqlDataAdapter(Query, CommonMethod.connect());

            da.Fill(dt);
            List<SelectListItem> list = new List<SelectListItem>();
            return dt;

        }
    }
    public class SubjectType
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
