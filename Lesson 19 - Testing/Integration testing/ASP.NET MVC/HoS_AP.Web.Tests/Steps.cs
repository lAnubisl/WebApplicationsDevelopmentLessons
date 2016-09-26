using System;
using System.Collections.Generic;
using System.Linq;
using HoS_AP.BLL.ServiceInterfaces;
using HoS_AP.DAL.Dto;
using HoS_AP.Misc;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace HoS_AP.Web.Tests
{
    [Binding]
    public class Steps
    {
        private static InternetExplorerDriver BrowserDriver => TestRunConfiguration.BrowserDriver;
        private static IStateAdapter StateAdapter = new DeleporterMockStateAdapter();

        [Given(@"I am logged in as “(.*)”")]
        [When(@"I am logged in as “(.*)”")]
        public void IAmLoggedInAsMegan(string username)
        {
            var userTable = new Table("Name", "Password");
            userTable.AddRow(username, UserManager.GetPassword(username));
            ThereIsAreFollowingUsersInTheSystem(userTable);
            INavigateToPage("Login");
            var table = new Table("UserName", "Password");
            table.AddRow("UserName", username);
            table.AddRow("Password", UserManager.GetPassword(username));
            FillInControlsAsFollows(table);
            ClickButton("Sign In");
        }

        [Given(@"Given there is are following users in the system")]
        public void ThereIsAreFollowingUsersInTheSystem(Table table)
        {
            var accounts = new List<Account>();
            var service = new EncryptionService();
            foreach (var tableRow in table.Rows)
            {
                accounts.Add(new Account
                {
                    UserName = tableRow[0],
                    Password = service.CreateHash(tableRow[1])
                });
            }

            StateAdapter.InitAccounts(accounts);
        }

        [Given(@"there are the following characters in system")]
        public void ThereAreTheFollowingCharactersInSystem(Table table)
        {
            var characters = new List<Character>();
            foreach (var tr in table.Rows)
            {
                characters.Add(new Character
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    Name = tr[0],
                    Price = Convert.ToDecimal(tr[1]),
                    Type = (CharacterTypes) Enum.Parse(typeof(CharacterTypes), tr[2], true),
                    Active = Convert.ToBoolean(tr[3]),
                    Deleted = Convert.ToBoolean(tr[4])
                });
            }

            StateAdapter.InitCharacters(characters);
        }

        [When(@"I fill in controls as follows")]
        public void FillInControlsAsFollows(Table table)
        {
            foreach (var row in table.Rows)
            {
                var element = BrowserDriver.FindElement(By.Name(row[0]));
                var elementType = element.GetAttribute("type");
                if (elementType == "text" || elementType == "password")
                {
                    element.Clear();
                    element.SendKeys(row[1]);
                }
                if (elementType == "select-one")
                {
                    var selectElement = new SelectElement(element);
                    selectElement.SelectByText(row[1]);
                }
                if (elementType == "checkbox")
                {
                    var needToBeChecked = Convert.ToBoolean(row[1]);
                    if ((element.Selected && !needToBeChecked) || (!element.Selected && needToBeChecked))
                    {
                        element.Click();
                    }
                }
            }
        }

        [When(@"I click ""(.*)"" button")]
        public void ClickButton(string buttonName)
        {
            try
            {
                var input =
                    BrowserDriver.FindElementByXPath(string.Format(
                        ".//input[@type='submit' and @value='{0}']", buttonName));
                input.Submit();
            }
            catch
            {
                try
                {
                    var button =
                        BrowserDriver.FindElementByXPath(
                            string.Format(".//button[@type='button' and text()='{0}']", buttonName));

                    button.Click();
                }
                catch
                {
                    var button =
                        BrowserDriver.FindElementByXPath(
                            string.Format(".//input[@type='button' and @value='{0}']", buttonName));

                    button.Click();
                }
            }
        }

        [Then(@"I should see character in list")]
        public void ShouldSeeCharacterInList(Table table)
        {
            var charactersHtmlTable = BrowserDriver.FindElementByTagName("table");
            var tbody = charactersHtmlTable.FindElement(By.TagName("tbody"));
            var charactersHtmlRows = tbody.FindElements(By.TagName("tr"));
            var actualCharacters = new List<string[]>();

            foreach (var row in charactersHtmlRows)
            {
                var character = new string[6];
                var columns = row.FindElements(By.TagName("td"));
                for (var j = 0; j < columns.Count; j++)
                {
                    character[j] = columns[j].Text;
                }

                actualCharacters.Add(character);
            }

            foreach (var row in table.Rows)
            {
                var rowFound = false;
                foreach (var character in actualCharacters)
                {
                    rowFound = row["Name"] == character[0]
                               && row["Type"] == character[1]
                               && row["Price"] == character[3]
                               && row["Active"] == character[4]
                               && row["Deleted"] == character[5];
                    if (rowFound) break;
                }

                Assert.IsTrue(rowFound);
            }
        }

        [When(@"I navigate to “(.*)” page")]
        public static void INavigateToPage(string pageName)
        {
            BrowserDriver.Navigate().GoToUrl(UrlManager.GetPage(pageName));
        }

        [Then(@"I should be on “(.*)” page")]
        public void ShouldBeOnPage(string pageName)
        {
            var url = BrowserDriver.Url;
            if (url.IndexOf("?") > -1)
            {
                url = url.Remove(url.IndexOf("?"));
            }

            Assert.AreEqual(UrlManager.GetPage(pageName), url);
        }

        [Then(@"I should not see text ""(.*)""")]
        public void ShouldNotSeeText(string text)
        {
            var elements = BrowserDriver.FindElementsByXPath($".//*[text() = \"{text}\"]");
            Assert.That(!elements.Any());
        }

        [Then(@"I should see text ""(.*)""")]
        public void ShouldSeeText(string text)
        {
            var elements = BrowserDriver.FindElementsByXPath($".//*[text() = \"{text}\"]");
            Assert.That(elements.Any());
        }
    }
}