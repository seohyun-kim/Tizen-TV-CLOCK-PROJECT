using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;

using System.Net.Http;
using System.Xml;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;




namespace Clock_Project
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
   

        public MainPage()
        {

            InitializeComponent();
            Weather();
            Time();
            SecMove();
            DayMove();
            SetTimer();
            News();
            Icon();
        }

        
        private async void Time()
        {
            while (true)
            {
                //현재시각
                HH.Text = DateTime.Now.ToString("hh"); //12시간 형식의 두자리
                MM.Text = DateTime.Now.ToString("mm");
                SS.Text = DateTime.Now.ToString("ss");
                TT.Text = DateTime.Now.ToString("tt"); //AM, PM

                //배경
                if (SS.Text == "00") // 0초일 때
                {
                    int min = DateTime.Now.Minute;
                    switch(min%3)
                    {
                        case 0: //나눈 나머지가 0일때 1 사라지면서 2 나타남
                            wall1.FadeTo(0, 1000);
                            wall2.FadeTo(1, 1000);
                            break;
                        case 1: 
                            wall2.FadeTo(0, 1000);
                            wall3.FadeTo(1, 1000);
                            break;
                        case 2:
                            wall3.FadeTo(0, 1000);
                            wall1.FadeTo(1, 1000);
                            break;
                    }
                }

           


                await Task.Delay(500);//0.5초 간격으로 딜레이 계속 줌
            }
        }
      

        private async void SecMove()
        {
            while (true)
            {
                SS.TranslateTo(0, 0, 500, Easing.Linear);
                SS.FadeTo(0, 500);
                await Task.Delay(500);
                SS.TranslationY = 80;
                SS.TranslateTo(0, 40, 500, Easing.Linear);
                SS.FadeTo(1, 500);
                await Task.Delay(500);
            }
        }

        private async void DayMove()
        {
            while (true)
            {
                DD.Text = DateTime.Now.ToString("yyyy - MMMM - dd  dddd"); //날짜 업데이트
                DD.TranslationX = 1920;
                await DD.TranslateTo(600, 0, 2000, Easing.Linear);
                await Task.Delay(5500);
                await DD.TranslateTo(-600, 0, 2500, Easing.Linear);
            }
        }


        private async void Weather()
        {

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://www.kma.go.kr/wid/queryDFSRSS.jsp?zone=1162054500");

            if(!response.IsSuccessStatusCode)//오류처리
            {
                return;
            }

            string content = await response.Content.ReadAsStringAsync(); //RSS데이터 읽기
                                                                         

            XmlDocument document = new XmlDocument();// XML 파싱
            document.LoadXml(content);

            XmlNode node = document.DocumentElement.SelectSingleNode("descendant::data"); //data태그 첫번째 항목 가져옴

            var wf = node.SelectSingleNode("wfKor"); //날씨
            var temp = node.SelectSingleNode("temp"); //온도
            WF.Text = wf.InnerText;
            TEMP.Text = temp.InnerText;

        }
        private void SetTimer()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;// 1초마다 실행
            timer.Enabled = true;
            timer.Elapsed += TimerElapsedEvent;
            timer.Start();
        }

        private void TimerElapsedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            Analog();
        }

        private async void Analog()
        {
            int sec = DateTime.Now.Second;
            int min = DateTime.Now.Minute;
            int hour = DateTime.Now.Hour;
            hour = hour % 12; //12시 보정

            AnalSec.RotateTo((sec * 6)%360, 0); //1초에 초침 6도
            AnalMin.RotateTo((min * 6 +sec*0.1)%360 , 0);
            AnalHour.RotateTo((hour * 30+ min * 0.5 + sec * (1/120))%360, 0); 

        }

        private async void News()
        {

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync("http://rss.edaily.co.kr/stock_news.xml");

            if (!response.IsSuccessStatusCode)//오류처리
            {

                return;
            }

            string content = await response.Content.ReadAsStringAsync(); //RSS데이터 읽기

            XmlDocument document = new XmlDocument();// XML 파싱
            document.LoadXml(content);
            

            XmlNodeList nodes = document.DocumentElement.SelectNodes("descendant::item"); //data태그 첫번째 항목 가져옴

            while (true)
            {
                foreach (XmlNode node in nodes)
                {
                    var title = node.SelectSingleNode("title");
                    NewsTitle.Text = title.InnerText;
                    await NewsTitle.FadeTo(1, 500);
                    await Task.Delay(3000);
                    await NewsTitle.FadeTo(0, 500);

                }
            }
        }


        private async void Icon()
        {
            while(true)
            {
                    await FaceBook.RotateTo(30, 500, Easing.Linear);
                    await FaceBook.RotateTo(-30, 250, Easing.Linear);
                    await FaceBook.RotateTo(0, 250, Easing.Linear);
                    await Task.Delay(1000);

                    await Insta.RotateTo(30, 500, Easing.Linear);
                    await Insta.RotateTo(-30, 250, Easing.Linear);
                    await Insta.RotateTo(0, 250, Easing.Linear);
                    await Task.Delay(1000);
                   
                    await Google.RotateTo(20, 500, Easing.Linear);
                    await Google.RotateTo(-20, 250, Easing.Linear);
                    await Google.RotateTo(0, 250, Easing.Linear);
                    await Task.Delay(1000);

                    await Netflix.RotateTo(20, 500, Easing.Linear);
                    await Netflix.RotateTo(-20, 250, Easing.Linear);
                    await Netflix.RotateTo(0, 250, Easing.Linear);
                    await Task.Delay(1000);

                    await Youtube.RotateTo(20, 500, Easing.Linear);
                    await Youtube.RotateTo(-20, 250, Easing.Linear);
                    await Youtube.RotateTo(0, 250, Easing.Linear);
                    await Task.Delay(1000);
 
            }

        }


    }
}


