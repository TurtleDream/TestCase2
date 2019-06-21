using System;
using System.Linq;
using System.Windows.Forms;
using OpenQA.Selenium;

namespace TestCase2
{
    class Program
    {
        static IWebDriver browser;

        static void Main(string[] args)
        {


            browser = new OpenQA.Selenium.Chrome.ChromeDriver();//1. Открыть браузер

            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            browser.Manage().Window.Maximize();//1.развернуть на весь экран

            browser.Navigate().GoToUrl("https://yandex.ru/");//2. Зайти на yandex.ru.

            browser.FindElement(By.XPath("//a[@href='https://market.yandex.ru/?clid=505&utm_source=face_abovesearch&utm_campaign=face_abovesearch']")).Click();//3. Перейти в яндекс маркет

            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            if (check("/html/body/div[4]/div[3]", browser))
            {
                browser.FindElement(By.XPath("/html/body/div[4]/div[3]/div/div/div[2]/div[1]")).Click();
                browser.FindElement(By.XPath("/html/body/div[1]/div/span/div[2]/noindex/div[2]/div/div/div/div[2]")).Click();//4. Выбрать раздел электроника
            }
            else browser.FindElement(By.XPath("/html/body/div[1]/div/span/div[2]/noindex/div[2]/div/div/div[2]")).Click();//4. Выбрать раздел электроника


            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            browser.FindElement(By.XPath("/html/body/div[1]/div[2]/div[7]/div/div/div[1]/div/div/div/div/div/div/div[2]/div[2]/ul/li[1]/div/a")).Click();//5. Выбрать раздел Наушники

            browser.FindElement(By.XPath("//*[@id='search-prepack']/div/div/div[3]/div/div[3]/div/a")).Click();//6.Зайти в расширеный поиск

            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(12);
            browser.FindElement(By.XPath("//*[@id='glf-pricefrom-var']")).SendKeys("5000");//7. Задать параметр поиска от 5000 рублей

            browser.FindElement(By.XPath("/html/body/div[1]/div[5]/div/div[1]/div[1]/div[4]/div[2]/div/div[1]/div[3]/a")).Click();//8. Выбрать производителя beats

            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);
            browser.FindElement(By.XPath("/html/body/div[1]/div[5]/div/div[1]/div[5]/a[2]")).Click();//9. Нажать кнопку Применить


            int count = browser.FindElements(By.ClassName("n-snippet-cell2__title")).Count;
            if(count == 12)
            {
                MessageBox.Show("Count = " + count);//10. Проверить, что элементов на странице 12.
            } else MessageBox.Show("Count = " + count + " != 12");//10. Проверить, что элементов на странице 12.

            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            String str = browser.FindElements(By.ClassName("n-snippet-cell2__title")).ElementAt(0).GetAttribute("textContent");//11. Запомнить первый элемент в списке.

            str = str.Replace("Наушники ", "");

            browser.FindElement(By.XPath("//*[@id='header-search']")).SendKeys(str);//12. В поисковую строку ввести запомненное значение.
            browser.FindElement(By.ClassName("suggest2-rich-item__body")).Click();

            String title = browser.FindElement(By.XPath("/html/body/div[1]/div[5]/div[2]/div/div/div[1]/div[1]/div/h1")).GetAttribute("textContent");
            title = title.Replace("Наушники ", "");

            if (str.Equals(title))
            {
                MessageBox.Show("Success!");
            }
            else MessageBox.Show("Fail!");//13. Найти и проверить, что наименование товара соответствует запомненному значению.
        }

        private static bool check(String str, IWebDriver driver)
        {
            try
            {
                driver.FindElement(By.XPath(str));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
