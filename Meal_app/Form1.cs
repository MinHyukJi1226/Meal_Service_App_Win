using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Meal_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // JSON 웹페이지 불러오기

            String url = "https://open.neis.go.kr/hub/mealServiceDietInfo?Type=json&ATPT_OFCDC_SC_CODE=D10&SD_SCHUL_CODE=7240454&MLSV_YMD=";
            WebClient webClient = new WebClient();
            webClient.Headers["Content-Type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;

            url += DateTime.Now.ToString("yyyyMMdd");
            //url += "20210327";

            Stream stream = webClient.OpenRead(url);
            StreamReader reader = new StreamReader(stream);

            // textBox1.Text = reader.ReadToEnd();

            // JSON 값 해석하기
            Root root = new Root();
            root = JsonConvert.DeserializeObject<Root>(reader.ReadToEnd());

            // JSON 해셕결과 값 출력

            //textBox1.Text = root.mealServiceDietInfo[1].row[0].DDISH_NM;

            String moning = null;
            String lunch = null;
            String dinner = null;

            try
            {
                moning = root.mealServiceDietInfo[1].row[0].DDISH_NM;
                if (moning== null) throw new NullReferenceException("this is null.");
            }
                
            catch
            { 
                textBox1.Text = "급식 정보가 없습니다.";
            }
            finally
            {
                if (moning != null)
                {
                    moning = moning.Replace("<br/>", "\r\n");
                    textBox1.Text = moning;
                }
            }

            try
            {
                lunch = root.mealServiceDietInfo[1].row[1].DDISH_NM;
                if (lunch == null) throw new NullReferenceException("this is null.");
            }

            catch
            {
                textBox2.Text = "급식 정보가 없습니다.";
            }
            finally
            {
                if (lunch != null)
                {
                    lunch = lunch.Replace("<br/>", "\r\n");
                    textBox2.Text = lunch;
                }
            }
            try
            {
                dinner = root.mealServiceDietInfo[1].row[2].DDISH_NM;
                if (dinner == null) throw new NullReferenceException("this is null.");
            }

            catch
            {
                textBox3.Text = "급식 정보가 없습니다.";   
            }
            finally
            {
                if (dinner != null)
                {
                    dinner = dinner.Replace("<br/>", "\r\n");
                    textBox3.Text = dinner;
                }
            }
        }
    }

    public class RESULT
    {
        public string CODE { get; set; }
        public string MESSAGE { get; set; }
    }

    public class Head
    {
        public int list_total_count { get; set; }
        public RESULT RESULT { get; set; }
    }

    public class Row
    {
        public string ATPT_OFCDC_SC_CODE { get; set; }
        public string ATPT_OFCDC_SC_NM { get; set; }
        public string SD_SCHUL_CODE { get; set; }
        public string SCHUL_NM { get; set; }
        public string MMEAL_SC_CODE { get; set; }
        public string MMEAL_SC_NM { get; set; }
        public string MLSV_YMD { get; set; }
        public string MLSV_FGR { get; set; }
        public string DDISH_NM { get; set; }
        public string ORPLC_INFO { get; set; }
        public string CAL_INFO { get; set; }
        public string NTR_INFO { get; set; }
        public string MLSV_FROM_YMD { get; set; }
        public string MLSV_TO_YMD { get; set; }
    }

    public class MealServiceDietInfo
    {
        public List<Head> head { get; set; }
        public List<Row> row { get; set; }
    }

    public class Root
    {
        public List<MealServiceDietInfo> mealServiceDietInfo { get; set; }
    }
}
